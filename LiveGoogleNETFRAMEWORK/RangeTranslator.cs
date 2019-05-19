using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Sheets
{
    public static class RangeTranslator
    {
        private static readonly char[] Numbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static void RangeStringToParameters(string range, out string sheetTitleId, out int leftColumnIndex, out int topRowIndex, out int columnCount, out int rowCount)
        {
            // Get index of the first letter of the range sheet title id.
            // Always equal 0;
            int sheetTitleIdStartIndex = 0;

            // Length of the string representing range sheet title id.
            // Equal to the index of the first separator last char '!'.
            int sheetTitleIdLenght = range.LastIndexOf(@"!");

            // Get index of the first letter of the string representing left part of range. 
            // Equal to the combined length of the sheet title and the first separator.
            int rangeLeftStartIndex = sheetTitleIdLenght + 1;

            // Get index of the first letter of the string representing right part of range. 
            int rangeRightStartIndex = range.LastIndexOf(@":") + 1;

            // Get string representing sheet title id - part of range.
            // And assign as an out parameter.
            sheetTitleId = range.Substring(sheetTitleIdStartIndex, sheetTitleIdLenght);

            // Get string representing right/bottom - part of range.
            string rangeRightBottom = range.Substring(rangeRightStartIndex)?.ToUpper();

            // Get string representing left/top - part of range.          
            string rangeLeftTop = range.Substring(rangeLeftStartIndex, rangeRightStartIndex - 1 - rangeLeftStartIndex)?.ToUpper();

            // Get index of the first letter of the string representing top part of range. 
            int rangeTopStartIndex = rangeLeftTop.IndexOfAny(Numbers);

            // Get index of the first letter of the string representing bottom part of range. 
            int rangeBottomStartIndex = rangeRightBottom.IndexOfAny(Numbers);

            // Get length of the string representing left part of the range.
            int rangeLeftLenght = rangeTopStartIndex;

            // Get length of the string representing right part of the range.
            int rangeRightLenght = rangeBottomStartIndex;

            // Get string representing left part of range.      
            string rangeLeft = rangeLeftTop.Substring(0, rangeLeftLenght);

            // Get string representing top part of range.
            string rangeTop = rangeLeftTop.Substring(rangeTopStartIndex).TrimEnd(':');

            // Get string representing right part of range.
            string rangeRight = rangeRightBottom.Substring(0, rangeRightLenght);

            // Get string representing bottom part of range.
            string rangeBottom = rangeRightBottom.Substring(rangeRightLenght);

            // Get value of the left column index described by the provided range.
            // And assign it as an out parameter.
            leftColumnIndex = RangeTranslator.ColumnIndexerStringToIndex(rangeLeft);

            // Get value of the top column index described by the provided range.
            // And assign it as an out parameter.       
            topRowIndex = RangeTranslator.RowIndexerStringToIndex(rangeTop);

            // Get value of the left column index described by the provided range.
            // And assign as an out parameter.    
            int rightColumnIndex = RangeTranslator.ColumnIndexerStringToIndex(rangeRight);

            // Get value of the left column index described by the provided range.
            // And assign as an out parameter.           
            int bottomRowIndex = RangeTranslator.RowIndexerStringToIndex(rangeBottom);

            // Calculate columns number. 5 2 1 
            columnCount = (rightColumnIndex + 1) - leftColumnIndex;

            // Calculate row number.
            rowCount = (bottomRowIndex + 1) - topRowIndex;
        }

        public static string GetRangeString(string sheetName, int columnLeftIndex, int rowTopIndex, int columnCount, int rowCount)
        {
            // Calculate index of the most right column and most bottom row
            int columnRightIndex = columnLeftIndex + (columnCount - 1);
            int rowBottomIndex = rowTopIndex + (rowCount - 1);

            return 
                // Sheet Title Id followed by the '!' separator
                sheetName + "!" +
                // Alphanumerical representation of the left/top corner of the range, followed by the ':' separator
                $"{IndexToCapitalChar(columnLeftIndex)}{rowTopIndex + 1}:" +
                // Alphanumerical representation of the right/bottom corner of the range.
                $"{IndexToCapitalChar(columnRightIndex)}{rowBottomIndex + 1}";
        }

        internal static string GetCellString(string sheetName, int columnIndex, int rowIndex)
        {
            return sheetName + "!" + $"{IndexToCapitalChar(columnIndex)}{rowIndex + 1}";
        }

        private static string IndexToCapitalChar(int index)
        {
            string rangeChars;

            if (index < 26)
                rangeChars = ((Char)(65 + index)).ToString();

            else
            {
                int multiplier = index / 26;

                int rest = index - (26 * multiplier);

                rangeChars = ((Char)(65 + (multiplier - 1))).ToString();
                rangeChars += ((Char)(65 + rest)).ToString();

            }

            return rangeChars;
        }

        private static int ColumnIndexerStringToIndex(string indexerString)
        {

            char char1 = indexerString[0];

            // One letter - A - 0    Z - 25
            if (indexerString.Length == 1)
            {
                return (char.ToUpper(char1) - 65);
            }
            // Two letters - AA - 26   ?? - 255   
            else
            {
                char char2 = indexerString[1];

                int firstCharScore = (((char.ToUpper(char1) - 65) * 26) + 26);
                int secondCharScore = (char.ToUpper(char2) - 65);

                return (firstCharScore + secondCharScore);
            }
        }

        private static int RowIndexerStringToIndex(string indexerString)
        {
            return (int.Parse(indexerString) - 1);
        }
    }
}
