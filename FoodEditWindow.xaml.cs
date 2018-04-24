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
using System.Windows.Shapes;

namespace _20180319Sample
{
    /// <summary>
    /// FoodEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodEditWindow : Window
    {
        /// <summary>
        /// 選択されている食材Index
        /// </summary>
        public int SelectedIndex { get; private set; }

        /// <summary>
        /// 変更前の期限日
        /// </summary>
        public DateTime PrevLimitDate { get; private set; }

        public FoodEditWindow(int selectedIndex)
        {
            InitializeComponent();

            this.SelectedIndex = selectedIndex;
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

        /// <summary>
        /// OKボタンクリック時のイベントです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            var dic = (CalendarConverter) App.Current.Resources["conv"];

            // 選択されている期限日が違う場合、Dictから削除し、変更後の期限日をKeyとしたコレクションに追加する
            if (this.PrevLimitDate.Date == this.LimitDate.SelectedDate.Value)
            {
                if (this.DataContext is Food food)
                {
                    dic.Dict[food.LimitDate][this.SelectedIndex] = food;
                }
                else
                {
                    throw new ArgumentException("DataContextが正しく設定されていません.");
                }
            }
            else
            {
                if (this.DataContext is Food food)
                {
                    //dic.Dict[this.LimitDate.SelectedDate.Value].RemoveAt(this.SelectedIndex);
                    dic.Dict[this.PrevLimitDate.Date].RemoveAt(this.SelectedIndex);

                    // コレクションがあれば、それに追加
                    if (dic.Dict.ContainsKey(this.LimitDate.SelectedDate.Value))
                    {
                        dic.Dict[this.LimitDate.SelectedDate.Value].Add(food);
                    }
                    else // コレクションがなければ新規追加
                    {
                        var foods = new ObservableCollection<Food>();
                        foods.Add(food);
                        dic.Dict.Add(this.LimitDate.SelectedDate.Value, foods);
                    }
                }
                else
                {
                    throw new ArgumentException("DataContextが正しく設定されていません.");
                }
            }

            this.Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            this.Close();
        }

        private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is Food food)
            {
                this.PrevLimitDate = food.LimitDate;
            }
            else
            {
                throw new ArgumentException("適切なDataContextが設定されていません.");
            }
        }
    }
}
