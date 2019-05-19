using Google.Apis.Sheets.v4.Data;
using LiveGoogle.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public partial class LiveRange : IList<LiveRow>, IList<IList<LiveCell>>, IDisposable
    {
        #region Private Fields
        // All the rows belonging to the sheet ordered from top to bottom.
        private IList<LiveRow> _rows;
        #endregion

        #region Properties
        // Flag representing whether the instance of the range has been destroyed.
        public bool Destroyed { get; private set; }

        // Flag representing whether the instance of the range has been replaced.
        public bool Replaced { get; internal set; }

        // Used by LiveSheet ONLY to alter this._rows content - Add and remove columns and rows.
        internal protected virtual IList<LiveRow> Rows { get { return this._rows; } }

        // Spreadsheet Id
        public string SpreadsheetId { get; private set; }

        // Sheet Title Id
        public string SheetTitleId { get; internal protected set; }

        // 0 based index of the first left column belonging the range.
        public int LeftIndex { get; internal protected set; }

        // 0 based index of the first top row belonging to the range.
        public int TopIndex { get; internal protected set; }

        // 0 based index of the last right column belonging the range.
        public int RightIndex { get { return this.LeftIndex + this.ColumnCount - 1; } }

        // 0 based index of the last bottom row belonging to the range.
        public int BottomIndex { get { return this.TopIndex + this.RowCount - 1; } }

        // Number of columns in the sheet
        public int ColumnCount { get; internal protected set; }

        // Number of rows in the sheet
        public int RowCount { get; internal protected set; }

        // Range string
        public string Range
        { get { return RangeTranslator.GetRangeString(this.SheetTitleId, this.LeftIndex, this.TopIndex, this.ColumnCount, this.RowCount); } }
        #endregion

        #region Constructors
        public LiveRange(string spreadsheetId, string sheetTitleId, int leftIndex, int topIndex, int columnCount, int rowCount, IList<LiveRow> rangeContent)
        {
            // Assign range spreadsheet id
            this.SpreadsheetId = spreadsheetId;

            // Assign range sheet title id
            this.SheetTitleId = sheetTitleId;

            // Assign range location properties.
            this.LeftIndex = leftIndex;
            this.TopIndex = topIndex;

            // Assign range dimensions properties.
            this.ColumnCount = columnCount;
            this.RowCount = rowCount;

            // Assign range content rows list
            this._rows = rangeContent;
        }
        #endregion

        #region Methods
        internal virtual void Destroy(bool andDispose)
        {
            // If current LiveRange is already destroyed, do nothing and return.
            if (this.Destroyed)
                return;

            // Set the flag indicating destruction of the range to true.
            this.Destroyed = true;

            // Removes current instance of the LiveRange stored in the factory production index
            LiveRange.Factory.RemoveStoredRange(this.SpreadsheetId, this.SheetTitleId, this.Range);

            // Get all the ranges, including the LiveRange representing LiveSheet,
            // related by sheet title id.
            IList<LiveRange> sheetRelatedRanges = LiveRange.Factory.GetExistingSheetRanges(this.SpreadsheetId, this.SheetTitleId);

            // Remove current LiveRange from that collection.
            sheetRelatedRanges.Remove(this);

            // Get all the ranges using the same LiveRow instances as the current LiveRange.
            IEnumerable<LiveRange> rangesWithOverlapingRows = sheetRelatedRanges.Where((range) =>
            {
                // If index of the most left column of the range is equal
                // with index of the most left column of the current LiveRange...
                return (this.LeftIndex == range.LeftIndex)
                // ... and index of the most right column of the range is equal
                // with index of the most right column of the current LiveRange...
                && (this.RightIndex == range.RightIndex)
                // ... and range top index is above, or equal with
                // the bottom index of the current LiveRange...
                && ((this.BottomIndex >= range.TopIndex)
                // ... and range bottom index is below, or equal with
                // the top index of the current LiveRange...
                || (this.TopIndex <= range.BottomIndex));
                // include the range in the returned enumeration as
                // part of it is overlapping with the current LiveRange.
            });

            // Fore each row in the current LiveRange...
            foreach (LiveRow row in this._rows)
                // ... check whether the row is used by any other
                if (rangesWithOverlapingRows.Count((range) =>
                {
                    // If current row index is greater then or equal to range top index...
                    return row.RowIndex >= range.TopIndex
                    // ... and current row index is greater then or equal to range bottom index...
                    && row.RowIndex <= range.BottomIndex;
                    // the range has been confirmed to be using the same row.
                }
                // If it has been confirmed that ZERO other LiveRange instances is
                // using the same LiveRow as the current LiveRange...
                ) == 0)
                    // ...destroy (and dispose if provided andDispose flag is true)
                    // such a row from the LiveRange factory production index.
                    if (andDispose)
                        row.Dispose();
                    else
                        row.Destroy();

            // Clears the content of the current LiveRange rows content list.
            this._rows.Clear();
        }

        // Re obtains the data using provided google sheets service.
        public void ReObtainData(LiveSpreadsheetsDb db)
        {
            // Obtain current data grid from the provided google service
            IList<IList<object>> currentData = db.ApisSheetsService.GetObjectsRangeDataSync(this.SpreadsheetId, this.Range);

            // 0, 0 representing buffer values
            this.SetDataFromRowsData<IList<object>, object>(0, 0, currentData, false);
        }

        public bool ColumnContainsCellWithData(int rangeRelativeColumnIndex, string cellData)
        {
            if (this.ColumnCount < rangeRelativeColumnIndex)
                return this._rows.Any((row) => { return row[rangeRelativeColumnIndex].GetDataAsString() == cellData; });
            else return false;
        }

        public bool RowContainsCellWithData(int rangeRelativeRowIndex, string cellData)
        {
            if (this.RowCount < rangeRelativeRowIndex)
                return this._rows[rangeRelativeRowIndex].Any((cell) => { return cell.GetDataAsString() == cellData; });
            else return false;
        }

        // Assures that the data currently stored in the LiveRow is persisted on google servers
        public void Upload(LiveSpreadsheetsDb db)
        {
            // Get  the row data in the form understandable to google service. 
            IList<IList<object>> rows = this.GetDataAsObjectsGrid();

            // Using provided google service update data stored by row into the google servers
            db.ApisSheetsService.UpdateRangeDataSync(this.SpreadsheetId, this.Range, rows);
        }

        #region GetDataAs
        // Returns range stored data as a strings grid. Rows / Columns
        public IList<IList<string>> GetDataAsStringsGrid()
        {
            // Create empty instance of the strings grid.
            List<IList<string>> stringsGrid = new List<IList<string>>();

            // Get data of each row as a list of strings
            // and add it in to the strings grid.
            foreach (LiveRow row in this._rows)
                stringsGrid.Add(row.GetDataAsStrings());

            // Return strings grid.
            return stringsGrid;
        }

        // Returns range stored data as a objects grid. Rows / Columns
        public IList<IList<object>> GetDataAsObjectsGrid()
        {
            // Create empty instance of the objects grid.
            List<IList<object>> objectsGrid = new List<IList<object>>();

            // Get data of each row as a list of objects
            // and add it in to the objects grid.
            foreach (LiveRow row in this._rows)
                objectsGrid.Add(row.GetDataAsObjects());

            // Return objects grid.
            return objectsGrid;
        }

        // Returns range stored data as rows list.
        public IList<LiveRow> GetDataAsRowsList()
        {
            // Create empty instance of the rows list.
            IList<LiveRow> rowsList = new List<LiveRow>();

            // Get data of each row as a list of LiveRow instance
            // and add it in to the LiveRow list.
            foreach (LiveRow row in this._rows)
                rowsList.Add(row);

            // Return rows list.
            return rowsList;
        }

        // Returns data from the row located on the specific index relative to LiveRange.
        public IList<string> GetRowDataAsStrings(int rowIndex)
        {
            // Create empty instance of the rows list.
            IList<string> cellsData = new List<string>();

            // Get data of each cell in the row specified with the provided row index;
            for (int cellIndex = 0; cellIndex < this.ColumnCount; cellIndex++)
            {
                string cellData = this._rows[rowIndex][cellIndex].GetDataAsString();

                cellsData.Add(cellData);
            }

            // Return row cells data list.
            return cellsData;
        }

        // Returns data from the row located on the specific index relative to LiveRange.
        public IList<object> GetRowDataAsObjects(int rowIndex)
        {
            // Create empty instance of the rows list.
            IList<object> cellsData = new List<object>();

            // Get data of each cell in the row specified with the provided row index; 
            for (int cellIndex = 0; cellIndex < this.ColumnCount; cellIndex++)
            {
                string cellData = this._rows[rowIndex][cellIndex].GetDataAsObject();

                cellsData.Add(cellData);
            }

            // Return row cells data list.
            return cellsData;
        }

        // Returns data from the row located on the specific index relative to LiveRange.
        public LiveRow GetRowDataAsRowData(int rowIndex)
        {
            // Return range row located on the provided index relative to the current range location.
            return this._rows.ElementAtOrDefault(rowIndex);
        }

        // Returns data from the row located on the specific index relative to LiveRange.
        public IList<string> GetColumnDataAsStrings(int columnIndex)
        {
            // Create empty instance of the rows list.
            IList<string> cellsData = new List<string>();

            // Get data of each cell in the row specified with the provided row index;
            for (int cellIndex = 0; cellIndex < this.RowCount; cellIndex++)
            {
                string cellData = this._rows[cellIndex][columnIndex].GetDataAsString();

                cellsData.Add(cellData);
            }

            // Return column cells data list.
            return cellsData;
        }

        // Returns data from the row located on the specific index relative to LiveRange.
        public IList<object> GetColumnDataAsObjects(int columnIndex)
        {
            // Create empty instance of the rows list.
            IList<object> cellsData = new List<object>();

            // Get data of each cell in the row specified with the provided row index;
            for (int cellIndex = 0; cellIndex < this.RowCount; cellIndex++)
            {
                string cellData = this._rows[cellIndex][columnIndex].GetDataAsObject();

                cellsData.Add(cellData);
            }

            // Return column cells data list.
            return cellsData;
        }
        #endregion

        #region SetDataFrom
        // Set range data from strings cells grid
        public void SetDataFromStringsGrid(int leftBufferIndex, int topBufferIndex, IList<IList<string>> cellsGridData, bool ignoreNullData = true)
        {
            this.SetDataFromRowsData<IList<string>, string>(leftBufferIndex, topBufferIndex, cellsGridData, ignoreNullData);
        }

        // Set range data from objects grid
        public void SetDataFromObjectsGrid(int leftBufferIndex, int topBufferIndex, IList<IList<object>> cellsGridData, bool ignoreNullData = true)
        {
            this.SetDataFromRowsData<IList<object>, object>(leftBufferIndex, topBufferIndex, cellsGridData, ignoreNullData);
        }

        // Set range data from objects grid
        public void SetDataFromCellsGrid(int leftBufferIndex, int topBufferIndex, IList<IList<CellData>> cellsGridData, bool ignoreNullData = true)
        {
            this.SetDataFromRowsData<IList<CellData>, CellData>(leftBufferIndex, topBufferIndex, cellsGridData, ignoreNullData);
        }

        // Set row data from strings list.
        internal void SetDataFromRowsData<Row, Cell>(int leftBufferIndex, int topBufferIndex, IList<Row> rowsData, bool ignoreNullData = true)
            where Row : class, IList<Cell> where Cell : class
        {
            // Get whichever is lower: Number of the rows in the provided rows data,
            // or the number of the rows in the range minus row top buffer index.
            int rowsCount = Math.Min(rowsData?.Count ?? 0, this.RowCount - topBufferIndex);

            // From 0 to the last row, either on the range or row of provided data.
            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                // Obtain row data for this specific row.
                IList<Cell> cellsData = rowsData?.ElementAtOrDefault(rowIndex);

                // Determine index of the row to be filled with the data, by adding 
                // top buffer index, to the index of the source row in the cells grid data.
                int bufferRelativeRowIndex = topBufferIndex + rowIndex;

                // Get row to be filled with the data.
                LiveRow row = this._rows[bufferRelativeRowIndex];

                // Fill the row with the data.
                row.SetDataFromCellsData<Cell>(leftBufferIndex, cellsData, ignoreNullData);
            }
        }

        // Set ramge data from rows list
        public void SetDataFromRowsData(int leftBufferIndex, int topBufferIndex, IList<RowData> rowsData, bool ignoreNullData = true)
        {
            // Get whichever is lower: Number of the rows in the provided rows data,
            // or the number of the rows in the range minus row top buffer index.
            int rowsCount = Math.Min(rowsData?.Count ?? 0, this.RowCount - topBufferIndex);

            // From 0 to the last row, either on the range or row of provided data.
            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                // Obtain row data for this specific row.
                RowData rowData = rowsData[rowIndex];

                // Determine index of the row to be filled with the data, by adding 
                // top buffer index, to the index of the source row in the cells grid data.
                int bufferRelativeRowIndex = topBufferIndex + rowIndex;

                // Get row to be filled with the data.
                LiveRow row = this._rows[bufferRelativeRowIndex];

                // Fill the row with the data.
                row.SetDataFromRowData(leftBufferIndex, rowData, ignoreNullData);
            }
        }

        // Set sheet data from the sheet
        public void SetDataFromSheetData(Sheet sheetData, bool ignoreNullData = true)
        {
            // Get sheet data as rows collection if available.
            IList<RowData> rows = sheetData.Data?.ElementAtOrDefault(0)?.RowData;

            // Set sheet data from rows list, starting from A1 cell.
            this.SetDataFromRowsData(0, 0, rows, ignoreNullData);
        }
        #endregion
        #endregion

        #region IList<LiveRow>, IList<IList<LiveCell>> interfaces - common part.
        public int Count { get { return this._rows.Count; } }

        public bool IsReadOnly { get { return this._rows.IsReadOnly; } }

        public LiveRow this[int rowIndex] { get { return this._rows[rowIndex]; } set { this._rows[rowIndex] = value; } }

        public int IndexOf(LiveRow item)
        {
            return this._rows.IndexOf(item);
        }

        public void Insert(int index, LiveRow item)
        {
            this._rows.Insert(index, item);
        }

        public void Add(LiveRow item)
        {
            this._rows.Add(item);
        }

        public bool Contains(LiveRow item)
        {
            return this._rows.Contains(item);
        }

        public void CopyTo(LiveRow[] array, int arrayIndex)
        {
            this._rows.CopyTo(array, arrayIndex);
        }

        public bool Remove(LiveRow item)
        {
            return this._rows.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._rows.RemoveAt(index);
        }

        public void Clear()
        {
            this._rows.Clear();
        }

        public IEnumerator<LiveRow> GetEnumerator()
        {
            return this._rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._rows.GetEnumerator();
        }
        #endregion

        #region IList<IList<LiveCell>> implementation
        IList<LiveCell> IList<IList<LiveCell>>.this[int rowIndex]
        {
            get { return this._rows[rowIndex]; }
            set
            {
                this._rows[rowIndex]
                    = LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        value[0].SpreadsheetId,
                        // First cell sheet title id.
                        value[0].SheetTitleId,
                        // First cell column index.
                        value[0].ColumnIndex,
                        // First cell row index.
                        value[0].RowIndex,
                        // Number of provided cell in the row.
                        value.Count());
            }
        }

        int IList<IList<LiveCell>>.IndexOf(IList<LiveCell> cells)
        {
            return this._rows.IndexOf(LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count()));
        }

        void IList<IList<LiveCell>>.Insert(int index, IList<LiveCell> cells)
        {
            this._rows.Insert(index, LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count()));
        }

        void ICollection<IList<LiveCell>>.Add(IList<LiveCell> cells)
        {
            this._rows.Add(LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count()));
        }

        bool ICollection<IList<LiveCell>>.Contains(IList<LiveCell> cells)
        {
            return this._rows.Contains(LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count()));
        }

        void ICollection<IList<LiveCell>>.CopyTo(IList<LiveCell>[] array, int arrayIndex)
        {
            LiveRow[] rowsArray = array.Select((cells) => LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count())).ToArray();

            this._rows.CopyTo(rowsArray, arrayIndex);
        }

        bool ICollection<IList<LiveCell>>.Remove(IList<LiveCell> cells)
        {
            return this._rows.Remove(LiveRow.Factory.Get(
                        // First cell spreadsheetId.
                        cells[0].SpreadsheetId,
                        // First cell sheet title id.
                        cells[0].SheetTitleId,
                        // First cell column index.
                        cells[0].ColumnIndex,
                        // First cell row index.
                        cells[0].RowIndex,
                        // Number of provided cell in the row.
                        cells.Count()));
        }

        IEnumerator<IList<LiveCell>> IEnumerable<IList<LiveCell>>.GetEnumerator()
        {
            return this._rows.GetEnumerator();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    this.Destroy(true);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.


                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~LiveRange() {
          // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //  Dispose(false);
        //}

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
