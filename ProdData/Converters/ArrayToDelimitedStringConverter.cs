using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ProdData.Converters
{
    public class ArrayToDelimitedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.GetType() == typeof(string[]))
            {
                string[] vs = (string[])value;
                string delimiter = ", ";
                if(parameter != null && parameter.GetType() == typeof(string))
                {
                    string s = (string)parameter ?? "";
                    delimiter = s;
                }
                return string.Join(delimiter, vs);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
