using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveGoogle.Sheets;

namespace LiveGoogle.Extensions
{    
    public static class SpreadsheetExtensions
    {
        //                sheet title id, sheet grid data
        public static IDictionary<string, IList<IList<string>>> GetDataAsStringsGridsIndex(this Spreadsheet spreadsheet, IDictionary<string, string> requiredRangesBySheetTitleId)
        {
            IDictionary<string, IList<IList<string>>> stringsGridsIndex = new Dictionary<string, IList<IList<string>>>();

            HashSet<string> requiredSheetTitleIds = new HashSet<string>(requiredRangesBySheetTitleId.Keys);
            
            // Foreach sheet
            foreach (string requiredSheetTitleId in requiredSheetTitleIds)
            {
                string sheetRequiredRange = requiredRangesBySheetTitleId[requiredSheetTitleId];

                // Declare range translator out variables
                // Translate string range back in to separate range parameters
                RangeTranslator.RangeStringToParameters(sheetRequiredRange, out string dummyString, out int sheetRequiredLeftIndex, out int sheetRequiredTopIndex, out int sheetRequiredColumnCount, out int sheetRequiredRowCount);
                             
                // Add sheet strings grids index placeholder dictionary entry
                stringsGridsIndex.Add(requiredSheetTitleId, null);

                // TODO confirm spreadsheet.Sheets is never null
                // Obtain sheet data if any available
                Sheet sheet = spreadsheet.Sheets.Where(s => s.Properties.Title == sheetRequiredRange).FirstOrDefault();

                if (!(sheet is null))
                    // Fill that sheet with a list representing required amount of rows.
                    stringsGridsIndex[requiredSheetTitleId] = sheet.GetDataAsStringsGrid(sheetRequiredColumnCount, sheetRequiredRowCount);
                else
                    // Fill that sheet with a list representing required amount of rows.
                    stringsGridsIndex[requiredSheetTitleId] = new List<IList<string>>(new IList<string>[sheetRequiredRowCount]);
            }

            // Return spreadsheet sheet grids index
            return stringsGridsIndex;
        }

        //                sheet title id, sheet grid data
        public static IDictionary<string, IList<IList<object>>> GetDataAsObjectsGridsIndex(this Spreadsheet spreadsheet, IDictionary<string, string> requiredRangesBySheetTitleId)
        {
            IDictionary<string, IList<IList<object>>> objectsGridsIndex = new Dictionary<string, IList<IList<object>>>();

            HashSet<string> requiredSheetTitleIds = new HashSet<string>(requiredRangesBySheetTitleId.Keys);

            // Foreach sheet
            foreach (string requiredSheetTitleId in requiredSheetTitleIds)
            {
                string sheetRequiredRange = requiredRangesBySheetTitleId[requiredSheetTitleId];

                // Declare range translator out variables 
                // Translate string range back in to separate range parameters
                RangeTranslator.RangeStringToParameters(sheetRequiredRange, out string dummyString, out int sheetRequiredLeftIndex, out int sheetRequiredTopIndex, out int sheetRequiredColumnCount, out int sheetRequiredRowCount);

                // Add sheet strings grids index placeholder dictionary entry
                objectsGridsIndex.Add(requiredSheetTitleId, null);

                // TODO confirm spreadsheet.Sheets is never null
                // Obtain sheet data if any available
                Sheet sheet = spreadsheet.Sheets.Where(s => s.Properties.Title == sheetRequiredRange).FirstOrDefault();

                if (!(sheet is null))
                    // Fill that sheet with a list representing required amount of rows.
                    objectsGridsIndex[requiredSheetTitleId] = sheet.GetDataAsObjectsGrid(sheetRequiredColumnCount, sheetRequiredRowCount);
                else
                    // Fill that sheet with a list representing required amount of rows.
                    objectsGridsIndex[requiredSheetTitleId] = new List<IList<object>>(new IList<object>[sheetRequiredRowCount]);
            }

            // Return spreadsheet sheet grids index
            return objectsGridsIndex;
        }
    }
}
