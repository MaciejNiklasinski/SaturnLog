using Google.Apis.Sheets.v4;
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
    // Spreadsheet
    public partial class LiveSpreadsheet : IDictionary<string, LiveSheet>, IDisposable
    {
        #region Private Fields
        // All the sheets belonging to the spreadsheet indexed by they sheet title id.
        private IDictionary<string, LiveSheet> _sheetsIndex;
        #endregion

        #region Properties
        // Flag representing whether the instance of the spreadsheet has been destroyed.
        public bool Destroyed { get; private set; }

        // Spreadsheet Id
        public string SpreadsheetId { get; }

        // Spreadsheet Title
        public string Title { get; }

        // Spreadsheet Url
        public string Url { get { return "https://docs.google.com/spreadsheets/d/" + this.SpreadsheetId + "/edit"; } }
        
        // All the sheets belonging to the spreadsheet.
        public IList<LiveSheet> Sheets { get { return this._sheetsIndex.Values.ToList(); } }
        #endregion

        #region Constructors
        public LiveSpreadsheet(string spreadsheetId, string title, IDictionary<string, LiveSheet> sheets)
        {
            // Assign spreadsheet id
            this.SpreadsheetId = spreadsheetId;

            // Assign spreadsheet title
            this.Title = title;

            // Assign provided sheets as the spreadsheet content.
            this._sheetsIndex = new Dictionary<string, LiveSheet>(sheets);
        }
        #endregion

        // Each tuple representing required sheet id/size in following way:
        // Tuple<string, int, int>.Item1 - sheet title id.
        // Tuple<string, int, int>.Item2 - required column count.
        // Tuple<string, int, int>.Item3 - required row count.
        #region Methods
        // IMPORTANT NOTE:
        // Tuple<string, int, int> represents following parameters of sheet range:
        // Tuple<string, int, int>.Item1 - sheet title id.
        // Tuple<string, int, int>.Item2 - column count.
        // Tuple<string, int, int>.Item3 - row count.

        internal void AssignSheetsIds(IList<Sheet> sheetsData)
        {
            foreach (LiveSheet liveSheet in this._sheetsIndex.Values)
                liveSheet.SheetId = sheetsData.First((sheet) => { return sheet.Properties.Title == liveSheet.SheetTitleId; })?.Properties.SheetId;
        }

        // Assures that the current LiveSpreadsheet content is of exactly requested size.
        public void AssureSpreadsheetSize(IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Create missing LiveSheet and assign them into the current instance of the LiveSheet.
            foreach (Tuple<string, int, int> sheetBlueprint in this.GetSheetsRequiringCreation(spreadsheetSizeBlueprint))
                this.AddSheetToRepositoryOnly(sheetBlueprint.Item1, sheetBlueprint.Item2, sheetBlueprint.Item3);

            // Resize all the existing size different in they size then in the relevant entry with the spreadsheetSizeBlueprint.
            foreach (Tuple<string, int, int> sheetBlueprint in this.GetSheetsRequiringResize(spreadsheetSizeBlueprint))
                this._sheetsIndex[sheetBlueprint.Item1].AssureSheetSize(sheetBlueprint.Item2, sheetBlueprint.Item3);
            
            // Remove all the existing entries not 
            foreach (string sheetTitleId in this.GetSheetsRequiringRemoval(spreadsheetSizeBlueprint))
                this.RemoveSheetFromRepositoryOnly(sheetTitleId);
        }

        // Destroys the entire spreadsheet together with its content.
        public void Destroy(bool andDispose)
        {
            // Loop thorough each spreadsheet sheet
            foreach (KeyValuePair<string, LiveSheet> sheetBySheetTitleId in this._sheetsIndex.ToList())
            {
                // remove it from the spreadsheets sheets index...
                this._sheetsIndex.Remove(sheetBySheetTitleId.Key);
                // ...dipose and destroy the sheet together with its content
                // if provided andDispose flag is true...
                if (andDispose)
                    sheetBySheetTitleId.Value.Dispose();
                // ...otherwise only destroy the sheet together with its content.
                else
                    sheetBySheetTitleId.Value.Destroy(false);
            }

            // Removes current spreadsheet from the LiveSpreadsheet factory production index.
            LiveSpreadsheet.Factory.RemoveStored(this.SpreadsheetId);
        }

        #region AdjustMultipleSheetsColumnsRangesWidthDiemansions / AdjustMultipleSheetsRowsRangesHeightDiemansions
        public void AdjustMultipleSheetsColumnsRangesWidthDiemansions(LiveSpreadsheetsDb db, IList<Tuple<string, int, int, int>> columnsRangesDiemansionsAdjustmentBlueprint)
        {
            // Validate provided blueprints
            foreach (Tuple<string, int, int, int> columnRangeDiemansionsAdjustmentBlueprint in columnsRangesDiemansionsAdjustmentBlueprint)
            {
                string sheetTitleId = columnRangeDiemansionsAdjustmentBlueprint.Item1;
                int leftColumnIndex = columnRangeDiemansionsAdjustmentBlueprint.Item2;
                int columnsToAssureWidthCount = columnRangeDiemansionsAdjustmentBlueprint.Item3;
                int columnWidthPixelsCount = columnRangeDiemansionsAdjustmentBlueprint.Item4;

                this._sheetsIndex.TryGetValue(sheetTitleId, out LiveSheet blueprintSheet);

                if (blueprintSheet is null)
                    throw new ArgumentException("At least one provided blueprint has sheet title id unrelated with any of the sheets containing by the current spreadsheet.", nameof(sheetTitleId));
                else if (leftColumnIndex < 0 || leftColumnIndex > blueprintSheet.RightIndex)
                    throw new ArgumentException("At least one provided blueprint has left column index outside of the scope the current sheet.", nameof(leftColumnIndex));
                else if (leftColumnIndex + columnsToAssureWidthCount - 1 < 0 || columnsToAssureWidthCount < 1)
                    throw new ArgumentException("At least one provided blueprint describes range outside of the scope of the current sheet.", nameof(columnsToAssureWidthCount));
                else if (columnWidthPixelsCount < 0)
                    throw new ArgumentException("At least one provided blueprint columnWidthPixelsCount is less then 0.", nameof(columnWidthPixelsCount));
            }

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            foreach (LiveSheet sheet in this.Sheets)
                if (sheet.SheetId is null)
                    sheet.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, sheet.SheetTitleId);

            // For each Tuple provided in columnsRangesDiemansionsAdjustmentBlueprint create associated sheetId containing tuple blueprint,
            // and return the list containing all of them.
            List<Tuple<int?, int, int, int>> columnsRangesDiemansionsAdjustmentSheetIdBlueprint = columnsRangesDiemansionsAdjustmentBlueprint.Select((columnRangeDiemansionsAdjustmentBlueprint) =>
            {
                // Build sheetId contacting tuple blueprint based on the one provided as a method parameter, without specified sheetId.
                return new Tuple<int?, int, int, int>(this[columnRangeDiemansionsAdjustmentBlueprint.Item1].SheetId, columnRangeDiemansionsAdjustmentBlueprint.Item2, columnRangeDiemansionsAdjustmentBlueprint.Item3, columnRangeDiemansionsAdjustmentBlueprint.Item4);
            }).ToList();
            
            // Adjusts multiple columns ranges width dimensions.
            sheetsService.AdjustMultipleColumnsRangesWidthDimensionsSync(this.SpreadsheetId, columnsRangesDiemansionsAdjustmentSheetIdBlueprint);
        }

        public void AdjustMultipleRowsRangesHeightDiemansions(LiveSpreadsheetsDb db, IList<Tuple<string, int, int, int>> rowsRangesDiemansionsAdjustmentBlueprint)
        {
            foreach (Tuple<string, int, int, int> rowRangeDiemansionsAdjustmentBlueprint in rowsRangesDiemansionsAdjustmentBlueprint)
            {
                string sheetTitleId = rowRangeDiemansionsAdjustmentBlueprint.Item1;
                int topRowIndex = rowRangeDiemansionsAdjustmentBlueprint.Item2;
                int rowsToAssureHeightCount = rowRangeDiemansionsAdjustmentBlueprint.Item3;
                int rowWidthPixelsCount = rowRangeDiemansionsAdjustmentBlueprint.Item4;

                this._sheetsIndex.TryGetValue(sheetTitleId, out LiveSheet blueprintSheet);

                if (blueprintSheet is null)
                    throw new ArgumentException("At least one provided blueprint has sheet title id unrelated with any of the sheets containing by the current spreadsheet.", nameof(sheetTitleId));
                else if(topRowIndex < 0 || topRowIndex > blueprintSheet.BottomIndex)
                    throw new ArgumentException("At least one provided blueprint has top row index outside of the scope the current sheet.", nameof(topRowIndex));
                else if (topRowIndex + rowsToAssureHeightCount - 1 < 0 || rowsToAssureHeightCount < 1)
                    throw new ArgumentException("At least one provided blueprint describes range outside of the scope of the current sheet.", nameof(rowsToAssureHeightCount));
                else if (rowWidthPixelsCount < 0)
                    throw new ArgumentException("At least one provided blueprint rownWidthPixelsCount is less then 0.", nameof(rowWidthPixelsCount));

            }

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            foreach (LiveSheet sheet in this.Sheets)
                if (sheet.SheetId is null)
                    sheet.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, sheet.SheetTitleId);


            // For each Tuple provided in rowsRangesDiemansionsAdjustmentSheetIdBlueprint create associated sheetId containing tuple blueprint,
            // and return the list containing all of them.
            List<Tuple<int?, int, int, int>> rowsRangesDiemansionsAdjustmentSheetIdBlueprint = rowsRangesDiemansionsAdjustmentBlueprint.Select((rowRangeDiemansionsAdjustmentBlueprint) =>
            {
                // Build sheetId contacting tuple blueprint based on the one provided as a method parameter, without specified sheetId.
                return new Tuple<int?, int, int, int>(this[rowRangeDiemansionsAdjustmentBlueprint.Item1].SheetId, rowRangeDiemansionsAdjustmentBlueprint.Item2, rowRangeDiemansionsAdjustmentBlueprint.Item3, rowRangeDiemansionsAdjustmentBlueprint.Item4);
            }).ToList();

            // Adjusts multiple rows ranges height dimensions.
            sheetsService.AdjustMultipleRowsRangesHeightDimensionsSync(this.SpreadsheetId, rowsRangesDiemansionsAdjustmentSheetIdBlueprint);
        }
        #endregion

        #region AddSheet
        // Adds the new instance of the LiveSheet into the current LiveSpreadsheet.
        public void AddSheet(LiveSpreadsheetsDb db, string sheetTitleId, int columnCount, int rowCount)
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (this._sheetsIndex.ContainsKey(sheetTitleId))
                throw new ArgumentException($"Unable to add LiveSheet indexed to provided sheet title id {sheetTitleId} because current LiveSpreadsheet already contains the sheet using the same sheet title id.", nameof(sheetTitleId));
            else if (columnCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one column. Please provide column count greater then 0.", nameof(columnCount));
            else if (rowCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one row. Please provide row count greater then 0.", nameof(rowCount));

            // Add sheet into the appropriate spreadsheet
            db.ApisSheetsService.AddSheetSync(this.SpreadsheetId, sheetTitleId, columnCount, rowCount);
            
            // Construct new instance of LiveSheet
            LiveSheet sheet = LiveSheet.Factory.GetSheet(this.SpreadsheetId, sheetTitleId, columnCount, rowCount);

            // Add sheet into the current LiveSpreadsheet sheets index.
            this._sheetsIndex.Add(sheetTitleId, sheet);
        }

        // Adds the new instance of the LiveSheet into the current LiveSpreadsheet.
        public void AddSheet<Row, Cell>(LiveSpreadsheetsDb db, string sheetTitleId, int columnCount, int rowCount, IList<Row> rowData)
            where Row : class, IList<Cell> where Cell : class
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (this._sheetsIndex.ContainsKey(sheetTitleId))
                throw new ArgumentException($"Unable to add LiveSheet indexed to provided sheet title id {sheetTitleId} because current LiveSpreadsheet already contains the sheet using the same sheet title id.", nameof(sheetTitleId));
            else if (columnCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one column. Please provide column count greater then 0.", nameof(columnCount));
            else if (rowCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one row. Please provide row count greater then 0.", nameof(rowCount));

            // Add sheet into the appropriate spreadsheet
            db.ApisSheetsService.AddSheetSync(this.SpreadsheetId, sheetTitleId, columnCount, rowCount);

            // Construct new instance of LiveSheet
            LiveSheet sheet = LiveSheet.Factory.GetSheet<Row, Cell>(this.SpreadsheetId, sheetTitleId, columnCount, rowCount, rowData);
            sheet.Upload(db);

            // Add sheet into the current LiveSpreadsheet sheets index.
            this._sheetsIndex.Add(sheetTitleId, sheet);
        }

        // Adds the new instance of the LiveSheet into the current LiveSpreadsheet.
        public void AddSheetToRepositoryOnly(string sheetTitleId, int columnCount, int rowCount)
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (this._sheetsIndex.ContainsKey(sheetTitleId))
                throw new ArgumentException($"Unable to add LiveSheet indexed to provided sheet title id {sheetTitleId} because current LiveSpreadsheet already contains the sheet using the same sheet title id.", nameof(sheetTitleId));
            else if (columnCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one column. Please provide column count greater then 0.", nameof(columnCount));
            else if (rowCount < 1)
                throw new ArgumentException("Unable to create a sheet with less then one row. Please provide row count greater then 0.", nameof(rowCount));
            

            // Construct new instance of LiveSheet
            LiveSheet sheet = LiveSheet.Factory.GetSheet(this.SpreadsheetId, sheetTitleId, columnCount, rowCount);

            // Add sheet into the current LiveSpreadsheet sheets index.
            this._sheetsIndex.Add(sheetTitleId, sheet);
        }
        #endregion

        #region RemoveSheet
        // Removes instance of the LiveSheet indexed into the current LiveSpreadsheet instance.
        public void RemoveSheet(LiveSpreadsheetsDb db, string sheetTitleId)
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (!this._sheetsIndex.ContainsKey(sheetTitleId))
                throw new ArgumentException($"Unable to remove LiveSheet indexed to provided sheet title id {sheetTitleId} because current LiveSpreadsheet doesn't contain any sheet indexed to that specific sheet title id.", nameof(sheetTitleId));

            throw new NotImplementedException();
        }

        // Removes instance of the LiveSheet indexed into the current LiveSpreadsheet instance.
        public void RemoveSheetFromRepositoryOnly(string sheetTitleId)
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (!this._sheetsIndex.ContainsKey(sheetTitleId))
                throw new ArgumentException($"Unable to remove LiveSheet indexed to provided sheet title id {sheetTitleId} because current LiveSpreadsheet doesn't contain any sheet indexed to that specific sheet title id.", nameof(sheetTitleId));

            // Get the sheet to be removed stored in the current spreadsheet.
            LiveSheet sheetToBeRemoved = this._sheetsIndex[sheetTitleId];
            
            // Destroy the sheet to be removed...
            sheetToBeRemoved.Destroy(true);

            // .. then remove it from the current spreadsheet sheets index.
            this._sheetsIndex.Remove(sheetTitleId);
        }
        #endregion

        #region GetDataAs
        // Returns sheets stored data as a strings grids index. Sheet Title Id / Rows / Columns
        public IDictionary<string, IList<IList<string>>> GetDataAsStringsGridsIndex()
        {
            // Create spreadsheet data placeholder
            Dictionary<string, IList<IList<string>>> stringsGridsIndex = new Dictionary<string, IList<IList<string>>>();

            // For each of the spreadsheet sheets.
            foreach (string sheetTitleId in this._sheetsIndex.Keys)
            {
                // Get sheet strings grid.
                IList<IList<string>> stringsGrid = this._sheetsIndex[sheetTitleId].GetDataAsStringsGrid();

                // Add grid in to the strings grids index.
                stringsGridsIndex.Add(sheetTitleId, stringsGrid);
            }

            // Return spreadsheet data as strings grids index.
            return stringsGridsIndex;
        }

        // Returns sheets stored data as a objects grids index. Sheet Title Id / Rows / Columns
        public IDictionary<string, IList<IList<object>>> GetDataAsObjectsGridsIndex()
        {
            // Create spreadsheet data placeholder
            Dictionary<string, IList<IList<object>>> objectsGridsIndex = new Dictionary<string, IList<IList<object>>>();

            // For each of the spreadsheet sheets.
            foreach (string sheetTitleId in this._sheetsIndex.Keys)
            {
                // Get sheet objects grid.
                IList<IList<object>> objectsGrid = this._sheetsIndex[sheetTitleId].GetDataAsObjectsGrid();

                // Add grid in to the objects grids index.
                objectsGridsIndex.Add(sheetTitleId, objectsGrid);
            }

            // Return spreadsheet data as objects grids index.
            return objectsGridsIndex;
        }

        // Returns sheets stored data as rows lists. Sheet Title Id / Rows
        public IDictionary<string, IList<LiveRow>> GetDataAsRowsListsIndex()
        {
            // Create spreadsheet data placeholder
            Dictionary<string, IList<LiveRow>> rowsListsIndex = new Dictionary<string, IList<LiveRow>>();

            // For each of the spreadsheet sheets.
            foreach (string sheetTitleId in this._sheetsIndex.Keys)
            {
                // Get sheet rows list.
                IList<LiveRow> rowsList = this._sheetsIndex[sheetTitleId].GetDataAsRowsList();

                // Add grid in to the rows list.
                rowsListsIndex.Add(sheetTitleId, rowsList);
            }

            // Return spreadsheet data as rows lists index.
            return rowsListsIndex;
        }

        // Returns sheets stored data as rows lists. Sheet Title Id / Rows
        public IDictionary<string, LiveSheet> GetDataAsSheetsIndex()
        {
            // Create spreadsheet data placeholder
            Dictionary<string, LiveSheet> sheetsIndex = new Dictionary<string, LiveSheet>();

            // For each of the spreadsheet sheets.
            foreach (string sheetTitleId in this._sheetsIndex.Keys)
                // Add sheet in to the sheets index.
                sheetsIndex.Add(sheetTitleId, this._sheetsIndex[sheetTitleId]);

            // Return spreadsheet data as sheets index.
            return sheetsIndex;
        }
        #endregion

        #region SetDataFrom

        // Set spreadsheet data from the objects grids index.
        internal void SetDataFromSheetsDataIndex<Sheet, Row, Cell>(IDictionary<string, Sheet> spreadsheetDataIndex, bool ignoreNullData = true)
            where Sheet : class, IList<Row> where Row : class, IList<Cell> where Cell : class
        {
            // For each sheet grid in the provided data objectsDataIndex
            foreach (KeyValuePair<string, Sheet> sheetDataIndex in spreadsheetDataIndex)
                // Filled value of the currently looped through sheet with the provided data.
                this._sheetsIndex[sheetDataIndex.Key].SetDataFromRowsData<Row, Cell>(0, 0, sheetDataIndex.Value, ignoreNullData);
        }

        // Set spreadsheet data from the strings grids index.
        public void SetDataFromStringsGridsIndex(IDictionary<string, IList<IList<string>>> spreadsheetDataIndex, bool ignoreNullData = true)
        {
            this.SetDataFromSheetsDataIndex<IList<IList<string>>, IList<string>, string>(spreadsheetDataIndex, ignoreNullData);
        }

        // Set spreadsheet data from the objects grids index.
        public void SetDataFromObjectsGridsIndex(IDictionary<string, IList<IList<object>>> spreadsheetDataIndex, bool ignoreNullData = true)
        {
            this.SetDataFromSheetsDataIndex<IList<IList<object>>, IList<object>, object>(spreadsheetDataIndex, ignoreNullData);
        }

        // Set spreadsheet data from the cells grids index.
        public void SetDataFromCellsGridsIndex(IDictionary<string, IList<IList<CellData>>> spreadsheetDataIndex, bool ignoreNullData = true)
        {
            this.SetDataFromSheetsDataIndex<IList<IList<CellData>>, IList<CellData>, CellData>(spreadsheetDataIndex, ignoreNullData);
        }

        // Set spreadsheet data from the rows lists index.
        public void SetDataFromRowsListsIndex(IDictionary<string, IList<RowData>> rowsDataIndex, bool ignoreNullData = true)
        {
            // For each sheet rows data list in the provided data rowsDataIndex
            foreach (KeyValuePair<string, IList<RowData>> rowData in rowsDataIndex)
                // Set value of the appropriate - equal sheet title id - spreadsheet sheet,
                // according the provided sheet rows data list. 0 and 0 for left and top buffer.
                this._sheetsIndex[rowData.Key].SetDataFromRowsData(0, 0, rowData.Value, ignoreNullData);
        }

        // Set sheet data from sheets index
        public void SetDataFromSheetsIndex(IDictionary<string, Sheet> sheetsDataIndex, bool ignoreNullData = true)
        {
            // For each sheet data in the provided data sheetsDataIndex
            foreach (KeyValuePair<string, Sheet> sheetData in sheetsDataIndex)
                // Set value of the appropriate - equal sheet title id - spreadsheet sheet
                // according the provided spreadsheet sheets data.
                this._sheetsIndex[sheetData.Key].SetDataFromSheetData(sheetData.Value, ignoreNullData);
        }

        // Set sheet data from sheets
        public void SetDataFromSheetsList(IList<Sheet> sheetsData, bool ignoreNullData = true)
        {
            // For each sheet data in the provided data sheetsData
            foreach (Sheet sheetData in sheetsData)
                // Set value of the appropriate - equal sheet title id - spreadsheet sheet
                // according the provided spreadsheet sheets data.
                this._sheetsIndex[sheetData.Properties.Title].SetDataFromSheetData(sheetData, ignoreNullData);
        }

        // Set spreadsheet data from the spreadsheet
        public void SetDataFromData(Spreadsheet spreadsheet, bool ignoreNullData = true)
        {
            // Set spreadsheet data from spreadsheet sheets collection.
            this.SetDataFromSheetsList(spreadsheet.Sheets, ignoreNullData);
        }
        #endregion
        #endregion

        #region Private Helpers
        // Returns list storing titles and dimensions of the sheets necessary to be created.
        private IList<Tuple<string, int, int>> GetSheetsRequiringCreation(IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Create list to store the title and dimensions of the sheets necessary to be created.
            IList<Tuple<string, int, int>> sheetsRequiringCreation = new List<Tuple<string, int, int>>();

            // Loop through spreadsheet size blueprint and compare 
            // each tuple with current LiveSpreadsheet contained LiveSheets...
            foreach (Tuple<string, int, int> requiredSheetDescription in spreadsheetSizeBlueprint)
            {
                // Obtain required sheet title id from the provided blueprint.
                string requiredSheetTitleId = requiredSheetDescription.Item1;

                // If the current LiveSpreadsheet doesn't contain the sheet indexed to the required sheet title id...
                if (!this._sheetsIndex.ContainsKey(requiredSheetTitleId))
                    // ... add currently looped through required sheet size description into the sheetsRequiredCreation list.
                    sheetsRequiringCreation.Add(requiredSheetDescription);
            }

            // Returns list containing sheet title ids and dimensions of the sheets which have to be created.
            return sheetsRequiringCreation;
        }

        private IList<Tuple<string, int, int>> GetSheetsRequiringResize(IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Create list to store the title and dimensions of the sheets necessary to be created.
            IList<Tuple<string, int, int>> sheetsRequiringResize = new List<Tuple<string, int, int>>();

            // Loop through spreadsheet size blueprint and compare 
            // each tuple with current LiveSpreadsheet contained LiveSheets...
            foreach (Tuple<string, int, int> requiredSheetDescription in spreadsheetSizeBlueprint)
            {
                // Obtain required sheet title id, as well as required columns, and rows number from the provided blueprint.
                string requiredSheetTitleId = requiredSheetDescription.Item1;
                int requiredColumnCount = requiredSheetDescription.Item2;
                int requiredRowCount = requiredSheetDescription.Item3;

                // If the current LiveSpreadsheet contains the sheet indexed to the required sheet title id...
                if (this._sheetsIndex.ContainsKey(requiredSheetTitleId)
                    // ...but such a LiveSheet hasn't got exactly the requested number of columns...
                    && this._sheetsIndex[requiredSheetTitleId].ColumnCount != requiredColumnCount
                    // ... or it hasn't got exactly the requested number of rows...
                    || this._sheetsIndex[requiredSheetTitleId].RowCount != requiredRowCount)
                    // ... add currently looped through required sheet size description into the sheetsRequiredResize list.
                    sheetsRequiringResize.Add(requiredSheetDescription);
            }

            // Returns list containing sheet title ids and dimensions of the sheets which require to be resize.
            return sheetsRequiringResize;
        }

        private IList<string> GetSheetsRequiringRemoval(IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Create list to store sheets title ids of the sheet designated for removal.
            IList<string> sheetsRequiringRemoval = new List<string>();

            foreach (string sheetTitleId in this._sheetsIndex.Keys)
                if (!spreadsheetSizeBlueprint.Any(tuple => tuple.Item1 == sheetTitleId))
                    sheetsRequiringRemoval.Add(sheetTitleId);
            
            // Returns list containing all the sheets titles ids of the sheet require to be removed.
            return sheetsRequiringRemoval;
        }















        #endregion

        #region Private Helpers - TODO move transfer delete 
        // Build sheets index based on the provided dimensions.
        private void BuildEmptySheetsIndexFromDiemensions(IDictionary<string, string> sheetDimensionsRanges)
        {
            // Build sheets index dictionary placeholder.
            this._sheetsIndex = new Dictionary<string, LiveSheet>();

            // Base on the provided sheet dimensions ranges, create appropriate
            // number of empty sheets of appropriate size and add it 
            // into the spreadsheet sheetIndex
            foreach (var sheetDimensionsRange in sheetDimensionsRanges)
            {
                // Translate range string in to the separate parameters.
                RangeTranslator.RangeStringToParameters(sheetDimensionsRange.Value, out string sheetTitleId, out int leftIndex, out int topIndex, out int columnCount, out int rowCount);

                // Create empty sheet based on the parameters obtained from the translation
                // of the dimensions range string back in to the separate parameters.
                LiveSheet emptySheet = LiveSheet.Factory.GetSheet(this.SpreadsheetId, sheetTitleId, columnCount, rowCount);

                // Add newly created empty sheet in to the spreadsheet sheets index.
                this._sheetsIndex.Add(sheetTitleId, emptySheet);
            }
        }

        // Build sheets index based on the provided sheets data.
        private void BuildSheetsIndexFromSpreadsheetsData(IList<Sheet> sheetsData)
        {
            // Assign placeholder dictionary 
            this._sheetsIndex = new Dictionary<string, LiveSheet>();

            // For each of the sheets in the provided spreadsheet.
            foreach (Sheet sheet in sheetsData)
                // Create and add sheet based on the Google.Apis.Sheets.v4.Data.Sheet
                // Google.Apis.Sheets.v4.Data found in the provided spreadsheet.
                this._sheetsIndex.Add(sheet.Properties.Title, LiveSheet.Factory.GetSheet(this.SpreadsheetId, sheet));
        }
        #endregion

        #region IDictionary<string, SheetRawData>
        public ICollection<string> Keys => this._sheetsIndex.Keys;

        public ICollection<LiveSheet> Values => this._sheetsIndex.Values;

        public int Count => this._sheetsIndex.Count;

        public bool IsReadOnly => this._sheetsIndex.IsReadOnly;

        public LiveSheet this[string key] { get => this._sheetsIndex[key]; set => this._sheetsIndex[key] = value; }

        public bool ContainsKey(string key)
        {
            return this._sheetsIndex.ContainsKey(key);
        }

        public void Add(string key, LiveSheet value)
        {
            this._sheetsIndex.Add(key, value);
        }

        public bool Remove(string key)
        {
            return this._sheetsIndex.Remove(key);
        }

        public bool TryGetValue(string key, out LiveSheet value)
        {
            return this._sheetsIndex.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, LiveSheet> item)
        {
            this._sheetsIndex.Add(item);
        }

        public void Clear()
        {
            this._sheetsIndex.Clear();
        }

        public bool Contains(KeyValuePair<string, LiveSheet> item)
        {
            return this._sheetsIndex.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, LiveSheet>[] array, int arrayIndex)
        {
            this._sheetsIndex.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, LiveSheet> item)
        {
            return this._sheetsIndex.Remove(item);
        }

        public IEnumerator<KeyValuePair<string, LiveSheet>> GetEnumerator()
        {
            return this._sheetsIndex.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._sheetsIndex.GetEnumerator();
        }
        #endregion

        #region IDisposable Support
        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    // Dispose the content of the spreadsheet.
                    this.Destroy(true);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                                
                // TODO: set large fields to null.
                this._sheetsIndex = null;

                // Set disposed flag to true.
                this.disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        //~LiveSpreadsheet() {
        //  // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
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
