using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using Telerik.Windows.Controls;

namespace ProdData.Converters
{
    public class RelativeLocationConverter : IMultiValueConverter

    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            RadCarousel items = (RadCarousel)values[0];
            Debug.WriteLine(items.SelectedItem?.ToString());
            var something = items.FindCarouselPanel();
            var again = something?.TopContainer;
            if(again == null)
            {
                return "0";
            }
            var somethingelse = (again as CarouselItem)?.DataContext;
            var individual = values[1];

            Debug.WriteLine(somethingelse?.ToString() + "\t|\t" + individual.ToString());
            if (somethingelse == individual)
            {
                return "2";
            }
            int topContainerPosition = items.Items.IndexOf(somethingelse);
            if(items.Items.IndexOf(individual) < topContainerPosition)
            {
                return "1";
            }    


            return "0";

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
