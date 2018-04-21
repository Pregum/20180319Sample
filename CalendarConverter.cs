using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Xml;
using _20180319Sample.Annotations;

namespace _20180319Sample
{
    [ValueConversion(typeof(DateTime), typeof(IList<Food>))]
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
            list.Add(new Food("第1号", new BitmapImage(new Uri("Resources/meat.png", UriKind.Relative)), 300f,
                DateTime.Today,
                DateTime.Today.AddDays(7)));

            var secondList = new ObservableCollection<Food>();
            secondList.Add(new Food("第2号", new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative)), 50f,
                new DateTime(2018, 4, 3),
                new DateTime(2018, 4, 3).AddDays(7)));

            dict.Add(DateTime.Today, list);
            dict.Add(new DateTime(2018, 4, 3), secondList);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                //return fod;
                return DependencyProperty.UnsetValue;
            }
            //if (!dict.TryGetValue(DateTime.Parse(value.ToString()), out fod))
            // Fix 今の状態だと今月だけでしか正常に動かないので、DataContextにDateTimeが入る個所を探す
            //if (14 == int.Parse((string)value))
            var tmpDt = DateTime.Today;
            //var endDate = tmpDt.AddMonths(1).AddDays(-1);
            var endDate = new DateTime(tmpDt.Year, tmpDt.AddMonths(1).Month, 1).AddDays(-1);
            //int convertedValue = int.Parse((string)value);
            //if (endDate.Day < convertedValue)
            //{
            //    return fod;
            //}

            //DateTime currDate = new DateTime(tmpDt.Year, tmpDt.Month, convertedValue);
            DateTime currDate = (DateTime)value;
            if (currDate.Year >= tmpDt.Year && this.dict.ContainsKey(currDate.Date))
            {
                //return dict[currDate][0].FoodImage;
                return dict[currDate];
            }

            //return fod;
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            return ((Food)value);
        }

    }
}