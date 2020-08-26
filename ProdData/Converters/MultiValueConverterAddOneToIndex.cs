using System;
using System.Globalization;
using System.Windows.Data;

namespace ProdData.Converters
{
    internal class MultiValueConverterAddOneToIndex : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int.TryParse(values[0]?.ToString(), out int property1);
            int.TryParse(values[1]?.ToString(), out int property2);
            if (property2 == 0)
            {
                return "0";
            }

            return (property1 + 1).ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}