namespace ProdData.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// Defines the <see cref="MultiValueConverterAddOneToIndex" />.
    /// </summary>
    internal class MultiValueConverterAddOneToIndex : IMultiValueConverter
    {
        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="values">The values<see cref="object[]"/>.</param>
        /// <param name="targetType">The targetType<see cref="Type"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
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

        /// <summary>
        /// The ConvertBack.
        /// </summary>
        /// <param name="value">The value<see cref="object"/>.</param>
        /// <param name="targetTypes">The targetTypes<see cref="Type[]"/>.</param>
        /// <param name="parameter">The parameter<see cref="object"/>.</param>
        /// <param name="culture">The culture<see cref="CultureInfo"/>.</param>
        /// <returns>The <see cref="object[]"/>.</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
