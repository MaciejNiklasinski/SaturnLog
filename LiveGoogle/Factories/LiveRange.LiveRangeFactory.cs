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
        #region Factory singleton
        // Factory singleton thread safety lock.
        private static readonly object _lock = new object();

        // LiveRange factory thread-safe singleton.
        public static LiveRangeFactory _factory;
        public static LiveRangeFactory Factory
        {
            get
            {
                if (LiveRange._factory is null)
                    lock (LiveRange._lock)
                        if (LiveRange._factory is null)
                            LiveRange._factory = new LiveRangeFactory();

                return LiveRange._factory;
            }
        }
        #endregion
        
        public partial class LiveRangeFactory
        {
            #region Private Fields
            // FactoryProductionIndex singleton - stores data allowing to assure that none of the sheet/range/rows/cells will be created more then once.
            private readonly FactoryProductionIndex _productionIndex = new FactoryProductionIndex();
            #endregion

            #region Methods - Outer layer, public and internal to be called.
            // Possibly more expensive performance wise, but it will assure that each row/cell has only one
            // instance of LiveRow(Multiple instances of LiveRow can cover the same cell or cells, as long as
            // these instances of LiveRow covering not exactly the same range in this row) /LiveCell 
            // representing it ever initialized. 
            #region Get
            #region Sheet
            // Empty from separate location/size parameters using already existing rows/cells wherever possible.
            public LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId, 
                    // Sheet range left column index
                    0, // column index 0 == column A
                    // Sheet range top row index 
                    0, // row index 0 = row 1
                    // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetSheet(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount);
            }
            
            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<IList<string>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<string>, string>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<IList<object>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<object>, object>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            internal LiveSheet GetSheet<Row, Cell>(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<Row> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<Row, Cell>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<IList<CellData>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<RowData> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);
                
                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Empty from range string using already existing rows/cells wherever possible.
            public LiveSheet GetSheet(string spreadsheetId, string sheetRangeString)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetSheet(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveSheet GetSheet(string spreadsheetId, string sheetRangeString, IList<IList<string>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<string>, string>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveSheet GetSheet(string spreadsheetId, string sheetRangeString, IList<IList<object>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<object>, object>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            internal LiveSheet GetSheet<Row, Cell>(string spreadsheetId, string sheetRangeString, IList<Row> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<Row, Cell>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveSheet GetSheet(string spreadsheetId, string sheetRangeString, IList<IList<CellData>> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveSheet GetSheet(string spreadsheetId, string sheetRangeString, IList<RowData> newSheetRowsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // Provided instance of Google.Apis.Sheets.v4.Data.Sheet
            // must contain value other than null for some basic parameters like
            // Sheet.Properties.Title, Sheet.Properties.GridProperties.ColumnCount,
            // Sheet.Properties.GridProperties.RowCount

            // Pre-filled with new row/cells pre-filled with provided data. Provided actual instance of Google Apis sheet data.
            public LiveSheet GetSheet(string spreadsheetId, Sheet newSheetData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Provided instance of Google.Apis.Sheets.v4.Data.Sheet
                // must contain value other than null for some basic parameters like
                // sheet title id, column count and row count.
                string sheetTitleId = newSheetData.Properties?.Title 
                    // Or if no string value can be found for sheet title id, throw appropriate exception.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.", nameof(newSheetData.Properties.Title));
                int columnCount = newSheetData.Properties?.GridProperties.ColumnCount
                    // Or if no int value can be found for column count, throw appropriate exception.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.", nameof(newSheetData.Properties.GridProperties.ColumnCount));
                int rowCount = newSheetData.Properties?.GridProperties.RowCount
                    // Or if no int value can be found for row count, throw appropriate exception.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.", nameof(newSheetData.Properties.GridProperties.RowCount));

                // If provided new sheet data contains any rows data obtain it,
                // otherwise proceed with null instead.
                IList<RowData> newSheetRowsData = newSheetData.Data?.FirstOrDefault()?.RowData;

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }
            #endregion

            #region Range
            // Empty from separate location/size parameters using already existing rows/cells wherever possible.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex, 
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount);
            }

            // Empty from parameters using already existing rows.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, string rangeString)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string rangeSheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using both rangeString
                // string and separate method parameters.
                return this.GetRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<IList<string>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<string>, string>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<IList<object>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);
                
                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<object>, object>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<IList<CellData>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);
                
                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            internal LiveRange GetRange<Row,Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<Row> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row: class, IList<Cell> where Cell : class
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);


                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<Row, Cell>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveRange GetRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<RowData> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveRange GetRange(string spreadsheetId, string rangeString, IList<IList<string>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<string>, string>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveRange GetRange(string spreadsheetId, string rangeString, IList<IList<object>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<object>, object>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            public LiveRange GetRange(string spreadsheetId, string rangeString, IList<IList<CellData>> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Pre-filled with new row/cells pre-filled with provided data. Provided range string.
            internal LiveRange GetRange<Row, Cell>(string spreadsheetId, string rangeString, IList<Row> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using both rangeString string and separate method parameters.
                return this.GetRange<Row, Cell>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Pre-filled with new row/cells pre-filled with provided data. Provided separate location/size parameters.
            public LiveRange GetRange(string spreadsheetId, string rangeString, IList<RowData> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the provided parameters,
                // as well as parameters obtained from the translation of them.
                return this.GetRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);
            }
            #endregion
            #endregion

            // Possible performance benefits - to be used safely, caller must be absolutely sure that
            // the sheet described by the provided parameters spreadsheetId, sheetTitleId, stringRange, 
            // leftColumnIndex, rowSheetRelativeIndex and columnCount.
            #region GetForcedNew
            #region Sheet
            // Returns new empty instance of LiveSheet forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);


                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount, IList<IList<string>> newSheetRowsData)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<string>, string>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount, IList<IList<object>> newSheetRowsData)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<object>, object>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount, IList<IList<CellData>> newSheetRowsData)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet<Row, Cell>(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount, IList<Row> newSheetRowsData)
                where Row : class, IList<Cell> where Cell : class
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<Row, Cell>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, int rowCount, int columnCount, IList<RowData> newSheetRowsData)
            {
                // Get sheet string range based on the provided parameters.
                string sheetRangeString = RangeTranslator.GetRangeString(
                    // Sheet title id
                    sheetTitleId,
                    // Sheet range left column index
                    0, // column index 0 == column A
                       // Sheet range top row index 
                    0, // row index 0 = row 1
                       // Number of the columns in the sheet
                    columnCount,
                    // Number of the rows in the sheet
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new empty instance of LiveSheet forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetRangeString)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetRangeString, IList<IList<string>> newSheetRowsData)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<string>, string>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetRangeString, IList<IList<object>> newSheetRowsData)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<object>, object>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetRangeString, IList<IList<CellData>> newSheetRowsData)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet<Row, Cell>(string spreadsheetId, string sheetRangeString, IList<Row> newSheetRowsData)
                where Row : class, IList<Cell> where Cell : class
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet<Row, Cell>(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveSheet pre-filled with provided data, forced to instantiation without confirming appropriate LiveSheet and/or it content doesn't exists already. 
            internal LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetRangeString, IList<RowData> newSheetRowsData)
            {
                // Translate provided sheet range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided sheet range string to be translated into separate parameters.
                    sheetRangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewSheet(spreadsheetId, sheetTitleId, sheetRangeString, rowCount, columnCount, newSheetRowsData);
            }
            #endregion

            #region Range
            // Returns new instance of LiveRange force to instantiation without confirming appropriate Live doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);


                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex,  rowCount, columnCount);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount, IList<IList<string>> newSheetRowsData)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);


                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<string>, string>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }
            
            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount, IList<IList<object>> newSheetRowsData)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<object>, object>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount, IList<IList<CellData>> newSheetRowsData)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange<Row, Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount, IList<Row> newSheetRowsData)
                where Row : class, IList<Cell> where Cell : class
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<Row, Cell>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }
            
            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int rowCount, int columnCount, IList<RowData> newSheetRowsData)
            {
                // Get range string based on the provided parameters.
                string rangeString = RangeTranslator.GetRangeString(
                    // Range sheet title id
                    sheetTitleId,
                    // Range first left column sheet relative index.
                    leftColumnIndex,
                    // Range first top row sheet relative index.
                    topRowIndex,
                    // Number of the covered by the range
                    columnCount,
                    // Number of the rows covered by range
                    rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveRange force to instantiation without confirming appropriate Live doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string rangeString)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string rangeString, IList<IList<string>> newSheetRowsData)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<string>, string>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string rangeString, IList<IList<object>> newSheetRowsData)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<object>, object>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string rangeString, IList<IList<CellData>> newSheetRowsData)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<IList<CellData>, CellData>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange<Row, Cell>(string spreadsheetId, string rangeString, IList<Row> newSheetRowsData)
                where Row : class, IList<Cell> where Cell : class
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange<Row, Cell>(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }

            // Returns new instance of LiveRange pre-filled with provided data, forced to instantiation without confirming appropriate LiveRange and/or it content doesn't exists already. 
            internal LiveRange GetForcedNewRange(string spreadsheetId, string rangeString, IList<RowData> newSheetRowsData)
            {
                // Translate provided range string into separate parameters
                RangeTranslator.RangeStringToParameters(
                    // The provided range string to be translated into separate parameters.
                    rangeString,
                    // Retrieved range string sheet title id
                    out string sheetTitleId,
                    // Retrieved range string left column index
                    out int leftColumnIndex,
                    // Retrieved range string top row index
                    out int topRowIndex,
                    // Retrieved range string column count
                    out int columnCount,
                    // Retrieved range string row count
                    out int rowCount);

                // Return result of the overridden version of this method using all the parameters.
                return this.GetForcedNewRange(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, rowCount, columnCount, newSheetRowsData);
            }
            #endregion
            #endregion

            // TODO consider to remove
            #region GetFromExistingRows
            //#region Sheet
            //// Returns a instance of a LiveSheet class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveSheet GetSheetFromExistingRows(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<LiveRow> exsistingRows)
            //{
            //    // Get sheet string range based on the provided parameters.
            //    string sheetRangeString = RangeTranslator.GetRangeString(
            //        // Sheet title id
            //        sheetTitleId,
            //        // Sheet range left column index
            //        0, // column index 0 == column A
            //           // Sheet range top row index 
            //        0, // row index 0 = row 1
            //           // Number of the columns in the sheet
            //        columnCount,
            //        // Number of the rows in the sheet
            //        rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetSheetFromExistingRows(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, exsistingRows);
            //}

            //// Returns a instance of a LiveSheet class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveSheet GetSheetFromExistingRows(string spreadsheetId, string sheetRangeString, IList<LiveRow> exsistingRows)
            //{
            //    // Translate provided sheet range string into separate parameters
            //    RangeTranslator.RangeStringToParameters(
            //        // The provided sheet range string to be translated into separate parameters.
            //        sheetRangeString,
            //        // Retrieved range string sheet title id
            //        out string sheetTitleId,
            //        // Retrieved range string left column index
            //        out int leftColumnIndex,
            //        // Retrieved range string top row index
            //        out int topRowIndex,
            //        // Retrieved range string column count
            //        out int columnCount,
            //        // Retrieved range string row count
            //        out int rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetSheetFromExistingRows(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, exsistingRows);
            //}
            
            //// Returns a instance of a LiveSheet class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveSheet GetForcedNewSheetFromExistingRows(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<LiveRow> exsistingRows)
            //{
            //    // Get sheet string range based on the provided parameters.
            //    string sheetRangeString = RangeTranslator.GetRangeString(
            //        // Sheet title id
            //        sheetTitleId,
            //        // Sheet range left column index
            //        0, // column index 0 == column A
            //           // Sheet range top row index 
            //        0, // row index 0 = row 1
            //           // Number of the columns in the sheet
            //        columnCount,
            //        // Number of the rows in the sheet
            //        rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetForcedNewSheetFromExistingRows(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, exsistingRows);
            //}

            //// Returns a instance of a LiveSheet class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveSheet GetForcedNewSheetFromExistingRows(string spreadsheetId, string sheetRangeString, IList<LiveRow> exsistingRows)
            //{
            //    // Translate provided sheet range string into separate parameters
            //    RangeTranslator.RangeStringToParameters(
            //        // The provided sheet range string to be translated into separate parameters.
            //        sheetRangeString,
            //        // Retrieved range string sheet title id
            //        out string sheetTitleId,
            //        // Retrieved range string left column index
            //        out int leftColumnIndex,
            //        // Retrieved range string top row index
            //        out int topRowIndex,
            //        // Retrieved range string column count
            //        out int columnCount,
            //        // Retrieved range string row count
            //        out int rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetForcedNewSheetFromExistingRows(spreadsheetId, sheetTitleId, sheetRangeString, columnCount, rowCount, exsistingRows);
            //}
            //#endregion

            //#region Range
            //// Returns a instance of a LiveRange class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveRange GetRangeFromExistingRows(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<LiveRow> exsistingRows)
            //{
            //    // Get range string based on the provided parameters.
            //    string rangeString = RangeTranslator.GetRangeString(
            //        // Range sheet title id
            //        sheetTitleId,
            //        // Range first left column sheet relative index.
            //        leftColumnIndex,
            //        // Range first top row sheet relative index.
            //        topRowIndex,
            //        // Number of the covered by the range
            //        columnCount,
            //        // Number of the rows covered by range
            //        rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetRangeFromExistingRows(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, exsistingRows);
            //}

            //// Returns a instance of a LiveRange class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveRange GetRangeFromExistingRows(string spreadsheetId, string rangeString, IList<LiveRow> exsistingRows)
            //{
            //    // Translate provided range string into separate parameters
            //    RangeTranslator.RangeStringToParameters(
            //        // The provided range string to be translated into separate parameters.
            //        rangeString,
            //        // Retrieved range string sheet title id
            //        out string rangeSheetTitleId,
            //        // Retrieved range string left column index
            //        out int leftColumnIndex,
            //        // Retrieved range string top row index
            //        out int topRowIndex,
            //        // Retrieved range string column count
            //        out int columnCount,
            //        // Retrieved range string row count
            //        out int rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetRangeFromExistingRows(spreadsheetId, rangeSheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, exsistingRows);
            //}

            //// Returns a instance of a LiveRange class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveRange GetForcedNewRangeFromExistingRows(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<LiveRow> exsistingRows)
            //{
            //    // Get range string based on the provided parameters.
            //    string rangeString = RangeTranslator.GetRangeString(
            //        // Range sheet title id
            //        sheetTitleId,
            //        // Range first left column sheet relative index.
            //        leftColumnIndex,
            //        // Range first top row sheet relative index.
            //        topRowIndex,
            //        // Number of the covered by the range
            //        columnCount,
            //        // Number of the rows covered by range
            //        rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetForcedNewRangeFromExistingRows(spreadsheetId, sheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, exsistingRows);
            //}

            //// Returns a instance of a LiveRange class, build or retrieve based on, and filled with the provided existing cells list.
            //public LiveRange GetForcedNewRangeFromExistingRows(string spreadsheetId, string rangeString, IList<LiveRow> exsistingRows)
            //{
            //    // Translate provided range string into separate parameters
            //    RangeTranslator.RangeStringToParameters(
            //        // The provided range string to be translated into separate parameters.
            //        rangeString,
            //        // Retrieved range string sheet title id
            //        out string rangeSheetTitleId,
            //        // Retrieved range string left column index
            //        out int leftColumnIndex,
            //        // Retrieved range string top row index
            //        out int topRowIndex,
            //        // Retrieved range string column count
            //        out int columnCount,
            //        // Retrieved range string row count
            //        out int rowCount);

            //    // Return result of the overridden version of this method using all the parameters.
            //    return this.GetForcedNewRangeFromExistingRows(spreadsheetId, rangeSheetTitleId, rangeString, leftColumnIndex, topRowIndex, columnCount, rowCount, exsistingRows);
            //}
            //#endregion
            #endregion
            #endregion

            #region Methods - public/private not helpers - underlying LOGIC
            internal void ReIndexRange(string spreadsheetId, string sheetTitleId, string oldRangeString)
            {
                this._productionIndex.ReIndexRange(spreadsheetId, sheetTitleId, oldRangeString);
            }


            // Re-Indexing all the ranges stock associated with the provided sheet title id
            internal void ReIndexSheetRelatedInventory(string spreadsheetId, string sheetTitleId)
            {
                this._productionIndex.ReIndexSheetRelatedInventory(spreadsheetId, sheetTitleId);
            }

            #region GetExisting
            // Return existing instance of the LiveSheet if any is indexed to the provided combination of spreadsheet id and sheet title id, or null if none found.
            internal LiveSheet GetExistingSheet(string spreadsheetId, string sheetTitleId)
            {
                return this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId);
            }

            // Return existing instance of the LiveRange if any is indexed to the provided combination of spreadsheet id, sheet title id, and range string, or null if none found.
            internal LiveRange GetExistingRange(string spreadsheetId, string sheetTitleId, string rangeString)
            {
                return this._productionIndex.GetExistingRange(spreadsheetId, sheetTitleId, rangeString);
            }

            // Return all the LiveRange instances associated with the sheet specified with the provided parameters.
            internal IList<LiveRange> GetExistingSheetRanges(string spreadsheetId, string sheetTitleId)
            {
                try { return this._productionIndex[spreadsheetId][sheetTitleId].Item2.Values.ToList(); }
                catch { return new List<LiveRange>(); }
            }
            #endregion

            #region Get
            #region Sheet
            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.

            // Empty from parameters using already existing cells wherever available.
            private LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, string sheetRangeString, int columnCount, int rowCount)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

                // Get sheet range covering requested area if already existing.
                LiveSheet requestedSheet = this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId);

                // If requested sheet already exists and covers the area
                // specified with the provided sheet range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedSheet is null))
                {
                    // ... assures that existing LiveSheet has exactly requested number of columns and rows..
                    requestedSheet.AssureSheetSize(columnCount, rowCount);

                    // ...return that instance of LiveSheet.
                    return requestedSheet;
                }

                // Obtain new sheet rows content without providing any new data,
                // using appropriate sheet rows content creation method.
                IList<LiveRow> sheetRowsContent = this.BuildSheetRowsContentWithoutNewData(spreadsheetId, sheetTitleId, columnCount, rowCount);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Pre-filled with new row/cells pre-filled with provided data.
            private LiveSheet GetSheet<Row, Cell>(string spreadsheetId, string sheetTitleId, string sheetRangeString, int columnCount, int rowCount, IList<Row> newSheetRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

                // Get sheet range covering requested area if already existing.
                LiveSheet requestedSheet = this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId);

                // If requested sheet already exists and covers the area
                // specified with the provided range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedSheet is null))
                {
                    // ... assures that existing LiveSheet has exactly requested number of columns and rows..
                    requestedSheet.AssureSheetSize(columnCount, rowCount);

                    // ... and provided override existing data flag is true
                    // as well as newRowCellsData has been provided...
                    if (overrideExistingData && !(newSheetRowsData is null))
                        //  ...use the provided new range rows data to set
                        // the values of existing rows/cells...
                        requestedSheet.SetDataFromRowsData<Row, Cell>(
                            // Do not get confuse left buffer index with left column index
                            leftBufferIndex: 0,
                            // Do not get confuse top buffer index with top row index
                            topBufferIndex: 0,
                            // Rows data
                            rowsData: newSheetRowsData,
                            // Ignore null data flag set according inversed overrideOnNullInput flag
                            ignoreNullData: !overrideOnNullInput);

                    // ...return that instance of LiveSheet.
                    return requestedSheet;
                }

                // Obtain new sheet rows content without providing any new data,
                // using appropriate sheet rows content creation method.
                IList<LiveRow> sheetRowsContent = this.BuildSheetRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.

            // Pre-filled with new row/cells pre-filled with provided data.
            private LiveSheet GetSheet(string spreadsheetId, string sheetTitleId, string sheetRangeString, int columnCount, int rowCount, IList<RowData> newSheetRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

                // Get sheet range covering requested area if already existing.
                LiveSheet requestedSheet = this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId);

                // If requested sheet already exists and covers the area
                // specified with the provided range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedSheet is null))
                {
                    // ... assures that existing LiveSheet has exactly requested number of columns and rows..
                    requestedSheet.AssureSheetSize(columnCount, rowCount);

                    // ... and provided override existing data flag is true
                    // as well as newRowCellsData has been provided...
                    if (overrideExistingData && !(newSheetRowsData is null))
                        //  ...use the provided new range rows data to set
                        // the values of existing rows/cells...
                        requestedSheet.SetDataFromRowsData(
                            // Do not get confuse left buffer index with left column index
                            leftBufferIndex: 0,
                            // Do not get confuse top buffer index with top row index
                            topBufferIndex: 0,
                            // Rows data
                            rowsData: newSheetRowsData,
                            // Ignore null data flag set according inversed overrideOnNullInput flag
                            ignoreNullData: !overrideOnNullInput);

                    // ...return that instance of LiveSheet.
                    return requestedSheet;
                }

                // Obtain new sheet rows content without providing any new data,
                // using appropriate sheet rows content creation method.
                IList<LiveRow> sheetRowsContent = this.BuildSheetRowsContentWithNewData(spreadsheetId, sheetTitleId, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class instance.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }
            #endregion

            #region Range
            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.

            // Empty from parameters using already existing cells wherever available.
            private LiveRange GetRange(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
            {
                // Validate the provided parameters validity (assuming that the provided rangeString representing exactly the same value
                // as the separately provided parameters sheet title id, leftColumnIndex, topRowIndex, columnCount, rowCount.

                // If requested range has less then one column throw appropriate exception.
                if (columnCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more columns.", nameof(columnCount));
                // If requested range has less than one row throw appropriate exception.
                else if (rowCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more rows.", nameof(rowCount));
                // If requested sheet is located on non existing LiveSheet throw appropriate exception.
                else if (!this._productionIndex.HasSheet(spreadsheetId, sheetTitleId))
                    throw new ArgumentException("Unable to build range because provided parameters spreadsheetId: {spreadsheetId} and sheet title it: {sheetTitleId} specifying unexisting LiveSheet.");
                // Otherwise confirm that the requested range can be contained within the related sheet borders.
                else
                {
                    // Obtain instance of the sheet related with the requested range.
                    LiveSheet rangeRelatedSheet = this._productionIndex[spreadsheetId, sheetTitleId];

                    // Calculate the sheet relative index of the last, most right cell contained by the requested sheet.
                    int requestedRangeRighIndex = leftColumnIndex + columnCount - 1;

                    // Calculate the sheet relative index of the last, most bottom cell contained by the requested sheet.
                    int requestedRangeBottomIndex = topRowIndex + rowCount - 1;

                    // Check whether the sheet related to the requested LiveRange has enough columns to fit it....
                    if (rangeRelatedSheet.RightIndex < requestedRangeRighIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using columns outside of the borders of the sheet related with it.");
                    // Check whether the sheet related to the requested LiveRange has enough rows to fit it....
                    else if (rangeRelatedSheet.BottomIndex < requestedRangeBottomIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using rows outside of the borders of the sheet related with it.");
                }

                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);

                // Get range covering requested area if already existing.
                LiveRange requestedRange = this._productionIndex.GetExistingRange(spreadsheetId, sheetTitleId, rangeString);

                // If requested range already exists and covers the area
                // specified with the provided range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedRange is null))
                    // ...return that instance of LiveRange.
                    return requestedRange;

                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildRangeRowsContentWithoutNewData(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object
            private LiveRange GetRange<Row, Cell>(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<Row> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Validate the provided parameters validity (assuming that the provided rangeString representing exactly the same value
                // as the separately provided parameters sheet title id, leftColumnIndex, topRowIndex, columnCount, rowCount.

                // If requested range has less then one column throw appropriate exception.
                if (columnCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more columns.", nameof(columnCount));
                // If requested range has less than one row throw appropriate exception.
                else if (rowCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more rows.", nameof(rowCount));
                // If requested sheet is located on non existing LiveSheet throw appropriate exception.
                else if (!this._productionIndex.HasSheet(spreadsheetId, sheetTitleId))
                    throw new ArgumentException("Unable to build range because provided parameters spreadsheetId: {spreadsheetId} and sheet title it: {sheetTitleId} specifying unexisting LiveSheet.");
                // Otherwise confirm that the requested range can be contained within the related sheet borders.
                else
                {
                    // Obtain instance of the sheet related with the requested range.
                    LiveSheet rangeRelatedSheet = this._productionIndex[spreadsheetId, sheetTitleId];

                    // Calculate the sheet relative index of the last, most right cell contained by the requested sheet.
                    int requestedRangeRighIndex = leftColumnIndex + columnCount - 1;

                    // Calculate the sheet relative index of the last, most bottom cell contained by the requested sheet.
                    int requestedRangeBottomIndex = topRowIndex + rowCount - 1;

                    // Check whether the sheet related to the requested LiveRange has enough columns to fit it....
                    if (rangeRelatedSheet.RightIndex < requestedRangeRighIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using columns outside of the borders of the sheet related with it.");
                    // Check whether the sheet related to the requested LiveRange has enough rows to fit it....
                    else if (rangeRelatedSheet.BottomIndex < requestedRangeBottomIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using rows outside of the borders of the sheet related with it.");
                }

                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);

                // Get range covering requested area if already existing.
                LiveRange requestedRange = this._productionIndex.GetExistingRange(spreadsheetId, sheetTitleId, rangeString);

                // If requested range already exists and covers the area
                // specified with the provided range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedRange is null))
                {
                    // ... and provided override existing data flag is true
                    // as well as newRowCellsData has been provided...
                    if (overrideExistingData && !(newRangeRowsData is null))
                        //  ...use the provided new range rows data to set
                        // the values of existing rows/cells...
                        requestedRange.SetDataFromRowsData<Row, Cell>(
                            // Do not get confuse left buffer index with left column index
                            leftBufferIndex: 0,
                            // Do not get confuse top buffer index with top row index
                            topBufferIndex: 0, 
                            // Rows data
                            rowsData: newRangeRowsData, 
                            // Ignore null data flag set according inversed overrideOnNullInput flag
                            ignoreNullData: !overrideOnNullInput);

                        // ...return that instance of LiveRange.
                    return requestedRange;
                }

                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildRangeRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.
            private LiveRange GetRange(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<RowData> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Validate the provided parameters validity (assuming that the provided rangeString representing exactly the same value
                // as the separately provided parameters sheet title id, leftColumnIndex, topRowIndex, columnCount, rowCount.

                // If requested range has less then one column throw appropriate exception.
                if (columnCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more columns.", nameof(columnCount));
                // If requested range has less than one row throw appropriate exception.
                else if (rowCount < 1)
                    throw new ArgumentException("LiveRange instance has to have 1 or more rows.", nameof(rowCount));
                // If requested sheet is located on non existing LiveSheet throw appropriate exception.
                else if (!this._productionIndex.HasSheet(spreadsheetId, sheetTitleId))
                    throw new ArgumentException("Unable to build range because provided parameters spreadsheetId: {spreadsheetId} and sheet title it: {sheetTitleId} specifying unexisting LiveSheet.");
                // Otherwise confirm that the requested range can be contained within the related sheet borders.
                else
                {
                    // Obtain instance of the sheet related with the requested range.
                    LiveSheet rangeRelatedSheet = this._productionIndex[spreadsheetId, sheetTitleId];

                    // Calculate the sheet relative index of the last, most right cell contained by the requested sheet.
                    int requestedRangeRighIndex = leftColumnIndex + columnCount - 1;

                    // Calculate the sheet relative index of the last, most bottom cell contained by the requested sheet.
                    int requestedRangeBottomIndex = topRowIndex + rowCount - 1;

                    // Check whether the sheet related to the requested LiveRange has enough columns to fit it....
                    if (rangeRelatedSheet.RightIndex < requestedRangeRighIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using columns outside of the borders of the sheet related with it.");
                    // Check whether the sheet related to the requested LiveRange has enough rows to fit it....
                    else if (rangeRelatedSheet.BottomIndex < requestedRangeBottomIndex)
                        // ...and throw appropriate exception if confirmed that it doesn't.
                        throw new ArgumentException("The provided parameters specified the range using rows outside of the borders of the sheet related with it.");
                }

                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);

                // Get range covering requested area if already existing.
                LiveRange requestedRange = this._productionIndex.GetExistingRange(spreadsheetId, sheetTitleId, rangeString);

                // If requested range already exists and covers the area
                // specified with the provided range string exactly
                // - no more or less rows and/or columns... 
                if (!(requestedRange is null))
                {
                    // ... and provided override existing data flag is true
                    // as well as newRowCellsData has been provided...
                    if (overrideExistingData && !(newRangeRowsData is null))
                        //  
                        requestedRange.SetDataFromRowsData(
                            // Do not get confuse left buffer index with left column index
                            leftBufferIndex: 0,
                            // Do not get confuse top buffer index with top row index
                            topBufferIndex: 0,
                            // Rows data
                            rowsData: newRangeRowsData,
                            // Ignore null data flag set according inversed overrideOnNullInput flag
                            ignoreNullData: !overrideOnNullInput);

                    // ...return that instance of LiveRange.
                    return requestedRange;
                }

                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildRangeRowsContentWithNewData(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }
            #endregion  
            #endregion

            #region GetForcedNew
            #region Sheet
            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.

            // Returns new instance of LiveSheet force to instantiation without confirming appropriate LiveSheet doesn't exists already. 
            private LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, string sheetRangeString, int rowCount, int columnCount)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);
                
                // Get instance of existing cells placeholder list without reusing existing rows/cells
                // and forcing they creation instead.
                IList<LiveRow> sheetRowsContent = this.BuildForcedSheetRowsContentWithoutNewData(spreadsheetId, sheetTitleId, columnCount, rowCount);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveSheet force to instantiation without confirming appropriate LiveSheet doesn't exists already. 
            private LiveSheet GetForcedNewSheet<Row, Cell>(string spreadsheetId, string sheetTitleId, string sheetRangeString, int rowCount, int columnCount, IList<Row> newSheetRowsData = null)
                where Row : class, IList<Cell> where Cell : class
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

                // Get instance of existing cells placeholder list without reusing existing rows/cells
                // and forcing they creation instead.
                IList<LiveRow> sheetRowsContent = this.BuildForcedSheetRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, columnCount, rowCount, newSheetRowsData);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must assure that valid parameters has been provided. Following things have to be assured:
            // - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            // as the same size of the sheet as provided integer parameters column count and row count.

            // Returns new instance of LiveSheet force to instantiation without confirming appropriate LiveSheet doesn't exists already. 
            private LiveSheet GetForcedNewSheet(string spreadsheetId, string sheetTitleId, string sheetRangeString, int rowCount, int columnCount, IList<RowData> newSheetRowsData = null)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

                // Get instance of existing cells placeholder list without reusing existing rows/cells
                // and forcing they creation instead.
                IList<LiveRow> sheetRowsContent = this.BuildForcedSheetRowsContentWithNewData(spreadsheetId, sheetTitleId, columnCount, rowCount, newSheetRowsData);

                // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
                return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, sheetRowsContent);
            }
            #endregion

            #region Range
            // IMPORTANT NOTE:
            // Method caller must be absolutely sure that instances of LiveRow/LiveCell for this range hasn't been yet initialized.
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.

            // Empty from parameters using already existing cells wherever available.
            private LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);
                
                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildForcedRangeRowsContentWithoutNewData(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must be absolutely sure that instances of LiveRow/LiveCell for this range hasn't been yet initialized.
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns new instance of LiveSheet force to instantiation without confirming appropriate LiveRange doesn't exists already. 
            private LiveRange GetForcedNewRange<Row, Cell>(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<Row> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Row : class, IList<Cell> where Cell : class
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);
                
                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildForcedRangeRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }

            // IMPORTANT NOTE:
            // Method caller must be absolutely sure that instances of LiveRow/LiveCell for this range hasn't been yet initialized.
            // - string rangeString string must describing the same sheet as the sheet title id is referring to, as well
            // as the same position on/size of the range as described by the provided integer parameters left column index,
            // top row index, column count and row count.

            // Returns new instance of LiveSheet force to instantiation without confirming appropriate LiveRange doesn't exists already. 
            private LiveRange GetForcedNewRange(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<RowData> newRangeRowsData = null, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);
                
                // Obtain new range rows content without providing any new data,
                // using appropriate range rows content creation method.
                IList<LiveRow> rangeRowsContent = this.BuildForcedRangeRowsContentWithNewData(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, newRangeRowsData);

                // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
                return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, rangeRowsContent);
            }
            #endregion
            #endregion

            #region GetFromExsistingRows
            //#region Sheet
            //// IMPORTANT NOTE:
            //// Method caller must assure that valid parameters has been provided. Following things have to be assured:
            //// - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            //// as the same size of the sheet as provided integer parameters column count and row count, as well as that
            //// area must match the provided existing rows list exactly.

            //// Returns instance of a LiveSheet class build based on the provided existing rows.
            //private LiveSheet GetSheetFromExistingRows(string spreadsheetId, string sheetTitleId, string sheetRangeString, int columnCount, int rowCount, IList<LiveRow> existingRows)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

            //    // Get sheet range covering requested area if already existing.
            //    LiveSheet requestedSheet = this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId, sheetRangeString);

            //    // If requested sheet already exists and covers the area
            //    // specified with the provided sheet range string exactly
            //    // - no more or less rows and/or columns... 
            //    if (!(requestedSheet is null))
            //        // ...return that instance of LiveSheet.
            //        return requestedSheet;

            //    // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
            //    return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, existingRows);
            //}

            //// IMPORTANT NOTE:
            //// Method caller must assure that valid parameters has been provided. Following things have to be assured:
            //// - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            //// as the same size of the sheet as provided integer parameters column count and row count, as well as that
            //// area must match the provided existing rows list exactly.

            //// Returns instance of a LiveRange class build based on the provided existing rows.
            //private LiveRange GetRangeFromExistingRows(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<LiveRow> existingRows)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);

            //    // Get range covering requested area if already existing.
            //    LiveRange requestedRange = this._productionIndex.GetExistingRange(spreadsheetId, sheetTitleId, rangeString);

            //    // If requested range already exists and covers the area
            //    // specified with the provided range string exactly
            //    // - no more or less rows and/or columns... 
            //    if (!(requestedRange is null))
            //        // ...return that instance of LiveRange.
            //        return requestedRange;

            //    // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
            //    return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, existingRows);
            //}
            //#endregion

            //#region Range
            //// IMPORTANT NOTE:
            //// Method caller must assure that valid parameters has been provided. Following things have to be assured:
            //// - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            //// as the same size of the range as provided integer parameters column count and row count, as well as that
            //// area must match the provided existing rows list exactly.

            //// Return forced new instance of a LiveRow class, build based on, and pre-filled with the provided existing cells list.
            //private LiveSheet GetForcedNewSheetFromExistingRows(string spreadsheetId, string sheetTitleId, string sheetRangeString, int columnCount, int rowCount, IList<LiveRow> existingRows)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureSheetStorageSpace(spreadsheetId, sheetTitleId, sheetRangeString);

            //    // Based on the provided parameters and sheet rows content, manufacture and return instance of LiveSheet class.
            //    return this.ManufactureSheet(spreadsheetId, sheetTitleId, columnCount, rowCount, existingRows);
            //}

            //// IMPORTANT NOTE:
            //// Method caller must assure that valid parameters has been provided. Following things have to be assured:
            //// - string sheetRangeString must describing the same sheet as the sheet title id is referring to, as well
            //// as the same size of the range as provided integer parameters column count and row count, as well as that
            //// area must match the provided existing rows list exactly.

            //// Return forced new instance of a LiveRow class, build based on, and pre-filled with the provided existing cells list.
            //private LiveRange GetForcedNewRangeFromExistingRows(string spreadsheetId, string sheetTitleId, string rangeString, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<LiveRow> existingRows)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureRangeStorageSpace(spreadsheetId, sheetTitleId, rangeString);

            //    // Based on the provided parameters and range rows content, manufacture and return instance of LiveRange class.
            //    return this.ManufactureRange(spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount, existingRows);
            //}
            //#endregion
            #endregion

            #region RemoveStored
            // Simply removed range stored in the factory production index.
            internal void RemoveStoredSheet(string spreadsheetId, string sheetTitleId)
            {
                this._productionIndex.RemoveSheet(spreadsheetId, sheetTitleId);
            }

            // Simply removed range stored in the factory production index.
            internal void RemoveStoredRange(string spreadsheetId, string sheetTitleId, string sheetRangeString)
            {
                this._productionIndex.RemoveRange(spreadsheetId, sheetTitleId, sheetRangeString);
            }
            #endregion
            #endregion

            #region Private Helpers
            // IMPORTANT NOTE
            // The unusual code design - repeatable code below - its a performance optimize design.
            // As many as possible if/else checks has been move into the level above to be 
            // executed once for each range/sheet rather then once for each row, and then based on that
            // execute appropriate build range/sheet rows content method.
            #region Build sheet rows/cells content
            #region Sheet
            #region Build
            // Builds and returns content of the sheet specified with the provided method parameters
            // spreadsheetId, sheetTitleId, columnCount, rowCount
            private IList<LiveRow> BuildSheetRowsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
            {
                // Provide 0, 0, as sheet content always covers entirety of the sheet.
                return this.BuildRangeRowsContentWithoutNewData(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Builds and returns content of the sheet specified with the provided method parameters
            // spreadsheetId, sheetTitleId, columnCount, rowCount. 
            private IList<LiveRow> BuildSheetRowsContentWithNewData<Row, Cell>(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<Row> newSheetRowsData, bool overrideExistingData, bool overrideOnNullInput)
                where Row : class, IList<Cell> where Cell : class
            {
                // Provide 0, 0, as range content always covers entirety of the sheet. Do not override existing content.
                // At this point there should be no existing rows data for this sheet to be overridden anyway.
                return this.BuildRangeRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, 0,0, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }

            // Builds and returns content of the sheet specified with the provided method parameters
            // spreadsheetId, sheetTitleId, columnCount, rowCount
            private IList<LiveRow> BuildSheetRowsContentWithNewData(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<RowData> newSheetRowsData, bool overrideExistingData, bool overrideOnNullInput)
            {
                // Provide 0, 0, as range content always covers entirety of the sheet. Do not override existing content.
                // At this point there should be no existing rows data for this sheet to be overridden anyway.
                return this.BuildRangeRowsContentWithNewData(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount, newSheetRowsData, overrideExistingData, overrideOnNullInput);
            }
            #endregion

            // SIDENOTE unlike on LiveRow.LiveRowFactory level, building forced, or not forced ranged rows content could be decided
            // based on the provided bool parameter rather then be separate version of the method which is nearly identical.
            // Only difference being calling LiveRow.Factory.GetForced(..) rather then LiveRow.Factory.Get(..)
            #region BuildForced
            // Returns content of the row forced to be instantiated without trying to reusing existing rows/cells.
            private IList<LiveRow> BuildForcedSheetRowsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
            {
                return this.BuildForcedRangeRowsContentWithoutNewData(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount);
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns content of the row forced to be instantiated without trying to reusing existing rows/cells.
            private IList<LiveRow> BuildForcedSheetRowsContentWithNewData<Row, Cell>(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<Row> rowsData)
                    where Row : class, IList<Cell> where Cell : class
            {
                return this.BuildForcedRangeRowsContentWithNewData<Row, Cell>(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount, rowsData);
            }
            
            // Returns content of the row forced to be instantiated without trying to reusing existing rows/cells.
            private IList<LiveRow> BuildForcedSheetRowsContentWithNewData(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<RowData> rowsData)
            {
                return this.BuildForcedRangeRowsContentWithNewData(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount, rowsData);
            }
            #endregion
            #endregion

            #region Range
            #region Build
            // Builds and returns content of the range specified with the provided method parameters
            // spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount
            private IList<LiveRow> BuildRangeRowsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
            {
                // Declare range rows content placeholder list.
                IList<LiveRow> rangeRowsContent = new List<LiveRow>();

                // Obtain existing rows/cells located on the area covered by the range specified with the provided parameters.
                // Loop from 0 to < rowCount 
                for (int rowRangeRelativeIndex = 0; rowRangeRelativeIndex < rowCount; rowRangeRelativeIndex++)
                {
                    // Calculate row sheet relative index for 
                    // this specific row range relative index.
                    int rowSheetRelativeIndex = rowRangeRelativeIndex + topRowIndex;

                    // Use row sheet relative index to obtain appropriate instance of the LiveRow.
                    LiveRow rangeContentRow = LiveRow.Factory.Get(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount);

                    // Add that LiveRow into the range rows content collection
                    rangeRowsContent.Add(rangeContentRow);
                }

                // Return constructed range rows/cells content.
                return rangeRowsContent;
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Builds and returns content of the range specified with the provided method parameters
            // spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount
            private IList<LiveRow> BuildRangeRowsContentWithNewData<Row, Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<Row> newSheetRowsData, bool overrideExsistingData, bool overrideOnNullInput)
                where Row : class, IList<Cell> where Cell : class
            {
                // Declare range rows content placeholder list.
                IList<LiveRow> rangeRowsContent = new List<LiveRow>();

                // Obtain existing rows/cells located on the area covered by the range specified with the provided parameters.
                // Loop from 0 to < rowCount 
                for (int rowRangeRelativeIndex = 0; rowRangeRelativeIndex < rowCount; rowRangeRelativeIndex++)
                {
                    // Calculate row sheet relative index for 
                    // this specific row range relative index.
                    int rowSheetRelativeIndex = rowRangeRelativeIndex + topRowIndex;

                    // Obtain new data for that row if any available
                    IList<Cell> newSheetRowData = newSheetRowsData?.ElementAtOrDefault(rowRangeRelativeIndex);

                    // Use row sheet relative index to obtain appropriate instance of the LiveRow.
                    LiveRow rangeContentRow = LiveRow.Factory.Get<Cell>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newSheetRowData, overrideExsistingData, overrideOnNullInput);

                    // Add that LiveRow into the range rows content collection
                    rangeRowsContent.Add(rangeContentRow);
                }

                // Return constructed range rows/cells content.
                return rangeRowsContent;
            }

            // Builds and returns content of the range specified with the provided method parameters
            // spreadsheetId, sheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount
            private IList<LiveRow> BuildRangeRowsContentWithNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<RowData> newSheetRowsData, bool overrideExsistingData, bool overrideOnNullInput)
            {
                // Declare range rows content placeholder list.
                IList<LiveRow> rangeRowsContent = new List<LiveRow>();

                // Obtain existing rows/cells located on the area covered by the range specified with the provided parameters.
                // Loop from 0 to < rowCount 
                for (int rowRangeRelativeIndex = 0; rowRangeRelativeIndex < rowCount; rowRangeRelativeIndex++)
                {
                    // Calculate row sheet relative index for 
                    // this specific row range relative index.
                    int rowSheetRelativeIndex = rowRangeRelativeIndex + topRowIndex;

                    // Obtain new data for that row if any available
                    RowData newSheetRowData = newSheetRowsData?.ElementAtOrDefault(rowRangeRelativeIndex);

                    // Use row sheet relative index to obtain appropriate instance of the LiveRow.
                    LiveRow rangeContentRow = LiveRow.Factory.Get(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newSheetRowData, overrideExsistingData, overrideOnNullInput);

                    // Add that LiveRow into the range rows content collection
                    rangeRowsContent.Add(rangeContentRow);
                }

                // Return constructed range rows/cells content.
                return rangeRowsContent;
            }
            #endregion

            // SIDENOTE unlike on LiveRow.LiveRowFactory level, building forced, or not forced ranged rows content could be decided
            // based on the provided bool parameter rather then be separate version of the method which is nearly identical.
            // Only difference being calling LiveRow.Factory.GetForced(..) rather then LiveRow.Factory.Get(..)
            #region BuildForced
            // Returns content of the row forced to be instantiated without trying to reusing existing cells.
            private IList<LiveRow> BuildForcedRangeRowsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
            {
                // Declare forced range rows content placeholder list.
                IList<LiveRow> forcedRangeRowsContent = new List<LiveRow>();

                // Fill empty forced rows content placeholder with appropriate number of rows
                // for the range of size and location specified with provided parameters.
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Determine row sheet relative index of the row to be obtained
                    // by adding top row index, to the row order index.
                    int rowSheetRelativeIndex = topRowIndex + rowIndex;

                    // Obtain appropriate row instance.
                    LiveRow newRangeContentRow = LiveRow.Factory.GetForcedNew(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount);

                    // Add obtained row to sheet rows content list.
                    forcedRangeRowsContent.Add(newRangeContentRow);
                }

                // Return constructed forced range rows/cells content.
                return forcedRangeRowsContent;
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns content of the row forced to be instantiated without trying to reusing existing cells.
            private IList<LiveRow> BuildForcedRangeRowsContentWithNewData<Row, Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<Row> rowsData)
               where Row : class, IList<Cell> where Cell : class
            {
                // Declare forced range rows content placeholder list.
                IList<LiveRow> forcedRangeRowsContent = new List<LiveRow>();

                // Fill empty forced rows content placeholder with appropriate number of rows
                // for the range of size and location specified with provided parameters.
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Determine row sheet relative index of the row to be obtained
                    // by adding top row index, to the row order index.
                    int rowSheetRelativeIndex = topRowIndex + rowIndex;

                    // Get row data of the type depending on the generic type the current method.
                    Row rowData = rowsData?.ElementAtOrDefault(rowIndex);

                    // Obtain appropriate row instance.
                    LiveRow newRangeContentRow = LiveRow.Factory.GetForcedNew<Cell>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, rowData);

                    // Add obtained row to sheet rows content list.
                    forcedRangeRowsContent.Add(newRangeContentRow);
                }

                // Return constructed forced range rows/cells content.
                return forcedRangeRowsContent;
            }

            // IMPORTANT NOTE:
            // - unwritten constrain: generic parameter Cell should inherit from string, CellData or object

            // Returns content of the row forced to be instantiated without trying to reusing existing cells.
            private IList<LiveRow> BuildForcedRangeRowsContentWithNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<RowData> rowsData)
            {
                // Declare forced range rows content placeholder list.
                IList<LiveRow> forcedRangeRowsContent = new List<LiveRow>();

                // Fill empty forced rows content placeholder with appropriate number of rows
                // for the range of size and location specified with provided parameters.
                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    // Determine row sheet relative index of the row to be obtained
                    // by adding top row index, to the row order index.
                    int rowSheetRelativeIndex = topRowIndex + rowIndex;

                    // Get row data of the type depending on the generic type the current method.
                    RowData rowData = rowsData?.ElementAtOrDefault(rowIndex);

                    // Obtain appropriate row instance.
                    LiveRow newRangeContentRow = LiveRow.Factory.GetForcedNew(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, rowData);

                    // Add obtained row to sheet rows content list.
                    forcedRangeRowsContent.Add(newRangeContentRow);
                }

                // Return constructed forced range rows/cells content.
                return forcedRangeRowsContent;
            }
            #endregion
            #endregion
            #endregion

            // The only method which should call the LiveRange constructor.
            private LiveRange ManufactureRange(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int topRowIndex, int columnCount, int rowCount, IList<LiveRow> rangeRowsContent)
            {
                // Build new instance of LiveRange class using provided parameters...
                LiveRange newRange = new LiveRange(
                        // Range rows content spreadsheet id
                        spreadsheetId,
                        // Range rows content sheet title id
                        sheetTitleId,
                        // Range most left column sheet relative index
                        leftColumnIndex,
                        // Range most top row sheet relative index
                        topRowIndex,
                        // Total number of the columns covered by the range 
                        columnCount,
                        // Total number of the rows covered by the range 
                        rowCount,
                        // Range rows content.
                        rangeRowsContent);
                        
                // Add newly instantiated LiveRange into the factory production index. 
                this._productionIndex.AddSpecificRange(newRange);

                // Get LiveSheet containing LiveRange
                this._productionIndex.GetExistingSheet(spreadsheetId, sheetTitleId);

                // Return newly created range.
                return newRange;
            }

            // The only method which should call the LiveSheet constructor.
            private LiveSheet ManufactureSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<LiveRow> sheetRowsContent)
            {
                // Build new instance of LiveSheet class using provided parameters...
                LiveSheet newSheet = new LiveSheet(
                        // Sheet rows content spreadsheet id
                        spreadsheetId,
                        // Sheet rows content sheet title id
                        sheetTitleId,
                        // Total number of the columns covered by the range 
                        columnCount,
                        // Total number of the rows covered by the range 
                        rowCount,
                        // Sheet rows content.
                        sheetRowsContent);

                // Add newly instantiated LiveSheet into the factory production index. 
                this._productionIndex.AddSpecificSheet(newSheet);

                // Return newly created sheet.
                return newSheet;
            }
            #endregion
        }
    }
}
