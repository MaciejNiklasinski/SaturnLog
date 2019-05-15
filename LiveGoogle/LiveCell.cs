using Google.Apis.Sheets.v4.Data;
using LiveGoogle.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    // LiveSpreadsheet cell
    public class LiveCell
    {
        #region Private Fields
        // Data stored by the cell. 
        // Null - data hasn't been loaded, by might exist.
        // "" - empty cell.
        private string _data;
        #endregion

        #region Properties
        // Flag representing whether the instance of the cell has been destroyed.
        public bool Destroyed { get; private set; }

        // Spreadsheet id
        public string SpreadsheetId { get; private set; }

        // Sheet title id
        public string SheetTitleId { get; private set; }

        // Index of the cell column - sheet, not range relative.
        public int ColumnIndex { get; internal set; }

        // Index of the cell row - sheet, not range relative.
        public int RowIndex { get; internal set; }

        // Range string
        public string Range
        { get { return RangeTranslator.GetCellString(this.SheetTitleId, this.ColumnIndex, this.RowIndex); } }
        #endregion

        #region Constructors
        // Empty from parameters
        private LiveCell(string spreadsheetId, string sheetTitleId, int sheetRelativeColumnIndex, int sheetRelativeRowIndex)
        {
            // Assign spreadsheet id.
            this.SpreadsheetId = spreadsheetId;

            // Assign sheet title id.
            this.SheetTitleId = sheetTitleId;

            // Assign cell location parameters.
            this.ColumnIndex = sheetRelativeColumnIndex;
            this.RowIndex = sheetRelativeRowIndex;
        }

        // Parameters and data
        private LiveCell(string spreadsheetId, string sheetTitleId, int sheetRelativeColumnIndex, int sheetRelativeRowIndex, string data)
            : this(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex)
        {
            // Fill cell with the provided data.
            this._data = data;
        }

        // Parameters and data
        private LiveCell(string spreadsheetId, string sheetTitleId, int sheetRelativeColumnIndex, int sheetRelativeRowIndex, object data)
            : this(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex, data?.ToString()) { }

        // Parameters and content
        private LiveCell(string spreadsheetId, string sheetTitleId, int sheetRelativeColumnIndex, int sheetRelativeRowIndex, CellData cell)
            : this(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex, cell?.GetDataAsString()) { }
        #endregion

        #region Methods
        // Destroys the cell.
        public void Destroy()
        {
            // Set destroyed flag to true.
            this.Destroyed = true;

            // Clear cell data.
            this._data = null;
        }

        // Returns new instance of LiveCell class.
        internal static LiveCell Construct(string spreadsheetId, string sheetTitleId, int sheetRelativeColumnIndex, int sheetRelativeRowIndex, Object cell)
        {
            // Use CellData to construct the LiveCell
            if (cell is CellData cellData)
                return new LiveCell(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex, cellData);
            // Use cell value in a string format to construct new instance of LiveCell.
            else if (cell is string cellString)
                return new LiveCell(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex, cellString);
            // Use an object of unknown type as a value to 
            else
                return new LiveCell(spreadsheetId, sheetTitleId, sheetRelativeColumnIndex, sheetRelativeRowIndex, cell);
        }

        public void Upload(LiveSpreadsheetsDb db)
        {
            // Get  the row data in the form understandable to google service. 
            IList<IList<object>> rows = new IList<object>[1] { new object[1] { this._data } };

            // Using provided google service update data stored by row into the google servers
            db.ApisSheetsService.UpdateRangeDataSync(this.SpreadsheetId, this.Range, rows);
        }

        // Re obtains the data using provided google sheets service.
        public void ReObtainData(LiveSpreadsheetsDb db)
        {
            // Obtain data from google sheets service.
            object currentData = db.ApisSheetsService.GetObjectCellDataSync(this.SpreadsheetId, this.Range);
            
            // Set current LiveCel data.
            this.SetData(currentData, false);
        }

        public bool HasValue()
        {
            return !(this._data is null);
        }

        #region GetDataAs
        // Returns cell data as string.
        public string GetDataAsString()
        {
            // Return cell data.
            return this._data;
        }

        // Returns cell data as object.
        public string GetDataAsObject()
        {
            // Return cell data.
            return this._data;
        }

        // Returns cell data as object.
        public CellData GetDataAsCellData()
        {
            CellData cellData = new CellData();
            cellData.UserEnteredValue = new ExtendedValue
            {
                StringValue = this._data
            };

            // Return new cell data.
            return cellData;
        }
        #endregion

        #region SetDataFrom
        // Sets cell data from the provided string.
        public void SetData(string data, bool ignoreNullData = true)
        {
            // If provided data is null, and ignore null data flag
            // is true, do not override stored data and return.
            if (data is null && ignoreNullData) return;

            // Set data string.
            this._data = data;
        }

        // Sets cell data from the provided object
        public void SetData(object data, bool ignoreNullData = true)
        {
            // If provided data is null, and ignore null data flag
            // is true, do not override stored data and return.
            if (data is null && ignoreNullData) return;

            // Set data from object string.
            this._data = data?.ToString();
        }

        // Sets cell data from the provided using Google.Apis.Sheets.v4.Data.CellData
        internal void SetData(CellData cellData, bool ignoreNullData = true)
        {
            // Obtain string data stored in the provided cell data.
            string data = cellData?.GetDataAsString();

            // Use that data to set cell data from the string.
            this.SetData(data, ignoreNullData);
        }
        #endregion
        #endregion

        // Delete
        // TODO
    }
}
