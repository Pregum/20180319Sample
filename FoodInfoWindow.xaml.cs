using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UserControl = System.Windows.Controls.UserControl;

namespace _20180319Sample
{
    /// <summary>
    /// UserControl2.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        /// <summary>
        /// 現在表示している食材インデックス
        /// </summary>
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
            if (this.DataContext is ObservableCollection<Food> list)
            {
                this.CurrentIndex = list.Count > this.CurrentIndex + 1 ? this.CurrentIndex + 1 : 0;
                this.Image_FoodIcon.Source = list[this.CurrentIndex].FoodImage;
                this.Label_FoodName.Content = list[this.CurrentIndex].Name;
                this.Label_Weight.Content = list[this.CurrentIndex].Weight;
                this.Label_BoughtDate.Content = list[this.CurrentIndex].BoughtDate.Date;
            }
        }

        /// <summary>
        /// DataContextが変更された時、発生します。
        /// 値が入っていない場合は発生しません。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is ObservableCollection<Food> list)
            {
                if (list.Count > 0)
                {
                    this.Image_FoodIcon.Source = list[0].FoodImage;
                    this.Label_FoodName.Content = list[0].Name;
                    this.Label_Weight.Content = list[0].Weight;
                    this.Label_BoughtDate.Content = list[0].BoughtDate.Date;
                }
            }
            else
            {
                //// FIXME: 食材が存在しない場合question.pngを表示させるようにする.
                var hoge = new BitmapImage(new Uri("Resources/question.png", UriKind.Relative));
                this.Image_FoodIcon.Source = hoge;
                //this.Image_FoodIcon.SetValue(Image.SourceProperty,
                //    new BitmapImage(new Uri("Resources/question.png", UriKind.Relative)));
                //MessageBox.Show(this.Image_FoodIcon.GetValue(Image.SourceProperty).ToString());
                this.Label_FoodName.Content = "???";
                this.Label_Weight.Content = "???";
                this.Label_BoughtDate.Content = "???";
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
            if (this.DataContext is ObservableCollection<Food> list)
            {
                this.CurrentIndex = 0 <= this.CurrentIndex - 1 ? this.CurrentIndex - 1 : list.Count - 1;
                this.Image_FoodIcon.Source = list[this.CurrentIndex].FoodImage;
                this.Label_FoodName.Content = list[this.CurrentIndex].Name;
                this.Label_Weight.Content = list[this.CurrentIndex].Weight;
                this.Label_BoughtDate.Content = list[this.CurrentIndex].BoughtDate.Date;
            }
        }
    }
}