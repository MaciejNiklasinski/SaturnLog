using LiveGoogle.Sheets;
using LiveGoogle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveGoogle.Extensions;

namespace LiveGoogle.Session
{
    internal partial class SessionsRepository
    {
        private class DataOperator
        {
            #region Private Fields
            private SessionsRepository _owner;
            #endregion

            #region Private properties
            private LiveGoogle _googleService { get { return this._owner._googleService; } }
            private LiveSheet _sessionsSheet { get { return this._owner._sessionsSheet; } }
            private SessionStamp _cachedSessionStamp { get { return this._owner._cachedSessionStamp; } set { this._owner._cachedSessionStamp = value; } }
            #endregion

            #region Constructor
            public DataOperator(SessionsRepository owner)
            {
                this._owner = owner;
            }
            #endregion

            #region Data Retrieve/Upload 
            public static int RetrieveInitialAvailableQuota(Google.Apis.Sheets.v4.SheetsService sheetsService, string sessionsSpreadsheetId)
            {
                int availableQuotaInt;

                try
                {
                    string availableQuotaStringRange = RangeTranslator.GetCellString(SessionsRepository.SessionsDbSpreadsheetId_SheetId, SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex, SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex);

                    string availableQuotaString = sheetsService.GetStringCellDataSync(sessionsSpreadsheetId, availableQuotaStringRange);

                    availableQuotaInt = int.Parse(availableQuotaString);
                }
                catch { availableQuotaInt = 0; }

                return availableQuotaInt;
            }

            // Obtains new SessionStamp and stores it as cached session stamp.
            public void RefreshCachedSessionStampSync()
            {
                // Get the live row containing data about the current sessions
                LiveRow sessionsRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // .. and use it to refresh data stored in sessions row..
                sessionsRow.ReObtainData(db);

                // .. obtain this data as a List<string> suitable to be used
                // as a construction parameters for SessionStamp ..
                IList<string> sessionStampParams = sessionsRow.GetDataAsStrings();

                // Get int value of numer of available requests according the spreadsheet - the value is used just for reference
                // and in case of activation, otherwise the number of available requests is tracked by SessionRequestsLimiter
                int lastAvailableRequests = int.Parse(sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex]);

                // .. and assign new instance of SessioStamp constructed from them
                // as a last cached session stamp
                this._cachedSessionStamp = new SessionStamp(
                    lastAvailableRequests: lastAvailableRequests,
                    lastForcedToDisconnect: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex],
                    disconnected: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex],
                    last: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex],
                    connected: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex],
                    connectedIn15: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex],
                    connectedIn30: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex],
                    connectedIn45: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex],
                    connectedIn60: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex],
                    connectedIn90: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex],
                    connectedIn120: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex]);
            }

            // Obtains new SessionStamp and stores it as cached session stamp.
            public async Task RefreshCachedSessionStampAsync()
            {
                // Get the live row containing data about the current sessions
                LiveRow sessionsRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // .. and use it to refresh data stored in sessions row..
                await sessionsRow.ReObtainDataAsync(db);

                // .. obtain this data as a List<string> suitable to be used
                // as a construction parameters for SessionStamp ..
                IList<string> sessionStampParams = sessionsRow.GetDataAsStrings();

                // Get int value of numer of available requests according the spreadsheet - the value is used just for reference
                // and in case of activation, otherwise the number of available requests is tracked by SessionRequestsLimiter
                int lastAvailableRequests = int.Parse(sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex]);

                // .. and assign new instance of SessioStamp constructed from them
                // as a last cached session stamp
                this._cachedSessionStamp = new SessionStamp(
                    lastAvailableRequests: lastAvailableRequests,
                    lastForcedToDisconnect: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex],
                    disconnected: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex],
                    last: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex],
                    connected: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex],
                    connectedIn15: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex],
                    connectedIn30: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex],
                    connectedIn45: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex],
                    connectedIn60: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex],
                    connectedIn90: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex],
                    connectedIn120: sessionStampParams[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex]);
            }
            
            //
            public async Task UpdateSessionActivationConnectedIn120Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;
                
                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(stamp);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn120To90Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData("");

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn90To60Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData("");

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn60Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn60To45Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData("");

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn45To30Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData("");

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn30Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;
                
                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn30To15Async(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData("");

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateSessionActivationConnectedIn15ToConnectedAsync(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData("");

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateEndSessionActivationAsync()
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost at least one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get not active stamp - current application instance session id
                string nonActiveStamp = SessionsRepository._sessionId;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(nonActiveStamp);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateMaintainActiveSessionAsync(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;
                
                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(stamp);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateEndActiveSessionAsync(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost at least one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Get not active stamp - current application instance session id
                string nonActiveStamp = SessionsRepository._sessionId;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(stamp);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(nonActiveStamp);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateForcedEndActiveSessionAsync(string stamp)
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost at least one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;
                
                // Get not active stamp - current application instance session id
                string nonActiveStamp = SessionsRepository._sessionId;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(stamp);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(stamp);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(nonActiveStamp);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // Update appropriate cell with the appropriate stamp value.
                await sessionStampRow.UploadAsync(db);
            }

            //
            public async Task UpdateClearOwnConnectedInStampsAsync()
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Flag indicating whether the data upload is necessary at all.
                bool changesToBeCommited = false;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn15))
                {
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData("");
                    changesToBeCommited = true;
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn30))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn45))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell  
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn60))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell   
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn90))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell   
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn120))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // If changesToBeCommited flag is true, update appropriate cell with the appropriate stamp value.
                if (changesToBeCommited) await sessionStampRow.UploadAsync(db);
            }

            // Removes all 'ConnectedIn' stamps representing unresponsive stamp
            public void UpdateClearConnectedInUnresponsiveStamps()
            {
                // Get last known number of available requests stamp (Minus one as uploading this stamp will cost one request)
                int lastAvailableRequests = SessionRequestsLimiter.Instance.AvailableQuota - 1;
                if (lastAvailableRequests < 0)
                    lastAvailableRequests = 0;

                // Flag indicating whether the data upload is necessary at all.
                bool changesToBeCommited = false;

                // Get the live row containing data about the current sessions
                LiveRow sessionStampRow = this._sessionsSheet[SessionsRepository.SessionsDbSpreadsheetId_DataRowIndex];

                // Set Content of LastAvailableRequestsCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex].SetData(lastAvailableRequests.ToString());

                // Set Content of ForcedtoDisconnectCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of DisconnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of LastCell
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_LastStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedCell 
                sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn15Cell 
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn15))
                {
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData("");
                    changesToBeCommited = true;
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn30Cell 
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn30))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn45Cell 
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn45))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn60Cell  
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn60))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn90Cell   
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn90))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex].SetData(null as object, false);

                // Set Content of ConnectedIn120Cell   
                if (StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.ConnectedIn120))
                {
                    changesToBeCommited = true;
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData("");
                }
                else
                    sessionStampRow[SessionsRepository.SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex].SetData(null as object, false);

                // Get live spreadsheets database reference...
                LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

                // If changesToBeCommited flag is true, update appropriate cell with the appropriate stamp value.
                if (changesToBeCommited) sessionStampRow.Upload(db);
            }
            #endregion
        }
    }
}
