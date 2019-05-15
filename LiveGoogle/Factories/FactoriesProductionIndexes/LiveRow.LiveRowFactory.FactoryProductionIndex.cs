using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public partial class LiveRow
    {
        public partial class LiveRowFactory
        {
            // Index off all of the LiveRows ever produced and not disposed, which has been ever created by LiveRowFactory.
            private class FactoryProductionIndex
            {
                #region Private Fields
                //            | K:Spreadsheet Id  |K: Sheet Title Id  |K:Sheet Relative row index|V: Lists of all the rows located
                // on the same sheet relative row index. (Same row index, but capturing different range of the row cells)
                private IDictionary<string, IDictionary<string, IDictionary<int, IList<LiveRow>>>> _producedRows { get; }
                    = new Dictionary<string, IDictionary<string, IDictionary<int, IList<LiveRow>>>>();
                #endregion

                #region Indexers
                // Returns rows produced for the spreadsheet related with the provided spreadsheet id.
                public IDictionary<string, IDictionary<int, IList<LiveRow>>> this[string spreadsheetId] { get => this._producedRows[spreadsheetId]; }

                // Returns rows produced for the sheet related with the provided spreadsheet id and sheet title id.
                public IDictionary<int, IList<LiveRow>> this[string spreadsheetId, string sheetTitleId] { get => this._producedRows[spreadsheetId][sheetTitleId]; }

                // Returns all the LiveRow 's produced for the row located on the sheet related with the provided
                // spreadsheet id and sheet title id, on the provided sheet relative row index
                public IList<LiveRow> this[string spreadsheetId, string sheetTitleId, int rowSheetRelativeIndex]
                { get => this._producedRows[spreadsheetId][sheetTitleId][rowSheetRelativeIndex]; }
                #endregion

                #region Methods
                // Assures that space for the row with the provided properties is ready. 
                // Should be use by the LiveRowFactory prior to the creation of the LiveRow instance,
                // and allocating it in the 
                public void AssureStorageSpace(string spreadsheetId, string sheetTitleId, int rowSheetRelativeIndex)
                {
                    // If appropriate space already exist return.
                    if (this.HasRowOnSheetRelativeIndex(spreadsheetId, sheetTitleId, rowSheetRelativeIndex))
                        return;

                    // Otherwise..

                    // Check and add spreadsheet placeholder if required
                    if (!this.HasSpreadsheet(spreadsheetId))
                        this.AddEmptySpreadsheet(spreadsheetId);

                    // Check and add Sheet placeholder if required
                    if (!this.HasSheet(spreadsheetId, sheetTitleId))
                        this.AddEmptySheet(spreadsheetId, sheetTitleId);

                    // Add rows sheet relative index placeholder
                    this.AddEmptyRowsIndexEntry(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);
                }

                // Returns existing row matching provided method parameters or null if none 
                // of the existing rows matching provided parameters exactly.
                public LiveRow GetExistingRow(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount)
                {
                    // Get all existing rows related with the provided spreadsheet id/ sheet title id / row index.
                    IList<LiveRow> existingRows = this[spreadsheetId][sheetTitleId][rowSheetRelativeIndex];

                    // Find and return row covering requested area if already existing.
                    return
                        // Foreach of the existing rows in the factory production index
                        // matching the provided spreadsheet id/ sheet title id / row index...
                        existingRows.Where(existingRow =>
                        // ...compare index of the left column in the existing row
                        // with the left column index specified by the  provided left column index...
                        existingRow.LeftIndex == leftColumnIndex
                        //...as well as compare the number of the columns in the existing row
                        // with the one specified by the provided column count parameter...
                        && existingRow.ColumnCount == columnCount).FirstOrDefault();
                }

                // Re-Indexing specified row, increasing the row index it is indexed to by the provided rowIndexChangeByValue
                internal void ReIndexStoredRow(LiveRow rowToBeReIndexed, int rowIndexChangeByValue)
                {
                    // Obtain the dictionary of lists, where each list each a collection of different instances of LiveRow
                    // indexed into the same integer sheet relative row index.
                    IDictionary<int, IList<LiveRow>> sheetRelatedRows = this[rowToBeReIndexed.SpreadsheetId, rowToBeReIndexed.SheetTitleId];

                    // Remove rowToBeReIndexed from its current, no longer valid position...
                    this.RemoveSpecificRowFromItsPreviousRowIndex(rowToBeReIndexed, rowToBeReIndexed.RowIndex - rowIndexChangeByValue);

                    // .. assure that the factory production index has a designated space for a row indexed at rowToBeReIndexed current row index.
                    this.AssureStorageSpace(rowToBeReIndexed.SpreadsheetId, rowToBeReIndexed.SheetTitleId, rowToBeReIndexed.RowIndex);
                    
                    // ...and add it into its correct position.
                    this.AddSpecificRow(rowToBeReIndexed);
                }
                #endregion

                #region Has Spreadsheet/Sheet/RowsIndex/SpecificRow
                // Answer the question whether the factory production index already contains a spreadsheet placeholder entry
                public bool HasSpreadsheet(string spreadsheetId)
                {
                    // If spreadsheet produced rows are available
                    return _producedRows.ContainsKey(spreadsheetId);
                }

                // Answer the question whether the factory production index already contains a sheet placeholder entry
                public bool HasSheet(string spreadsheetId, string sheetTitleId)
                {
                    // If spreadsheet produced rows are available
                    return _producedRows.ContainsKey(spreadsheetId)
                    // And if sheet produced rows are available
                    && this[spreadsheetId].ContainsKey(sheetTitleId);
                }

                // Answer the question whether the factory production index already contains a sheet row index placeholder entry
                public bool HasRowOnSheetRelativeIndex(string spreadsheetId, string sheetTitleId, int rowSheetRelativeIndex)
                {
                    // If spreadsheet produced rows are available
                    return _producedRows.ContainsKey(spreadsheetId)
                    // And if sheet produced rows are available
                    && this[spreadsheetId].ContainsKey(sheetTitleId)
                    // And at least one row covering cells indexed on the provided
                    // rowIndex relatively to the sheet containing them is available
                    && this[spreadsheetId][sheetTitleId].ContainsKey(rowSheetRelativeIndex);
                }
                #endregion

                #region Add Spreadsheet/Sheet/RowsIndex/SpecificRow
                // Adds new instance of empty Dictionary<string, IDictionary<int, IList<LiveRow>>>, indexed according 
                // provided spreadsheet id.
                internal void AddEmptySpreadsheet(string spreadsheetId)
                {
                    this._producedRows.Add(spreadsheetId, new Dictionary<string, IDictionary<int, IList<LiveRow>>>());
                }

                // Adds new instance of empty Dictionary<int, IList<LiveRow>>, indexed according 
                // provided spreadsheet id and sheetTitle id.
                internal void AddEmptySheet(string spreadsheetId, string sheetTitleId)
                {
                    this[spreadsheetId].Add(sheetTitleId, new Dictionary<int, IList<LiveRow>>());
                }

                // Adds new instance of empty IList<LiveRow>, indexed according 
                // provided spreadsheet id, sheetTitle id and rowSheetRelativeIndex.
                internal void AddEmptyRowsIndexEntry(string spreadsheetId, string sheetTitleId, int rowSheetRelativeIndex)
                {
                    this[spreadsheetId][sheetTitleId].Add(rowSheetRelativeIndex, new List<LiveRow>());
                }

                internal void AddSpecificRow(LiveRow row)
                {
                    this[row.SpreadsheetId][row.SheetTitleId][row.RowIndex].Add(row);
                }
                #endregion

                #region Remove Spreadsheet/Sheet/RowsIndex/SpecificRow
                // Removes spreadsheet related rows placeholder dictionary.
                private void RemoveSpreadsheet(string spreadsheetId)
                {
                    // Removes instance of Dictionary<string, IDictionary<int, IList<LiveRow>>> indexed 
                    // to the provided spreadsheetId at spreadsheets dictionary placeholder.
                    this._producedRows.Remove(spreadsheetId);
                }

                // Removes sheet related rows placeholder dictionary.
                private void RemoveSheet(string spreadsheetId, string sheetTitleId)
                {
                    // Removes instance of Dictionary<string, IDictionary<int, IList<LiveRow>>> indexed 
                    // to the provided spreadsheetId at spreadsheets dictionary placeholder.
                    this[spreadsheetId].Remove(sheetTitleId);

                    if (this[spreadsheetId].Count == 0)
                        this.RemoveSpreadsheet(spreadsheetId);
                }

                // Removes row sheet relative index related rows placeholder list.
                private void RemoveRowsIndexEntry(string spreadsheetId, string sheetTitleId, int rowSheetRelativeIndex)
                {
                    this[spreadsheetId][sheetTitleId].Remove(rowSheetRelativeIndex);

                    if (this[spreadsheetId][sheetTitleId].Count == 0)
                        this.RemoveSheet(spreadsheetId, sheetTitleId);
                }

                // Removes row
                public void RemoveSpecificRow(LiveRow row)
                {
                    this[row.SpreadsheetId][row.SheetTitleId][row.RowIndex].Remove(row);

                    if (this[row.SpreadsheetId][row.SheetTitleId][row.RowIndex].Count == 0)
                        this.RemoveRowsIndexEntry(row.SpreadsheetId, row.SheetTitleId, row.RowIndex);
                }

                // Removes row
                private void RemoveSpecificRowFromItsPreviousRowIndex(LiveRow row, int previousRowIndex)
                {
                    this[row.SpreadsheetId][row.SheetTitleId][previousRowIndex].Remove(row);

                    if (this[row.SpreadsheetId][row.SheetTitleId][previousRowIndex].Count == 0)
                        this.RemoveRowsIndexEntry(row.SpreadsheetId, row.SheetTitleId, previousRowIndex);
                }
                #endregion
            }
        }
    }
}
