using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace _20180319Sample
{
    /// <summary>
    /// UserControl2.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl2 : UserControl
    {

        private int CurrentIndex { get; set; }

        public UserControl2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 現在表示している食材リストで一つ後の食材を表示します。
        /// 現在表示している食材が一番最後の場合、一番最初の食材が表示されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //// implement: ここで食材の詳細表示を変更する

            //var list = this.DataContext;
            if (this.DataContext is ObservableCollection<Food>)
            {
                var list = (ObservableCollection<Food>)this.DataContext;
                //if (list.Count > this.CurrentIndex + 1)
                //{
                this.CurrentIndex = list.Count > this.CurrentIndex + 1 ? this.CurrentIndex + 1 : 0;
                    this.Image_FoodIcon.Source = list[this.CurrentIndex].FoodImage;
                    this.Label_FoodName.Content = list[this.CurrentIndex].Name;
                    this.Label_Weight.Content = list[this.CurrentIndex].Weight;
                //}
            }
        }

        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is ObservableCollection<Food>)
            {
                var list = (ObservableCollection<Food>)this.DataContext;
                if (list.Count > 0)
                {
                    this.Image_FoodIcon.Source = list[0].FoodImage;
                    this.Label_FoodName.Content = list[0].Name;
                    this.Label_Weight.Content = list[0].Weight;
                }
            }
            else
            {
                //// IMPLEMENT: デフォルトの画像を用意する
                //this.Image_FoodIcon.Source = 
                this.Label_FoodName.Content = "???";
                this.Label_Weight.Content = "???";
            }

            this.CurrentIndex = 0;
        }

        /// <summary>
        /// 現在表示している食材リストで一つ前の食材を表示します。
        /// 現在表示している食材が一番最初の場合、一番最後の食材が表示されます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ObservableCollection<Food>)
            {
                var list = (ObservableCollection<Food>)this.DataContext;
                //if (list.Count > this.CurrentIndex + 1)
                //{
                this.CurrentIndex = 0 <= this.CurrentIndex - 1 ? this.CurrentIndex - 1 : list.Count - 1;
                    this.Image_FoodIcon.Source = list[this.CurrentIndex].FoodImage;
                    this.Label_FoodName.Content = list[this.CurrentIndex].Name;
                    this.Label_Weight.Content = list[this.CurrentIndex].Weight;
                //}
            }
        }
    }
}
