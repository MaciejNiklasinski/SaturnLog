﻿using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Extensions
{
    public static class CellDataExtensions
    {
        /// <summary>
        /// Returns the data stored in the cell in a form of string.
        /// </summary>
        /// <param name="cell">The cell which data is expected to be returned.</param>
        /// <returns>Data of stored in the cell.</returns>
        public static string GetDataAsString(this CellData cell)
        {
            return cell.EffectiveValue?.StringValue ?? cell.EffectiveValue?.NumberValue.ToString();
        }

        /// <summary>
        /// Returns the data stored in the cell in a form of object.
        /// </summary>
        /// <param name="cell">The cell which data is expected to be returned.</param>
        /// <returns>Data of stored in the cell.</returns>
        public static object GetDataAsObject(this CellData cell)
        {
            return cell.EffectiveValue?.StringValue ?? cell.EffectiveValue?.NumberValue.ToString();
        }
    }
}
