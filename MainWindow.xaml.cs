using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace _20180319Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 食材リスト
        /// </summary>
        private ObservableCollection<Food> _foodList = new ObservableCollection<Food>();

        /// <summary>
        /// 食材の追加を行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMenu_OnClick(object sender, RoutedEventArgs e)
        {
            var foodAddWindow = new FoodAddWindow();
            foodAddWindow.FoodCreated += FoodEditWindow_FoodCreated;
            foodAddWindow.ShowDialog();

            var hoge = (CalendarConverter) Application.Current.Resources["conv"];
            if (hoge.ObserveTable.Contains(foodAddWindow.EditFood.LimitDate.Date) == false)
            {
                hoge.ObserveTable.Add(foodAddWindow.EditFood.LimitDate.Date, this._foodList);
                this._foodList = new ObservableCollection<Food>();

                // HACK: 即時プロパティ変更の方法がわからなかったため、表示を変更させイベントが発生するように設定
                var date = this.CalendarControl.FoodCalendar.DisplayDate;
                var selectedDate = this.CalendarControl.FoodCalendar.SelectedDate;
                this.CalendarControl.FoodCalendar.DisplayDate = date.AddMonths(1);
                if (selectedDate.HasValue)
                {
                    this.CalendarControl.FoodCalendar.SelectedDate = selectedDate;
                }

                this.CalendarControl.FoodCalendar.DisplayDate = date;
            }
            else
            {
                if (this._foodList.Count <= 0) return;

                hoge.ObserveTable[foodAddWindow.EditFood.LimitDate.Date].Add(this._foodList[0]);
                this._foodList = new ObservableCollection<Food>();
            }
        }

        /// <summary>
        /// 食材が追加されたときに発生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoodEditWindow_FoodCreated(object sender, FoodCreatedArgs e)
        {
            this._foodList.Add(e.FoodInfo);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// カレンダーの日をクリック時、選択日が賞味期限日の食材リストを取得する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarControl_DayClick(object sender, RoutedEventArgs e)
        {
            var hoge = (CalendarConverter) Application.Current.Resources["conv"];
            var currDate = this.CalendarControl.SelectedDay;
            if (hoge.ObserveTable.Contains(currDate))
            {
                var tmp = hoge.ObserveTable[currDate];
                this.FoodInformation.DataContext = tmp;
            }
            else
            {
                this.FoodInformation.DataContext = null;
            }
        }

        /// <summary>
        /// 編集画面の表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMenu_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedDate = this.CalendarControl.SelectedDay;
            var convertDic = (CalendarConverter) Application.Current.Resources["conv"];
            if (convertDic.ObserveTable.Contains(selectedDate) && convertDic.ObserveTable[selectedDate].Any())
            {
                var hoge = new FoodEditWindow(this.FoodInformation.CurrentIndex)
                {
                    DataContext = this.FoodInformation.SelectedFood
                };
                hoge.ShowDialog();
                this.FoodInformation.DataContext = null;
                this.FoodInformation.DataContext =
                    convertDic.ObserveTable[selectedDate];
            }
            else
            {
                MessageBox.Show("食材が表示されている日付を選択してください.");
            }
        }
    }
}