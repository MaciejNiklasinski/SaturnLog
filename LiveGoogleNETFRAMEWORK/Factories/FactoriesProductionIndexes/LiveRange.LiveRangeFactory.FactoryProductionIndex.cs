using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public partial class LiveRange
    {
        // Index off all of the LiveRange and LiveSheet s ever produced and not disposed, which has been ever created by LiveRangeFactory.
        private class FactoryProductionIndex
        {
            //            | K:Spreadsheet Id  |K: Sheet Title Id  Dictionary of sheets indexed by the sheet title id.
            private IDictionary<string, IDictionary<string, Pair<LiveSheet, IDictionary<string, LiveRange>>>> _producedSheets { get; }
                = new Dictionary<string, IDictionary<string, Pair<LiveSheet, IDictionary<string, LiveRange>>>>();

            #region Indexers
            public IDictionary<string, Pair<LiveSheet, IDictionary<string, LiveRange>>> this[string spreadsheetId] { get { return this._producedSheets[spreadsheetId]; } }
            
            public LiveSheet this[string spreadsheetId, string sheetTitleId] { get {return this._producedSheets[spreadsheetId][sheetTitleId].Item1; } }

            public LiveRange this[string spreadsheetId, string sheetTitleId, string sheetRangeString] { get { return this._producedSheets[spreadsheetId][sheetTitleId].Item2[sheetRangeString]; } }
            #endregion

            #region Methods
            // Assures that space for the sheet with the provided properties is ready. 
            // Should be use by the LiveRowFactory prior to the creation of the LiveRow instance,
            // and alocating it in the 
            public void AssureSheetStorageSpace(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // If appropriate space already exist return.
                if (this.HasSheet(spreadsheetId, sheetTitleId))
                    return;

                // Otherwise..

                // Check and add spreadsheet placeholder if required
                if (!this.HasSpreadsheet(spreadsheetId))
                    this.AddEmptySpreadsheet(spreadsheetId);

                // Add sheet placeholder
                this.AddEmptySheet(spreadsheetId, sheetTitleId, sheetRangeString);
            }

            // Assures that space for the sheet with the provided properties is ready. 
            // Should be use by the LiveRowFactory prior to the creation of the LiveRow instance,
            // and alocating it in the 
            public void AssureRangeStorageSpace(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // If appropriate space already exist return.
                if (this.HasRange(spreadsheetId, sheetTitleId, sheetRangeString))
                    return;

                // Check and add sheet placeholder if required
                if (!this.HasSheet(spreadsheetId, sheetTitleId))
                    throw new InvalidOperationException("Unable to assure range storage space before sheet storage space will be assured.");

                this.AddEmptyRange(spreadsheetId, sheetTitleId, sheetRangeString);
            }
            
            // Returns existing sheet matching provided method parameters or null if none 
            // of the existing rows matching provided parameters exactly.
            public LiveSheet GetExistingSheet(string spreadsheetId, string sheetTitleId)
            {
                // Get existing sheet from the factory production index if any indexed under provided parameters.
                try { return this[spreadsheetId][sheetTitleId].Item1; }
                catch { return null; }
            }

            public LiveRange GetExistingRange(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // Returns existing LiveRange matching provided parameters from the factory production index
                try { return this[spreadsheetId][sheetTitleId].Item2[sheetRangeString]; }
                // If placeholder hasn't been assured - which shouldn't happened anyway - return null.
                catch (KeyNotFoundException) { return null; }
            }

            public void ReIndexRange(string spreadsheetId, string sheetTitleId, string oldRangeString)
            {
                // Get appropriate Dictionary placeholder storing all the ranges associated with the sheet title id. 
                IDictionary<string, LiveRange> rangesByRangeStrings = this._producedSheets[spreadsheetId][sheetTitleId].Item2;

                // Get range indexed to the provided oldRangeStirng.
                LiveRange rangeToReIndex = rangesByRangeStrings[oldRangeString];

                // Remove dictionary entry representing range indexed to the provided oldRangeStirng.
                rangesByRangeStrings.Remove(oldRangeString);

                // Re-index range using it current range string.
                rangesByRangeStrings.Add(rangeToReIndex.Range, rangeToReIndex);

            }
            // Re-Indexing all the ranges stock associated with the provided sheet title id
            public void ReIndexSheetRelatedInventory(string spreadsheetId, string sheetTitleId)
            {
                // Get appropriate Dictionary placeholder storing all the ranges associated with the sheet title id. 
                IDictionary<string, LiveRange> rangesByRangeStrings = this._producedSheets[spreadsheetId][sheetTitleId].Item2;

                // Get all the ranges stored by the dictionary placeholder...
                ICollection<LiveRange> ranges = rangesByRangeStrings.Values;

                // ... then clear the dictionary placeholder itself.
                rangesByRangeStrings.Clear();

                // For each range which has been a member of the dictionary placeholder
                // be fore then have been cleared ...
                foreach (LiveRange range in ranges)
                    // ... add it back in to the placeholder with newly calculated range.Range string.
                    rangesByRangeStrings.Add(range.Range, range);
            }
            #endregion

            #region Has Spreadsheet/Sheet/Range
            // Answer the question whether the factory production index already contains a spreadsheet placeholder entry
            public bool HasSpreadsheet(string spreadsheetId)
            {
                // If spreadsheet produced ranges are available
                return _producedSheets.ContainsKey(spreadsheetId);
            }

            // Answer the question whether the factory production index already contains a sheet placeholder entry
            public bool HasSheet(string spreadsheetId, string sheetTitleId)
            {
                // If spreadsheet produced ranges are available
                return this.HasSpreadsheet(spreadsheetId)
                // And if sheet produced ranges are available
                && this[spreadsheetId].ContainsKey(sheetTitleId);
            }

            // Answer the question whether the factory production index already contains a range placeholder entry
            public bool HasRange(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // If spreadsheet produced ranges are available
                return this.HasSpreadsheet(spreadsheetId)
                // And if sheet produced ranges are available
                && this.HasSheet(spreadsheetId, sheetTitleId) 
                // And if one of sheets ranges covers exactly an area described with the provided sheet range strung, 
                && this[spreadsheetId][sheetTitleId].Item2.ContainsKey(sheetRangeString);

            }
            #endregion

            #region Add Spreadsheet/Sheet/RowsIndex/SpecificRow
            // Adds new instance of empty Dictionary<string, IDictionary<int, IList<LiveRow>>>, indexed according 
            // provided spreadsheet id.
            internal void AddEmptySpreadsheet(string spreadsheetId)
            {
                this._producedSheets.Add(spreadsheetId, new Dictionary<string, Pair<LiveSheet, IDictionary<string, LiveRange>>>());
            }

            // Adds new instance of empty Pair<LiveSheet, IDictionary<string, LiveRange>>(null, new Dictionary<string, LiveRange>,
            // indexed according provided spreadsheet id and sheetTitle id.  
            internal void AddEmptySheet(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // Add sheet placeholder.
                this[spreadsheetId].Add(sheetTitleId, new Pair<LiveSheet, IDictionary<string, LiveRange>>(null, new Dictionary<string, LiveRange>()));
                this.AddEmptyRange(spreadsheetId, sheetTitleId, sheetRangeString);
            }

            // Adds new instance of empty Pair<LiveSheet, IDictionary<string, LiveRange>>(null, new Dictionary<string, LiveRange>,
            // indexed according provided spreadsheet id and sheetTitle id.  
            internal void AddEmptyRange(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                // Add sheet placeholder.
                this._producedSheets[spreadsheetId][sheetTitleId].Item2.Add(sheetRangeString, null);
            }

            // Adds provided instance of the LiveSheet into the live range factory production index.
            internal void AddSpecificSheet(LiveSheet sheet)
            {
                // Add live sheet into the live range factory production index.
                this[sheet.SpreadsheetId][sheet.SheetTitleId].Item1 = sheet;

                // Add live range into the live range factory production index.
                this.AddSpecificRange(sheet);
            }

            // Adds provided instance of the LiveRange into the live range factory production index.
            internal void AddSpecificRange(LiveRange range)
            {
                // Add live range into the live range factory production index.
                this[range.SpreadsheetId][range.SheetTitleId].Item2[range.Range] = range;
            }
            #endregion
            
            #region Remove Spreadsheet/Sheet/RowsIndex/SpecificRow
            // Removes spreadsheet related rows placeholder dictionary.
            private void RemoveSpreadsheet(string spreadsheetId)
            {
                // Removes instance of Dictionary<string, IDictionary<int, IList<LiveRow>>> indexed 
                // to the provided spreadsheetId at spreadsheets dictionary placeholder.
                this._producedSheets.Remove(spreadsheetId);
            }

            // Removes sheet related rows placeholder dictionary.
            public void RemoveSheet(string spreadsheetId, string sheetTitleId)
            {
                // Removes instance of Dictionary<string, IDictionary<int, IList<LiveRow>>> indexed 
                // to the provided spreadsheetId at spreadsheets dictionary placeholder.
                this[spreadsheetId].Remove(sheetTitleId);

                if (this[spreadsheetId].Count == 0)
                    this.RemoveSpreadsheet(spreadsheetId);
            }

            // Removes specified
            public void RemoveRange(string spreadsheetId, string sheetTitleId, string rangeString)
            {
                // Removes provided instance of LiveRange specified with the provided parameters.
                this[spreadsheetId][sheetTitleId].Item2.Remove(rangeString);
            }

            // Removes provided
            public void RemoveSpecificRange(LiveRange range)
            {
                // Removes provided instance of LiveRange.
                this[range.SpreadsheetId][range.SheetTitleId].Item2.Remove(range.Range);
            }
            #endregion
        }
    }
}
