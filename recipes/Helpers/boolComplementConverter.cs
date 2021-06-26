using System;
using System.Globalization;
using Xamarin.Forms;

namespace recipes.Helpers
{
    public class boolComplementConverter : IValueConverter
    {
        //can't believe I have to do this nonsense....
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}