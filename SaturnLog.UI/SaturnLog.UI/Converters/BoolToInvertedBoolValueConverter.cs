using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SaturnLog.UI.Converters
{
    public class BoolToInvertedBoolValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolValue = null;

            try { boolValue = (bool)value; } catch { }

            if (boolValue == true)
                return false;
            else if (boolValue == false)
                return true;
            else
                throw new ArgumentException("Provided valid is invalid for this specific value converter", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
