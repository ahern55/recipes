using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace recipes.Helpers
{
    public class collectionIsNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = (Collection<object>)value;
            return collection.Count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}