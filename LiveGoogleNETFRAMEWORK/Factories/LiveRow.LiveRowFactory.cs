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
        #region Factory singleton
        // Factory singleton thread safety lock.
        private static readonly object _lock = new object();

        // LiveRow factory thread-safe singleton.
        public static LiveRowFactory _factory;
        public static LiveRowFactory Factory
        {
            get
            {
                if (LiveRow._factory is null)
                    lock (LiveRow._lock)
                        if (LiveRow._factory is null) 
                            LiveRow._factory = new LiveRowFactory();

                return LiveRow._factory;
            }
        }
        #endregion
            
        public partial class LiveRowFactory
        {
            #region Private Fields
            // FactoryProductionIndex singleton - stores data allowing to assure that none of the row/cells will be created more then once.
            private FactoryProductionIndex _productionIndex { get; } = new FactoryProductionIndex();
            #endregion

            #region Methods
            // Re-Indexing provided row, increasing the row index it is indexed to by the provided rowIndexChangeByValue
            internal void ReIndexStoredRow(LiveRow rowToBeReIndexed, int rowIndexChangeByValue)
            {
                this._productionIndex.ReIndexStoredRow(rowToBeReIndexed, rowIndexChangeByValue);
            }

            // Possibly more expensive performance wise, but it will assure that each cell has only one
            // instance of LiveCell representing it ever initialized. 
            #region Get
            // Empty from parameters using already existing cells wherever available.
            internal LiveRow Get(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

                // Get row covering requested area if already existing.
                LiveRow requestedRow = this._productionIndex.GetExistingRow(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount);

                // If requested row already exists... 
                if (!(requestedRow is null))
                    // ...return it.
                    return requestedRow;

                // Build new row cells content using appropriate row cells content creation method to create
                // the new row cells content build from the existing cells wherever possible...
                IList<LiveCell> rowCellsContent = this.BuildRowCellsContentWithoutNewData(
                    // Row cells content spreadsheet id
                    spreadsheetId,
                    // Row cells content sheet title id
                    sheetTitleId,
                    // Row most left column index
                    leftColumnIndex,
                    // Row sheet relative index
                    rowSheetRelativeIndex,
                    // Total number of the cells covered by the row 
                    columnCount);
                
                // Manufacture and return newly create row.
                return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, rowCellsContent);
            }

            // Pre-filled with new row cells data, as well as using already existing cells wherever available.
            internal LiveRow Get(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<string> newRowCellsData, bool overrideExsistingData = true, bool overrideOnNullInput = false)
            {
                // Returns result of appropriate generic version of the private base of the current method.
                return this.Get<string>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newRowCellsData, overrideExsistingData, overrideOnNullInput);
            }

            // Pre-filled with new row cells data, as well as using already existing cells wherever available.
            internal LiveRow Get(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<object> newRowCellsData, bool overrideExsistingData = true, bool overrideOnNullInput = false)
            {
                // Returns result of appropriate generic version of the private base of the current method.
                return this.Get<object>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newRowCellsData, overrideExsistingData, overrideOnNullInput);
            }

            // Pre-filled with new row cells data, as well as using already existing cells wherever available.
            internal LiveRow Get(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<CellData> newRowCellsData, bool overrideExsistingData = true, bool overrideOnNullInput = false)
            {
                // Returns result of appropriate generic version of the private base of the current method.
                return this.Get<CellData>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newRowCellsData, overrideExsistingData, overrideOnNullInput);
            }

            // Pre-filled with new row data, as well as using already existing cells wherever available.
            internal LiveRow Get(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, RowData newRowData, bool overrideExsistingData = true, bool overrideOnNullInput = false)
            {
                // Return result of appropriate generic version of the private base of the current method.
                return this.Get<CellData>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newRowData?.Values, overrideExsistingData, overrideOnNullInput);
            }
            
            // Internal generic base
            // Returns new instance of LiveRow class, build based on, and pre-filled with the provided new row cell data.
            // The row will be created assuring that all of the cells which already existing will be reused, new cells
            // created where existing ones not available, and both of them - depending on the boolean flags provided
            // - will be filled with provided data, possibly overwriting exsisting data.
            internal LiveRow Get<Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<Cell> newRowCellsData, bool overrideExistingData = true, bool overrideOnNullInput = false)
                where Cell : class
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

                // Get row covering requested area if already existing.
                LiveRow requestedRow = this._productionIndex.GetExistingRow(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount);

                // If requested row already exists... 
                if (!(requestedRow is null))
                {
                    // ... and if provided override existing data flag is true
                    // as well as newRowCellsData has been provided...
                    if (overrideExistingData && !(newRowCellsData is null))
                        // Set data of existing row using provided new row cells data.
                        requestedRow.SetDataFromCellsData(
                            // Do not get confuse left buffer index with left column index
                            leftBufferIndex: 0, 
                            // Cells data
                            cellsData: newRowCellsData,
                            // Ignore null data flag set according inversed overrideOnNullInput flag
                            ignoreNullData: !overrideOnNullInput);

                        // ...return it.
                    return requestedRow;
                }

                // Declare new row cells content...
                IList<LiveCell> newRowCellsContent;

                // If no new row cells data designated for the new row has been provided...
                if (newRowCellsData is null)
                {
                    //... use appropriate row cells content creation method to create
                    // the new row cells content build from the existing cells wherever possible...
                    newRowCellsContent = this.BuildRowCellsContentWithoutNewData(
                            // Row cells content spreadsheet id
                            spreadsheetId,
                            // Row cells content sheet title id
                            sheetTitleId,
                            // Row most left column index
                            leftColumnIndex,
                            // Row sheet relative index
                            rowSheetRelativeIndex,
                            // Total number of the cells covered by the row 
                            columnCount);
                }
                // Else if new row cells data has been provided, and override existing data
                // flag indicating that it should not be overriding any existing data...
                else if (!overrideExistingData)
                {
                    //... use appropriate row cells content creation method to create
                    // the new row cells content, build from the existing cells wherever possible. 
                    // As well as creating new cells pre-filled with new row cells data wherever 
                    // existing cell is not available...
                    newRowCellsContent = this.BuildRowCellsContentWithoutDataOverride(
                            // Row cells content spreadsheet id
                            spreadsheetId,
                            // Row cells content sheet title id
                            sheetTitleId,
                            // Row most left column index
                            leftColumnIndex,
                            // Row sheet relative index
                            rowSheetRelativeIndex,
                            // Total number of the cells covered by the row 
                            columnCount,
                            // Null as no other data then already existing is available. - Optional parameter null is default value.
                            newRowCellsData);
                }
                // Else if new row cells data has been provided, and override existing data
                // flag indicating that it should override the existing data...
                else
                {
                    //... use appropriate row cells content creation method to create
                    // the new row cells content, build from the existing cells wherever possible,
                    // and overriding existing cells data wherever provided new row cells data
                    // is overlapping any existing cell as well as creating new cells pre-filled
                    // with new row cells data wherever existing cell is not available... 
                    newRowCellsContent = this.BuildRowCellsContentWithDataOverride(
                            // Row cells content spreadsheet id
                            spreadsheetId,
                            // Row cells content sheet title id
                            sheetTitleId,
                            // Row most left column index
                            leftColumnIndex,
                            // Row sheet relative index
                            rowSheetRelativeIndex,
                            // Total number of the cells covered by the row 
                            columnCount,
                            // Null as no other data then already existing is available. - Optional parameter null is default value.
                            newRowCellsData,
                            // False as existing data should not be overridden. As null has been provided above
                            // providing true should not matter anyway. - Optional parameter false is NOT default value.
                            overrideOnNullInput);
                }

                // Manufacture and return newly create row.
                return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, newRowCellsContent);
            }
            #endregion

            // Possible performance benefits - to be used safely, caller must be absolutely sure that
            // the row described by the provided parameters spreadsheetId, sheetTitleId, leftColumnIndex,
            // rowSheetRelativeIndex and columnCount doesn't contain any already existing LiveCell s.
            #region GetForcedNew
            // Returns new instance of LiveRow force to instantiation without confirming appropriate LiveRow doesn't exists already. 
            internal LiveRow GetForcedNew(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount)
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

                // Get instance of existing cells placeholder list without reusing existing cells.
                IList<LiveCell> forcedRowCellsContent
                    = this.BuildForcedRowCellsContentWithoutNewData(
                        // Row cells content spreadsheet id
                        spreadsheetId,
                        // Row cells content sheet title id
                        sheetTitleId,
                        // Row most left column index
                        leftColumnIndex,
                        // Row sheet relative index
                        rowSheetRelativeIndex,
                        // Total number of the cells covered by the row 
                        columnCount);

                // Manufacture and return newly create forced row.
                return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, forcedRowCellsContent);
            }

            // Returns new instance of a LiveRow class, build based on, and pre-filled with the provided cells data strings list.
            // The row will be created without confirming that appropriate row does not exists already, as well as without reusing
            // already existing LiveCell s located on the area covered by the provided parameters.
            internal LiveRow GetForcedNew(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<string> cellsData)
            {
                // Returns result of the private generic base version of the method.
                return this.GetForcedNew<string>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, cellsData);
            }

            // Returns new instance of a LiveRow class, build based on, and pre-filled with the provided cells data objects list.
            // The row will be created without confirming that appropriate row does not exists already, as well as without reusing
            // already existing LiveCell s located on the area covered by the provided parameters.
            internal LiveRow GetForcedNew(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<object> cellsData)
            {
                // Returns result of the private generic base version of the method.
                return this.GetForcedNew<object>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, cellsData);
            }

            // Returns new instance of a LiveRow class, build based on, and pre-filled with the provided cells data list.
            // The row will be created without confirming that appropriate row does not exists already, as well as without reusing
            // already existing LiveCell s located on the area covered by the provided parameters.
            internal LiveRow GetForcedNew(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<CellData> cellsData)
            {
                // Returns result of the private generic base version of the method.
                return this.GetForcedNew<CellData>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, cellsData);
            }

            // Returns new instance of a LiveRow class, build based on, and pre-filled with the provided row data.
            // The row will be created without confirming that appropriate row does not exists already, as well as without reusing
            // already existing LiveCell s located on the area covered by the provided parameters.
            internal LiveRow GetForcedNew(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, RowData rowData)
            {
                // Returns result of the private generic base version of the method.
                return this.GetForcedNew<CellData>(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, rowData?.Values);
            }

            // Internal generic base.
            // Returns new instance of a LiveRow class, build based on, and pre-filled with the provided cells data list.
            // The row will be created without confirming that appropriate row does not exists already, as well as without reusing
            // already existing LiveCell s located on the area covered by the provided parameters.
            internal LiveRow GetForcedNew<Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<Cell> cellsData)
                where Cell : class
            {
                // Assure that the appropriate space in the factory production index is existing to start the production process.
                this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

                // Get instance of existing cells placeholder list.
                IList<LiveCell> rowCellsContent = this.BuildForcedRowCellsContentWithNewData(
                            // Row cells content spreadsheet id
                            spreadsheetId,
                            // Row cells content sheet title id
                            sheetTitleId,
                            // Row most left column index
                            leftColumnIndex,
                            // Row sheet relative index
                            rowSheetRelativeIndex,
                            // Total number of the cells covered by the row 
                            columnCount,
                            // Row cells new data
                            cellsData);

                // Manufacture and return newly create row.
                return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, rowCellsContent);
            }
            #endregion

            // TODO consider to remove
            #region GetFromExistingCells
            //// Returns a instance of a LiveRow class, build or retrieve based on, and filled with the provided existing cells list.
            //internal LiveRow GetFromExistingCells(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<LiveCell> exsistingCells)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

            //    // Get row covering requested area if already existing.
            //    LiveRow requestedRow = this._productionIndex.GetExistingRow(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount);

            //    // If requested row already exists... 
            //    if (!(requestedRow is null))
            //        // ...return it.
            //        return requestedRow;
                
            //    // Manufacture and return newly created row.
            //    return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, exsistingCells);
            //}

            //// Return forced new instance of a LiveRow class, build based on, and pre-filled with the provided existing cells list.
            //internal LiveRow GetForcedNewFromExistingCells(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<LiveCell> exsistingCells)
            //{
            //    // Assure that the appropriate space in the factory production index is existing to start the production process.
            //    this._productionIndex.AssureStorageSpace(spreadsheetId, sheetTitleId, rowSheetRelativeIndex);

            //    // Manufacture and return newly created forced row.
            //    return this.Manufacture(spreadsheetId, sheetTitleId, leftColumnIndex, rowSheetRelativeIndex, columnCount, exsistingCells);
            //}
            #endregion

            // Return the list containing all the rows from all the ranges related with the specified LiveSheet.
            public IList<LiveRow> GetSheetAllRangesRows(string spreadsheetId, string sheetTitleId)
            {
                return this.GetSheetAllRangesRowsBelowIndex(spreadsheetId, sheetTitleId, 0);
            }

            // Return the list containing all the rows from all the ranges related with the specified LiveSheet.
            public IList<LiveRow> GetSheetAllRangesRowsBelowIndex(string spreadsheetId, string sheetTitleId, int topRowIndex)
            {
                // Declare rows list placeholder.
                List<LiveRow> rows = new List<LiveRow>();

                try
                {
                    // Foreach dictionary entry specificIndexRows containing all the rows from different LiveRange
                    // instances related to the same LiveSheet and located on the same sheet relative row index....
                    foreach (KeyValuePair<int, IList<LiveRow>> specificIndexRows in this._productionIndex[spreadsheetId][sheetTitleId])
                        // .. if entry contains rows indexed on, or below (greater value) provided topRowIndex...
                        if (specificIndexRows.Key >= topRowIndex)
                            // ... add entire content into the rows list placeholder.
                            rows.AddRange(specificIndexRows.Value);
                }
                catch (KeyNotFoundException) { return rows; }

                // Return the rows list placeholder filled with the content.
                return rows;
            }

            // Return the list containing all the rows located on the specified row index from all the ranges related with the specified LiveSheet.
            public IList<LiveRow> GetSheetAllRangesRows(string spreadsheetId, string sheetTitleId, int rowIndex)
            {
                try
                { return this._productionIndex[spreadsheetId][sheetTitleId][rowIndex]; }
                catch (KeyNotFoundException) { return new List<LiveRow>(); }
            }

            #region RemoveStored
            // Simply removed row stored in the factory production index.
            public void RemoveStoredRow(LiveRow toBeRemovedRow)
            {
                this._productionIndex.RemoveSpecificRow(toBeRemovedRow);
            }
            #endregion
            #endregion

            #region Private Helpers            
            #region Build row cells content
            // Returns content of the row forced to be instantiated without trying to reusing existing cells.
            private IList<LiveCell> BuildForcedRowCellsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount)
            {
                // Get instance of existing cells placeholder list.
                IList<LiveCell> forcedRowCellsContent = new List<LiveCell>();

                // Fill empty cells placeholder with appropriate number of empty cells
                // for the row of the number of columns specified with the provided column count.
                for (int cellIndex = 0; cellIndex < columnCount; cellIndex++)
                {
                    // Determine cell sheet relative index of the cell to be created,
                    //  by adding left column index, to the cell order index.
                    int cellSheetRelativeIndex = leftColumnIndex + cellIndex;
                    
                    // Build new cell instance.
                    LiveCell cell = LiveCell.Construct(spreadsheetId, sheetTitleId, cellSheetRelativeIndex, rowSheetRelativeIndex, null);

                    // Add created cell to row cells content list.
                    forcedRowCellsContent.Add(cell);
                }

                // Return 
                return forcedRowCellsContent;
            }

            // Returns content of the row forced to be instantiated without trying to reusing existing cells.
            private IList<LiveCell> BuildForcedRowCellsContentWithNewData<Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<Cell> cellsData)
                where Cell : class
            {
                // Get instance of existing cells placeholder list.
                IList<LiveCell> forcedRowCellsContent = new List<LiveCell>();

                // Fill empty cells placeholder with appropriate number of empty cells
                // for the row of the number of columns specified with the provided column count.
                for (int cellIndex = 0; cellIndex < columnCount; cellIndex++)
                {
                    // Determine cell sheet relative index of the cell to be created,
                    //  by adding left column index, to the cell order index.
                    int cellSheetRelativeIndex = leftColumnIndex + cellIndex;

                    // Get cell data of the type depending on the generic type the current method.
                    Cell cellData = cellsData?.ElementAtOrDefault(cellIndex);

                    // Build new cell instance.
                    LiveCell cell = LiveCell.Construct(spreadsheetId, sheetTitleId, cellSheetRelativeIndex, rowSheetRelativeIndex, cellData);

                    // Add created cell to row cells content list.
                    forcedRowCellsContent.Add(cell);
                }

                // Return 
                return forcedRowCellsContent;
            }
            
            // IMPORTANT NOTE
            // The unusual code design - repeatable code below - its a performance optimize design.
            // As many as possible if/else checks has been move into the level above to be 
            // executed once for each row rather then once for each cell, and then based on that
            // execute appropriate build row cells content method:
            // BuildRowCellsContentWithoutNewData / BuildRowCellsContentWithoutDataOverride / BuildRowCellsContentWithDataOverride

            // Building Row Cells Content reusing existing cells where available:

            // Size and content of provided IList<Cell> newRowCellsData must matching other provided parameters string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount.
            private IList<LiveCell> BuildRowCellsContentWithoutNewData(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount)
            {
                // Get all existing rows data related with the provided spreadsheet id/ sheet title id / row index.
                IList<LiveRow> existingRowData = this._productionIndex[spreadsheetId][sheetTitleId][rowSheetRelativeIndex];

                // Get instance of existing cells placeholder list.
                IList<LiveCell> rowCellsContent = new List<LiveCell>();

                // Obtain existing cells located on the area covered by the provided parameters.
                // Loop from 0 to < columnCount
                for (int cellRangeRelativeIndex = 0; cellRangeRelativeIndex < columnCount; cellRangeRelativeIndex++)
                {
                    // Calculate cell sheet relative index for 
                    // this specific cell range relative index.
                    int cellSheetRelativeIndex = cellRangeRelativeIndex + leftColumnIndex;

                    // Loop through each existing row...
                    foreach (LiveRow exsistingRow in existingRowData)
                    {
                        // ...if this specific existing row contains the cell located
                        // on the appropriate cell sheet relative index....
                        if (exsistingRow.HasCellOnSheetRelativeColumnIndex(cellSheetRelativeIndex))
                        {
                            // ... obtain that cell...
                            LiveCell existingCell = exsistingRow.GetCellFromSheetRelativeColumnIndex(cellSheetRelativeIndex);

                            // ...and add it to the existing cells list. ...
                            rowCellsContent.Add(existingCell);

                            // ...Then break out from foreach loop.
                            break;
                        }
                    }

                    // If existing cell hasn't been found and added in to the existing cells list,
                    // in the above foreach loop...
                    if (rowCellsContent.Count == cellRangeRelativeIndex)
                    {
                        // ...build new empty cell instance...
                        LiveCell emptyCell = LiveCell.Construct(spreadsheetId, sheetTitleId, cellSheetRelativeIndex, rowSheetRelativeIndex, null);

                        // ...and add it to the existing cells list.
                        rowCellsContent.Add(emptyCell);
                    }
                }

                // Return constructed row cells content.
                return rowCellsContent;
            }

            // Size and content of provided IList<Cell> newRowCellsData must matching other provided parameters string spreadsheetId, string sheetTitleId, int leftColumnIndex, 
            // int rowSheetRelativeIndex.
            private IList<LiveCell> BuildRowCellsContentWithoutDataOverride<Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<Cell> newRowCellsData)
                where Cell : class
            {

                // Get all existing rows data related with the provided spreadsheet id/ sheet title id / row index.
                IList<LiveRow> existingRowData = this._productionIndex[spreadsheetId][sheetTitleId][rowSheetRelativeIndex];

                // Get instance of existing cells placeholder list.
                IList<LiveCell> rowCellsContent = new List<LiveCell>();

                // If new row cells data has been provided simply obtain existing cells
                // located on the area covered by the provided parameters,
                // then assign the data, but only 

                // Loop from 0 to < columnCount. Obtain existing cells wherever they are available,
                for (int cellRangeRelativeIndex = 0; cellRangeRelativeIndex < columnCount; cellRangeRelativeIndex++)
                {
                    // Calculate cell sheet relative index for 
                    // this specific cell range relative index.
                    int cellSheetRelativeIndex = cellRangeRelativeIndex + leftColumnIndex;

                    // Loop through each existing row...
                    foreach (LiveRow exsistingRow in existingRowData)
                    {
                        // ...if this specific existing row contains the cell located
                        // on the appropriate cell sheet relative index....
                        if (exsistingRow.HasCellOnSheetRelativeColumnIndex(cellSheetRelativeIndex))
                        {
                            // ... obtain that cell...
                            LiveCell existingCell = exsistingRow.GetCellFromSheetRelativeColumnIndex(cellSheetRelativeIndex);

                            // ...then add cell in to the row cells content. ...
                            rowCellsContent.Add(existingCell);

                            // ...Then break out from foreach loop.
                            break;
                        }
                    }

                    // If existing cell hasn't been found and added in to the existing cells list,
                    // in the above foreach loop...
                    if (rowCellsContent.Count == cellRangeRelativeIndex)
                    {
                        // ... obtain new data for that cell if any available
                        Cell newRowCellData = newRowCellsData?.ElementAtOrDefault(cellRangeRelativeIndex);

                        // ...build new empty cell instance based on that data, or empty cell for null...
                        LiveCell emptyCell = LiveCell.Construct(spreadsheetId, sheetTitleId, cellSheetRelativeIndex, rowSheetRelativeIndex, newRowCellData);

                        // ...and add it to the existing cells list.
                        rowCellsContent.Add(emptyCell);
                    }
                }

                // Return constructed row cells content.
                return rowCellsContent;
            }

            // Size and content of provided IList<Cell> newRowCellsData must matching other provided parameters string spreadsheetId, string sheetTitleId, int leftColumnIndex,
            // int rowSheetRelativeIndex, int columnCount.
            private IList<LiveCell> BuildRowCellsContentWithDataOverride<Cell>(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<Cell> newRowCellsData, bool ignoreNullData)
                where Cell : class
            {
                // Get all existing rows data related with the provided spreadsheet id/ sheet title id / row index.
                IList<LiveRow> existingRowData = this._productionIndex[spreadsheetId][sheetTitleId][rowSheetRelativeIndex];

                // Get instance of existing cells placeholder list.
                IList<LiveCell> rowCellsContent = new List<LiveCell>();

                // If new row cells data has been provided simply obtain existing cells
                // located on the area covered by the provided parameters,
                // then assign the data, but only 

                // Loop from 0 to < columnCount. Obtain existing cells wherever they are available,
                for (int cellRangeRelativeIndex = 0; cellRangeRelativeIndex < columnCount; cellRangeRelativeIndex++)
                {
                    // Calculate cell sheet relative index for 
                    // this specific cell range relative index.
                    int cellSheetRelativeIndex = cellRangeRelativeIndex + leftColumnIndex;

                    // ... obtain new data for that cell if any available
                    Cell newRowCellData = newRowCellsData?.ElementAtOrDefault(cellRangeRelativeIndex);

                    // Loop through each existing row...
                    foreach (LiveRow exsistingRow in existingRowData)
                    {
                        // ...if this specific existing row contains the cell located
                        // on the appropriate cell sheet relative index....
                        if (exsistingRow.HasCellOnSheetRelativeColumnIndex(cellSheetRelativeIndex))
                        {
                            // ... obtain that cell...
                            LiveCell existingCell = exsistingRow.GetCellFromSheetRelativeColumnIndex(cellSheetRelativeIndex);

                            // ... override data for the existing cell using provided ignoreNullData flag
                            existingCell.SetData(newRowCellData, ignoreNullData);

                            // ...then add cell in to the row cells content. ...
                            rowCellsContent.Add(existingCell);

                            // ...Then break out from foreach loop.
                            break;
                        }
                    }

                    // If existing cell hasn't been found and added in to the existing cells list,
                    // in the above foreach loop...
                    if (rowCellsContent.Count == cellRangeRelativeIndex)
                    {
                        // ...build new empty cell instance based on that data, or empty cell for null...
                        LiveCell emptyCell = LiveCell.Construct(spreadsheetId, sheetTitleId, cellSheetRelativeIndex, rowSheetRelativeIndex, newRowCellData);

                        // ...and add it to the existing cells list.
                        rowCellsContent.Add(emptyCell);
                    }
                }

                // Return constructed row cells content.
                return rowCellsContent;
            }
            #endregion

            // The only method which should call the LiveRow constructor.
            private LiveRow Manufacture(string spreadsheetId, string sheetTitleId, int leftColumnIndex, int rowSheetRelativeIndex, int columnCount, IList<LiveCell> rowCellsContent)
            {
                // Build new instance of LiveRow class using provided parameters...
                LiveRow newRow = new LiveRow(
                        // Row cells content spreadsheet id
                        spreadsheetId,
                        // Row cells content sheet title id
                        sheetTitleId,
                        // Row most left column index
                        leftColumnIndex,
                        // Row sheet relative index
                        rowSheetRelativeIndex,
                        // Total number of the cells covered by the row 
                        columnCount,
                        // Row cells content
                        rowCellsContent);


                // Add newly instantiated LiveRow into the factory production index. 
                this._productionIndex.AddSpecificRow(newRow);

                // Return newly created row.
                return newRow;
            }
            #endregion
        }
    }
}
