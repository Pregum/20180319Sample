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
using System.Windows.Shapes;

namespace _20180319Sample
{
    /// <summary>
    /// FoodEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodEditWindow : Window
    {
        public FoodEditWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// アイコンをクリックしたとき変更します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new FoodIconSelectWindow();
            window.ShowDialog();
            var img = window.SelectedImage;
            if (img != null)
            {
                this.FoodImage.Source = img.Source;
            }
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            this.Close();
        }
    }
}
