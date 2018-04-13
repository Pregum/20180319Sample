using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;

namespace _20180319Sample
{
    public class CalendarConverter : IValueConverter
    {
        //private Dictionary<DateTime, Food> dict = new Dictionary<DateTime, Food>();
        public Dictionary<DateTime, IList<Food>> dict = new Dictionary<DateTime, IList<Food>>();

        public CalendarConverter()
        {
            var list = new List<Food>();
            list.Add(new Food("第1号", new BitmapImage(new Uri("Resources/meat.png", UriKind.Relative)), 100f,
                DateTime.Today,
                DateTime.Today.AddDays(7)));
            
            dict.Add(DateTime.Today,list);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Food fod = null;
            //if (!dict.TryGetValue(DateTime.Parse(value.ToString()), out fod))
            if (14 == int.Parse((string)value))
            {
                return dict[DateTime.Today];
            }
            return fod;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Food) value);
        }
    }
}