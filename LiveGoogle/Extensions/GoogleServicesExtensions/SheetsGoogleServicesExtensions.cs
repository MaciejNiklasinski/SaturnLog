using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveGoogle.Session;
using LiveGoogle.Sheets;
using LiveGoogle.Factories;

namespace LiveGoogle.Extensions
{
    // LiveSheet specific google sheets service extensions class.
    internal static class SheetsGoogleServicesExtensions
    {
        #region Create
        // Add new sheet into the existing spreadsheet specified with provided spreadsheetId.
        internal static void AddSheetSync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate add sheet BatchUpdateRequest.
            SpreadsheetsResource.BatchUpdateRequest addSheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSheetRequest(sheetsService, spreadsheetId, sheetTitleId, columnCount, rowCount);

            // Execute add sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(addSheetRequest);
        }

        // Add new sheet into the existing spreadsheet specified with provided spreadsheetId.
        internal static async Task AddSheetAsync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate add sheet BatchUpdateRequest.
            SpreadsheetsResource.BatchUpdateRequest addSheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSheetRequest(sheetsService, spreadsheetId, sheetTitleId, columnCount, rowCount);

            // Execute add sheet request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(addSheetRequest);
        }
        #endregion

        #region Read
        // Retrieves sheet int? Id based on the provided spreadsheetId and sheetTitleId.
        internal static int? GetSheetIdFromSheetTitleIdIdSync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain get spreadsheet shell request.
            SpreadsheetsResource.GetRequest request = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Get spreadsheet shell data in safe synchronous way.
            Spreadsheet spreadsheetShellData = RequestsExecutor.SafeExecuteSync<Spreadsheet>(request);

            // Loop through all the sheet shells and return the int? sheetId of the one with Title equal to provided sheetTitleId.
            return spreadsheetShellData.Sheets.Where((sheetShell) => { return sheetShell.Properties.Title == sheetTitleId; }).First().Properties.SheetId;
        }

        // Retrieves sheet int? Id based on the provided spreadsheetId and sheetTitleId.
        internal static async Task<int?> GetSheetIdFromSheetTitleIdIdAsync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain get spreadsheet shell request.
            SpreadsheetsResource.GetRequest request = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Get spreadsheet shell data in safe synchronous way.
            Spreadsheet spreadsheetShellData = await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(request);

            // Loop through all the sheet shells and return the int? sheetId of the one with Title equal to provided sheetTitleId.
            return spreadsheetShellData.Sheets.Where((sheetShell) => { return sheetShell.Properties.Title == sheetTitleId; }).First().Properties.SheetId;
        }
        #endregion

        #region Update
        // Adjust columns range width dimensions.
        internal static void AdjustColumnsWidthDimensionSync(this SheetsService sheetsService, string spreadsheetId, int? sheetId, int leftColumnIndex, int columnsToAdjustWidthCount, int columnWidthPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();
            
            // Obtain adjust columns width dimension request.
            SpreadsheetsResource.BatchUpdateRequest adjustColumnsWidthDimensionRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustColumnsWidthDimensionRequest(sheetsService, spreadsheetId, sheetId, leftColumnIndex, columnsToAdjustWidthCount, columnWidthPixelsCount);
            
            // Execute add sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(adjustColumnsWidthDimensionRequest);
        }

        // Adjust columns range width dimensions.
        internal static async Task AdjustColumnsWidthDimensionAsync(this SheetsService sheetsService, string spreadsheetId, int? sheetId, int leftColumnIndex, int columnsToAdjustWidthCount, int columnWidthPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain adjust columns width dimension request.
            SpreadsheetsResource.BatchUpdateRequest adjustColumnsWidthDimensionRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustColumnsWidthDimensionRequest(sheetsService, spreadsheetId, sheetId, leftColumnIndex, columnsToAdjustWidthCount, columnWidthPixelsCount);

            // Execute add sheet request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(adjustColumnsWidthDimensionRequest);
        }

        // Adjust rows range height dimensions.
        internal static void AdjustRowsHeightDimensionSync(this SheetsService sheetsService, string spreadsheetId, int? sheetId, int topRowIndex, int rowsToAdjustHeightCount, int rowHeightPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain adjust columns width dimension request.
            SpreadsheetsResource.BatchUpdateRequest adjustRowsHeightDimensionRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustRowsHeightDimensionRequest(sheetsService, spreadsheetId, sheetId, topRowIndex, rowsToAdjustHeightCount, rowHeightPixelsCount);

            // Execute add sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(adjustRowsHeightDimensionRequest);
        }

        // Adjust rows range height dimensions.
        internal static async Task AdjustRowsHeightDimensionAsync(this SheetsService sheetsService, string spreadsheetId, int? sheetId, int topRowIndex, int rowsToAdjustHeightCount, int rowHeightPixelsCount)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain adjust columns width dimension request.
            SpreadsheetsResource.BatchUpdateRequest adjustRowsHeightDimensionRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustRowsHeightDimensionRequest(sheetsService, spreadsheetId, sheetId, topRowIndex, rowsToAdjustHeightCount, rowHeightPixelsCount);

            // Execute add sheet request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(adjustRowsHeightDimensionRequest);
        }
        
        // Appends existing sheet with new range of rows
        internal static void AppendRowsSync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, int rowsToAdd, IList<IList<object>> newRowsData)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate append rows request.
            SpreadsheetsResource.ValuesResource.AppendRequest appendRowsRequest = GoogleServicesExtensionsRequestsFactory.GetAppendRowsRequest(sheetsService, spreadsheetId, sheetTitleId, columnCount, rowCount, rowsToAdd, newRowsData);

            // Execute append sheet rows request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<AppendValuesResponse>(appendRowsRequest);
        }

        // Appends existing sheet with new range of rows
        internal static async Task AppendRowsAsync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int columnCount, int rowCount, int rowsToAdd, IList<IList<object>> newRowsData)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate append rows request.
            SpreadsheetsResource.ValuesResource.AppendRequest appendRowsRequest = GoogleServicesExtensionsRequestsFactory.GetAppendRowsRequest(sheetsService, spreadsheetId, sheetTitleId, columnCount, rowCount, rowsToAdd, newRowsData);

            // Execute append sheet rows request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<AppendValuesResponse>(appendRowsRequest);
        }

        // Inserts rows range into the existing sheet
        internal static void InsertRowsSync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int? sheetId, int columnCount, int toBeInsertedTopRowIndex, int toBeInsertedRowCount, IList<IList<object>> newRowsData)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate insert row batch request.
            SpreadsheetsResource.BatchUpdateRequest insertRowsRequest = GoogleServicesExtensionsRequestsFactory.GetInsertRowsRequest(sheetsService, spreadsheetId, sheetId, toBeInsertedTopRowIndex, toBeInsertedRowCount);

            // Execute insert sheet rows request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(insertRowsRequest);

            // If new rows data haven't been provided
            if (!(newRowsData is null))
            {
                // Obtain new rows range string.
                string newRowsRangeString = RangeTranslator.GetRangeString(sheetTitleId, 0, toBeInsertedTopRowIndex, columnCount, toBeInsertedRowCount);

                // Fills inserted rows with provided data.
                sheetsService.UpdateRangeDataSync(spreadsheetId, newRowsRangeString, newRowsData);
            }
        }

        // Inserts rows range into the existing sheet
        internal static async Task InsertRowsAsync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int? sheetId, int columnCount, int toBeInsertedTopRowIndex, int toBeInsertedRowCount, IList<IList<object>> newRowsData)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate insert row batch request.
            SpreadsheetsResource.BatchUpdateRequest insertRowsRequest = GoogleServicesExtensionsRequestsFactory.GetInsertRowsRequest(sheetsService, spreadsheetId, sheetId, toBeInsertedTopRowIndex, toBeInsertedRowCount);

            // Execute insert sheet rows request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(insertRowsRequest);

            // If new rows data haven't been provided
            if (!(newRowsData is null))
            {
                // Obtain new rows range string.
                string newRowsRangeString = RangeTranslator.GetRangeString(sheetTitleId, 0, toBeInsertedTopRowIndex, columnCount, toBeInsertedRowCount);

                // Fills inserted rows with provided data.
                await sheetsService.UpdateRangeDataAsync(spreadsheetId, newRowsRangeString, newRowsData);
            }
        }

        // Removes specified rows range from the existing sheet.
        internal static void RemoveRowsSync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int? sheetId, int toBeRemovedTopRowIndex, int toBeRemovedRowCount)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate remove rows batch request.
            SpreadsheetsResource.BatchUpdateRequest removeRowsRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveRowsRequest(sheetsService, spreadsheetId, sheetId, toBeRemovedTopRowIndex, toBeRemovedRowCount);

            // Execute remove rows request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(removeRowsRequest);
        }

        // Removes specified rows range from the existing sheet.
        internal static async Task RemoveRowsAsync(this SheetsService sheetsService, string spreadsheetId, string sheetTitleId, int? sheetId, int toBeRemovedTopRowIndex, int toBeRemovedRowCount)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate remove rows batch request.
            SpreadsheetsResource.BatchUpdateRequest removeRowsRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveRowsRequest(sheetsService, spreadsheetId, sheetId, toBeRemovedTopRowIndex, toBeRemovedRowCount);

            // Execute remove rows request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(removeRowsRequest);
        }
        #endregion

        #region Delete
        // Remove existing, (not-last in a spreadsheet) sheet from the specified existing spreadsheet.
        internal static void RemoveSheetSync(this SheetsService sheetsService, string spreadsheetId, int? sheetId)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate remove sheet BatchUpdateRequest.
            SpreadsheetsResource.BatchUpdateRequest removeSheetRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveSheetRequest(sheetsService, spreadsheetId, sheetId);

            // Execute remove sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(removeSheetRequest);
        }

        // Remove existing, (not-last in a spreadsheet) sheet from the specified existing spreadsheet.
        internal static async Task RemoveSheetAsync(this SheetsService sheetsService, string spreadsheetId, int? sheetId)
        {
            // If provided sheetId is null, throw appropriate exception.
            if (sheetId is null)
                throw new ArgumentNullException(nameof(sheetId));

            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate remove sheet BatchUpdateRequest.
            SpreadsheetsResource.BatchUpdateRequest removeSheetRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveSheetRequest(sheetsService, spreadsheetId, sheetId);

            // Execute remove sheet request in safe asynchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(removeSheetRequest);
        }
        #endregion
    }
}
