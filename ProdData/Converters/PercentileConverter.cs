using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ProdData.Converters
{
    class PercentileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(double) || value.GetType() == typeof(int))
            {
                if(parameter != null && parameter.GetType() == typeof(double))
                {
                    double val = (double)value;
                    double param = (double)parameter;

                    return val * param;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
