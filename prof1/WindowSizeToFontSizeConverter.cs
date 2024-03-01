using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace prof1
{
    class WindowSizeToFontSizeConverter : IValueConverter
    {
        private double factor;

        public double Factor
        {
            get { return factor; }
            set { factor = value; }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            double windowSize = (double)value;

            double fontSize = factor * windowSize;
            
            if (parameter != null && parameter.ToString() == "Bottom")
            {
                return new Thickness(0, 0, 0, windowSize * factor);
            }

            return fontSize; 
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
