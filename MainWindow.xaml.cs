using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
        public ObservableCollection<Food> foodList = new ObservableCollection<Food>();

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

            var hoge = (CalendarConverter)Application.Current.Resources["conv"];
            //if (hoge.Dict.ContainsKey(foodAddWindow.EditFood.LimitDate.Date) == false)
            if(hoge.ObserveTable.Contains(foodAddWindow.EditFood.LimitDate.Date) == false)
            {
                //hoge.Dict.Add(foodAddWindow.EditFood.LimitDate.Date, this.foodList);
                hoge.ObserveTable.Add(new KeyValuePair<DateTime, ObservableCollection<Food>>(foodAddWindow.EditFood.LimitDate.Date, this.foodList));
                this.foodList = new ObservableCollection<Food>();
            }
            else
            {
                if (this.foodList.Count <= 0) return;

                //hoge.Dict[foodAddWindow.EditFood.LimitDate.Date].Add(this.foodList[0]);
                hoge.ObserveTable[foodAddWindow.EditFood.LimitDate.Date].Value.Add(this.foodList[0]);
                this.foodList = new ObservableCollection<Food>();
            }
        }

        /// <summary>
        /// 食材が追加されたときに発生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoodEditWindow_FoodCreated(object sender, FoodCreatedArgs e)
        {
            this.foodList.Add(e.FoodInfo);
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
            var hoge = (CalendarConverter)Application.Current.Resources["conv"];
            var currDate = this.CalendarControl.SelectedDay;
            //if (hoge.Dict.ContainsKey(currDate))
            if(hoge.ObserveTable.Contains(currDate))
            {
                //var tmp = hoge.Dict[currDate];
                var tmp = hoge.ObserveTable[currDate].Value;
                this.FoodInformation.DataContext = tmp;
            }
            else
            {
                //this.FoodInformation.DataContext = DependencyProperty.UnsetValue;
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
            //if (convertDic.Dict.ContainsKey(selectedDate) && convertDic.Dict[selectedDate].Any())
            if (convertDic.ObserveTable.Contains(selectedDate) && convertDic.ObserveTable[selectedDate].Value.Any())
            {
                var hoge = new FoodEditWindow(this.FoodInformation.CurrentIndex);
                //hoge.DataContext = this.FoodInformation.DataContext;
                hoge.DataContext = this.FoodInformation.SelectedFood;
                hoge.ShowDialog();
            }
            else
            {
                MessageBox.Show("食材が表示されている日付を選択してください.");
            }

        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }

}
