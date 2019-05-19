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
    // Row specific google sheets service extensions class.
    internal static class RowsGoogleServicesExtensions
    {
        #region Create
        #endregion

        #region Read
        // Return range data as grid of objects, where each object represents content of the cell in the grid.
        internal static IList<object> GetObjectsRowDataSync(this SheetsService sheetsService, string spreadsheetId, string rowRangeString)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate get row data request.
            SpreadsheetsResource.ValuesResource.GetRequest getRowDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, rowRangeString);
            
            // Execute get row data request in safe synchronous manner.
            ValueRange getRangeDataResponse = RequestsExecutor.SafeExecuteSync<ValueRange>(getRowDataRequest);

            // Return response objects data grid
            return getRangeDataResponse.Values?[0];
        }

        // Return range data as grid of objects, where each object represents content of the cell in the grid.
        internal static async Task<IList<object>> GetObjectsRowDataAsync(this SheetsService sheetsService, string spreadsheetId, string rowRangeString)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate get row data request.
            SpreadsheetsResource.ValuesResource.GetRequest getRowDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, rowRangeString);

            // Execute get row data request in safe synchronous manner.
            ValueRange getRangeDataResponse = await RequestsExecutor.SafeExecuteAsync<ValueRange>(getRowDataRequest);

            // Return response objects data grid
            return getRangeDataResponse.Values?[0];
        }

        // Return range data as grid of strings, where each object represents content of the cell in the grid.
        internal static IList<string> GetStringsRowDataSync(this SheetsService sheetsService, string spreadsheetId, string rowRangeString)
        {
            return sheetsService.GetObjectsRowDataSync(spreadsheetId, rowRangeString).Select(o => o?.ToString())?.ToList();
        }

        // Return range data as grid of strings, where each object represents content of the cell in the grid.
        internal static async Task<IList<string>> GetStringsRowDataAsync(this SheetsService sheetsService, string spreadsheetId, string rowRangeString)
        {
            IList<object> objectsRowData = await sheetsService.GetObjectsRowDataAsync(spreadsheetId, rowRangeString);

            return objectsRowData.Select(o => o?.ToString())?.ToList();
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
