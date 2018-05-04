using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace _20180319Sample
{
    //[ValueConversion(typeof(DateTime), typeof(SolidColorBrush))]
    public class DayOfWeekConverter : IValueConverter
    {
        public DayOfWeekConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo cultureInfo)
        {
            if (value is DateTime dt)
            {
                //return dt.DayOfWeek;
                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return new SolidColorBrush(Colors.LightPink);
                    case DayOfWeek.Saturday:
                            return new SolidColorBrush(Colors.LightSkyBlue);
                    default:
                        //return new SolidColorBrush(Colors.Turquoise);
                        return new SolidColorBrush(Colors.Transparent);
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}