using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public partial class LiveSpreadsheet
    {
        #region Factory singleton
        // Factory singleton thread safety lock.
        private static readonly object _lock = new object();

        // LiveRow factory thread-safe singleton.
        public static LiveSpreadsheetFactory _factory;
        public static LiveSpreadsheetFactory Factory
        {
            get
            {
                if (LiveSpreadsheet._factory is null)
                    lock (LiveSpreadsheet._lock)
                        if (LiveSpreadsheet._factory is null)
                            LiveSpreadsheet._factory = new LiveSpreadsheetFactory();

                return LiveSpreadsheet._factory;
            }
        }
        #endregion

        public class LiveSpreadsheetFactory
        {
            #region Private Fields
            // FactoryProductionIndex singleton - stores data allowing to assure that none of the sheet/range/rows/cells will be created more then once.
            private FactoryProductionIndex _productionIndex { get; } = new FactoryProductionIndex();
            #endregion

            #region Methods
            internal LiveSpreadsheet GetExistingSpreadsheet(string spreadsheetId)
            {
                return this._productionIndex[spreadsheetId];
            }

            public IDictionary<string, LiveSpreadsheet> GetAllExisting()
            {
                return this._productionIndex.GetAllExisting();
            }


            // Simply removed range stored in the factory production index.
            internal void RemoveStored(string spreadsheetId)
            {
                this._productionIndex.RemoveSpreadsheet(spreadsheetId);
            }


            #region Get
            // IMPORTANT NOTE:
            // Tuple<string,int,int,int,int> represents following parameters of sheet range:
            // Tuple<string,int,int,int,int>.Item1 - sheet title id.
            // Tuple<string,int,int,int,int>.Item2 - column count.
            // Tuple<string,int,int,int,int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IList<string> spreadsheetSheetsRanges)
            {
                // Based on the provided spreadsheet sheet data build spreadsheet size blueprint.
                IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = spreadsheetSheetsRanges.Select((sheetRangeString) =>
                {
                    // Translate currently looped through sheet range string into separate parameters
                    RangeTranslator.RangeStringToParameters(
                        // The sheet range string to be translated into separate parameters.
                        sheetRangeString,
                        // Retrieved range string sheet title id
                        out string requiredSheetTitleId,
                        // Retrieved range string left column index
                        out int requiredLeftColumnIndex,
                        // Retrieved range string top row index
                        out int requiredTopRowIndex,
                        // Retrieved range string column count
                        out int requiredColumnCount,
                        // Retrieved range string row count
                        out int requiredRowCount);

                    // Return tuple build based on the provided sheet data.
                    return new Tuple<string, int, int>(requiredSheetTitleId, requiredColumnCount, requiredRowCount);
                }).ToList();

                // Use obtained blueprint to execute, and return result of the appropriate overloaded version of the method.
                return this.Get(spreadsheetId, title, spreadsheetSizeBlueprint);
            }

            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get<Sheet, Row, Cell>(string spreadsheetId, string title, IList<string> spreadsheetSheetsRanges, IDictionary<string, Sheet> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Sheet : class, IList<Row> where Row : class, IList<Cell> where Cell : class
            {
                // Based on the provided spreadsheet sheet data build spreadsheet size blueprint.
                IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = spreadsheetSheetsRanges.Select((sheetRangeString) =>
                {
                    // Translate currently looped through sheet range string into separate parameters
                    RangeTranslator.RangeStringToParameters(
                        // The sheet range string to be translated into separate parameters.
                        sheetRangeString,
                        // Retrieved range string sheet title id
                        out string requiredSheetTitleId,
                        // Retrieved range string left column index
                        out int requiredLeftColumnIndex,
                        // Retrieved range string top row index
                        out int requiredTopRowIndex,
                        // Retrieved range string column count
                        out int requiredColumnCount,
                        // Retrieved range string row count
                        out int requiredRowCount);

                    // Return tuple build based on the provided sheet data.
                    return new Tuple<string, int, int>(requiredSheetTitleId, requiredColumnCount, requiredRowCount);
                }).ToList();

                // Use obtained blueprint to execute, and return result of the appropriate overloaded version of the method.
                return this.Get<Sheet, Row, Cell>(spreadsheetId, title, spreadsheetSizeBlueprint, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IList<string> spreadsheetSheetsRanges, IDictionary<string, IList<RowData>> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Based on the provided spreadsheet sheet data build spreadsheet size blueprint.
                IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = spreadsheetSheetsRanges.Select((sheetRangeString) =>
                {
                    // Translate currently looped through sheet range string into separate parameters
                    RangeTranslator.RangeStringToParameters(
                        // The sheet range string to be translated into separate parameters.
                        sheetRangeString,
                        // Retrieved range string sheet title id
                        out string requiredSheetTitleId,
                        // Retrieved range string left column index
                        out int requiredLeftColumnIndex,
                        // Retrieved range string top row index
                        out int requiredTopRowIndex,
                        // Retrieved range string column count
                        out int requiredColumnCount,
                        // Retrieved range string row count
                        out int requiredRowCount);

                    // Return tuple build based on the provided sheet data.
                    return new Tuple<string, int, int>(requiredSheetTitleId, requiredColumnCount, requiredRowCount);
                }).ToList();

                // Use obtained blueprint to execute, and return result of the appropriate overloaded version of the method.
                return this.Get(spreadsheetId, title, spreadsheetSizeBlueprint, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);
            }

            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
            {
                // Get sheet range covering requested area if already existing.
                LiveSpreadsheet requestedSpreadsheet = this._productionIndex.GetExistingSpreadsheet(spreadsheetId);

                // If requested spreadsheet already exists.... 
                if (!(requestedSpreadsheet is null))
                {
                    // Assure that existing spreadsheet containing exactly right number
                    // of sheet of exactly specified size.
                    requestedSpreadsheet.AssureSpreadsheetSize(spreadsheetSizeBlueprint);

                    // Return that instance of LiveSpreadsheet.
                    return requestedSpreadsheet;
                }

                // Obtain new spreadsheet sheets content without providing any new data,
                // using appropriate spreadsheet sheets content creation method.
                IDictionary<string, LiveSheet> spreadsheetSheetsContent = this.BuilSpreadsheetSheetsContentWithoutNewData(spreadsheetId, spreadsheetSizeBlueprint);

                // Based on the provided parameters and spreadsheet sheet content, manufacture instance of LiveSpreadsheet class instance.
                return this.Manufacture(spreadsheetId, title, spreadsheetSheetsContent);
            }

            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get<Sheet, Row, Cell>(string spreadsheetId, string title, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint, IDictionary<string, Sheet> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Sheet : class, IList<Row> where Row : class, IList<Cell> where Cell : class
            {
                // Get sheet range covering requested area if already existing.
                LiveSpreadsheet requestedSpreadsheet = this._productionIndex.GetExistingSpreadsheet(spreadsheetId);

                // If requested spreadsheet already exists.... 
                if (!(requestedSpreadsheet is null))
                {
                    // Assure that existing spreadsheet containing exactly right number
                    // of sheet of exactly specified size.
                    requestedSpreadsheet.AssureSpreadsheetSize(spreadsheetSizeBlueprint);
                    
                    // Fill the existing sheet with the provided data if the overrideExistingData flag indicating so.
                    if (overrideExistingData)
                        requestedSpreadsheet.SetDataFromSheetsDataIndex<Sheet, Row, Cell>(newSpreadsheetSheetsData, !overrideOnNullInput);

                    // Return that instance of LiveSpreadsheet.
                    return requestedSpreadsheet;
                }

                // Obtain new spreadsheet sheets content without providing any new data,
                // using appropriate spreadsheet sheets content creation method.
                IDictionary<string, LiveSheet> spreadsheetSheetsContent = this.BuilSpreadsheetSheetsContentWithNewData<Sheet, Row, Cell>(spreadsheetId, spreadsheetSizeBlueprint, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and spreadsheet sheet content, manufacture instance of LiveSpreadsheet class instance.
                return this.Manufacture(spreadsheetId, title, spreadsheetSheetsContent);
            }
            
            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint, IDictionary<string, IList<RowData>> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet range covering requested area if already existing.
                LiveSpreadsheet requestedSpreadsheet = this._productionIndex.GetExistingSpreadsheet(spreadsheetId);

                // If requested spreadsheet already exists.... 
                if (!(requestedSpreadsheet is null))
                {
                    // Assure that existing spreadsheet containing exactly right number
                    // of sheet of exactly specified size.
                    requestedSpreadsheet.AssureSpreadsheetSize(spreadsheetSizeBlueprint);

                    // Fill the existing sheet with the provided data if the overrideExistingData flag indicating so.
                    if (overrideExistingData) requestedSpreadsheet.SetDataFromRowsListsIndex(newSpreadsheetSheetsData, !overrideOnNullInput);

                    // Return that instance of LiveSpreadsheet.
                    return requestedSpreadsheet;
                }

                // Obtain new spreadsheet sheets content without providing any new data,
                // using appropriate spreadsheet sheets content creation method.
                IDictionary<string, LiveSheet> spreadsheetSheetsContent = this.BuilSpreadsheetSheetsContentWithNewData(spreadsheetId, spreadsheetSizeBlueprint, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and spreadsheet sheet content, manufacture instance of LiveSpreadsheet class instance.
                return this.Manufacture(spreadsheetId, title, spreadsheetSheetsContent);
            }

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IDictionary<string, Sheet> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet range covering requested area if already existing.
                LiveSpreadsheet requestedSpreadsheet = this._productionIndex.GetExistingSpreadsheet(spreadsheetId);

                // If requested spreadsheet already exists.... 
                if (!(requestedSpreadsheet is null))
                {
                    // Based on the provided spreadsheet sheet data build spreadsheet size blueprint.
                    IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = newSpreadsheetSheetsData.Select((item) =>
                    {
                        // Provided instances of Google.Apis.Sheets.v4.Data.Sheet
                        // must contain value other than null for some basic parameters like
                        // sheet title id, column count and row count.
                        string requiredSheetTitleId = item.Key;
                        int requiredColumnCount = item.Value.Properties?.GridProperties.ColumnCount
                            // Or if no int value can be found for column count, throw appropriate exception.
                            ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.");
                        int requiredRowCount = item.Value.Properties?.GridProperties.RowCount
                            // Or if no int value can be found for row count, throw appropriate exception.
                            ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.");

                        // Return tuple build based on the provided sheet data.
                        return new Tuple<string, int, int>(requiredSheetTitleId, requiredColumnCount, requiredRowCount);
                    }).ToList();

                    // Assure that existing spreadsheet containing exactly right number
                    // of sheet of exactly specified size.
                    requestedSpreadsheet.AssureSpreadsheetSize(spreadsheetSizeBlueprint);

                    // Fill the existing sheet with the provided data if the overrideExistingData flag indicating so.
                    if (overrideExistingData) requestedSpreadsheet.SetDataFromSheetsIndex(newSpreadsheetSheetsData, !overrideOnNullInput);

                    // Assure use occasion to assure that sheets int? ids are assigned.
                    // (Do it now as this data is available now anyway, performance cost is
                    // loss at this time, and potentially this will cause application to need
                    // one less spreadsheet get request).
                    requestedSpreadsheet.AssignSheetsIds(newSpreadsheetSheetsData.Values.ToList());

                    // Return that instance of LiveSpreadsheet.
                    return requestedSpreadsheet;
                }

                // Obtain new spreadsheet sheets content without providing any new data,
                // using appropriate spreadsheet sheets content creation method.
                IDictionary<string, LiveSheet> spreadsheetSheetsContent = this.BuilSpreadsheetSheetsContentWithNewData(spreadsheetId, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);

                // Based on the provided parameters and spreadsheet sheet content, manufacture instance of LiveSpreadsheet class instance.
                requestedSpreadsheet = this.Manufacture(spreadsheetId, title, spreadsheetSheetsContent);
                
                // Assure use occasion to assure that sheets int? ids are assigned.
                // (Do it now as this data is available now anyway, performance cost is
                // loss at this time, and potentially this will cause application to need
                // one less spreadsheet get request).
                requestedSpreadsheet.AssignSheetsIds(newSpreadsheetSheetsData.Values.ToList());
                
                // Return that instance of LiveSpreadsheet.
                return requestedSpreadsheet;
            }

            // Empty spreadsheet filled with empty sheets matching provided sheet dimensions ranges tuples.
            public LiveSpreadsheet Get(string spreadsheetId, string title, IList<Sheet> newSpreadsheetSheetsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Get sheet range covering requested area if already existing.
                LiveSpreadsheet requestedSpreadsheet = this._productionIndex.GetExistingSpreadsheet(spreadsheetId);

                // If requested spreadsheet already exists.... 
                if (!(requestedSpreadsheet is null))
                {
                    // Based on the provided spreadsheet sheet data build spreadsheet size blueprint.
                    IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = newSpreadsheetSheetsData.Select((sheet) =>
                    {
                        // Provided instances of Google.Apis.Sheets.v4.Data.Sheet
                        // must contain value other than null for some basic parameters like
                        // sheet title id, column count and row count.
                        string requiredSheetTitleId = sheet.Properties?.Title
                            // Or if no string value can be found for sheet title id, throw appropriate exception.
                            ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.");
                        int requiredColumnCount = sheet.Properties?.GridProperties.ColumnCount
                            // Or if no int value can be found for column count, throw appropriate exception.
                            ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.");
                        int requiredRowCount = sheet.Properties?.GridProperties.RowCount
                            // Or if no int value can be found for row count, throw appropriate exception.
                            ?? throw new ArgumentException($"Provided instance of {typeof(Sheet)} newSheetData does not contain all the required properties - some of the required ones are null.");
                        
                        // Return tuple build based on the provided sheet data.
                        return new Tuple<string, int, int>(requiredSheetTitleId, requiredColumnCount, requiredRowCount);
                    }).ToList();

                    // Assure that existing spreadsheet containing exactly right number
                    // of sheet of exactly specified size.
                    requestedSpreadsheet.AssureSpreadsheetSize(spreadsheetSizeBlueprint);
                    
                    // Fill the existing sheet with the provided data if the overrideExistingData flag indicating so.
                    if (overrideExistingData) requestedSpreadsheet.SetDataFromSheetsList(newSpreadsheetSheetsData, !overrideOnNullInput);

                    // Assure use occasion to assure that sheets int? ids are assigned.
                    // (Do it now as this data is available now anyway, performance cost is
                    // loss at this time, and potentially this will cause application to need
                    // one less spreadsheet get request).
                    requestedSpreadsheet.AssignSheetsIds(newSpreadsheetSheetsData);

                    // Return that instance of LiveSpreadsheet.
                    return requestedSpreadsheet;
                }

                // Obtain new spreadsheet sheets content without providing any new data,
                // using appropriate spreadsheet sheets content creation method.
                IDictionary<string, LiveSheet> spreadsheetSheetsContent = this.BuilSpreadsheetSheetsContentWithNewData(spreadsheetId, newSpreadsheetSheetsData, overrideExistingData, overrideOnNullInput);
                
                // Based on the provided parameters and spreadsheet sheet content, manufacture instance of LiveSpreadsheet class instance.
                requestedSpreadsheet = this.Manufacture(spreadsheetId, title, spreadsheetSheetsContent);

                // Assure use occasion to assure that sheets int? ids are assigned.
                // (Do it now as this data is available now anyway, performance cost is
                // loss at this time, and potentially this will cause application to need
                // one less spreadsheet get request).
                requestedSpreadsheet.AssignSheetsIds(newSpreadsheetSheetsData);

                return requestedSpreadsheet;
            }
            
            // Spreadsheet build base on the provided Google.Apis.Sheets.v4.Data.Spreadsheet.
            // Contains sheets build base on the data found in the provided spreadsheet.
            public LiveSpreadsheet Get(Spreadsheet spreadsheetData, bool overrideExistingData = true, bool overrideOnNullInput = false)
            {
                // Based on the provided instance of Google.Apis.Sheets.v4.Data.Spreadsheet
                // must contain value other than null for some basic parameters like
                // spreadsheet id, spreadsheet title, and spreadsheet sheets content...
                string spreadsheetId = spreadsheetData.SpreadsheetId
                    // ... or throw appropriate exception if required parameter is not available.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Spreadsheet)} spreadsheetData does not contain all the required properties - some of the required ones are null.", nameof(spreadsheetData.SpreadsheetId));
                string spradsheetTitle = spreadsheetData.Properties?.Title
                    // ... or throw appropriate exception if required parameter is not available.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Spreadsheet)} spreadsheetData does not contain all the required properties - some of the required ones are null.", nameof(spreadsheetData.Properties.Title));
                IList<Sheet> sheets = spreadsheetData.Sheets
                    // ... or throw appropriate exception if required parameter is not available.
                    ?? throw new ArgumentException($"Provided instance of {typeof(Spreadsheet)} spreadsheetData does not contain all the required properties - some of the required ones are null.", nameof(spreadsheetData.Sheets));

                // Use obtained blueprint to execute, and return result of the appropriate overloaded version of the method.
                return this.Get(spreadsheetId, spradsheetTitle, sheets, overrideExistingData, overrideOnNullInput);
            }
            #endregion

            //#region GetForcedNew

            //#endregion

            //#region GetFromExistingContent

            //#endregion
            #endregion

            #region Private Helpers
            // IMPORTANT NOTE:
            // Tuple<string, int, int> represents following parameters of sheet range:
            // Tuple<string, int, int>.Item1 - sheet title id.
            // Tuple<string, int, int>.Item2 - column count.
            // Tuple<string, int, int>.Item3 - row count.

            // Build and return the content for the current spreadsheet. Sheets of requested sizes and empty rows/cells.
            private IDictionary<string, LiveSheet> BuilSpreadsheetSheetsContentWithoutNewData(string spreadsheetId, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
            {
                // Build spreadsheet sheets content index dictionary placeholder.
                Dictionary<string, LiveSheet> spreadsheetSheetsContent = new Dictionary<string, LiveSheet>();

                // Base on the provided spreadsheet size blueprint, create appropriate
                // number of empty sheets of appropriate size and add it
                // into the spreadsheetSheetsContent dictionary.
                foreach (Tuple<string, int, int> sheetRangeTuple in spreadsheetSizeBlueprint)
                {
                    // Obtain required LiveSheet of requested size filled with empty rows/cells.
                    LiveSheet spreadsheetContentSheet = LiveSheet.Factory.GetSheet(spreadsheetId, sheetRangeTuple.Item1, sheetRangeTuple.Item2, sheetRangeTuple.Item3);

                    // Add newly created empty sheet in to the spreadsheet sheets index.
                    spreadsheetSheetsContent.Add(spreadsheetContentSheet.SheetTitleId, spreadsheetContentSheet);
                }

                // Return constructed spreadsheet sheets/rows/cells content.
                return spreadsheetSheetsContent;
            }

            // Build and return the content for the current spreadsheet. Sheets of requested sizes and rows/cells pre-filled with data if provided.
            private IDictionary<string, LiveSheet> BuilSpreadsheetSheetsContentWithNewData<Sheet, Row, Cell>(string spreadsheetId, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint, IDictionary<string, Sheet> newSpreadsheetSheetsData, bool overrideExistingData, bool overrideOnNullInput)
                where Sheet : class, IList<Row> where Row : class, IList<Cell> where Cell : class
            {
                // Build spreadsheet sheets content index dictionary placeholder.
                Dictionary<string, LiveSheet> spreadsheetSheetsContent = new Dictionary<string, LiveSheet>();

                // Base on the provided spreadsheet size blueprint, create appropriate
                // number of empty sheets of appropriate size and add it
                // into the spreadsheetSheetsContent dictionary.
                foreach (Tuple<string, int, int> sheetRangeTuple in spreadsheetSizeBlueprint)
                {
                    // From currently looped through tuple, obtain the required sheet parameters.
                    string requiredSheetTitleId = sheetRangeTuple.Item1;
                    int requiredSheetColumnCount = sheetRangeTuple.Item2;
                    int requiredSheetRowCount = sheetRangeTuple.Item3;

                    // Declare sheet data variable.
                    Sheet sheetData;

                    // Obtain appropriate sheet data if any available for the sheet
                    // with the sheet title id equal to requiredSheetTitleId.                    
                    newSpreadsheetSheetsData.TryGetValue(requiredSheetTitleId, out sheetData);

                    // Obtain required LiveSheet of requested size filled with empty rows/cells.
                    LiveSheet spreadsheetContentSheet = LiveSheet.Factory.GetSheet<Row, Cell>(spreadsheetId, requiredSheetTitleId, requiredSheetColumnCount, requiredSheetRowCount, sheetData, overrideExistingData, overrideOnNullInput);

                    // Add newly created empty sheet in to the spreadsheet sheets index.
                    spreadsheetSheetsContent.Add(spreadsheetContentSheet.SheetTitleId, spreadsheetContentSheet);
                }

                // Return constructed spreadsheet sheets/rows/cells content.
                return spreadsheetSheetsContent;
            }

            // Build and return the content for the current spreadsheet. Sheets of requested sizes and rows/cells pre-filled with data if provided.
            private IDictionary<string, LiveSheet> BuilSpreadsheetSheetsContentWithNewData(string spreadsheetId, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint, IDictionary<string, IList<RowData>> newSpreadsheetSheetsData, bool overrideExistingData, bool overrideOnNullInput)
            {
                // Build spreadsheet sheets content index dictionary placeholder.
                Dictionary<string, LiveSheet> spreadsheetSheetsContent = new Dictionary<string, LiveSheet>();

                // Base on the provided spreadsheet size blueprint, create appropriate
                // number of empty sheets of appropriate size and add it
                // into the spreadsheetSheetsContent dictionary.
                foreach (Tuple<string, int, int> sheetRangeTuple in spreadsheetSizeBlueprint)
                {
                    // From currently looped through tuple, obtain the required sheet parameters.
                    string requiredSheetTitleId = sheetRangeTuple.Item1;
                    int requiredSheetColumnCount = sheetRangeTuple.Item2;
                    int requiredSheetRowCount = sheetRangeTuple.Item3;

                    // Declare sheet data variable.
                    IList<RowData> sheetData;

                    // Obtain appropriate sheet data if any available for the sheet
                    // with the sheet title id equal to requiredSheetTitleId.                    
                    newSpreadsheetSheetsData.TryGetValue(requiredSheetTitleId, out sheetData);

                    // Obtain required LiveSheet of requested size filled with empty rows/cells.
                    LiveSheet spreadsheetContentSheet = LiveSheet.Factory.GetSheet(spreadsheetId, requiredSheetTitleId, requiredSheetColumnCount, requiredSheetRowCount, sheetData, overrideExistingData, overrideOnNullInput);

                    // Add newly created empty sheet in to the spreadsheet sheets index.
                    spreadsheetSheetsContent.Add(spreadsheetContentSheet.SheetTitleId, spreadsheetContentSheet);
                }

                // Return constructed spreadsheet sheets/rows/cells content.
                return spreadsheetSheetsContent;
            }

            // Build and return the content for the current spreadsheet. Sheets of requested sizes and rows/cells pre-filled with data if provided.
            private IDictionary<string, LiveSheet> BuilSpreadsheetSheetsContentWithNewData(string spreadsheetId, IDictionary<string, Sheet> newSpreadsheetSheetsData, bool overrideExistingData, bool overrideOnNullInput)
            {
                // Build spreadsheet sheets content index dictionary placeholder.
                Dictionary<string, LiveSheet> spreadsheetSheetsContent = new Dictionary<string, LiveSheet>();

                // Base on the provided spreadsheet size blueprint, create appropriate
                // number of empty sheets of appropriate size and add it
                // into the spreadsheetSheetsContent dictionary.
                foreach (KeyValuePair<string, Sheet> sheetData in newSpreadsheetSheetsData)
                {
                    // Obtain required LiveSheet of requested size filled with empty rows/cells.
                    LiveSheet spreadsheetContentSheet = LiveSheet.Factory.GetSheet(spreadsheetId, sheetData.Value, overrideExistingData, overrideOnNullInput);

                    // Assign appropriate sheetId.
                    spreadsheetContentSheet.SheetId = sheetData.Value.Properties.SheetId;

                    // Add newly created empty sheet in to the spreadsheet sheets index.
                    spreadsheetSheetsContent.Add(spreadsheetContentSheet.SheetTitleId, spreadsheetContentSheet);
                }

                // Return constructed spreadsheet sheets/rows/cells content.
                return spreadsheetSheetsContent;
            }

            // Build and return the content for the current spreadsheet. Sheets of requested sizes and rows/cells pre-filled with data if provided.
            private IDictionary<string, LiveSheet> BuilSpreadsheetSheetsContentWithNewData(string spreadsheetId, IList<Sheet> newSpreadsheetSheetsData, bool overrideExistingData, bool overrideOnNullInput)
            {
                // Build spreadsheet sheets content index dictionary placeholder.
                Dictionary<string, LiveSheet> spreadsheetSheetsContent = new Dictionary<string, LiveSheet>();

                // Base on the provided spreadsheet size blueprint, create appropriate
                // number of empty sheets of appropriate size and add it
                // into the spreadsheetSheetsContent dictionary.
                foreach (Sheet sheetData in newSpreadsheetSheetsData)
                {
                    // Obtain required LiveSheet of requested size filled with empty rows/cells.
                    LiveSheet spreadsheetContentSheet = LiveSheet.Factory.GetSheet(spreadsheetId, sheetData, overrideExistingData, overrideOnNullInput);

                    // Assign appropriate sheetId.
                    spreadsheetContentSheet.SheetId = sheetData.Properties.SheetId;

                    // Add newly created empty sheet in to the spreadsheet sheets index.
                    spreadsheetSheetsContent.Add(spreadsheetContentSheet.SheetTitleId, spreadsheetContentSheet);
                }

                // Return constructed spreadsheet sheets/rows/cells content.
                return spreadsheetSheetsContent;
            }

            // The only method which should call the LiveSpreadsheet constructor.
            private LiveSpreadsheet Manufacture(string spreadsheetId, string title, IDictionary<string, LiveSheet> sheetsBySheetTitileIdsContent)
            {
                // Build the instance of LiveSpreadsheet based on the provided spreadsheet content.
                LiveSpreadsheet newSpreadsheet = new LiveSpreadsheet(spreadsheetId, title, sheetsBySheetTitileIdsContent);

                // Add newly manufacture sheet into the factory production index.
                this._productionIndex.AddSpreadsheet(newSpreadsheet);

                // Return manufactured spreadsheet.
                return newSpreadsheet;
            }
            #endregion
        }
    }
}
