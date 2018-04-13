using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;
using _20180319Sample.Annotations;

namespace _20180319Sample
{
    public class CalendarConverter : IValueConverter
    {
        //private Dictionary<DateTime, Food> dict = new Dictionary<DateTime, Food>();
        public Dictionary<DateTime, ObservableCollection<Food>> __dict =
            new Dictionary<DateTime, ObservableCollection<Food>>();

        public Dictionary<DateTime, ObservableCollection<Food>> dict
        {
            get { return __dict; }
            set
            {
                __dict = value;
            }
        }

        public CalendarConverter()
        {
            var list = new ObservableCollection<Food>();
            list.Add(new Food("第1号", new BitmapImage(new Uri("Resources/meat.png", UriKind.Relative)), 100f,
                DateTime.Today,
                DateTime.Today.AddDays(7)));

            dict.Add(DateTime.Today, list);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Food fod = null;
            //if (!dict.TryGetValue(DateTime.Parse(value.ToString()), out fod))
            // Fix 今の状態だと今月だけでしか正常に動かないので、DataContextにDateTimeが入る個所を探す
            //if (14 == int.Parse((string)value))
            var tmpDt = DateTime.Today;
            //var endDate = tmpDt.AddMonths(1).AddDays(-1);
            var endDate = new DateTime(tmpDt.Year, tmpDt.AddMonths(1).Month, 1).AddDays(-1);
            int convertedValue = int.Parse((string) value);
            if (endDate.Day < convertedValue)
            {
                return fod;
            }

            DateTime currDate = new DateTime(tmpDt.Year, tmpDt.Month, convertedValue);
            if (currDate.Year >= tmpDt.Year && this.dict.ContainsKey(currDate.Date))
            {
                return dict[currDate];
            }

            return fod;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Food) value);
        }

    }
}