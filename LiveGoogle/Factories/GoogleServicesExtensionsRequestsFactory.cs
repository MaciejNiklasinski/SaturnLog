using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Google.Apis.Requests;
using LiveGoogle.Sheets;
using LiveGoogle.Session;

namespace LiveGoogle.Factories
{
    internal static class GoogleServicesExtensionsRequestsFactory
    {
        #region Constants
        private const int LiveSheetLeftIndex = 0;
        private const int LiveSheetTopIndex = 0;
        #endregion

        #region Spreadsheet
        #region Spreadsheet - Create
        // Returns create spreadsheet request - from dateless spreadsheet blueprint
        internal static SpreadsheetsResource.CreateRequest GetAddSpreadsheetRequest(SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int>> spreadsheetBlueprint)
        {
            // Create new spreadsheet instance.
            Spreadsheet spreadsheet = new Spreadsheet();

            // Create new spreadsheet properties object and assign it
            // to newly created spreadsheet.
            spreadsheet.Properties = new SpreadsheetProperties();

            // Assign requested title to the newly created spreadsheet instance. 
            spreadsheet.Properties.Title = spreadsheetTitle;

            // Create new default text and number format.
            spreadsheet.Properties.DefaultFormat = new CellFormat()
            {
                TextFormat = new TextFormat(),
                NumberFormat = new NumberFormat()
            };

            // Assign new list of sheets into the spreadsheet..
            spreadsheet.Sheets = new List<Sheet>();
            // .. and fill it with appropriate Sheet instances.
            foreach (Tuple<string, int, int> sheetSizeBlueprint in spreadsheetBlueprint)
            {
                // Create new sheet instance.
                Sheet sheet = new Sheet();

                // Create new sheet properties object and assign it
                // to newly created sheet.
                sheet.Properties = new SheetProperties();

                // Assign newly created sheet title.
                sheet.Properties.Title = sheetSizeBlueprint.Item1;

                // Create new sheet GridProperties instance.
                sheet.Properties.GridProperties = new GridProperties();

                // Assign appropriate number of columns and rows.
                sheet.Properties.GridProperties.ColumnCount = sheetSizeBlueprint.Item2;
                sheet.Properties.GridProperties.RowCount = sheetSizeBlueprint.Item3;

                // Add newly created, and adjusted sheet into the spreadsheet sheets list.
                spreadsheet.Sheets.Add(sheet);
            }

            // Return request created based on the provided spreadsheet instance.
            return sheetsService.Spreadsheets.Create(spreadsheet);
        }

        // Returns create spreadsheet request - from date containing spreadsheet blueprint
        internal static SpreadsheetsResource.CreateRequest GetAddSpreadsheetRequest(SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint)
        {
            // Create new spreadsheet instance.
            Spreadsheet spreadsheet = new Spreadsheet();

            // Create new spreadsheet properties object and assign it
            // to newly created spreadsheet.
            spreadsheet.Properties = new SpreadsheetProperties();

            // Assign requested title to the newly created spreadsheet instance. 
            spreadsheet.Properties.Title = spreadsheetTitle;

            // Create new default text and number format.
            spreadsheet.Properties.DefaultFormat = new CellFormat()
            {
                TextFormat = new TextFormat(),
                NumberFormat = new NumberFormat()
            };

            // Assign new list of sheets into the spreadsheet..
            spreadsheet.Sheets = new List<Sheet>();
            // .. and fill it with appropriate Sheet instances.
            foreach (Tuple<string, int, int, IList<IList<string>>> sheetSizeBlueprint in spreadsheetBlueprint)
            {
                // Create new sheet instance.
                Sheet sheet = new Sheet();

                // Create new sheet properties object and assign it
                // to newly created sheet.
                sheet.Properties = new SheetProperties();

                // Assign newly created sheet title.
                sheet.Properties.Title = sheetSizeBlueprint.Item1;

                // Create new sheet GridProperties instance.
                sheet.Properties.GridProperties = new GridProperties();

                // Assign appropriate number of columns and rows.
                sheet.Properties.GridProperties.ColumnCount = sheetSizeBlueprint.Item2;
                sheet.Properties.GridProperties.RowCount = sheetSizeBlueprint.Item3;

                // Get blueprint sheet data.
                IList<IList<string>> sheetData = sheetSizeBlueprint.Item4;

                // If blueprint sheet data has been provided for this specific sheet,
                // fill newly created empty spreadsheet with it.
                if (!(sheetData is null))
                {
                    // Create sheet, grid data placeholder.
                    sheet.Data = new List<GridData>();

                    // Construct spreadsheet row data based on the provided sheet blueprint data.
                    IList<RowData> rowsData = sheetData.Select<IList<string>, RowData>((rowData) =>
                    {
                        // Get new row data instance..
                        RowData row = new RowData();

                        // .. fill it with cell data values
                        row.Values = rowData.Select<string, CellData>((cellData) => { return new CellData() { FormattedValue = cellData, EffectiveValue = new ExtendedValue { StringValue = cellData }, UserEnteredValue = new ExtendedValue { StringValue = cellData } }; }).ToList();

                        // return the row
                        return row;
                    }).ToList();

                    // Create Grid data 
                    GridData gridData = new GridData
                    {
                        StartColumn = 0,
                        StartRow = 0,
                        RowData = rowsData
                    };

                    // Assign created grid data into the designed sheet.
                    sheet.Data.Add(gridData);
                }

                // Add newly created, and adjusted sheet into the spreadsheet sheets list.
                spreadsheet.Sheets.Add(sheet);
            }

            // Return request created based on the provided spreadsheet instance.
            return sheetsService.Spreadsheets.Create(spreadsheet);
        }
        #endregion

        #region Spreadsheet - Read
        // Return get spreadsheet shell request.
        internal static SpreadsheetsResource.GetRequest GetGetSpreadsheetShellRequest(SheetsService sheetsService, string spreadsheetId)
        {
            // Construct new get spreadsheet request associated with provided spreadsheet id.
            SpreadsheetsResource.GetRequest request = sheetsService.Spreadsheets.Get(spreadsheetId);

            // Set IncludeGridData to false, to assure that the spreadsheet data will NOT be included.
            request.IncludeGridData = false;

            // Return constructed spreadsheet.
            return request;
        }

        // Return get spreadsheet request.
        internal static SpreadsheetsResource.GetRequest GetGetSpreadsheetRequest(SheetsService sheetsService, string spreadsheetId)
        {
            // Construct new get spreadsheet request associated with provided spreadsheet id.
            SpreadsheetsResource.GetRequest request = sheetsService.Spreadsheets.Get(spreadsheetId);

            // Set IncludeGridData to true, to assure that the spreadsheet data will be included.
            request.IncludeGridData = true;

            // Return constructed spreadsheet.
            return request;
        }

        // Return get spreadsheet listed sheets only request.
        internal static SpreadsheetsResource.GetRequest GetGetSpreadsheetListedSheetsOnlyRequest(SheetsService sheetsService, string spreadsheetId, IList<string> sheetTitleIds)
        {
            // Get spreadsheet containing only specific ranges 
            SpreadsheetsResource.GetRequest request = sheetsService.Spreadsheets.Get(spreadsheetId);
            request.IncludeGridData = true;
            request.Ranges = new Google.Apis.Util.Repeatable<string>(sheetTitleIds);

            // Return constructed spreadsheet - only listed spreadsheet. Not listed spreadsheets hasn't been returned at all, not even  as sheet shells.
            return request;
        }
        #endregion

        #region Spreadsheet - Update
        // Returns google request which once executed makes spreadsheet view access public
        internal static BatchRequest GetAssureViewIsPublicRequest(DriveService driveService, string spreadsheetId)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating public view access.
            Permission anyonePermission = new Permission()
            {
                Type = "anyone",
                Role = "reader"
            };

            // Construct appropriate permission resources partial request to allow all public spreadsheet view.
            PermissionsResource.CreateRequest anyoneCanViewRequest = driveService.Permissions.Create(anyonePermission, spreadsheetId);
            anyoneCanViewRequest.Fields = "id";

            // Add partial permission resources anyoneCanViewRequest into the batch update queue.
            batchRequest.Queue<Permission>(anyoneCanViewRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide pubic view permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }

        // Returns google request which once executed makes spreadsheet view access allowed for specified domain
        internal static BatchRequest GetAssureViewIsDomainPermittedRequest(DriveService driveService, string spreadsheetId, string domain)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating view access for the specified domain.
            Permission domainPermission = new Permission()
            {
                Type = "domain",
                Role = "reader",
                Domain = domain
            };

            // Construct appropriate permission resources partial request to allow specified domain spreadsheet view.
            PermissionsResource.CreateRequest domainCanViewRequest = driveService.Permissions.Create(domainPermission, spreadsheetId);
            domainCanViewRequest.Fields = "id";

            // Add partial permission resources domainCanViewRequest into the batch update queue.
            batchRequest.Queue<Permission>(domainCanViewRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide domain specific view permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }

        // Returns google request which once executed makes spreadsheet view access allowed for specified email address
        internal static BatchRequest GetAssureViewIsEmailPermittedRequest(DriveService driveService, string spreadsheetId, string email)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating specified email address view access.
            Permission emailAddressPermission = new Permission()
            {
                Type = "user",
                Role = "reader",
                EmailAddress = email
            };

            // Construct appropriate permission resources partial request to allow specified email address spreadsheet view.
            PermissionsResource.CreateRequest emailAddressCanViewRequest = driveService.Permissions.Create(emailAddressPermission, spreadsheetId);
            emailAddressCanViewRequest.Fields = "id";

            // Add partial permission resources emailAddressCanViewRequest into the batch update que.
            batchRequest.Queue<Permission>(emailAddressCanViewRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide email address specific view permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }

        // Returns google request which once executed makes spreadsheet edit access public
        internal static BatchRequest GetAssureEditIsPublicRequest(DriveService driveService, string spreadsheetId)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating public edit access.
            Permission anyonePermission = new Permission()
            {
                Type = "anyone",
                Role = "writer"
            };

            // Construct appropriate permission resources partial request to allow all public spreadsheet edit access.
            PermissionsResource.CreateRequest anyoneCanEditRequest = driveService.Permissions.Create(anyonePermission, spreadsheetId);
            anyoneCanEditRequest.Fields = "id";

            // Add partial permission resources anyoneCanEditRequest into the batch update queue.
            batchRequest.Queue<Permission>(anyoneCanEditRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide pubic edit permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }

        // Returns google request which once executed makes spreadsheet edit access allowed for specified domain
        internal static BatchRequest GetAssureEditIsDomainPermittedRequest(DriveService driveService, string spreadsheetId, string domain)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating edit access for the specified domain.
            Permission domainPermission = new Permission()
            {
                Type = "domain",
                Role = "writer",
                Domain = domain
            };

            // Construct appropriate permission resources partial request to allow specified domain spreadsheet edit access.
            PermissionsResource.CreateRequest domainCanEditRequest = driveService.Permissions.Create(domainPermission, spreadsheetId);
            domainCanEditRequest.Fields = "id";

            // Add partial permission resources domainCanEditRequest into the batch update queue.
            batchRequest.Queue<Permission>(domainCanEditRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide domain specific edit permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }

        // Returns google request which once executed makes spreadsheet edit access allowed for specified email address
        internal static BatchRequest GetAssureEditIsEmailPermittedRequest(DriveService driveService, string spreadsheetId, string email)
        {
            // Construct new instance of batch request
            BatchRequest batchRequest = new BatchRequest(driveService);

            // Build instance of permission class, indicating specified email address edit access.
            Permission emailAddressPermission = new Permission()
            {
                Type = "user",
                Role = "writer",
                EmailAddress = email
            };

            // Construct appropriate permission resources partial request to allow specified email address spreadsheet edit access.
            PermissionsResource.CreateRequest emailAddressCanEditRequest = driveService.Permissions.Create(emailAddressPermission, spreadsheetId);
            emailAddressCanEditRequest.Fields = "id";

            // Add partial permission resources emailAddressCanEditRequest into the batch update queue.
            batchRequest.Queue<Permission>(emailAddressCanEditRequest, new BatchRequest.OnResponse<Permission>((permission, error, index, message) =>
            {
                // On request response... if any error found throw appropriate exception.
                if (error != null)
                    throw new InvalidOperationException($"Google drive apis is unable to provide email address specific view permission for the spreadsheet id: {spreadsheetId} Read request error message for more details: {error.Message}");
            }));

            return batchRequest;
        }
        #endregion

        #region Spreadsheet - Delete
        internal static FilesResource.DeleteRequest GetRemoveSpreadsheetRequest(DriveService driveService, string spreadsheetId)
        {
            // Constructs and returns appropriate remove spreadsheet request.
            return driveService.Files.Delete(spreadsheetId);
        }
        #endregion
        #endregion

        #region Sheet
        #region Sheet - Create
        // Add 
        internal static SpreadsheetsResource.BatchUpdateRequest GetAddSheetRequest(SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
        {
            // Create new add sheet partial request
            AddSheetRequest addSheetRequest = new AddSheetRequest();

            // Create and assign instance of SheetProperties class into the addSheetRequest.
            addSheetRequest.Properties = new SheetProperties();

            // Set sheet title id.
            addSheetRequest.Properties.Title = sheetTitleId;

            // Create new sheet grid properties, and set number of columns and rows according the provided columnCount and row count property.
            addSheetRequest.Properties.GridProperties = new GridProperties();
            addSheetRequest.Properties.GridProperties.ColumnCount = columnCount;
            addSheetRequest.Properties.GridProperties.RowCount = rowCount;

            // Create new instance of BatchUpdateSpreadsheetRequest, data of the to be returned instance of SpreadsheetsResource.BatchUpdateRequest ..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequestData = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequestData.Requests = new List<Request>();
            // .. and add sheet partial request to it.
            batchUpdateSpreadsheetRequestData.Requests.Add(new Request { AddSheet = addSheetRequest });

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequestData.ResponseIncludeGridData = false;

            // Construct and return SpreadsheetsResource.BatchUpdateRequest.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequestData, spreadsheetId);
        }
        #endregion

        #region Sheet - Read
        #endregion

        #region Sheet - Update
        #region Sheet - Append/Insert/Remove Rows
        // Appends existing sheet with specified rows.
        internal static SpreadsheetsResource.ValuesResource.AppendRequest GetAppendRowsRequest(SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, int rowsToAdd, IList<IList<object>> newRowsData)
        {
            // Construct string representing range(rows) appending the sheet
            string appendedSheetRange = RangeTranslator.GetRangeString(sheetTitleId, LiveSheetLeftIndex, LiveSheetTopIndex, columnCount, rowCount + rowsToAdd);

            // Construct requestBody value range.
            ValueRange requestBody = new ValueRange();

            // Assign request body range string.
            requestBody.Range = appendedSheetRange;

            // Assign request body values.
            requestBody.Values = newRowsData;

            // Constructs append rows request.
            SpreadsheetsResource.ValuesResource.AppendRequest request = sheetsService.Spreadsheets.Values.Append(requestBody, spreadsheetId, appendedSheetRange);

            // Specify the way in which input data should be interpreted.
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

            // Specify the way in which input data should be inserted.
            request.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest.InsertDataOptionEnum.INSERTROWS;

            // Return constructed append rows request.
            return request;
        }

        // Inserts rows into the existing spreadsheet.
        internal static SpreadsheetsResource.BatchUpdateRequest GetInsertRowsRequest(SheetsService sheetsService, string spreadsheetId, int? sheetId, int toBeInsertedTopRowIndex, int toBeInsertedRowCount)
        {
            // Build insert dimensions partial request...
            InsertDimensionRequest request = new InsertDimensionRequest();
            request.Range = new DimensionRange();
            request.Range.SheetId = sheetId ?? throw new ArgumentNullException(nameof(sheetId));
            request.Range.Dimension = "ROWS";
            request.Range.StartIndex = toBeInsertedTopRowIndex;
            request.Range.EndIndex = toBeInsertedTopRowIndex + toBeInsertedRowCount;
            if (toBeInsertedTopRowIndex > 0)
                request.InheritFromBefore = true;

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            // .. and add appropriate partial request.
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { InsertDimension = request });

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        }

        // Removes specified rows.
        internal static SpreadsheetsResource.BatchUpdateRequest GetRemoveRowsRequest(SheetsService sheetsService, string spreadsheetId, int? sheetId, int toBeRemovedTopRowIndex, int toBeRemovedRowCount)
        {
            // Build delete rows partial request...
            DeleteDimensionRequest request = new DeleteDimensionRequest();
            request.Range = new DimensionRange();
            request.Range.SheetId = sheetId ?? throw new ArgumentNullException(nameof(sheetId));
            request.Range.Dimension = "ROWS";
            request.Range.StartIndex = toBeRemovedTopRowIndex;
            request.Range.EndIndex = toBeRemovedTopRowIndex + toBeRemovedRowCount;

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            // .. and add appropriate partial request.
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { DeleteDimension = request });

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        }
        #endregion

        #region Sheet - Append/Insert/Remove Columns
        // TODO
        #endregion

        #region Adjust Columns/Rows Width/Height Dimension
        // Adjust the pixelSize columns width.
        internal static SpreadsheetsResource.BatchUpdateRequest GetAdjustColumnsWidthDimensionRequest(SheetsService sheetsService, string spreadsheetId, int? sheetId, int leftColumnIndex, int columnsToAdjustWidthCount, int columnWidthPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Create appropriate partial request of type UpdateDimensionPropertiesRequest, where columns are considered as the dimension to adjust size.
            UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustColumnsWidthDimensionPartialRequest(spreadsheetId, sheetId, leftColumnIndex, columnsToAdjustWidthCount, columnWidthPixelsCount);

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            // .. and add appropriate partial request.
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { UpdateDimensionProperties = updateDimensionPropertiesRequest });

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        }

        // Adjust the pixelSize rows height.
        internal static SpreadsheetsResource.BatchUpdateRequest GetAdjustRowsHeightDimensionRequest(SheetsService sheetsService, string spreadsheetId, int? sheetId, int topRowIndex, int rowsToAdjustHeightCount, int rowHeightPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Create appropriate partial request of type UpdateDimensionPropertiesRequest, where rows are considered as the dimension to adjust size.
            UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustRowsHeightDimensionPartialRequest(spreadsheetId, sheetId, topRowIndex, rowsToAdjustHeightCount, rowHeightPixelsCount);

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            // .. and add appropriate partial request.
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { UpdateDimensionProperties = updateDimensionPropertiesRequest });

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);
        }

        // Adjust the pixelSize width of the multiple specified columns ranges.
        internal static SpreadsheetsResource.BatchUpdateRequest GetAdjustMultipleColumnsRangesWidthDimensionsRequest(SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> columnsRangesDimensionsAdjustmentBlueprint)
        {
            // Declare partial requests list container.
            IList<Request> requests = new List<Request>();

            // Loop through all the columns ranges dimensions adjustment blueprints.
            foreach (Tuple<int?, int, int, int> columnRangeDimensionsAdjustmentBlueprint in columnsRangesDimensionsAdjustmentBlueprint)
            {
                // Obtain sheet id from currently looped through Tuple blueprint
                int? sheetId = columnRangeDimensionsAdjustmentBlueprint.Item1 ?? throw new ArgumentNullException(nameof(sheetId));

                // Obtain index of the most left column which width will be affected. 
                int leftColumnIndex = columnRangeDimensionsAdjustmentBlueprint.Item2;

                // Obtain number of columns to adjust start counting from the one specified with leftColumnIndex to the right.
                int columnsToAdjustWidthCount = columnRangeDimensionsAdjustmentBlueprint.Item3;

                // Obtain width in pixels of the columns in the range after adjustment.
                int columnWidthPixelsCount = columnRangeDimensionsAdjustmentBlueprint.Item4;

                // Get appropriate partial update columns width dimensions request ...
                UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustColumnsWidthDimensionPartialRequest(commonSpreadsheetId, sheetId, leftColumnIndex, columnsToAdjustWidthCount, columnWidthPixelsCount);

                // .. and add it into the collection contain all the partial requests.
                requests.Add(new Request { UpdateDimensionProperties = updateDimensionPropertiesRequest });
            }

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize and assign gathered collection of partial requests.
            batchUpdateSpreadsheetRequest.Requests = requests;

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, commonSpreadsheetId);
        }

        // Adjust the pixelSize width of the multiple specified rows ranges.
        internal static SpreadsheetsResource.BatchUpdateRequest GetAdjustMultipleRowsRangesHeightDimensionsRequest(SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> rowsRangesDimensionsAdjustmentBlueprint)
        {
            // Declare partial requests list container.
            IList<Request> requests = new List<Request>();

            // Loop through all the columns ranges dimensions adjustment blueprints.
            foreach (Tuple<int?, int, int, int> rowRangeDiemansionsAdjustmentBlueprint in rowsRangesDimensionsAdjustmentBlueprint)
            {
                // Obtain sheet id from currently looped through Tuple blueprint
                int? sheetId = rowRangeDiemansionsAdjustmentBlueprint.Item1 ?? throw new ArgumentNullException(nameof(sheetId));

                // Obtain index of the most top row which height will be affected. 
                int topRowIndex = rowRangeDiemansionsAdjustmentBlueprint.Item2;

                // Obtain number of rows to adjust start counting from the one specified with topRowIndex to the bottom.
                int rowsToAdjustHeightCount = rowRangeDiemansionsAdjustmentBlueprint.Item3;

                // Obtain height in pixels of the rows in the range after adjustment.
                int rowHeightPixelsCount = rowRangeDiemansionsAdjustmentBlueprint.Item4;

                // Get appropriate partial update columns width dimensions request ...
                UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustRowsHeightDimensionPartialRequest(commonSpreadsheetId, sheetId, topRowIndex, rowsToAdjustHeightCount, rowHeightPixelsCount);

                // .. and add it into the collection contain all the partial requests.
                requests.Add(new Request { UpdateDimensionProperties = updateDimensionPropertiesRequest });
            }

            // Construct new batch update request body..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize and assign gathered collection of partial requests.
            batchUpdateSpreadsheetRequest.Requests = requests;

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return appropriate batch update request based on the provided request body and spreadsheet id.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, commonSpreadsheetId);
        }

        #region Sheet - Update - Private Helpers
        // Private Helper - provide partial request for adjusting columns width dimension using BatchUpdateRequest.
        private static UpdateDimensionPropertiesRequest GetAdjustColumnsWidthDimensionPartialRequest(string spreadsheetId, int? sheetId, int leftColumnIndex, int columnsToAdjustWidthCount, int columnWidthPixelsCount)
        {
            // Create appropriate partial request of type UpdateDimensionPropertiesRequest, where columns are considered as the dimension to adjust size.
            UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = new UpdateDimensionPropertiesRequest();
            updateDimensionPropertiesRequest.Range = new DimensionRange();
            updateDimensionPropertiesRequest.Range.SheetId = sheetId ?? throw new ArgumentNullException(nameof(sheetId));
            updateDimensionPropertiesRequest.Range.Dimension = "COLUMNS";
            updateDimensionPropertiesRequest.Range.StartIndex = leftColumnIndex;
            updateDimensionPropertiesRequest.Range.EndIndex = leftColumnIndex + columnsToAdjustWidthCount;

            // Create new instance of DiemansionProperties, and set its new pixel size according the value of the provided columnWidthPixelsCount parameter.
            updateDimensionPropertiesRequest.Properties = new DimensionProperties();
            updateDimensionPropertiesRequest.Properties.PixelSize = columnWidthPixelsCount;

            // Set fields to be update to "pixelSize" string.
            updateDimensionPropertiesRequest.Fields = "pixelSize";

            // Return update columns width dimension partial request.
            return updateDimensionPropertiesRequest;
        }

        // Private Helper - provide partial request for adjusting rows height dimension using BatchUpdateRequest.
        private static UpdateDimensionPropertiesRequest GetAdjustRowsHeightDimensionPartialRequest(string spreadsheetId, int? sheetId, int topRowIndex, int rowsToAdjustHeightCount, int rowHeightPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Create appropriate partial request of type UpdateDimensionPropertiesRequest, where rows are considered as the dimension to adjust size.
            UpdateDimensionPropertiesRequest updateDimensionPropertiesRequest = new UpdateDimensionPropertiesRequest();
            updateDimensionPropertiesRequest.Range = new DimensionRange();
            updateDimensionPropertiesRequest.Range.SheetId = sheetId ?? throw new ArgumentNullException(nameof(sheetId));
            updateDimensionPropertiesRequest.Range.Dimension = "ROWS";
            updateDimensionPropertiesRequest.Range.StartIndex = topRowIndex;
            updateDimensionPropertiesRequest.Range.EndIndex = topRowIndex + rowsToAdjustHeightCount;

            // Create new instance of DiemansionProperties, and set its new pixel size according the value of the provided rowHeightPixelsCount parameter.
            updateDimensionPropertiesRequest.Properties = new DimensionProperties();
            updateDimensionPropertiesRequest.Properties.PixelSize = rowHeightPixelsCount;

            // Set fields to be update to "pixelSize" string.
            updateDimensionPropertiesRequest.Fields = "pixelSize";

            // Return update rows height dimension partial request.
            return updateDimensionPropertiesRequest;
        }
        #endregion
        #endregion
        #endregion

        #region Delete
        // Removes Sheet specified by provided spreadsheetId and non-null sheetId.
        internal static SpreadsheetsResource.BatchUpdateRequest GetRemoveSheetRequest(SheetsService sheetsService, string spreadsheetId, int? sheetId)
        {
            // Get remove existing spreadsheet, existing sheet request.
            DeleteSheetRequest deleteSheetRequest = new DeleteSheetRequest();

            // Assign sheet Id, if provided sheetId is null, obtain it based on the provided sheeTitleId. If provided sheetId is null, throw appropriate exception.
            deleteSheetRequest.SheetId = sheetId ?? throw new ArgumentNullException(nameof(sheetId));

            // Create new instance of BatchUpdateSpreadsheetRequest, data of the to be returned instance of SpreadsheetsResource.BatchUpdateRequest ..
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest();
            // .. initialize new list of partial requests ..
            batchUpdateSpreadsheetRequest.Requests = new List<Request>();
            // .. and add sheet partial request to it.
            batchUpdateSpreadsheetRequest.Requests.Add(new Request { DeleteSheet = deleteSheetRequest });

            // Set ResponseIncludeGridData flag to false to increase performance.
            batchUpdateSpreadsheetRequest.ResponseIncludeGridData = false;

            // Construct and return SpreadsheetsResource.BatchUpdateRequest.
            return sheetsService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadsheetId);

        }
        #endregion
        #endregion

        #region Range
        #region Create
        #endregion

        #region Read
        internal static SpreadsheetsResource.ValuesResource.GetRequest GetGetRangeDataRequest(SheetsService sheetsService, string spreadsheetId, string rangeString)
        {
            // Construct and return appropriate get range data request
            return sheetsService.Spreadsheets.Values.Get(spreadsheetId, rangeString);
        }
        #endregion

        #region Update
        // Updates data in a specified range.
        internal static SpreadsheetsResource.ValuesResource.BatchUpdateRequest GetUpdateRangeDataRequest(SheetsService sheetsService, string spreadsheetId, string rangeString, IList<IList<object>> rangeData)
        {
            // Declare update data list.
            List<ValueRange> data = new List<ValueRange>();

            // Construct new instance of ValueRange.
            ValueRange valueRange = new ValueRange();

            // Assign value range locations/dimensions coordinates.
            valueRange.Range = rangeString;

            // Assign rangeData to update.
            valueRange.Values = rangeData;

            // Add value range to data list.
            data.Add(valueRange);
            
            // Build google apis request body.
            BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest();

            // Set request body value input option - "RAW" or "USER_ENTERED"
            requestBody.ValueInputOption = "RAW";

            // Assign data into the request body.
            requestBody.Data = data;

            // Construct and return google apis request based on the constructed request body for the spreadsheet specified with the provided spreadsheet id.
            return sheetsService.Spreadsheets.Values.BatchUpdate(requestBody, spreadsheetId);
        }
        #endregion

        #region Delete
        #endregion
        #endregion

        #region Row
        #region Create
        #endregion

        #region Read
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
        #endregion

        #region Column
        #region Create
        #endregion

        #region Read
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
        #endregion


        #region Cell
        #region Create
        #endregion

        #region Read
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
        #endregion
    }
}
