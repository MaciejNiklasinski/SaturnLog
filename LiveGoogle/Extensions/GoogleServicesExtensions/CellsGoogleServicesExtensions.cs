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
    // Cell specific google sheets service extensions class.
    internal static class CellsGoogleServicesExtensions
    {
        #region Create
        #endregion

        #region Read
        internal static object GetObjectCellDataSync(this SheetsService sheetsService, string spreadsheetId, string cellRangeString)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate get cell data request.
            SpreadsheetsResource.ValuesResource.GetRequest getCellDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, cellRangeString);

            // Execute get cell data request in safe synchronous manner.
            ValueRange getRangeDataResponse = RequestsExecutor.SafeExecuteSync<ValueRange>(getCellDataRequest);

            // Return response objects data grid
            return getRangeDataResponse.Values?[0]?[0];
        }

        internal static async Task<object> GetObjectCellDataAsync(this SheetsService sheetsService, string spreadsheetId, string cellRangeString)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate get cell data request.
            SpreadsheetsResource.ValuesResource.GetRequest getCellDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, cellRangeString);

            // Execute get cell data request in safe synchronous manner.
            ValueRange getRangeDataResponse = await RequestsExecutor.SafeExecuteAsync<ValueRange>(getCellDataRequest);

            // Return response objects data grid
            return getRangeDataResponse.Values?[0]?[0];
        }

        internal static string GetStringCellDataSync(this SheetsService sheetsService, string spreadsheetId, string cellRangeString)
        {
            // Get object cell data, translate to string and return
            return sheetsService.GetObjectCellDataSync(spreadsheetId, cellRangeString).ToString();
        }

        internal static async Task<string> GetStringCellDataAsync(this SheetsService sheetsService, string spreadsheetId, string cellRangeString)
        {
            // Get an return cell data
            object objectData = await sheetsService.GetObjectCellDataAsync(spreadsheetId, cellRangeString);

            // Translate to string and return
            return objectData.ToString();
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
