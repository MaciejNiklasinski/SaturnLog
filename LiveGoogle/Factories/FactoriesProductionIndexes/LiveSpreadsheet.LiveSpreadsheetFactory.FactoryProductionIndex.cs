using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public partial class LiveSpreadsheet
    {
        #region Private class
        private class FactoryProductionIndex 
        {
            #region Private Fields
            // Stores all the created spreadsheets.
            private IDictionary<string, LiveSpreadsheet> _producedSpreadsheets { get; }
                = new Dictionary<string, LiveSpreadsheet>();
            #endregion

            #region Indexed
            public LiveSpreadsheet this[string spreadsheetId] { get { return this._producedSpreadsheets[spreadsheetId]; } }
            #endregion

            #region Methods
            public IDictionary<string, LiveSpreadsheet> GetAllExisting()
            {
                return new Dictionary<string, LiveSpreadsheet>(_producedSpreadsheets);
            }

            public LiveSpreadsheet GetExistingSpreadsheet(string spreadsheetId)
            {
                try { return this[spreadsheetId]; }
                catch { return null; }
            }
            #endregion

            #region Has Spreadsheet/Sheet/Range
            // Answer the question whether the factory production index already contains a spreadsheet placeholder entry
            public bool HasSpreadsheet(string spreadsheetId)
            {
                // If spreadsheet produced ranges are available
                return _producedSpreadsheets.ContainsKey(spreadsheetId);
            }
            #endregion

            #region Add/Remove Spreadsheet
            // Adds constructed instance of LiveSpreadsheet class into the production index.
            internal void AddSpreadsheet(LiveSpreadsheet spreadsheet)
            {
                this._producedSpreadsheets.Add(spreadsheet.SpreadsheetId, spreadsheet);
            }

            // Adds constructed instance of LiveSpreadsheet class into the production index.
            internal void RemoveSpreadsheet(string spreadsheedId)
            {
                this._producedSpreadsheets.Remove(spreadsheedId);
            }
            #endregion
        }
        #endregion
    }
}
