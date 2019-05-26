using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SaturnLog.UI.Converters
{
    public class BoolToStyleValueConverter : IValueConverter
    {
        //public readonly EqualityComparer<Xamarin.Forms.Style> Comparer = EqualityComparer<Xamarin.Forms.Style>.Default;

        public const string EnabledButtonStyleKey = "EnabledButton";
        public const string DisabledButtonStyleKey = "DisabledButton";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Application.Current.Resources[EnabledButtonStyleKey];
            else
                return Application.Current.Resources[DisabledButtonStyleKey];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Style valueStyle = value as Style;

            Style enabledButtonStyle = Application.Current.Resources[EnabledButtonStyleKey] as Style;
            Style disabledButtonStyle = Application.Current.Resources[DisabledButtonStyleKey] as Style;

            EqualityComparer<Style> comperer = EqualityComparer<Style>.Default;

            if (comperer.Equals(enabledButtonStyle, valueStyle))
                return true;
            else if (comperer.Equals(disabledButtonStyle, valueStyle))
                return false;
            else if (valueStyle is null)
                throw new ArgumentException("Provided value is not an instance of Xamarin.Forms.Style type.", nameof(value));
            else
                throw new ArgumentException("Provided Xamarin.Forms.Style is not associated with button being enabled or disabled.", nameof(value)); ;
        }
    }
}
