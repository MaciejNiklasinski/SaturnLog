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
    public class LiveSheet : LiveRange
    {
        #region Properties
        // Sheet integer id
        internal int? SheetId { get; set; }
        
        // LiveRow can be referenced to as long as they are part of LiveSheet. (and optionally LiveRange as well as LiveSheet)
        public IList<LiveRow> SheetRows { get { return base.Rows; } }

        // List containing all of the ranges describing content of this sheet.
        public IList<LiveRange> Ranges
        {
            get
            {
                // Obtain all the ranges related with the current LiveSheet...
                IList<LiveRange> ranges = LiveSheet.Factory.GetExistingSheetRanges(this.SpreadsheetId, this.SheetTitleId);

                // ... apart of the one representing LiveSheet itself - remove it from retrieved results.
                ranges.Remove(this);

                // return obtained ranges.
                return ranges;
            }
        }
        #endregion

        #region Constructor
        public LiveSheet(string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, IList<LiveRow> rowsData)
            : base(spreadsheetId, sheetTitleId, 0, 0, columnCount, rowCount, rowsData) { }
        #endregion

        #region Methods
        // Destroy the LiveSheet together with all its content.
        internal override void Destroy(bool andDispose)
        {
            // Loop through each of the ranges related with the current sheet...
            // ... and destroy (and dispose if provided andDispose flag is true)
            // each and every one of them.
            foreach (LiveRange range in this.Ranges)
                if (andDispose)
                    range.Dispose();
                else
                    range.Destroy(false);

            // Loop through all the rows of the current sheet...
            foreach (LiveRow row in this.Rows)
                // .. loop through all the cells in the row...
                foreach (LiveCell cell in row.Cells)
                    // .. and destroy each and every one of them.
                    cell.Destroy();

            // Execute base destruction method inherited from LiveRange.
            base.Destroy(andDispose);

            LiveSheet.Factory.RemoveStoredSheet(this.SpreadsheetId, this.SheetTitleId);
        }

        // Assure that the current size has exactly requested number of columns and rows.
        internal void AssureSheetSize(int columnCount, int rowCount)
        {
            // If the current LiveSheet has different number of columns then specified with the provided method parameter columnCount...
            if (this.ColumnCount != columnCount)
            {
                // ... calculate the difference in the number of the requested columns and existing ones...
                int columnCountDiffrence = Math.Abs(this.ColumnCount - columnCount);

                // Check whether current LiveSheet have more, or less columns then specified with the provided method parameter columnCount.
                if (this.ColumnCount < columnCount)
                    // Append the current LiveSheet with required number of columns to even it out with the one specified with the provided method parameter columnCount.
                    this.AddColumns(this.ColumnCount, columnCountDiffrence);
                else
                    // Remove the required number of columns to even it out with the one specified with the provided method parameter columnCount.
                    this.RemoveColumns(this.ColumnCount, columnCountDiffrence);
            }

            // If the current LiveSheet has different number of rows then specified with the provided method parameter rowCount...
            if (this.RowCount != rowCount)
            {
                // ... calculate the difference in the number of the requested rows and existing ones...
                int rowCountDiffrence = Math.Abs(this.RowCount - rowCount);

                // Check whether current LiveSheet have more, or less rows then specified with the provided method parameter rowCount.
                if (this.RowCount < rowCount)
                    // Append the current LiveSheet with required number of rows to even it out with the one specified with the provided method parameter rowCount.
                    this.AddRows(this.RowCount, rowCountDiffrence);
                else
                    // Remove the required number of rows to even it out with the one specified with the provided method parameter rowCount.
                    this.RemoveRows(this.RowCount, rowCountDiffrence);
            }
        }

        public void AdjustColumnsWidthDiemansions(LiveSpreadsheetsDb db, int leftColumnIndex, int columnsToAssureWidthCount, int columnWidthPixelsCount)
        {
            if (leftColumnIndex < 0 || leftColumnIndex > this.RightIndex)
                throw new ArgumentException("Provided left column index is outside of the scope the current sheet.", nameof(leftColumnIndex));
            else if (leftColumnIndex + columnsToAssureWidthCount - 1 < 0 || columnsToAssureWidthCount < 1)
                throw new ArgumentException("Described range is outside of the scope of the current sheet.", nameof(columnsToAssureWidthCount));
            else if (columnWidthPixelsCount < 0)
                throw new ArgumentException("columnWidthPixelsCount cannot be less then 0.", nameof(columnWidthPixelsCount));

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            if (this.SheetId is null)
                this.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, this.SheetTitleId);

            // Request google service to adjust columns width.
            sheetsService.AdjustColumnsWidthDimensionSync(this.SpreadsheetId, this.SheetId, leftColumnIndex, columnsToAssureWidthCount, columnWidthPixelsCount);
        }

        public void AdjustRowsHeightDiemansions(LiveSpreadsheetsDb db, int topRowIndex, int rowsToAssureHeightCount, int rowHeightPixelsCount)
        {
            if (topRowIndex < 0 || topRowIndex > this.BottomIndex)
                throw new ArgumentException("Provided top row index is outside of the scope the current sheet.", nameof(topRowIndex));
            else if (topRowIndex + rowsToAssureHeightCount - 1 < 0 || rowsToAssureHeightCount < 1)
                throw new ArgumentException("Described range is outside of the scope of the current sheet.", nameof(rowsToAssureHeightCount));
            else if (rowHeightPixelsCount < 0)
                throw new ArgumentException("rowWidthPixelsCount cannot be less then 0.", nameof(rowHeightPixelsCount));

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            if (this.SheetId is null)
                this.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, this.SheetTitleId);

            // Request google service to adjust rows height.
            sheetsService.AdjustRowsHeightDimensionSync(this.SpreadsheetId, this.SheetId, topRowIndex, rowsToAssureHeightCount, rowHeightPixelsCount);
        }

        public void AdjustMultipleColumnsRangesWidthDiemansions(LiveSpreadsheetsDb db, IList<Tuple<int, int, int>> columnsRangesDiemansionsAdjustmentBlueprint)
        {
            // Validate whether all the provided blueprints are valid
            foreach (Tuple<int, int, int> columnRangeDiemansionsAdjustmentBlueprint in columnsRangesDiemansionsAdjustmentBlueprint)
            {
                int leftColumnIndex = columnRangeDiemansionsAdjustmentBlueprint.Item1;
                int columnsToAssureWidthCount = columnRangeDiemansionsAdjustmentBlueprint.Item2;
                int columnWidthPixelsCount = columnRangeDiemansionsAdjustmentBlueprint.Item3;

                if (leftColumnIndex < 0 || leftColumnIndex > this.RightIndex)
                    throw new ArgumentException("At least one provided blueprint has left column index outside of the scope the current sheet.", nameof(leftColumnIndex));
                else if (leftColumnIndex + columnsToAssureWidthCount - 1 < 0 || columnsToAssureWidthCount < 1)
                    throw new ArgumentException("At least one provided blueprint describes range outside of the scope of the current sheet.", nameof(columnsToAssureWidthCount));
                else if (columnWidthPixelsCount < 0)
                    throw new ArgumentException("At least one provided blueprint columnWidthPixelsCount is less then 0.", nameof(columnWidthPixelsCount));
            }

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            if (this.SheetId is null)
                this.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, this.SheetTitleId);

            // For each Tuple provided in columnsRangesDiemansionsAdjustmentBlueprint create associated sheetId containing tuple blueprint,
            // and return the list containing all of them.
            List<Tuple<int?, int, int, int>> columnsRangesDiemansionsAdjustmentSheetIdBlueprint = columnsRangesDiemansionsAdjustmentBlueprint.Select((columnRangeDiemansionsAdjustmentBlueprint) =>
            {
                // Build sheetId contacting tuple blueprint based on the one provided as a method parameter, without specified sheetId.
                return new Tuple<int?, int, int, int>(this.SheetId, columnRangeDiemansionsAdjustmentBlueprint.Item1, columnRangeDiemansionsAdjustmentBlueprint.Item2, columnRangeDiemansionsAdjustmentBlueprint.Item3);
            }).ToList();

            // Adjusts multiple columns ranges width dimensions.
            sheetsService.AdjustMultipleColumnsRangesWidthDimensionsSync(this.SpreadsheetId, columnsRangesDiemansionsAdjustmentSheetIdBlueprint);
        }

        public void AdjustMultipleRowsRangesHeightDiemansions(LiveSpreadsheetsDb db, IList<Tuple<int, int, int>> rowsRangesDiemansionsAdjustmentBlueprint)
        {
            // Validate whether all the provided blueprints are valid
            foreach (Tuple<int, int, int> rowRangeDiemansionsAdjustmentBlueprint in rowsRangesDiemansionsAdjustmentBlueprint)
            {
                int topRowIndex = rowRangeDiemansionsAdjustmentBlueprint.Item1;
                int rowsToAssureHeightCount = rowRangeDiemansionsAdjustmentBlueprint.Item2;
                int rowWidthPixelsCount = rowRangeDiemansionsAdjustmentBlueprint.Item3;

                if (topRowIndex < 0 || topRowIndex > this.BottomIndex)
                    throw new ArgumentException("At least one provided blueprint has top row index outside of the scope the current sheet.", nameof(topRowIndex));
                else if (topRowIndex + rowsToAssureHeightCount - 1 < 0 || rowsToAssureHeightCount < 1)
                    throw new ArgumentException("At least one provided blueprint describes range outside of the scope of the current sheet.", nameof(rowsToAssureHeightCount));
                else if (rowWidthPixelsCount < 0)
                    throw new ArgumentException("At least one provided blueprint rownWidthPixelsCount is less then 0.", nameof(rowWidthPixelsCount));

            }

            // Get google apis SheetsService instance
            SheetsService sheetsService = db.ApisSheetsService;

            // if current sheet SheetId is null, obtain it from based on the sheet title id.
            if (this.SheetId is null)
                this.SheetId = sheetsService.GetSheetIdFromSheetTitleIdIdSync(this.SpreadsheetId, this.SheetTitleId);

            // For each Tuple provided in rowsRangesDiemansionsAdjustmentBlueprint create associated sheetId containing tuple blueprint,
            // and return the list containing all of them.
            List<Tuple<int?, int, int, int>> rowsRangesDiemansionsAdjustmentSheetIdBlueprint = rowsRangesDiemansionsAdjustmentBlueprint.Select((rowRangeDiemansionsAdjustmentBlueprint) =>
            {
                // Build sheetId contacting tuple blueprint based on the one provided as a method parameter, without specified sheetId.
                return new Tuple<int?, int, int, int>(this.SheetId, rowRangeDiemansionsAdjustmentBlueprint.Item1, rowRangeDiemansionsAdjustmentBlueprint.Item2, rowRangeDiemansionsAdjustmentBlueprint.Item3);
            }).ToList();

            // Adjusts multiple rows ranges height dimensions.
            sheetsService.AdjustMultipleRowsRangesHeightDimensionsSync(this.SpreadsheetId, rowsRangesDiemansionsAdjustmentSheetIdBlueprint);

        }
        
        #region AppendRows
        public void AppendRows(LiveSpreadsheetsDb db, int rowsToAdd)
        {
            // Commits appended rows into google servers database
            db.ApisSheetsService.AppendRowsSync(this.SpreadsheetId, this.SheetTitleId, this.ColumnCount, this.RowCount, rowsToAdd, new IList<object>[rowsToAdd]);

            // Appends current live sheet content with requested number of empty rows.
            this.AddRows(this.BottomIndex + 1, rowsToAdd);
        }

        public void AppendRows(LiveSpreadsheetsDb db, int rowsToAdd, IList<IList<string>> rowsData)
        {
            // From each list of strings in the provided rows data...
            IList<IList<object>> rowsObjectsData = rowsData.Select<IList<string>, IList<object>>((row) =>
            {
                // ... transform list of strings to list of objects....
                return row.Select<string, object>(str => str).ToList();
            }).ToList();
            
            // ... and use that result to execute appropriate overloaded version of this method.
            this.AppendRows(db, rowsToAdd, rowsObjectsData);
        }

        public void AppendRows(LiveSpreadsheetsDb db, int rowsToAdd, IList<IList<object>> rowsData)
        {
            // Commits appended rows into google servers database
            db.ApisSheetsService.AppendRowsSync(this.SpreadsheetId, this.SheetTitleId, this.ColumnCount, this.RowCount, rowsToAdd, rowsData);

            // Appends current live sheet content with requested number of empty rows.
            this.AddRows<IList<object>, object>(this.BottomIndex + 1, rowsToAdd, rowsData);
        }

        internal void AppendRows(LiveSpreadsheetsDb db, int rowsToAdd, IList<RowData> rowsData)
        {
            // From each row in provided list of row data...
            IList<IList<object>> rowsObjectsData = rowsData.Select<RowData, IList<object>>((row) =>
            {
                // ...obtain list of objects representing cell data....
                return row.Values?.Select<CellData, object>(cell => cell.GetDataAsObject()).ToList()
                // ... or new empty list of objects if row data value is empty
                ?? new List<object>();
            }).ToList();

            // ... and used that result to execute appropriate overloaded version of the current method.
            this.AppendRows(db, rowsToAdd, rowsObjectsData);
        }
        #endregion

        #region InsertRows
        public void InsertRows(LiveSpreadsheetsDb db, int toBeInsertedRowTopIndex, int toBeInsertedRowCount)
        {
            db.ApisSheetsService.InsertRowsSync(this.SpreadsheetId, this.SheetTitleId, this.SheetId, this.ColumnCount, toBeInsertedRowTopIndex, toBeInsertedRowCount, null);

            this.AddRows(toBeInsertedRowTopIndex, toBeInsertedRowCount);
        }

        public void InsertRows(LiveSpreadsheetsDb db, int toBeInsertedRowTopIndex, int toBeInsertedRowCount, IList<IList<string>> rowsData)
        {
            // From each list of strings in the provided rows data...
            IList<IList<object>> rowsObjectsData = rowsData.Select<IList<string>, IList<object>>((row) =>
            {
                // ... transform list of strings to list of objects....
                return row.Select<string, object>(str => str).ToList();
            }).ToList();

            // ... and used that result to execute appropriate overloaded version of the current method.
            this.InsertRows(db, toBeInsertedRowTopIndex, toBeInsertedRowCount, rowsObjectsData);
        }

        public void InsertRows(LiveSpreadsheetsDb db, int toBeInsertedRowTopIndex, int toBeInsertedRowCount, IList<IList<object>> rowsData)
        {
            db.ApisSheetsService.InsertRowsSync(this.SpreadsheetId, this.SheetTitleId, this.SheetId, this.ColumnCount, toBeInsertedRowTopIndex, toBeInsertedRowCount, rowsData);

            this.AddRows<IList<object>, object>(toBeInsertedRowTopIndex, toBeInsertedRowCount, rowsData);
        }

        internal void InsertRows(LiveSpreadsheetsDb db, int toBeInsertedRowTopIndex, int toBeInsertedRowCount, IList<RowData> rowsData)
        {
            // From each row in provided list of row data...
            IList<IList<object>> rowsObjectsData = rowsData.Select<RowData, IList<object>>((row) =>
            {
                // ...obtain list of objects representing cell data....
                return row.Values?.Select<CellData, object>(cell => cell.GetDataAsObject()).ToList()
                // ... or new empty list of objects if row data value is empty
                ?? new List<object>();
            }).ToList();


            // ... and used that result to execute appropriate overloaded version of the current method.
            this.InsertRows(db, toBeInsertedRowTopIndex, toBeInsertedRowCount, rowsObjectsData);

        }
        #endregion

        #region RemoveRows
        public void RemoveRows(LiveSpreadsheetsDb db, int toBeRemovedRowTopIndex, int toBeRemovedRowsCount)
        {
            db.ApisSheetsService.RemoveRowsSync(this.SpreadsheetId, this.SheetTitleId, this.SheetId, toBeRemovedRowTopIndex, toBeRemovedRowsCount);

            this.RemoveRows(toBeRemovedRowTopIndex, toBeRemovedRowsCount);
        }
        #endregion

        // TODO
        #region AppendColumns

        #endregion
        // TODO
        #region InsertColumns

        #endregion
        // TODO
        #region RemoveColumns

        #endregion
        #endregion

        #region Private Helpers
        #region AddRange
        // Add the range of requested size/location in the current LiveSheet if it doesn't contain one of matching size/location already, otherwise do nothing.
        public void AddRange(int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
        {
            // Validate provided parameters and throw appropriate exception if unable to proceed.
            if (leftColumnIndex < 0)
                throw new ArgumentException("Invalid left column index provided. Please provide value equal or greater then 0.", nameof(leftColumnIndex));
            else if (topRowIndex < 0)
                throw new ArgumentException("Invalid top row index provided. Please provide value equal greater then 0.", nameof(leftColumnIndex));
            else if (columnCount < 1)
                throw new ArgumentException("Invalid number of columns provided. Unable to create a range containing less then one column.", nameof(columnCount));
            else if (rowCount < 1)
                throw new ArgumentException("Invalid number of rows provided. Unable to create a range containing less then one row.", nameof(rowCount));

            // Constructs the instance of the LiveRange and coupling it with the current LiveSheet in the LiveRange factory production index.
            LiveRange.Factory.GetRange(this.SpreadsheetId, this.SheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount);
        }
        
        // Add the range of requested size/location in the current LiveSheet if it doesn't contain one of matching size/location already, otherwise do nothing.
        public void AddRange(string rangeString)
        {
            // Translate provided range string into separate parameters...
            RangeTranslator.RangeStringToParameters(rangeString, out string sheetTitleId, out int leftColumnIndex, out int topRowIndex, out int ColumnCount, out int rowCount);

            // ... and use them to execute appropriate overloaded version of the current method.
            this.AddRange(leftColumnIndex, topRowIndex, ColumnCount, rowCount);
        }
        #endregion

        #region RemoveRange
        // Remove the range if the current LiveSheet contain one of matching size/location, otherwise do nothing.
        public void RemoveRange(int leftColumnIndex, int topRowIndex, int columnCount, int rowCount)
        {
            // Obtain range string...
            string rangeString = RangeTranslator.GetRangeString(this.SheetTitleId, leftColumnIndex, topRowIndex, columnCount, rowCount);

            // ... and use it to execute appropriate overloaded version of the current method.
            this.RemoveRange(rangeString);

        }

        // Remove the range if the current LiveSheet contain one of matching size/location, otherwise do nothing.
        public void RemoveRange(string rangeString)
        {
            // If the provided rangeString describing boundaries of the current LiveSheet, throw appropriate exception.
            if (this.Range == rangeString)
                throw new InvalidOperationException("Unable to remove the range representing entire LiveSheet.");

            // Destroy the existing LiveRange if any available.
            LiveRange.Factory.GetExistingRange(this.SpreadsheetId, this.SheetTitleId, rangeString)?.Destroy(true);
        }
        #endregion

        // BE AWARE - Private helpers of this specific method braking SRP multiple times.
        // It is purposely implemented performance optimized design trying to keep
        // looping through the same collections as rare as possible and still do the work.
        #region AddColumns

        // IMPORTANT NOTE:
        // This will effect the content off all the ranges related with the current LiveSheet.

        // Adds newly created empty columns into the specified place on the sheet.
        private void AddColumns(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsCount)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedColumnsLeftIndex < 0)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be lower then 0.");
            else if (toBeAddedColumnsLeftIndex > this.RightIndex + 1)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be greater then sheet right index + 1, indicating the next column to the right from the most right existing column of the sheet.");
            else if (toBeAddedColumnsCount < 0)
                throw new ArgumentException("Provided toBeAddedColumnsCount cannot be lower then 0.");
            // If requested to add 0 columns, simply do nothing and return.
            else if (toBeAddedColumnsCount == 0)
                return;

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;
            
            // Calculate the future right column index of the current LiveSheet, after columns addition will get finalized.
            int futureSheetRightIndex = this.RightIndex + toBeAddedColumnsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedColumnsRightIndex = toBeAddedColumnsLeftIndex + toBeAddedColumnsCount - 1;

            // Side-note: order matters 
            
            // Extends the size of LiveRows instances to create enough space for new columns by shifting current content of this rows to the right.
            this.PrepareSheetRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount, futureSheetRightIndex);
            
            // Creates the cells required for new columns and assign then into appropriate places
            // on the current LiveSheet used LiveRows, as well as modifying column index of all 
            // the LiveCells which have been transfered to the right to create space for new columns.
            this.PrepareCellsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount);
            
            // Based on the already edited content of the current LiveSheet, adjust content of all 
            // the LiveRows which are related with the current LiveSheet, but not used by the sheet itself.
            this.PrepareSheetRangesRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsCount, futureSheetRightIndex);

            // As the LiveRows used by the current LiveSheet changed they columns count,
            // but some of the LiveRanges which use to covered the area as wide as the LiveSheet
            // itself, now end up with LiveRows wider than them self, and as such all these
            // LiveRanges have to obtain new LiveRows using appropriate factory.
            this.PrepareUnifiedRowsUsageForColumnsAdditon(toBeAddedColumnsCount);

            // Increase the value of the current LiveSheet columns count.
            this.ColumnCount += toBeAddedColumnsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        // IMPORTANT NOTE:
        // This will effect the content off all the ranges related with the current LiveSheet.

        // Adds newly created, pre-filled with provided data columns into the specified place on the sheet.
        private void AddColumns<Row,Cell>(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsCount, IList<Row> newColumnsData)
            where Row : class, IList<Cell> where Cell : class
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedColumnsLeftIndex < 0)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be lower then 0.");
            else if (toBeAddedColumnsLeftIndex > this.RightIndex + 1)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be greater then sheet right index + 1, indicating the next column to the right from the most right existing column of the sheet.");
            else if (toBeAddedColumnsCount < 0)
                throw new ArgumentException("Provided toBeAddedColumnsCount cannot be lower then 0.");
            // If requested to add 0 columns, simply do nothing and return.
            else if (toBeAddedColumnsCount == 0)
                return;

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;

            // Calculate the future right column index of the current LiveSheet, after columns addition will get finalized.
            int futureSheetRightIndex = this.RightIndex + toBeAddedColumnsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedColumnsRightIndex = toBeAddedColumnsLeftIndex + toBeAddedColumnsCount - 1;

            // Side-note: order matters 

            // Extends the size of LiveRows instances to create enough space for new columns by shifting current content of this rows to the right.
            this.PrepareSheetRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount, futureSheetRightIndex);

            // Creates the cells required for new columns and assign then into appropriate places
            // on the current LiveSheet used LiveRows, as well as modifying column index of all 
            // the LiveCells which have been transfered to the right to create space for new columns.
            this.PrepareCellsForColumnsAddition<Row,Cell>(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount, newColumnsData);

            // Based on the already edited content of the current LiveSheet, adjust content of all 
            // the LiveRows which are related with the current LiveSheet, but not used by the sheet itself.
            this.PrepareSheetRangesRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsCount, futureSheetRightIndex);

            // As the LiveRows used by the current LiveSheet changed they columns count,
            // but some of the LiveRanges which use to covered the area as wide as the LiveSheet
            // itself, now end up with LiveRows wider than them self, and as such all these
            // LiveRanges have to obtain new LiveRows using appropriate factory.
            this.PrepareUnifiedRowsUsageForColumnsAdditon(toBeAddedColumnsCount);

            // Increase the value of the column count by the number of the columns to be added.
            this.ColumnCount += toBeAddedColumnsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        // IMPORTANT NOTE:
        // This will effect the content off all the ranges related with the current LiveSheet.

        // Adds newly created, pre-filled with provided data columns into the specified place on the sheet.
        private void AddColumns(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsCount, IList<RowData> newColumnsData)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedColumnsLeftIndex < 0)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be lower then 0.");
            else if (toBeAddedColumnsLeftIndex > this.RightIndex + 1)
                throw new ArgumentException("Provided toBeAddedColumnsLeftIndex cannot be greater then sheet right index + 1, indicating the next column to the right from the most right existing column of the sheet.");
            else if (toBeAddedColumnsCount < 0)
                throw new ArgumentException("Provided toBeAddedColumnsCount cannot be lower then 0.");
            // If requested to add 0 columns, simply do nothing and return.
            else if (toBeAddedColumnsCount == 0)
                return;

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;

            // Calculate the future right column index of the current LiveSheet, after columns addition will get finalized.
            int futureSheetRightIndex = this.RightIndex + toBeAddedColumnsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedColumnsRightIndex = toBeAddedColumnsLeftIndex + toBeAddedColumnsCount - 1;

            // Side-note: order matters 

            // Extends the size of LiveRows instances to create enough space for new columns by shifting current content of this rows to the right.
            this.PrepareSheetRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount, futureSheetRightIndex);

            // Creates the cells required for new columns and assign then into appropriate places
            // on the current LiveSheet used LiveRows, as well as modifying column index of all 
            // the LiveCells which have been transfered to the right to create space for new columns.
            this.PrepareCellsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsRightIndex, toBeAddedColumnsCount, newColumnsData);

            // Based on the already edited content of the current LiveSheet, adjust content of all 
            // the LiveRows which are related with the current LiveSheet, but not used by the sheet itself.
            this.PrepareSheetRangesRowsForColumnsAddition(toBeAddedColumnsLeftIndex, toBeAddedColumnsCount, futureSheetRightIndex);

            // As the LiveRows used by the current LiveSheet changed they columns count,
            // but some of the LiveRanges which use to covered the area as wide as the LiveSheet
            // itself, now end up with LiveRows wider than them self, and as such all these
            // LiveRanges have to obtain new LiveRows using appropriate factory.
            this.PrepareUnifiedRowsUsageForColumnsAdditon(toBeAddedColumnsCount);

            // Increase the value of the column count by the number of the columns to be added.
            this.ColumnCount += toBeAddedColumnsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        #region AddColumns - Private Helpers
        // Extends the size of LiveRows instances to create enough space for new columns by shifting current content of this rows to the right.
        private void PrepareSheetRowsForColumnsAddition(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsRightIndex, int toBeAddedColumnsCount, int futureSheetRightIndex)
        {
            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow sheetRow in this.Rows)
            {
                // ... increase sheetRow size to make enough space for cells to be added...
                for (int i = 0; i < toBeAddedColumnsCount; i++)
                    sheetRow.Add(null);

                // .. adjust current row column count to account for changes...
                sheetRow.ColumnCount += toBeAddedColumnsCount;

                // .. starting from the index of the last, most right column of the current LiveSheet
                // after the columns addition gonna get finalized, loop through the current LiveSheet rows,
                // ending it(looping through) on the index right of the most right column to be add....
                for (int cIndex = futureSheetRightIndex; cIndex > toBeAddedColumnsRightIndex; cIndex--)
                    // ...and shift the content of the row to the right to make enough space for cells to be added...
                    sheetRow.Cells[cIndex] = sheetRow.Cells[cIndex - toBeAddedColumnsCount];                
            }
        }

        // Creates the cells required for new columns and assign then into appropriate places
        // on the current LiveSheet used LiveRows. The method also modifies column index of all 
        // the LiveCells which have been transfered to the right to create space for new columns.
        private void PrepareCellsForColumnsAddition(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsRightIndex, int toBeAddedColumnsCount)
        {            
            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow row in this.Rows)
            {
                // ... loop through each column indexed right of the first, most left column to be added
                // starting from the last, most right column in the row (at this time cells have been transferred
                // already and each sheet row contains required number of entries to fit all new columns)...
                for (int i = row.RightIndex; i >= toBeAddedColumnsLeftIndex; i--)
                {
                    // ... if column is located right of the last, most right column to be added...
                    if (i > toBeAddedColumnsRightIndex)
                        // ... increase cell column index by the number of cells to add...
                        row.Cells[i].ColumnIndex += toBeAddedColumnsCount;
                    // ... otherwise create and assign new LiveCell instance.
                    // (at these time newly created LiveCell should override 
                    // cell which have already been transfered accordingly 
                    // the toBeAddedColumnsCount)
                    else row.Cells[i] = LiveCell.Construct(this.SpreadsheetId, this.SheetTitleId, i, row.RowIndex, null);
                }
            }
        }
        
        // Creates the cells required for new columns and assign then into appropriate places
        // on the current LiveSheet used LiveRows. The method also modifies column index of all 
        // the LiveCells which have been transfered to the right to create space for new columns.
        private void PrepareCellsForColumnsAddition(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsRightIndex, int toBeAddedColumnsCount, IList<RowData> newColumnsData)
        {
            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow row in this.Rows)
            {
                // ... loop through each column indexed right of the first, most left column to be added
                // starting from the last, most right column in the row (at this time cells have been transferred
                // already and each sheet row contains required number of entries to fit all new columns)...
                for (int i = row.RightIndex; i >= toBeAddedColumnsLeftIndex; i--)
                {
                    // ... if column is located right of the last, most right column to be added...
                    if (i > toBeAddedColumnsRightIndex)
                        // ... increase cell column index by the number of cells to add...
                        row.Cells[i].ColumnIndex += toBeAddedColumnsCount;
                    // ... otherwise create and assign new LiveCell instance.
                    // (at these time newly created LiveCell should override 
                    // cell which have already been transfered accordingly 
                    // the toBeAddedColumnsCount)
                    else
                    {
                        // Calculate data relative cell index
                        int dataCellIndex = i - toBeAddedColumnsLeftIndex;

                        // Obtain data for the new cell if any available.
                        CellData cellData = newColumnsData?.ElementAtOrDefault(row.RowIndex)?.Values?.ElementAtOrDefault(dataCellIndex);

                        // Creates an instance of Live cell using provided cell data if any available.
                        row.Cells[i] = LiveCell.Construct(this.SpreadsheetId, this.SheetTitleId, i, row.RowIndex, cellData);
                    }
                }
            }
        }

        // Creates the cells required for new columns and assign then into appropriate places
        // on the current LiveSheet used LiveRows. The method also modifies column index of all 
        // the LiveCells which have been transfered to the right to create space for new columns.
        private void PrepareCellsForColumnsAddition<Row, Cell>(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsRightIndex, int toBeAddedColumnsCount, IList<Row> newColumnsData)
            where Row : class, IList<Cell> where Cell : class
        {
            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow row in this.Rows)
            {
                // ... loop through each column indexed right of the first, most left column to be added
                // starting from the last, most right column in the row (at this time cells have been transferred
                // already and each sheet row contains required number of entries to fit all new columns)...
                for (int i = row.RightIndex; i >= toBeAddedColumnsLeftIndex; i--)
                {
                    // ... if column is located right of the last, most right column to be added...
                    if (i > toBeAddedColumnsRightIndex)
                        // ... increase cell column index by the number of cells to add...
                        row.Cells[i].ColumnIndex += toBeAddedColumnsCount;
                    // ... otherwise create and assign new LiveCell instance.
                    // (at these time newly created LiveCell should override 
                    // cell which have already been transfered accordingly 
                    // the toBeAddedColumnsCount)
                    else
                    {
                        // Calculate data relative cell index
                        int dataCellIndex = i - toBeAddedColumnsLeftIndex;

                        // Obtain data for the new cell if any available.
                        Cell cellData = newColumnsData?.ElementAtOrDefault(row.RowIndex)?.ElementAtOrDefault(dataCellIndex);

                        // Creates an instance of Live cell using provided cell data if any available.
                        row.Cells[i] = LiveCell.Construct(this.SpreadsheetId, this.SheetTitleId, i, row.RowIndex, cellData);
                    }
                }
            }
        }
        
        // Based on the already edited content of the current LiveSheet, adjust content of all 
        // the LiveRows which are related with the current LiveSheet, but not used by the sheet itself.
        private void PrepareSheetRangesRowsForColumnsAddition(int toBeAddedColumnsLeftIndex, int toBeAddedColumnsCount, int futureSheetRightIndex)
        {
            // Get all the rows used by the current LiveSheet, or any of the LiveRange related with it...
            IEnumerable<LiveRow> notSheetRows = LiveRow.Factory.GetSheetAllRangesRows(this.SpreadsheetId, this.SheetTitleId)
                // ... and filter out all the LiveRow instances used directly by the current LiveSheet.
                .Where((row) => { return !(row.LeftIndex == this.LeftIndex && row.RightIndex == futureSheetRightIndex); });
                        
            // For each of the rows not being in use by the LiveSheet itself.
            foreach (LiveRow notSheetRow in notSheetRows)
            {
                // Calculate row/range relative index of the first, most left cell to be added.
                int toBeAddedCellsRowRelativeLeftIndex = toBeAddedColumnsLeftIndex - notSheetRow.LeftIndex;

                // Reassign each notSheetRow cell content, starting from range relative
                // index of the first, most left column to be added up to the right edge of the row.
                for (int cIndex = toBeAddedCellsRowRelativeLeftIndex; cIndex < notSheetRow.Count; cIndex++)
                {
                    // Get appropriate cell from the appropriate current LiveSheet used LiveRow 
                    // (which by this time should be already completely edited - filled with new
                    // LiveCell as well as necessary cells moved to the right)...
                    LiveCell cell = this.Rows[notSheetRow.RowIndex].Cells[cIndex + notSheetRow.LeftIndex];

                    // ... and assign in into its new space in the currently looped through notSheetRow.
                    notSheetRow.Cells[cIndex] = cell;
                }
            }
        }

        // As the LiveRows used by the current LiveSheet changed they columns count,
        // but some of the LiveRanges which use to covered the area as wide as the LiveSheet
        // itself, now end up with LiveRows wider than them self, and as such all these
        // LiveRanges have to obtain new LiveRows using appropriate factory.
        private void PrepareUnifiedRowsUsageForColumnsAdditon(int toBeAddedColumnsCount)
        {
            // Get the all the ranges which are using the same instances of LiveRow that current LiveSheet itself.
            IEnumerable<LiveRange> rangesRequiringRowsReplacement = this.Ranges.Where((range) => 
            {
                // Include each range with left column index equal to
                // the current LiveSheet left column index - 0...
                return (range.LeftIndex == this.LeftIndex
                // ... and the index of the last, most right column,
                // equal to the right column index of the LiveSheet
                // before columns have been added (as the current LiveSheet
                // column count should not be modified yet, that should
                // be equal to the current LiveSheet right index)
                && range.RightIndex == this.RightIndex);
            });

            // For each of the rows requiring rows replacement...
            foreach (LiveRange range in rangesRequiringRowsReplacement)
            {
                // ... loop through each of the row indexes contained by the range...
                for (int i = range.TopIndex; i <= range.BottomIndex; i++)
                    range.Rows[i - range.TopIndex] = LiveRow.Factory.Get(range.SpreadsheetId, range.SheetTitleId, range.LeftIndex, i, range.ColumnCount);
            }
        }
        #endregion
        #endregion
        
        // BE AWARE - Private helpers of this specific method braking SRP multiple times.
        // It is purposely implemented performance optimized design trying to keep
        // looping through the same collections as rare as possible and still do the work.
        #region RemoveColumns

        // IMPORTANT NOTE:
        // This will effect the content off all the ranges related with the current LiveSheet
        // and might destroy/replace some instances of LiveRange/LiveRow classes.

        // Removes specified number of columns starting from provided left column index.
        internal void RemoveColumns(int toBeRemovedColumnsLeftIndex, int toBeRemovedColumnsCount)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeRemovedColumnsLeftIndex > this.RightIndex)
                throw new ArgumentException($"Unable to remove the requested number of columns starting at the provided index: {toBeRemovedColumnsLeftIndex}. Provided index is outside of the scope of the current LiveSheet instance which has only {this.ColumnCount} columns.");
            else if (this.ColumnCount - toBeRemovedColumnsLeftIndex - toBeRemovedColumnsCount < 0)
                throw new ArgumentException($"Unable to remove {toBeRemovedColumnsCount} columns starting at the provided index: {toBeRemovedColumnsLeftIndex} as this sheet: {this.SheetTitleId} does not have enough columns when started counting from that index.");
            else if (toBeRemovedColumnsCount < 0)
                throw new ArgumentException("Provided toBeRemovedColumnsCount cannot be lower then 0.");
            else if (this.ColumnCount - toBeRemovedColumnsCount < 1)
                throw new ArgumentException($"Unable to remove {toBeRemovedColumnsCount} columns as this sheet: {this.SheetTitleId} would end up with less then one column, and this is not allowed.");
            // If requested to add 0 columns, simply do nothing and return.
            else if (toBeRemovedColumnsCount == 0)
                return;

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;
            
            // Side-note: order matters 

            // Assures that any LiveRange covering the area which will stop existing, 
            // or covering exactly the same area LiveSheet will cover after
            // columns removal will get finalized, will get destroyed together with
            // LiveRow contained by it.
            this.DestroyRangesAndRowsForColumnsRemoval(toBeRemovedColumnsCount);

            // Assures that any LiveRow covering the area which will be covered by
            // the LiveRow used by the LiveSheet itself, after the columns removal
            // will get finalized, will get destroyed, and replaced in any LiveRange 
            // using it with the LiveRow used by the LiveSheet itself. 
            this.PrepareUnifiedRowsUsageForColumnsRemoval(toBeRemovedColumnsCount);

            // Destroys all the cells to be removed as well as edits 
            // the ones located to the right of them to adjust they 
            // column index accordingly.
            this.PrepareCellsForColumnsRemoval(toBeRemovedColumnsLeftIndex, toBeRemovedColumnsCount);
            
            // Transfer LiveRow content LiveCell appropriately to cover removed cells 
            // with the ones located to the right of them.
            this.PrepareRowsForColumnsRemoval(toBeRemovedColumnsLeftIndex, toBeRemovedColumnsCount);

            // Decrease current LiveSheet column count.
            this.ColumnCount -= toBeRemovedColumnsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        #region RemoveColumns - Private Helpers
        // Destroys the LiveRange class instances covering - at least partially - the area of
        // the current LiveSheet which will stop existing after columns removal.
        private void DestroyRangesAndRowsForColumnsRemoval(int toBeRemovedColumnsCount)
        {
            // Calculate the future right column index the current LiveSheet, after columns removal.
            int futureSheetRightIndex = this.RightIndex - toBeRemovedColumnsCount;

            // For each of the ranges related with the current LiveSheet...
            foreach (LiveRange range in this.Ranges)
                // ... if the range covers - at least partially -
                // the area which will stop existing once 
                // the requested columns get removed... 
                if (range.RightIndex > futureSheetRightIndex)
                    // ... destroy the range.
                    range.Destroy(false);
                // ... else if the range covers exactly the area the current LiveSheet
                // itself will cover when then columns will get removed...
                else if (range.LeftIndex == this.LeftIndex
                    && range.TopIndex == this.TopIndex
                    && range.RightIndex == futureSheetRightIndex
                    && range.BottomIndex == this.BottomIndex)
                {
                    // .. mark range as replaced ...
                    range.Replaced = true;

                    // ... destroy the range.
                    range.Destroy(false);
                }
        }

        // Finds the all the ranges related with the current LiveSheet which are currently not
        // equally wide as LiveSheet is(and as such using different instances of LiveRow), 
        // but after the columns will be removed they will become exactly as wide as the current
        // LiveSheet(and as such should be using the same instances of LiveRow),
        // and replace all the rows used by them with the rows used by the current LiveSheet.
        private void PrepareUnifiedRowsUsageForColumnsRemoval(int toBeRemovedColumnsCount)
        {
            // Calculate the future right column index the current LiveSheet, after columns removal.
            int futureSheetRightIndex = this.RightIndex - toBeRemovedColumnsCount;

            // Find all the ranges which will be able to use the same LiveRows instances as the current LiveSheet itself
            // after columns will be removed, but they do not using the same LiveRows instance at this time.
            IEnumerable<LiveRange> rangesToUnifySheetRowsUsage = Ranges.Where((range) =>
            {
                // Include range if the index of the first left column of the range
                // is equal with the index of the first left column of the sheet - 0...
                return ((range.LeftIndex == this.LeftIndex)
                // ... and the index of the last, most right column of the range
                // is equal to the index of the last most right column of
                // the current LiveSheet - after the columns will be removed.
                && (range.RightIndex == futureSheetRightIndex));
            });

            // Loop through all the ranges which requiring row usage to get unified with the LiveSheet...
            foreach (LiveRange range in rangesToUnifySheetRowsUsage)
                // ... loop through all the rows of such a range.
                for (int i = 0; i < range.ColumnCount; i++)
                {
                    // Mark currently used instance of LiveRange as 'Replaced'...
                    range.Rows[i].Replaced = true;

                    // ... destroy it...
                    range.Rows[i].Destroy();

                    // ... and replace it with the row used by the current LiveSheet.
                    range.Rows[i] = this.Rows[i + range.TopIndex];
                }     
        }

        // Destroys all the cells to be removed as well as edits 
        // the ones located to the right of them to adjust they 
        // column index accordingly.
        private void PrepareCellsForColumnsRemoval(int toBeRemovedColumnsLeftIndex, int toBeRemovedColumnsCount)
        {
            // Calculate the sheet relative index of the last column to removed
            int toBeRemovedColumnsRightIndex = toBeRemovedColumnsLeftIndex + toBeRemovedColumnsCount - 1;

            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow row in this.Rows)
            {
                // for each cell in to row requiring destruction...
                for (int i = toBeRemovedColumnsLeftIndex; i <= toBeRemovedColumnsRightIndex; i++)
                    // ...destroy it.
                    row[i].Destroy();

                // Decrease column index of each cell currently indexed with the index
                // greater then the index of the last, most right column to be removed.
                for (int i = toBeRemovedColumnsRightIndex + 1; i < this.ColumnCount; i++)
                    row[i].ColumnIndex -= toBeRemovedColumnsCount;
            }
        }
        
        // Transfer LiveRow content LiveCell appropriately to cover removed cells 
        // with the ones located to the right of them.
        private void PrepareRowsForColumnsRemoval(int toBeRemovedColumnsLeftIndex, int toBeRemovedColumnsCount)
        {
            // Get all the rows NOT used by the current LiveSheet, but in use by any of the LiveRange related with it.
            IList<LiveRow> notSheetRows = LiveRow.Factory.GetSheetAllRangesRows(this.SpreadsheetId, this.SheetTitleId)
                // Filter out all the rows in use by the current LiveSheet.
                .Where((r) => { return (r.LeftIndex != this.LeftIndex || r.RightIndex != this.RightIndex); }).ToList();

            // For each of the rows belonging to the current LiveSheet...
            foreach (LiveRow sheetRow in this.Rows)
            {
                // Shift the content of the row to cover the destroyed cells
                // with the cells currently placed to the right of them.
                for (int cIndex = toBeRemovedColumnsLeftIndex; cIndex < sheetRow.ColumnCount - toBeRemovedColumnsCount; cIndex++)
                    sheetRow.Cells[cIndex] = sheetRow.Cells[cIndex + toBeRemovedColumnsCount];

                // Remove the surplus cells from the ending of the cells list.
                for (int i = sheetRow.ColumnCount - 1; i >= sheetRow.ColumnCount - toBeRemovedColumnsCount; i--)
                    sheetRow.Cells.RemoveAt(i);

                // Adjust current row column count to account for changes.
                sheetRow.ColumnCount -= toBeRemovedColumnsCount;
            }

            // For each of the rows not being in use by the LiveSheet itself.
            foreach (LiveRow notSheetRow in notSheetRows)
            {
                // Calculate row/range relative index of the first, most left cell to be removed.
                int toBeRemovedCellsRowRelativeLeftIndex = toBeRemovedColumnsLeftIndex - notSheetRow.LeftIndex;

                // Shift the content of the row to cover the destroyed cells with the cells currently placed to the right of them.
                for (int cIndex = toBeRemovedCellsRowRelativeLeftIndex; cIndex < notSheetRow.ColumnCount; cIndex++)
                {
                    // Get appropriate cell to be transfered over the destroyed/already transfered cell...
                    LiveCell cell = this.Rows[notSheetRow.RowIndex].Cells[cIndex + notSheetRow.LeftIndex];

                    // ... and assign in into its new space in the row.
                    notSheetRow.Cells[cIndex] = cell;
                }
            }
        }
        #endregion
        #endregion

        // BE AWARE - Private helpers of this specific method braking SRP multiple times.
        // It is purposely implemented performance optimized design trying to keep
        #region AddRows

        // IMPORTANT NOTE:
        // This will effect the content off all the ranges related with the current LiveSheet
        // and might destroy some instances of LiveRow classes which does are not in use
        // by the LiveSheet itself.

        // Adds newly created empty rows into the specified place on the sheet.
        private void AddRows(int toBeAddedRowsTopIndex, int toBeAddedRowsCount)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedRowsTopIndex < 0)
                throw new ArgumentException("Provided newRowsTopIndex cannot be lower then 0.");
            else if (toBeAddedRowsTopIndex > this.BottomIndex + 1)
                throw new ArgumentException("Provided newRowsTopIndex cannot be greater then sheet right index + 1, indicating the row right below the most bottom existing row of the sheet.");
            else if (toBeAddedRowsCount < 1)
                throw new ArgumentException("Provided newRowsToAdd cannot be lower then 1.");  

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;

            // Calculate the future bottom row index of the current LiveSheet, after rows addition will get finalized.
            int futureSheetBottomIndex = this.BottomIndex + toBeAddedRowsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedRowsBottomIndex = toBeAddedRowsTopIndex + toBeAddedRowsCount - 1;

            // Destroys all the LiveRow(without destroying LiveCells contained by it) not in use by the LiveSheet itself
            // which have to be transfered outside they current LiveRange to make a space for new LiveRows. Destroy such a row
            // as long as it has been confirmed that it will not be used by any other LiveRange after the rows transfer will get finalized.
            this.DestroyRowsForRowsAddition(toBeAddedRowsCount);

            // Appends current LiveSheet rows list with the sufficient amount of empties spaces
            // to fit all the rows to be added, then transfers the rows existing on top of
            // the spaces designated for the rows to be added appropriated number of rows down
            // both in the current LiveSheet itself as well as in all the LiveRanges related with it.
            this.PrepareSheetAndRangesForRowsAddition(toBeAddedRowsBottomIndex, toBeAddedRowsCount, futureSheetBottomIndex);

            // Adjust the row index property of all the LiveCells placed in the LiveRow currently indexed
            // on top of the spaces designated for the rows to be added, or below(greater value) them.
            this.PrepareCellsForRowsAddition(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, futureSheetBottomIndex);

            // Creates and assigns required LiveRows with empty cells.
            this.PrepareRowsForRowsAddition(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, toBeAddedRowsCount);

            // Increase the value of the current LiveSheet row count.
            this.RowCount += toBeAddedRowsCount;
            
            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        // Adds newly created, pre-filled with provided data rows into the specified place on the sheet.
        private void AddRows<Row, Cell>(int toBeAddedRowsTopIndex, int toBeAddedRowsCount, IList<Row> newRowsData)
            where Row : class, IList<Cell> where Cell : class
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedRowsTopIndex < 0)
                throw new ArgumentException("Provided newRowsTopIndex cannot be lower then 0.");
            else if (toBeAddedRowsTopIndex > this.BottomIndex + 1)
                throw new ArgumentException("Provided newRowsTopIndex cannot be greater then sheet right index + 1, indicating the row right below the most bottom existing row of the sheet.");
            else if (toBeAddedRowsCount < 1)
                throw new ArgumentException("Provided newRowsToAdd cannot be lower then 1.");

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;

            // Calculate the future bottom row index of the current LiveSheet, after rows addition will get finalized.
            int futureSheetBottomIndex = this.BottomIndex + toBeAddedRowsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedRowsBottomIndex = toBeAddedRowsTopIndex + toBeAddedRowsCount - 1;

            // Destroys all the LiveRow(without destroying LiveCells contained by it) not in use by the LiveSheet itself
            // which have to be transfered outside they current LiveRange to make a space for new LiveRows. Destroy such a row
            // as long as it has been confirmed that it will not be used by any other LiveRange after the rows transfer will get finalized.
            this.DestroyRowsForRowsAddition(toBeAddedRowsCount);

            // Appends current LiveSheet rows list with the sufficient amount of empties spaces
            // to fit all the rows to be added, then transfers the rows existing on top of
            // the spaces designated for the rows to be added appropriated number of rows down
            // both in the current LiveSheet itself as well as in all the LiveRanges related with it.
            this.PrepareSheetAndRangesForRowsAddition(toBeAddedRowsBottomIndex, toBeAddedRowsCount, futureSheetBottomIndex);

            // Adjust the row index property of all the LiveCells placed in the LiveRow currently indexed
            // on top of the spaces designated for the rows to be added, or below(greater value) them.
            this.PrepareCellsForRowsAddition(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, futureSheetBottomIndex);

            // Creates and assigns required LiveRows with cells pre-filled with provided data.
            this.PrepareRowsForRowsAddition<Row, Cell>(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, newRowsData, toBeAddedRowsCount);

            // Increase the value of the current LiveSheet row count.
            this.RowCount += toBeAddedRowsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        // Adds newly created, pre-filled with provided data rows into the specified place on the sheet.
        private void AddRows(int toBeAddedRowsTopIndex, int toBeAddedRowsCount, IList<RowData> newRowsData)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeAddedRowsTopIndex < 0)
                throw new ArgumentException("Provided newRowsTopIndex cannot be lower then 0.");
            else if (toBeAddedRowsTopIndex > this.BottomIndex + 1)
                throw new ArgumentException("Provided newRowsTopIndex cannot be greater then sheet right index + 1, indicating the row right below the most bottom existing row of the sheet.");
            else if (toBeAddedRowsCount < 1)
                throw new ArgumentException("Provided newRowsToAdd cannot be lower then 1.");

            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;

            // Calculate the future bottom row index of the current LiveSheet, after rows addition will get finalized.
            int futureSheetBottomIndex = this.BottomIndex + toBeAddedRowsCount;

            // Calculate the sheet relative index of the last column to be added.
            int toBeAddedRowsBottomIndex = toBeAddedRowsTopIndex + toBeAddedRowsCount - 1;

            // Destroys all the LiveRow(without destroying LiveCells contained by it) not in use by the LiveSheet itself
            // which have to be transfered outside they current LiveRange to make a space for new LiveRows. Destroy such a row
            // as long as it has been confirmed that it will not be used by any other LiveRange after the rows transfer will get finalized.
            this.DestroyRowsForRowsAddition(toBeAddedRowsCount);

            // Appends current LiveSheet rows list with the sufficient amount of empties spaces
            // to fit all the rows to be added, then transfers the rows existing on top of
            // the spaces designated for the rows to be added appropriated number of rows down
            // both in the current LiveSheet itself as well as in all the LiveRanges related with it.
            this.PrepareSheetAndRangesForRowsAddition(toBeAddedRowsBottomIndex, toBeAddedRowsCount, futureSheetBottomIndex);

            // Adjust the row index property of all the LiveCells placed in the LiveRow currently indexed
            // on top of the spaces designated for the rows to be added, or below(greater value) them.
            this.PrepareCellsForRowsAddition(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, futureSheetBottomIndex);

            // Creates and assigns required LiveRows with cells pre-filled with provided data.
            this.PrepareRowsForRowsAddition(toBeAddedRowsTopIndex, toBeAddedRowsBottomIndex, newRowsData, toBeAddedRowsCount);

            // Increase the value of the current LiveSheet row count.
            this.RowCount += toBeAddedRowsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        #region AddRows - Private Fields
        // Edit sheet and all ranges, shift the content in each ranges 
        // To make enough space for new rows.

        // Destroys all the LiveRow(without destroying LiveCells contained by it) not in use by the LiveSheet itself
        // which have to be transfered outside they current LiveRange to make a space for new rows. Destroy such a row
        // as long as it has been confirmed that it will not be used by any other LiveRange after the rows transfer will get finalized.
        private void DestroyRowsForRowsAddition(int toBeAddedRowsCount)
        {
            // Get all the ranges related with the current LiveSheet, but not LiveSheet itself.
            IList<LiveRange> ranges = this.Ranges;

            // Loop through each of the ranges related with the current LiveSheet (apart of LiveSheet itself).
            foreach (LiveRange range in ranges)
            {
                // ... if the currently looped through LiveRange range is not using the same LiveRows as the current LiveSheet itself...
                if (range.LeftIndex != this.LeftIndex || range.RightIndex != this.RightIndex)
                {

                    // ... get all the ranges using the same LiveRow instances as the currently looped through LiveRange...
                    IList<LiveRange> rangesWithOverlapingRows = ranges.Where((r) =>
                    {
                        // If index of the most left column of the LiveRange r is equal
                        // with index of the most left column of the currently looped through LiveRange...
                        return (range.LeftIndex == r.LeftIndex)
                        // ... and index of the most right column of the LiveRange r is equal
                        // with index of the most right column of the currently looped through LiveRange...
                        && (range.RightIndex == r.RightIndex)
                        // ... and LiveRange r top index is above, or equal with
                        // the bottom index of the currently looped through LiveRange...
                        && ((range.BottomIndex >= r.TopIndex)
                        // ... and LiveRange r bottom index is below, or equal with
                        // the top index of the currently looped through LiveRange...
                        || (range.TopIndex <= r.BottomIndex));
                        // include the currently looped through LiveRange range in the returned enumeration as
                        // part of it is overlapping with the current LiveRange.
                    }).ToList();

                    // ...remove currently looped through LiveRange range to avoid comparing it below with itself.
                    rangesWithOverlapingRows.Remove(range);

                    // Loop through all the rows of the currently looped through range
                    // which will end up outside of its borders after the rows withing they ranges get finalized.
                    for (int i = range.RowCount - 1; i < range.RowCount - 1 - toBeAddedRowsCount; i--)
                    {
                        // Get the range which will have to be transfered beyond the borders
                        // of the currently looped through range and as such it potentially require to be destroyed.
                        LiveRow rowPotentiallyRequiringDestruction = range.Rows[i];

                        // Calculate currently looped through sheet relative row index.
                        int futureSheetRelativeRowIndex = rowPotentiallyRequiringDestruction.RowIndex + toBeAddedRowsCount;

                        // Check whether any other range with overlapping rows will be using
                        // the currently looped through row potentially requiring destruction, 
                        // after the rows will be transfered by comparing...
                        bool willStayInUse = rangesWithOverlapingRows.Any((r) =>
                        {
                            // .. the sheet relative row index of the LiveRow potentially requiring destruction
                            //  with the range r top and bottom index...
                            return (futureSheetRelativeRowIndex >= r.TopIndex
                            && futureSheetRelativeRowIndex <= r.BottomIndex);
                            // .. and return true if at least one of overlapping ranges will use
                            // the row potentially requiring destruction after existing rows 
                            // will get transfered within they ranges.
                        });

                        // If the row potentially requiring  destruction have been found
                        // to not be in use by any other range 
                        if (!willStayInUse)
                            rowPotentiallyRequiringDestruction.Destroy();
                    }
                }
            }

        }

        // Appends current LiveSheet rows list with the sufficient amount of empties spaces
        // to fit all the rows to be added, then transfers the rows existing on top of
        // the spaces designated for the rows to be added appropriated number of rows down
        // both in the current LiveSheet itself as well as in all the LiveRanges related with it.
        private void PrepareSheetAndRangesForRowsAddition(int toBeAddedRowsBottomIndex, int toBeAddedRowsCount, int futureSheetBottomIndex)
        {
            // Add required amount of row entries to the current LiveSheet rows list.
            for (int i = 0; i < toBeAddedRowsCount; i++)
                this.Rows.Add(null);

            // Loop through each of the current LiveSheet rows starting from the last, most bottom one,
            // and transfer them down required number of spaces to make a space for the new row.
            // End looping through on the index greater then the index of the last, most bottom row to be added.
            for (int i = futureSheetBottomIndex; i > toBeAddedRowsBottomIndex; i--)
                this.Rows[i] = this.Rows[i - toBeAddedRowsCount];

            // For each of the LiveRange related with the current LiveSheet
            // but not the LiveSheet itself...
            foreach (LiveRange range in this.Ranges)
            {
                // ... calculate range relative index of the last, most bottom index of the row to be added...
                int toBeAddedRowsRangeRelativeBottomIndex = toBeAddedRowsBottomIndex - range.TopIndex;

                // Loop through each of the rows of the range starting from the last, most bottom one,
                // and transfer them down required number of spaces to make a space for the new row.
                // End looping through on the index greater then the index of the last, most bottom row to be added.

                // Starting from the bottom edge of the range loop through each of its rows indexed
                // below (greater value) the range relative index of the last, most bottom row to be added. 
                for (int i = range.RowCount - 1; i > toBeAddedRowsRangeRelativeBottomIndex; i--)
                    // If retentively to the range, index of the current row to be transfered is above (lower value) range top edge...
                    if (i - toBeAddedRowsCount < 0)
                        // ... break out from for loop as each lower row index will also be above range top edge.
                        break;
                    // Otherwise if confirmed that the range to be transfered index is within the range scope
                    else
                        // ... move the row onto its new position.
                        range.Rows[i] = range.Rows[i - toBeAddedRowsCount];
            }
        }

        // Adjust the row index property of all the LiveCells placed in the LiveRow currently indexed
        // on top of the spaces designated for the rows to be added, or below(greater value) them.
        private void PrepareCellsForRowsAddition(int toBeAddedRowsTopIndex, int toBeAddedRowsBottomIndex, int futureSheetBottomIndex)
        {
            // Side-note this should be executed when the current LiveSheet rows 
            // have been already transfered appropriate number of places in the list down(greater value).

            // Loop through each of the current LiveSheet rows located below (greater value)
            // then row index of the last, most bottom row to be added....
            for (int i = toBeAddedRowsBottomIndex + 1; i <= futureSheetBottomIndex; i++)
                // ... loop through all the cells on the currently looped through row index...
                foreach (LiveCell cell in this.Rows[i].Cells)
                    // ... and adjust the cell row index increasing it by the number of
                    cell.RowIndex += toBeAddedRowsBottomIndex;
        }

        // Creates and assigns required LiveRows with empty cells.
        private void PrepareRowsForRowsAddition(int toBeAddedRowsTopIndex, int toBeAddedRowsBottomIndex, int toBeAddedRowsCount)
        {
            // Get all the rows effected with changes.
            IList<LiveRow> rowsEffected = LiveRow.Factory.GetSheetAllRangesRowsBelowIndex(this.SpreadsheetId, this.SheetTitleId, toBeAddedRowsTopIndex);

            // Loop through all the obtained rows...
            foreach (LiveRow row in rowsEffected)
            {
                // ... adjust the row index...
                row.RowIndex += toBeAddedRowsCount;

                // ... adjust its storage position in the LiveRow factory production index.
                LiveRow.Factory.ReIndexStoredRow(row, toBeAddedRowsCount);
            }
            
            // Loop through all the row indexes to be added...
            for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
                // ... build and assign required instance of LiveRow filled with new empty LiveCells.
                this.Rows[i] = LiveRow.Factory.Get(this.SpreadsheetId, this.SheetTitleId, this.LeftIndex, i, this.ColumnCount);

            // For each of the LiveRange related with the current LiveSheet
            // but not the LiveSheet itself...
            foreach (LiveRange range in this.Ranges)
                // ... loop through all the row indexes to be added....
                for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
                {
                    // Calculate the range relative index of the row to be added...
                    int rangeRelativeRowIndex = i - range.TopIndex;

                    // ... if the row to be added is indexed below the bottom range edge
                    if (rangeRelativeRowIndex > range.BottomIndex)
                        // ... break out from the loop.
                        break;

                    // ... otherwise if the row to be added is on, or above the top edge
                    // of the range...
                    else if (rangeRelativeRowIndex >= 0)
                        // ... obtain or build and assign required instance of LiveRow filled with existing LiveCells.
                        range.Rows[rangeRelativeRowIndex] = LiveRow.Factory.Get(range.SpreadsheetId, range.SheetTitleId, range.LeftIndex, i, range.ColumnCount);
                }
        }

        // Creates and assigns required LiveRows with cells pre-filled with provided data.
        private void PrepareRowsForRowsAddition<Row, Cell>(int toBeAddedRowsTopIndex, int toBeAddedRowsBottomIndex, IList<Row> newRowsData, int toBeAddedRowsCount)
            where Row : class, IList<Cell> where Cell : class
        {
            // Get all the rows effected with changes.
            IList<LiveRow> rowsEffected = LiveRow.Factory.GetSheetAllRangesRowsBelowIndex(this.SpreadsheetId, this.SheetTitleId, toBeAddedRowsTopIndex);

            // Loop through all the obtained rows...
            foreach (LiveRow row in rowsEffected)
            {
                // ... adjust the row index...
                row.RowIndex += toBeAddedRowsCount;

                // ... adjust its storage position in the LiveRow factory production index.
                LiveRow.Factory.ReIndexStoredRow(row, toBeAddedRowsCount);
            }

            // Loop through all the row indexes to be added...
            for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
            {
                // Get data relative row index.
                int dataRelativeRowIndex = i - toBeAddedRowsTopIndex;

                // Obtain appropriate data if any available.
                Row newRowData = newRowsData?.ElementAtOrDefault(dataRelativeRowIndex);

                // ... build and assign required instance of LiveRow filled with new data pre-filled LiveCells.
                this.Rows[i] = LiveRow.Factory.Get<Cell>(this.SpreadsheetId, this.SheetTitleId, this.LeftIndex, i, this.ColumnCount, newRowData);
            }

            // For each of the LiveRange related with the current LiveSheet
            // but not the LiveSheet itself...
            foreach (LiveRange range in this.Ranges)
                // ... loop through all the row indexes to be added....
                for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
                {
                    // Calculate the range relative index of the row to be added...
                    int rangeRelativeRowIndex = i - range.TopIndex;

                    // ... if the row to be added is indexed below the bottom range edge
                    if (rangeRelativeRowIndex > range.BottomIndex)
                        // ... break out from the loop.
                        break;

                    // ... otherwise if the row to be added is on, or above the top edge
                    // of the range...
                    else if (rangeRelativeRowIndex >= 0)
                        // ... obtain or build and assign required instance of LiveRow filled with existing LiveCells.
                        range.Rows[rangeRelativeRowIndex] = LiveRow.Factory.Get(range.SpreadsheetId, range.SheetTitleId, range.LeftIndex, i, range.ColumnCount);
                }
        }

        // Creates and assigns required LiveRows with cells pre-filled with provided data.
        private void PrepareRowsForRowsAddition(int toBeAddedRowsTopIndex, int toBeAddedRowsBottomIndex, IList<RowData> newRowsData, int toBeAddedRowsCount)
        {
            // Get all the rows effected with changes.
            IList<LiveRow> rowsEffected = LiveRow.Factory.GetSheetAllRangesRowsBelowIndex(this.SpreadsheetId, this.SheetTitleId, toBeAddedRowsTopIndex);

            // Loop through all the obtained rows...
            foreach (LiveRow row in rowsEffected)
            {
                // ... adjust the row index...
                row.RowIndex += toBeAddedRowsCount;

                // ... adjust its storage position in the LiveRow factory production index.
                LiveRow.Factory.ReIndexStoredRow(row, toBeAddedRowsCount);
            }

            // Loop through all the row indexes to be added...
            for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
            {
                // Get data relative row index.
                int dataRelativeRowIndex = i - toBeAddedRowsTopIndex;

                // Obtain appropriate data if any available.
                RowData newRowData = newRowsData?.ElementAtOrDefault(dataRelativeRowIndex);

                // ... build and assign required instance of LiveRow filled with new data pre-filled LiveCells.
                this.Rows[i] = LiveRow.Factory.Get(this.SpreadsheetId, this.SheetTitleId, this.LeftIndex, i, this.ColumnCount, newRowData);
            }

            // For each of the LiveRange related with the current LiveSheet
            // but not the LiveSheet itself...
            foreach (LiveRange range in this.Ranges)
                // ... loop through all the row indexes to be added....
                for (int i = toBeAddedRowsTopIndex; i <= toBeAddedRowsBottomIndex; i++)
                {
                    // Calculate the range relative index of the row to be added...
                    int rangeRelativeRowIndex = i - range.TopIndex;

                    // ... if the row to be added is indexed below the bottom range edge
                    if (rangeRelativeRowIndex > range.BottomIndex)
                        // ... break out from the loop.
                        break;

                    // ... otherwise if the row to be added is on, or above the top edge
                    // of the range...
                    else if (rangeRelativeRowIndex >= 0)
                        // ... obtain or build and assign required instance of LiveRow filled with existing LiveCells.
                        range.Rows[rangeRelativeRowIndex] = LiveRow.Factory.Get(range.SpreadsheetId, range.SheetTitleId, range.LeftIndex, i, range.ColumnCount);
                }
        }
        #endregion
        #endregion

        // BE AWARE - Private helpers of this specific method braking SRP multiple times.
        // It is purposely implemented performance optimized design trying to keep
        // looping through the same collections as rare as possible and still do the work.
        #region RemoveRows

        // Removes requested number of rows starting at the provided sheet relative toBeRemovedRowsTopIndex. 
        private void RemoveRows(int toBeRemovedRowsTopIndex, int toBeRemovedRowsCount)
        {
            // Check provided parameters validity and throw appropriate exception if unable to proceed with provided values.
            if (toBeRemovedRowsTopIndex > this.BottomIndex)
                throw new ArgumentException($"Unable to remove the requested number of rows starting at the provided index: {toBeRemovedRowsTopIndex}. Provided index is outside of the scope of the current LiveSheet instance which has only {this.RowCount} rows.");
            else if (this.BottomIndex + 1 - toBeRemovedRowsTopIndex - toBeRemovedRowsCount < 0)
                throw new ArgumentException($"Unable to remove {toBeRemovedRowsCount} rows starting at the provided index: {toBeRemovedRowsTopIndex} as this sheet: {this.SheetTitleId} does not have enough rows when started counting from that index.");
            else if (toBeRemovedRowsCount < 0)
                throw new ArgumentException("Provided toBeRemovedRowsCount cannot be lower then 0.");
            else if (this.RowCount - toBeRemovedRowsCount < 1)
                throw new ArgumentException($"Unable to remove {toBeRemovedRowsCount} columns as this sheet: {this.SheetTitleId} would end up with less then one column, and this is not allowed.");
            // If requested to add 0 row, simply do nothing and return.
            else if (toBeRemovedRowsCount == 0)
                return;
            
            // Store the value of the current LiveSheet range before any changes occurred.
            string preChangesSheetRangeString = this.Range;
            
            // Calculate the sheet relative index of the last row to be removed
            int toBeRemovedRowsBottomIndex = toBeRemovedRowsTopIndex + toBeRemovedRowsCount - 1;

            // Calculate the future bottom row the current LiveSheet, after rows removal.
            int futureSheetBottomIndex = this.BottomIndex - toBeRemovedRowsCount;

            // Side-note: order matters 

            // Assures that any LiveRange covering the area which will stop existing, 
            // or covering exactly the same area LiveSheet will cover after
            // rows removal will get finalized will get destroyed, will get destroyed together with
            // LiveRow contained by it.
            this.DestroyRangesRowsAndCellsForRowsRemoval(toBeRemovedRowsTopIndex, toBeRemovedRowsBottomIndex, futureSheetBottomIndex);

            // Transfers all the LiveRows in use by current LiveSheet itself or any of the relate LiveRange which are located above the removed rows.
            // Move these rows appropriate number (number of removed rows) spaces up(lower index) in the sheet Rows list(if they belonging to the LiveSheet itself),
            // adjusting their row index property, as well as row index property of all the LiveCell instances
            // included contained by these LiveRows, and remove the surplus entries from the current LiveSheet rows list.
            this.PrepareRowsAndCellsForRowsRemoval(toBeRemovedRowsTopIndex, toBeRemovedRowsBottomIndex, toBeRemovedRowsCount, futureSheetBottomIndex);
            
            // Replaces all the LiveRows indexed on, or below the index of the first, most top removed row, and replacing them with appropriate ones.
            this.PrepareRangesForRowsRemoval(toBeRemovedRowsTopIndex, toBeRemovedRowsCount, futureSheetBottomIndex);
            
            // Decrease current LiveSheet row count.
            this.RowCount -= toBeRemovedRowsCount;

            // Re-Index the LiveSheet representing range in the LiveRange factory production index.
            LiveRange.Factory.ReIndexRange(this.SpreadsheetId, this.SheetTitleId, preChangesSheetRangeString);
        }

        #region RemoveRows - Private Helpers
        // Destroys the LiveRange class instances covering - at least partially - the area of
        // the current LiveSheet which will stop existing after rows removal, as well as all 
        // the rows placed on the row indexes between first, most left and last, most right
        // row index together with they cells.
        private void DestroyRangesRowsAndCellsForRowsRemoval(int toBeRemovedRowsTopIndex, int toBeRemovedRowsBottomIndex, int futureSheetBottomIndex)
        {
            // For each of the ranges related with the current LiveSheet...
            foreach (LiveRange range in this.Ranges)
                // ... if the range covers - at least partially -
                // the area which will stop existing once 
                // the requested rows get removed... 
                if (range.BottomIndex > futureSheetBottomIndex)
                    // ... destroy the range.
                    range.Destroy(false);
                else if
                    // ...  else if the range covers exactly the area the current LiveSheet
                    // itself will cover when then rows will get removed...
                    (range.LeftIndex == this.LeftIndex
                    && range.TopIndex == this.TopIndex
                    && range.RightIndex == this.RightIndex
                    && range.BottomIndex == futureSheetBottomIndex)
                {
                    // .. mark range as replaced ...
                    range.Replaced = true;

                    // ... destroy the range.
                    range.Destroy(false);
                }
            
            // Loop through all the rows in use by the current LiveSheet, and designated for removal...
            for (int i = toBeRemovedRowsTopIndex; i <= toBeRemovedRowsBottomIndex; i++)
            {
                // ... loop through all the cells in the row and destroy them...
                foreach (LiveCell cell in this.Rows[i].Cells)
                    cell.Destroy();

                // .. then destroy the row itself.
                this.Rows[i].Destroy();
            }

            // Loop through all the rows not in use by the current LiveSheet, and designated for removal... 
            for (int i = toBeRemovedRowsTopIndex; i <= toBeRemovedRowsBottomIndex; i++)
            {
                // Get all the rows located on the currently looped through row index from all the ranges related with the current LiveSheet.
                IList<LiveRow> toBeDestroyedRows = LiveRow.Factory.GetSheetAllRangesRows(this.SpreadsheetId, this.SheetTitleId, i);

                foreach (LiveRow rowToBeDestroyed in toBeDestroyedRows)
                    // Destroy the row.
                    this.Rows[i].Destroy();
            }
        }

        // Transfers all the LiveRows in use by current LiveSheet itself or any of the relate LiveRange which are located above the removed rows.
        // Move these rows appropriate number (number of removed rows) spaces up(lower index) in the sheet Rows list(if they belonging to the LiveSheet itself),
        // adjusting their row index property, as well as row index property of all the LiveCell instances
        // included contained by these LiveRows, and remove the surplus entries from the current LiveSheet rows list.
        private void PrepareRowsAndCellsForRowsRemoval(int toBeRemovedRowsTopIndex, int toBeRemovedRowsBottomIndex, int toBeRemovedRowsCount, int futureSheetBottomIndex)
        {
            // Get all the rows used by the current LiveSheet, or any of the LiveRange related with 
            // the current LiveSheet, as long as the LiveRow row index is greater the row index of
            // of the last, most bottom removed row.
            IList<LiveRow> rowsAboveRemovedRows = LiveRow.Factory.GetSheetAllRangesRows(this.SpreadsheetId, this.SheetTitleId)
                .Where((row) => { return row.RowIndex > toBeRemovedRowsBottomIndex; }).ToList();

            // Loop through row indexes on the current LiveSheet, starting on 
            // the index of the first, most top row to be removed, and ending on 
            // the bottom index of the current LiveSheet when rows removal get finalized.
            for (int i = toBeRemovedRowsTopIndex; i <= futureSheetBottomIndex; i++)
            {
                // Override the row indexed to the currently looped through row index value
                // with the one located appropriate number of places lower.
                this.Rows[i] = this.Rows[i + toBeRemovedRowsCount];

                // Adjust LiveRow.RowIndex by appropriate value.
                this.Rows[i].RowIndex -= toBeRemovedRowsCount;

                // Move the current LiveRow in the LiveRow factory production index 
                // according the provided rowIndexChangeByValue 
                LiveRow.Factory.ReIndexStoredRow(this.Rows[i], -(toBeRemovedRowsCount));

                // For each cell belonging to the currently looped through LiveRow...
                foreach (LiveCell cell in this.Rows[i].Cells)
                    // ... adjust the cell row index using currently looped through LiveRow.RowIndex.
                    cell.RowIndex = this.Rows[i].RowIndex;

                // Remove the adjusted row from the rowsAboveRemovedRows list to avoid
                // it having it being transfered again.
                rowsAboveRemovedRows.Remove(this.Rows[i]);
            }

            // Remove unnecessary entries in the current LiveSheet rows list.
            for (int i = this.BottomIndex; i > futureSheetBottomIndex; i--)
                this.Rows.RemoveAt(i);

            // Adjust the row index of all of the
            foreach (LiveRow row in rowsAboveRemovedRows)
            {
                // Adjust LiveRow.RowIndex by appropriate value.
                row.RowIndex -= toBeRemovedRowsCount;

                // Move the current LiveRow in the LiveRow factory production index 
                // according the provided rowIndexChangeByValue 
                LiveRow.Factory.ReIndexStoredRow(row, -(toBeRemovedRowsCount));
            }
        }

        // Replaces all the LiveRows indexed on, or below the index of the first, most top removed row, and replacing them with appropriate ones.
        private void PrepareRangesForRowsRemoval(int toBeRemovedRowsTopIndex, int toBeRemovedRowsCount, int futureSheetBottomIndex)
        {
            // For each of the ranges related with the current LiveSheet, but not LiveSheet itself...
            foreach (LiveRange range in this.Ranges)
            {
                // ... loop through all the range row indexes...
                for (int i = toBeRemovedRowsTopIndex; i <= range.BottomIndex; i++)
                {
                    // ... and override the row indexed to the currently looped through row index value
                    // with the obtained from the factory. (In edge case scenario creation of new row will be required)
                    range.Rows[i - range.TopIndex] = LiveRow.Factory.Get(this.SpreadsheetId, this.SheetTitleId, range.LeftIndex, i, range.ColumnCount);
                }
            }
        }
        #endregion
        #endregion
        #endregion
    }
}
