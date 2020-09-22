namespace ProdData.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="RelativePositionalFix" />.
    /// </summary>
    public class RelativePositionalFix : IValueConverter
    {
        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
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

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
