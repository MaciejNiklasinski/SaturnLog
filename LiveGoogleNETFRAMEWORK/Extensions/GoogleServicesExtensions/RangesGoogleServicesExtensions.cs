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
    internal static class RangesGoogleServicesExtensions
    {
        #region Create
        // TODO implement creating protected ranges.
        #endregion

        #region Read
        // Return range data as grid of objects, where each object represents content of the cell in the grid.
        internal static IList<IList<object>> GetObjectsRangeDataSync(this SheetsService sheetsService, string spreadsheetId, string rangeString)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Obtain appropriate get range data request.
            SpreadsheetsResource.ValuesResource.GetRequest getRangeDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, rangeString);
            
            // Execute get range data request in safe synchronous manner.
            ValueRange getRangeDataResponse = RequestsExecutor.SafeExecuteSync<ValueRange>(getRangeDataRequest);
            
            // Return response objects data grid
            return getRangeDataResponse.Values;
        }

        // Return range data as grid of objects, where each object represents content of the cell in the grid.
        internal static async Task<IList<IList<object>>> GetObjectsRangeDataAsync(this SheetsService sheetsService, string spreadsheetId, string rangeString)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Obtain appropriate get range data request.
            SpreadsheetsResource.ValuesResource.GetRequest getRangeDataRequest = GoogleServicesExtensionsRequestsFactory.GetGetRangeDataRequest(sheetsService, spreadsheetId, rangeString);

            // Execute get range data request in safe synchronous manner.
            ValueRange getRangeDataResponse = await RequestsExecutor.SafeExecuteAsync<ValueRange>(getRangeDataRequest);

            // Return response objects data grid
            return getRangeDataResponse.Values;
        }

        // Return range data as grid of strings, where each string represents content of the cell in the grid.
        internal static IList<IList<string>> GetStringsRangeDataSync(this SheetsService sheetsService, string spreadsheetId, string rangeString)
        {
            // Get response objects grid ..
            IList<IList<object>> cellsGridObjectsData = sheetsService.GetObjectsRangeDataSync(spreadsheetId, rangeString);
            // .. and translate it into the strings grid.
            IList<IList<string>> cellsGridStringsData = cellsGridObjectsData.Select<IList<object>, IList<string>>((objectsList) =>
            {
                return objectsList.Select<object, string>((obj) =>
                {
                    return obj.ToString();
                }).ToList();
            }).ToList();


            // Return response string data grid
            return cellsGridStringsData;
        }

        // Return range data as grid of strings, where each string represents content of the cell in the grid.
        internal static async Task<IList<IList<string>>> GetStringsRangeDataAsync(this SheetsService sheetsService, string spreadsheetId, string rangeString)
        {
            // Get response objects grid ..
            IList<IList<object>> cellsGridObjectsData = await sheetsService.GetObjectsRangeDataAsync(spreadsheetId, rangeString);
            // .. and translate it into the strings grid.
            IList<IList<string>> cellsGridStringsData = cellsGridObjectsData.Select<IList<object>, IList<string>>((objectsList) =>
            {
                return objectsList.Select<object, string>((obj) =>
                {
                    return obj.ToString();
                }).ToList();
            }).ToList();
            
            // Return response string data grid
            return cellsGridStringsData;
        }
        #endregion

        #region Update
        // Update data stored in the specified range.
        internal static void UpdateRangeDataSync(this SheetsService sheetsService, string spreadsheetId, string rangeString, IList<IList<object>> rangeData)
        {
            // Wait for google apis request quota availability.
            SessionRequestsLimiter.Instance.Wait();

            // Construct appropriate update range data google apis request.
            SpreadsheetsResource.ValuesResource.BatchUpdateRequest updateRangeDataRequest = GoogleServicesExtensionsRequestsFactory.GetUpdateRangeDataRequest(sheetsService, spreadsheetId, rangeString, rangeData);
        
            // Execute update range data request in safe synchronous manner.
            RequestsExecutor.SafeExecuteSync<BatchUpdateValuesResponse>(updateRangeDataRequest);
        }

        // Update data stored in the specified range.
        internal static async Task UpdateRangeDataAsync(this SheetsService sheetsService, string spreadsheetId, string rangeString, IList<IList<object>> rangeData)
        {
            // Wait for google apis request quota availability.
            await SessionRequestsLimiter.Instance.WaitAsync();

            // Construct appropriate update range data google apis request.
            SpreadsheetsResource.ValuesResource.BatchUpdateRequest updateRangeDataRequest = GoogleServicesExtensionsRequestsFactory.GetUpdateRangeDataRequest(sheetsService, spreadsheetId, rangeString, rangeData);

            // Execute update range data request in safe synchronous manner.
            await RequestsExecutor.SafeExecuteAsync<BatchUpdateValuesResponse>(updateRangeDataRequest);
        }
        #endregion

        #region Delete
        // TODO implement removing protected ranges.
        #endregion
    }
}
