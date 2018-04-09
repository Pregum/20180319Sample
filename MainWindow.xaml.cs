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

        public IList<Food> foodList = new ObservableCollection<Food>();

        //private void CalendarControl_OnCanvasClick(object sender, RoutedEventArgs e)
        //{
        //    FoodInfomation.Label_FoodName.Content = this.CalendarControl.SelectedDay;
        //    FoodInfomation.Image_FoodIcon.Source = new BitmapImage(new Uri("Resources/green_apple.png", UriKind.Relative));
        //    //FoodInfomation.Image_FoodIcon.Stretch = Stretch.Fill;

        //    if (DateTime.Now.Day == this.CalendarControl.SelectedDay)
        //    {
        //        FoodInfomation.Label_FoodName.Content = "おーいお茶";
        //        FoodInfomation.Image_FoodIcon.Source = new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative));
        //        FoodInfomation.Label_Weight.Content = "100";
        //    }
        //}
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

            MessageBox.Show("とじました");
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
    }
}
