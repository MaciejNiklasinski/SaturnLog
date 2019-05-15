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
using LiveGoogle.Factories;

namespace LiveGoogle.Extensions
{
    // LiveSpreadsheetsDb specific google sheets and drive service extensions class.
    internal static class SpreadsheetsGoogleServicesExtensions
    {
        #region Create - SheetsService Extensions Methods
        // Create new spreadsheet from dateless blueprint
        internal static Spreadsheet AddSpreadsheetSync(this SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain create spreadsheet request.
            SpreadsheetsResource.CreateRequest createSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSpreadsheetRequest(sheetsService, spreadsheetTitle, spreadsheetSizeBlueprint);

            // Execute createSpreadsheetRequest in safe synchronous manner, and return it result.
            return RequestsExecutor.SafeExecuteSync<Spreadsheet>(createSpreadsheetRequest);
        }

        // Create new spreadsheet from dateless blueprint
        internal static async Task<Spreadsheet> AddSpreadsheetAsync(this SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain create spreadsheet request.
            SpreadsheetsResource.CreateRequest createSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSpreadsheetRequest(sheetsService, spreadsheetTitle, spreadsheetSizeBlueprint);

            // Execute createSpreadsheetRequest in safe asynchronous manner, and return the result
            return await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(createSpreadsheetRequest);
        }
        
        // Create new spreadsheet from data containing spreadsheet blueprint
        internal static Spreadsheet AddSpreadsheetSync(this SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain create spreadsheet request.
            SpreadsheetsResource.CreateRequest createSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSpreadsheetRequest(sheetsService, spreadsheetTitle, spreadsheetBlueprint);
            
            // Execute createSpreadsheetRequest in safe synchronous manner, and store revived dateless spreadsheet instance
            Spreadsheet spreadsheet = RequestsExecutor.SafeExecuteSync<Spreadsheet>(createSpreadsheetRequest);

            // Obtain get spreadsheet request.
            SpreadsheetsResource.GetRequest getSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetRequest(sheetsService, spreadsheet.SpreadsheetId);
            
            // Execute getSpreadsheetRequest in safe synchronous manner.
            return RequestsExecutor.SafeExecuteSync<Spreadsheet>(getSpreadsheetRequest);
        }

        // Create new spreadsheet from data containing spreadsheet blueprint
        internal static async Task<Spreadsheet> AddSpreadsheetAsync(this SheetsService sheetsService, string spreadsheetTitle, IList<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain create spreadsheet request.
            SpreadsheetsResource.CreateRequest createSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetAddSpreadsheetRequest(sheetsService, spreadsheetTitle, spreadsheetBlueprint);

            // Execute createSpreadsheetRequest in safe asynchronous manner, and store the result in a local variable.
            Spreadsheet spreadsheet = await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(createSpreadsheetRequest);

            // Obtain get spreadsheet request.
            SpreadsheetsResource.GetRequest getSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetRequest(sheetsService, spreadsheet.SpreadsheetId);

            // Execute getSpreadsheetRequest in safe synchronous manner.
            return await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(getSpreadsheetRequest);
        }
        #endregion

        #region Read - SheetsService Extensions Methods
        // Returns existing spreadsheet dataless shell
        internal static Spreadsheet GetSpreadsheetShellSync(this SheetsService sheetsService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain get spreadsheet request
            SpreadsheetsResource.GetRequest getSpreadsheetShellRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            return RequestsExecutor.SafeExecuteSync<Spreadsheet>(getSpreadsheetShellRequest);
        }

        // Returns existing spreadsheet dataless shell
        internal static async Task<Spreadsheet> GetSpreadsheetShellAsync(this SheetsService sheetsService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain get spreadsheet request
            SpreadsheetsResource.GetRequest getSpreadsheetShellRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            return await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(getSpreadsheetShellRequest);
        }

        // Returns existing spreadsheet 
        internal static Spreadsheet GetSpreadsheetSync(this SheetsService sheetsService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain get spreadsheet request
            SpreadsheetsResource.GetRequest getSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            return RequestsExecutor.SafeExecuteSync<Spreadsheet>(getSpreadsheetRequest);
        }

        // Returns existing spreadsheet 
        internal static async Task<Spreadsheet> GetSpreadsheetAsync(this SheetsService sheetsService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain get spreadsheet request
            SpreadsheetsResource.GetRequest getSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            return await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(getSpreadsheetRequest);
        }

        // Returns spreadsheet with only listed sheets filled with data, and the non listed ones being obtained only as a sheet dataless shells.
        internal static Spreadsheet GetSpreadsheetPartiallySync(this SheetsService sheetsService, string spreadsheetId, IList<string> sheetTitleIds)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain get spreadsheet shell request
            SpreadsheetsResource.GetRequest getSpreadsheetShellRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            Spreadsheet spreadsheet = RequestsExecutor.SafeExecuteSync<Spreadsheet>(getSpreadsheetShellRequest);
            
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain get spreadsheet listed sheets only request
            SpreadsheetsResource.GetRequest getSpreadsheetListedSheetsOnlyRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetListedSheetsOnlyRequest(sheetsService, spreadsheetId, sheetTitleIds);

            // Execute google request in a safe synchronous manner.
            Spreadsheet spreadsheetSheetsData = RequestsExecutor.SafeExecuteSync<Spreadsheet>(getSpreadsheetListedSheetsOnlyRequest);
            
            // Loops through sheets with data and replaces they empty representations in the spreadsheetDataShell with them.
            foreach (Sheet sheetData in spreadsheetSheetsData.Sheets)
            {
                // Find spreadsheet shell 
                Sheet sheetSheetToBeReplaced = spreadsheet.Sheets.Where((sheetShell) => { return sheetShell.Properties.Title == sheetData.Properties.Title; }).First();

                // Replace sheet shell, with instance of a sheet containing data.s
                spreadsheet.Sheets.Remove(sheetSheetToBeReplaced);
                spreadsheet.Sheets.Add(sheetData);
            }

            // Return spreadsheet shell filled with data of the listed sheets.
            return spreadsheet;
        }

        // Returns spreadsheet with only listed sheets filled with data, and the non listed ones being obtained only as a sheet dataless shells.
        internal static async Task<Spreadsheet> GetSpreadsheetPartiallyAsync(this SheetsService sheetsService, string spreadsheetId, IList<string> sheetTitleIds)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain get spreadsheet shell request
            SpreadsheetsResource.GetRequest getSpreadsheetShellRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetShellRequest(sheetsService, spreadsheetId);

            // Execute google request in a safe synchronous manner.
            Spreadsheet spreadsheet = await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(getSpreadsheetShellRequest);

            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain get spreadsheet listed sheets only request
            SpreadsheetsResource.GetRequest getSpreadsheetListedSheetsOnlyRequest = GoogleServicesExtensionsRequestsFactory.GetGetSpreadsheetListedSheetsOnlyRequest(sheetsService, spreadsheetId, sheetTitleIds);

            // Execute google request in a safe synchronous manner.
            Spreadsheet spreadsheetSheetsData = await RequestsExecutor.SafeExecuteAsync<Spreadsheet>(getSpreadsheetListedSheetsOnlyRequest);

            // Loops through sheets with data and replaces they empty representations in the spreadsheetDataShell with them.
            foreach (Sheet sheetData in spreadsheetSheetsData.Sheets)
            {
                // Find spreadsheet shell 
                Sheet sheetSheetToBeReplaced = spreadsheet.Sheets.Where((sheetShell) => { return sheetShell.Properties.Title == sheetData.Properties.Title; }).First();

                // Replace sheet shell, with instance of a sheet containing data.s
                spreadsheet.Sheets.Remove(sheetSheetToBeReplaced);
                spreadsheet.Sheets.Add(sheetData);
            }

            // Return spreadsheet shell filled with data of the listed sheets.
            return spreadsheet;
        }
        #endregion

        #region Update - SheetsService & DriveService Extensions Methods
        #region Update - Assure multiple columns/rows ranges width/height dimensions - SheetService Extensions Methods
        // Adjust multiple columns ranges width dimensions.
        internal static void AdjustMultipleColumnsRangesWidthDimensionsSync(this SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> columnsRangesDiemansionsAdjustmentBlueprint)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate adjust multiple columns ranges width dimension request
            SpreadsheetsResource.BatchUpdateRequest adjustMultipleColumnsRangesWidthDimansionsRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustMultipleColumnsRangesWidthDimensionsRequest(sheetsService, commonSpreadsheetId, columnsRangesDiemansionsAdjustmentBlueprint);

            // Execute adjust multiple spreadsheet columns ranges with dimensions sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(adjustMultipleColumnsRangesWidthDimansionsRequest);
        }

        // Adjust multiple columns ranges width dimensions.
        internal static async Task AdjustMultipleColumnsRangesWidthDimensionsAsync(this SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> columnsRangesDiemansionsAdjustmentBlueprint)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate adjust multiple columns ranges width dimension request
            SpreadsheetsResource.BatchUpdateRequest adjustMultipleColumnsRangesWidthDimansionsRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustMultipleColumnsRangesWidthDimensionsRequest(sheetsService, commonSpreadsheetId, columnsRangesDiemansionsAdjustmentBlueprint);

            // Execute adjust multiple spreadsheet columns ranges width dimensions sheet request in safe asynchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(adjustMultipleColumnsRangesWidthDimansionsRequest);
        }

        // Adjust multiple rows ranges height dimensions.
        internal static void AdjustMultipleRowsRangesHeightDimensionsSync(this SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> rowsRangesDiemansionsAdjustmentBlueprint)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate adjust multiple rows ranges height dimension request
            SpreadsheetsResource.BatchUpdateRequest adjustMultipleRowsRangesHeightDimensionsRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustMultipleRowsRangesHeightDimensionsRequest(sheetsService, commonSpreadsheetId, rowsRangesDiemansionsAdjustmentBlueprint);

            // Execute adjust multiple spreadsheet rows ranges height dimensions sheet request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateSpreadsheetResponse>(adjustMultipleRowsRangesHeightDimensionsRequest);
        }

        // Adjust multiple rows ranges height dimensions.
        internal static async Task AdjustMultipleRowsRangesHeightDimensionsAsync(this SheetsService sheetsService, string commonSpreadsheetId, IList<Tuple<int?, int, int, int>> rowsRangesDiemansionsAdjustmentBlueprint)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate adjust multiple rows ranges height dimension request
            SpreadsheetsResource.BatchUpdateRequest adjustMultipleRowsRangesHeightDimensionsRequest = GoogleServicesExtensionsRequestsFactory.GetAdjustMultipleRowsRangesHeightDimensionsRequest(sheetsService, commonSpreadsheetId, rowsRangesDiemansionsAdjustmentBlueprint);

            // Execute adjust multiple spreadsheet rows ranges height dimensions sheet request in safe asynchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateSpreadsheetResponse>(adjustMultipleRowsRangesHeightDimensionsRequest);
        }
        #endregion

        #region Update - Assure spreadsheet availability (view/edit rights) - DriveService Extensions Methods
        // Assures that existing spreadsheet view is public.
        internal static void AssureSpreadsheetViewIsPublicSync(this DriveService driveService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest anyoneCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsPublicRequest(driveService, spreadsheetId);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(anyoneCanViewRequest);
        }

        // Assures that existing spreadsheet view is public.
        internal static async Task AssureSpreadsheetViewPublicAsync(this DriveService driveService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest anyoneCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsPublicRequest(driveService, spreadsheetId);

            // Execute batch request in safe asynchronous way.
            await RequestsExecutor.SafeExecuteAsync(anyoneCanViewRequest);
        }

        // Assures that existing spreadsheet view is allowed for specified domain.
        internal static void AssureSpreadsheetViewIsDomainPermittedSync(this DriveService driveService, string spreadsheetId, string domain)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest domainCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsDomainPermittedRequest(driveService, spreadsheetId, domain);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(domainCanViewRequest);
        }

        // Assures that existing spreadsheet view is allowed for specified domain.
        internal static async Task AssureSpreadsheetViewIsDomainPermitteAsync(this DriveService driveService, string spreadsheetId, string domain)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest domainCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsDomainPermittedRequest(driveService, spreadsheetId, domain);

            // Execute batch request in safe synchronous way.
            await RequestsExecutor.SafeExecuteAsync(domainCanViewRequest);
        }

        // Assures that existing spreadsheet view is allowed for specified email address.
        internal static void AssureSpreadsheetViewIsEmailPermittedSync(this DriveService driveService, string spreadsheetId, string email)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest emailAddressCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsEmailPermittedRequest(driveService, spreadsheetId, email);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(emailAddressCanViewRequest);

        }

        // Assures that existing spreadsheet view is allowed for specified email address.
        internal static async Task AssureSpreadsheetViewIsEmailPermittedAsync(this DriveService driveService, string spreadsheetId, string email)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest emailAddressCanViewRequest = GoogleServicesExtensionsRequestsFactory.GetAssureViewIsEmailPermittedRequest(driveService, spreadsheetId, email);

            // Execute batch request in safe synchronous way.
            await RequestsExecutor.SafeExecuteAsync(emailAddressCanViewRequest);

        }

        // Assures that existing spreadsheet edit rights are public.
        internal static void AssureSpreadsheetIsEditPublicSync(this DriveService driveService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest anyoneCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsPublicRequest(driveService, spreadsheetId);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(anyoneCanEditRequest);
        }

        // Assures that existing spreadsheet edit rights are public.
        internal static async Task AssureSpreadsheetIsEditPublicAsync(this DriveService driveService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest anyoneCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsPublicRequest(driveService, spreadsheetId);

            // Execute batch request in safe asynchronous way.
            await RequestsExecutor.SafeExecuteAsync(anyoneCanEditRequest);
        }

        // Assures that existing spreadsheet edit rights are available for the specified domain.
        internal static void AssureSpreadsheetEditIsDomainPermittedSync(this DriveService driveService, string spreadsheetId, string domain)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest domainCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsDomainPermittedRequest(driveService, spreadsheetId, domain);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(domainCanEditRequest);
        }

        // Assures that existing spreadsheet edit rights are available for the specified domain.
        internal static async Task AssureSpreadsheetEditIsDomainPermittedAsync(this DriveService driveService, string spreadsheetId, string domain)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest domainCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsDomainPermittedRequest(driveService, spreadsheetId, domain);

            // Execute batch request in safe synchronous way.
            await RequestsExecutor.SafeExecuteAsync(domainCanEditRequest);
        }

        // Assures that existing spreadsheet edit rights are available for the specified email address.
        internal static void AssureSpreadsheetEditIsEmailPermittedSync(this DriveService driveService, string spreadsheetId, string email)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct new instance of batch request
            BatchRequest emailAddressCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsEmailPermittedRequest(driveService, spreadsheetId, email);

            // Execute batch request in safe synchronous way.
            RequestsExecutor.SafeExecuteSync(emailAddressCanEditRequest);
        }

        // Assures that existing spreadsheet edit rights are available for the specified email address.
        internal static async Task AssureSpreadsheetEditIsEmailPermittedAsync(this DriveService driveService, string spreadsheetId, string email)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct new instance of batch request
            BatchRequest emailAddressCanEditRequest = GoogleServicesExtensionsRequestsFactory.GetAssureEditIsEmailPermittedRequest(driveService, spreadsheetId, email);

            // Execute batch request in safe synchronous way.
            await RequestsExecutor.SafeExecuteAsync(emailAddressCanEditRequest);
        }
        #endregion
        #endregion

        #region Remove - DriveService Extensions Methods
        // Removes existing spreadsheet.
        internal static void RemoveSpreadsheetSync(this DriveService driveService, string spreadsheetId)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Get appropriate request instance
            FilesResource.DeleteRequest removeSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveSpreadsheetRequest(driveService, spreadsheetId);

            // Execute removeSpreadsheetRequest in safe, synchronous manner.
            RequestsExecutor.SafeExecuteSync(removeSpreadsheetRequest);
        }

        // Removes existing spreadsheet asynchronously.
        internal static async Task RemoveSpreadsheetAsync(this DriveService driveService, string spreadsheetId)
        {
            // Await for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Get appropriate request instance
            FilesResource.DeleteRequest removeSpreadsheetRequest = GoogleServicesExtensionsRequestsFactory.GetRemoveSpreadsheetRequest(driveService, spreadsheetId);

            // Await execution results of the removeSpreadsheetRequest in safe, asynchronous manner.
            await RequestsExecutor.SafeExecuteAsync(removeSpreadsheetRequest);
        }
        #endregion
    }
}
