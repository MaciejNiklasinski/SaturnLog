using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Extensions
{
    public static class SheetExtensions
    {
        // sheet grid data
        public static IList<IList<string>> GetDataAsStringsGrid(this Sheet sheet, int columnsRequired, int rowsRequired)
        {
            // Create empty sheet strings grid placeholder with required 
            // number of the empty spaces for each row. 
            IList<IList<string>> sheetStringsGrid = new IList<string>[rowsRequired];
            
            // Foreach row
            for (int rIndex = 0; rIndex < rowsRequired; rIndex++)
            {
                // Obtain row data if any available.
                RowData row = sheet.Data?.ElementAtOrDefault(0)?.RowData?.ElementAtOrDefault(rIndex);

                // If row data is available for this specific row.
                if (!(row is null) && !(row.Values is null))
                    // Allocate appropriate size list filled with the data
                    // obtained from the row data on the sheet strings grid.
                    sheetStringsGrid[rIndex] = row.GetDataAsStrings(columnsRequired);

                // If row data is not available
                else
                    // Allocate appropriate size list on the sheet strings grid.
                    sheetStringsGrid[rIndex] = new List<string>(new string[columnsRequired]);
            }

            // Return sheet grid filled with row data.
            return sheetStringsGrid;
        }

        // sheet grid data
        public static IList<IList<object>> GetDataAsObjectsGrid(this Sheet sheet, int columnsRequired, int rowsRequired)
        {
            // Create empty sheet strings grid placeholder with required 
            // number of the empty spaces for each row. 
            IList<IList<object>> sheetObjectsGrid = new IList<object>[rowsRequired];

            // Foreach row
            for (int rIndex = 0; rIndex < rowsRequired; rIndex++)
            {
                // Obtain row data if any available.
                RowData row = sheet.Data?.ElementAtOrDefault(0)?.RowData?.ElementAtOrDefault(rIndex);

                // If row data is available for this specific row.
                if (!(row is null) && !(row.Values is null))
                    // Allocate appropriate size list filled with the data
                    // obtained from the row data on the sheet objects grid.
                    sheetObjectsGrid[rIndex] = row.GetDataAsObjects(columnsRequired);

                // If row data is not available
                else
                    // Allocate appropriate size list on the sheet objects grid.
                    sheetObjectsGrid[rIndex] = new List<object>(new object[columnsRequired]);
            }

            // Return sheet grid
            return sheetObjectsGrid;
        }
    }
}
