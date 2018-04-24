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
    /// FoodIconSelectWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodIconSelectWindow : Window
    {
        public Image SelectedImage { get; set; }

        public FoodIconSelectWindow()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBoxItem lbi)
            {
                MessageBox.Show(lbi.Content.ToString());
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox lb)
            {
                if (lb.SelectedItems[0] is ListBoxItem lbi)
                {
                    if (lbi.Content is Image img)
                    {
                        this.SelectedImage = img;
                        this.Close();
                    }
                }
            }
        }
    }
}
