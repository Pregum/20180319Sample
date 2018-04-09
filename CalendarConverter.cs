using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Xml;

namespace _20180319Sample
{
    public class CalendarConverter : IValueConverter
    {
        public CalendarConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Food fod = null;
            if (value is Food)
            {
                fod = (Food)value;
            }

            return fod;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Food) value);
        }
    }
}