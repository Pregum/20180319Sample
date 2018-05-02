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
    //public class CalendarConverter : IValueConverter, INotifyPropertyChanged
    public class CalendarConverter : IValueConverter
    {
        /// <summary>
        /// key : 日付、value : 食材リストのディクショナリ
        /// </summary>
        private Dictionary<DateTime, ObservableCollection<Food>> _dict =
            new Dictionary<DateTime, ObservableCollection<Food>>();

        /// <summary>
        /// key : 日付、value : 食材リストのディクショナリ
        /// </summary>
        public Dictionary<DateTime, ObservableCollection<Food>> Dict
        {
            get { return _dict; }
            set
            {
                _dict = value;
                //OnPropertyChanged(nameof(Dict));
            }
        }

        private ObservableDictionary<DateTime, ObservableCollection<Food>> _observeTable =
            new ObservableDictionary<DateTime, ObservableCollection<Food>>();

        public ObservableDictionary<DateTime, ObservableCollection<Food>> ObserveTable
        {
            get { return _observeTable; }
            set { _observeTable = value; }
        }


        public CalendarConverter()
        {
            //// サンプル食材
            var list = new ObservableCollection<Food>
            {
                new Food("第1号", new BitmapImage(new Uri("Resources/meat.png", UriKind.Relative)), 300f,
                    DateTime.Today,
                    DateTime.Today.AddDays(7))
            };

            var secondList = new ObservableCollection<Food>
            {
                new Food("第2号", new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative)), 50f,
                    new DateTime(2018, 4, 3),
                    new DateTime(2018, 4, 3).AddDays(7))
            };

            //Dict.Add(DateTime.Today.AddDays(7), list);
            //Dict.Add(new DateTime(2018, 4, 3).AddDays(7), secondList);

            this.ObserveTable.Add(
                new KeyValuePair<DateTime, ObservableCollection<Food>>(DateTime.Today.AddDays(7), list));
            //Dict.Add(new DateTime(2018, 4, 3).AddDays(7), secondList);
            this.ObserveTable.Add(
                new KeyValuePair<DateTime, ObservableCollection<Food>>(new DateTime(2018, 4, 3).AddDays(7),
                    secondList));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //// DateTimeに変換可能ならcurrDateに変換
            if (value is DateTime currDate)
            {
                //if (this.Dict.ContainsKey(currDate.Date))
                //{
                //    return Dict[currDate];
                //}

                if (this.ObserveTable.Contains(currDate.Date))
                {
                    return ObserveTable[currDate].Value;
                }
            }

            // データがなければnullを返す.
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}