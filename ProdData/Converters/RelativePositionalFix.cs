using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace ProdData.Converters
{
    public class RelativePositionalFix : IValueConverter
    {
        //TODO This is a fix I did to handle the right/left cards layout, but it's far from perfect
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (double)value == 0)
            {
                return "";
            }

            double elementPositionX = (double)value;
            double windowSize = Application.Current.MainWindow.ActualWidth;
            double elementSize = 144;
            double approximateMiddle = ((windowSize / 2) - elementSize);;

            if (elementPositionX > approximateMiddle + 30)
            {
                return "Left";
            }
            else if (elementPositionX < approximateMiddle - 30)
            {
                return "Right";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
