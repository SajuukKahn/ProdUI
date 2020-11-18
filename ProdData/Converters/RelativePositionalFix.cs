namespace ProdData.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <inheritdoc/>
    public class RelativePositionalFix : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (double)value == 0)
            {
                return string.Empty;
            }

            double elementPositionX = (double)value;
            double windowSize = Application.Current.MainWindow.ActualWidth;
            double elementSize = 144;
            double approximateMiddle = (windowSize / 2) - elementSize;

            if (elementPositionX > approximateMiddle + 30)
            {
                return "Left";
            }
            else if (elementPositionX < approximateMiddle - 30)
            {
                return "Right";
            }

            return string.Empty;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
