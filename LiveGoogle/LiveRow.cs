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
    // If the row is a part of LiveRange rather then LiveSheet, and the LiveRange left column index is not equal 0,
    // current LiveRow.LeftIndex will not be 0, but rather will be equal to the index of the first left column 
    // of the containing LiveRange, as well as all the cells contained by the row will be indexed relatively 
    // to the Range rather then relatively to the sheet.
    public partial class LiveRow : IList<LiveCell>, IDisposable
    {
        #region Private Fields
        // Rows of cells.
        private IList<LiveCell> _cells;
        #endregion

        #region Properties
        // Flag representing whether the instance of the row has been destroyed.
        public bool Destroyed { get; private set; }

        // Flag representing whether the instance of the row has been replaced.
        public bool Replaced { get; internal set; }

        // Used by LiveSheet ONLY to alter this._cells content - Add and remove cells.
        internal protected IList<LiveCell> Cells { get { return this._cells; } }

        // Spreadsheet Id
        public string SpreadsheetId { get; private set; }

        // Sheet Title Id
        public string SheetTitleId { get; private set; }

        // Index of the row in the sheet
        public int RowIndex { get; internal set; }

        // Index of the first left cell in the row. 
        public int LeftIndex { get; private set; }

        // Index of the last right cell in the row. 
        public int RightIndex { get { return this.LeftIndex + this.ColumnCount - 1; } }

        // Number of the columns in the row relative to the number of columns in the LiveRange
        // rather then LiveSheet, if current LiveRow is a part of one.
        public int ColumnCount { get; internal set; }
        
        // Range string
        public string Range
        { get { return RangeTranslator.GetRangeString(this.SheetTitleId, this.LeftIndex, this.RowIndex, this.ColumnCount, 1); } }
        #endregion

        #region Constructors
        // Private Base. Not to be use on its own. Cells data placeholder has to be filled with
        // cells, as well as ColumCount has to have appropriate value assigned to it.
        private LiveRow(string spreadsheetId, string sheetTitleId, int columnIndex, int rowIndex, int columnCount, IList<LiveCell> cells)
        {
            // Assign spreadsheet id
            this.SpreadsheetId = spreadsheetId;

            // Assign sheet title id
            this.SheetTitleId = sheetTitleId;

            // Assign row dimensions/location.
            this.LeftIndex = columnIndex;
            this.RowIndex = rowIndex;
            this.ColumnCount = columnCount;

            // Assign row content cells list.
            this._cells = cells;
        }
        #endregion

        #region Methods 
        public void Destroy()
        {
            // If current LiveRow is already destroyed, do nothing and return.
            if (this.Destroyed)
                return;

            // Set the value of the flag indicating the current row have been already destroyed to true.
            this.Destroyed = true;

            // ...remove the current tow from the LiveRow factory production index.
            LiveRow.Factory.RemoveStoredRow(this);
        }

        // Re obtains the data using provided google sheets service.
        public void ReObtainData(LiveSpreadsheetsDb db)
        {
            // Obtain data from google sheets service.
            IList<object> currentData = db.ApisSheetsService.GetObjectsRowDataSync(this.SpreadsheetId, this.Range);

            // Set current LiveCel data.
            this.SetDataFromCellsData<object>(0, currentData, false);
        }

        // Re obtains the data using provided google sheets service.
        public async Task ReObtainDataAsync(LiveSpreadsheetsDb db)
        {
            // Obtain data from google sheets service.
            IList<object> currentData = await db.ApisSheetsService.GetObjectsRowDataAsync(this.SpreadsheetId, this.Range);

            // Set current LiveCel data.
            this.SetDataFromCellsData<object>(0, currentData, false);
        }

        // Clears the data in all the cells contained by the current LiveRow.
        public void ClearData()
        {
            IList<string> nullData = new List<string>();
            for (int i = 0; i < this.ColumnCount; i++)
                nullData.Add(null);

            this.SetDataFromCellsData<string>(0, nullData, false);
        }

        // Answer the question whether the current LiveRow contains a cell
        // located on the provided sheet relative column index.
        public bool HasCellOnSheetRelativeColumnIndex(int sheetRelativeColumnIndex)
        {
            return (this.LeftIndex <= sheetRelativeColumnIndex
                && this.RightIndex >= sheetRelativeColumnIndex);
        }

        // Assures that the data currently stored in the LiveRow is persisted on google servers
        public void Upload(LiveSpreadsheetsDb db)
        {
            // Get  the row data in the form understandable to google service. 
            IList<IList<object>> rows = new IList<object>[1] { this.GetDataAsObjects() };

            // Using provided google service update data stored by row into the google servers
            db.ApisSheetsService.UpdateRangeDataSync(this.SpreadsheetId, this.Range, rows);
        }

        // Assures that the data currently stored in the LiveRow is persisted on google servers
        public async Task UploadAsync(LiveSpreadsheetsDb db)
        {
            // Get  the row data in the form understandable to google service. 
            IList<IList<object>> rows = new IList<object>[1] { this.GetDataAsObjects() };

            // Using provided google service update data stored by row into the google servers
            await db.ApisSheetsService.UpdateRangeDataAsync(this.SpreadsheetId, this.Range, rows);
        }

        #region GetCell
        // Return sheet indexed under the provided sheet relative column index
        public LiveCell GetCellFromSheetRelativeColumnIndex(int sheetRelativeColumnIndex)
        {
            return this._cells[sheetRelativeColumnIndex - this.LeftIndex];
        }

        // Return sheet indexed under the provided range relative column index
        public LiveCell GetCellFromRangeRelativeColumnIndex(int rangeRelativeColumnIndex)
        {
            return this._cells[rangeRelativeColumnIndex];
        }
        #endregion

        #region GetDataAs
        // Get row data as strings list
        public IList<string> GetDataAsStrings()
        {
            // Create empty instance of the strings list.
            List<string> stringsList = new List<string>();

            // For each cell in the row.
            foreach (LiveCell cell in this._cells)
            {
                // Get cell string data
                string cellString = cell.GetDataAsString();

                // Add it in to the string list.
                stringsList.Add(cellString);
            }

            // Return row data as a strings list.
            return stringsList;
        }

        // Get row data as objects list
        public IList<object> GetDataAsObjects()
        {
            // Create empty instance of the objects list.
            List<object> objectsList = new List<object>();

            // For each cell in the row.
            foreach (LiveCell cell in this._cells)
            {
                // Get cell string data
                string cellObject = cell.GetDataAsObject();

                // Add it in to the object list.
                objectsList.Add(cellObject);
            }

            // Return row data as a objects list.
            return objectsList;
        }

        // Get row data as a data as row data
        public IList<LiveCell> GetDataAsCells()
        {
            // Create empty instance of the cells list.
            List<LiveCell> cellsList = new List<LiveCell>();

            // Get each cell and add it in to the cells list.
            foreach (LiveCell cell in this._cells)
                cellsList.Add(cell);

            // Return row data as a cells list.
            return cellsList;
        }

        // Get row data as a data as row data
        public RowData GetDataAsRowData()
        {
            // Create new RowData instance
            RowData rowData = new RowData();

            // Create empty instance of the cells list.
            rowData.Values = new List<CellData>();

            // For each cell in the row.
            foreach (LiveCell cell in this._cells)
            {
                // Get cell string data
                CellData cellData = cell.GetDataAsCellData();

                // Add it in to the row data values list.
                rowData.Values.Add(cellData);
            }

            // Return row data as a cells list.
            return rowData;
        }
        #endregion

        #region SetDataFrom
        // Set live row data from cells data string list.
        public void SetDataFromStringsData(int leftBufferIndex, IList<string> cellsData, bool ignoreNullData = true)
        {
            // Use provided cells data to set row data.
            this.SetDataFromCellsData<string>(leftBufferIndex, cellsData, ignoreNullData);
        }

        // Set live row data from cells data object list.
        public void SetDataFromObjectsData(int leftBufferIndex, IList<object> cellsData, bool ignoreNullData = true)
        {
            // Use provided cells data to set row data.
            this.SetDataFromCellsData<object>(leftBufferIndex, cellsData, ignoreNullData);
        }

        // Set live row data from cells data list.
        public void SetDataFromCellsData(int leftBufferIndex, IList<CellData> cellsData, bool ignoreNullData = true)
        {
            // Get whichever is lower: Number of the columns in the provided cells data,
            // or the number of the cells in the row minus column left buffer index.
            int cellsCount = Math.Min(cellsData?.Count ?? 0, this.ColumnCount - leftBufferIndex);

            // From 0 to the last cell, either on the row or cells of provided data.
            for (int cellIndex = 0; cellIndex < cellsCount; cellIndex++)
            {
                // Obtain a cell data for this specific cell.
                CellData cellData = cellsData?.ElementAtOrDefault(cellIndex);

                // Determine index of the cell to be filled with the cell data, by adding 
                // left buffer index, to the index of the source cell in the cells data list.
                int bufferRelativeCellIndex = leftBufferIndex + cellIndex;

                // Get cell to be filled with the data.
                LiveCell cell = this._cells[bufferRelativeCellIndex];

                // Fill the cell with the cell data.
                cell.SetData(cellData, ignoreNullData);
            }
        }

        // Set row data from strings list.
        internal void SetDataFromCellsData<Cell>(int leftBufferIndex, IList<Cell> cellsData, bool ignoreNullData = true)
            where Cell : class
        {
            // Get whichever is lower: Number of the columns in the provided cells data,
            // or the number of the cells in the row minus column left buffer index.
            int cellsCount = Math.Min(cellsData?.Count ?? 0, this.ColumnCount - leftBufferIndex);

            // From 0 to the last cell, either on the row or cells of provided data.
            for (int cellIndex = 0; cellIndex < cellsCount; cellIndex++)
            {
                // Obtain a cell data for this specific cell.
                Cell cellData = cellsData?.ElementAtOrDefault(cellIndex);

                // Determine index of the cell to be filled with the cell data, by adding 
                // left buffer index, to the index of the source cell in the cells data list.
                int bufferRelativeCellIndex = leftBufferIndex + cellIndex;

                // Get cell to be filled with the data.
                LiveCell cell = this._cells[bufferRelativeCellIndex];

                // Fill the cell with the cell data.
                cell.SetData(cellData, ignoreNullData);
            }
        }


        // Set live row data from google row data.
        public void SetDataFromRowData(int leftBufferIndex, RowData rowData, bool ignoreNullData = true)
        {
            // Use cells from the provided row data to set row data.
            this.SetDataFromCellsData(leftBufferIndex, rowData?.Values, ignoreNullData);
        }
        #endregion
        #endregion

        #region IList<LiveCell>
        public int Count { get { return this._cells.Count; } }

        public bool IsReadOnly { get { return this._cells.IsReadOnly; } }

        public LiveCell this[int cellRowRelativeIndex]
        {
            get { return this._cells[cellRowRelativeIndex]; }
            set { this._cells[cellRowRelativeIndex] = value; }
        }

        public int IndexOf(LiveCell item)
        {
            return this._cells.IndexOf(item);
        }

        public void Insert(int index, LiveCell item)
        {
            this._cells.Insert(index, item);
        }

        public void Add(LiveCell item)
        {
            this._cells.Add(item);
        }

        public bool Contains(LiveCell item)
        {
            return this._cells.Contains(item);
        }

        public void CopyTo(LiveCell[] array, int arrayIndex)
        {
            this._cells.CopyTo(array, arrayIndex);
        }

        public void Clear()
        {
            this._cells.Clear();
        }

        public bool Remove(LiveCell item)
        {
            return this._cells.Remove(item);
        }

        public void RemoveAt(int index)
        {
            this._cells.RemoveAt(index);
        }

        public IEnumerator<LiveCell> GetEnumerator()
        {
            return this._cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._cells.GetEnumerator();
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
                    this.Destroy();
                }


                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this._cells = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LiveRow() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}