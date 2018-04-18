using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

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

        public ObservableCollection<Food> foodList = new ObservableCollection<Food>();

        /// <summary>
        /// 食材の追加・編集を行います
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var foodEditWindow = new FoodEditWindow();
            foodEditWindow.FoodCreated += FoodEditWindow_FoodCreated;
            foodEditWindow.ShowDialog();
            //foodEditWindow.Show();

            //MessageBox.Show("とじました");

            //var hoge = Resources["conv"];
            var hoge = (CalendarConverter)App.Current.Resources["conv"];
            if (hoge.dict.ContainsKey(foodEditWindow.EditFood.LimitDate.Date) == false)
            {
                hoge.dict.Add(foodEditWindow.EditFood.LimitDate.Date,this.foodList);
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

        private void CalendarControl_CanvasClick(object sender, RoutedEventArgs e)
        {
             var hoge = (CalendarConverter)App.Current.Resources["conv"];

            var date = this.CalendarControl.SelectedDay;
            var currDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, date);
            //hoge.dict[]
            if (hoge.dict.ContainsKey(currDate))
            {
                var tmp = hoge.dict[currDate];
                this.FoodInfomation.Image_FoodIcon.Source =  tmp[0].FoodImage;
                this.FoodInfomation.Label_FoodName.Content = tmp[0].Name;
                this.FoodInfomation.Label_Weight.Content = tmp[0].Weight;
            }
            else
            {
                
                //this.FoodInfomation.Image_FoodIcon.Source =  
                this.FoodInfomation.Label_FoodName.Content = "???";
                this.FoodInfomation.Label_Weight.Content = "???";
            }
        }
    }

}
