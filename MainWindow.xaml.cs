using System;
using System.Collections.Generic;
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

namespace _20180319Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //private NavigationService _navi;

        public MainWindow()
        {
            InitializeComponent();
            //_navi = this.MyFrame.NavigationService;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //Page1 na = new Page1();
            //_navi.Navigate(na);

        }

        private void CalendarControl_OnCanvasClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //if (e.Source is UserControl1 hoge)
            //{
            //    var fuga = hoge.SelectedCanvas;
            //    BitmapImage bitmap = new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative));
            //    Image ima = new Image() { Source = bitmap };
            //    fuga.Children.Add(ima);
            //}


            //InfoControl.Label_FoodName.Content = "ほげ";
            InfoControl.Label_FoodName.Content = this.CalendarControl.SelectedDay;
            //InfoControl.Image_FoodIcon.Source = new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative));
            InfoControl.Image_FoodIcon.Source = new BitmapImage(new Uri("Resources/green_apple.png", UriKind.Relative));
            //InfoControl.Image_FoodIcon.Stretch = Stretch.Fill;

            if (DateTime.Now.Day == this.CalendarControl.SelectedDay)
            {
                InfoControl.Label_FoodName.Content = "おーいお茶";
                InfoControl.Image_FoodIcon.Source = new BitmapImage(new Uri("Resources/non_alcohol.png", UriKind.Relative));
                InfoControl.Label_Weight.Content = "100";
            }
        }
    }
}
