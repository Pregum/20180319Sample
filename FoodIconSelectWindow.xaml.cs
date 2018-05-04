using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

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

            Init();
        }

        private void Init()
        {
            foreach (var file in Directory.EnumerateFiles(@"..\..\Resources\"))
            {
                //MessageBox.Show(file);
                var listBoxItem = new ListBoxItem();
                var ima = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(Path.GetFullPath(file), UriKind.Absolute)),
                    Height = 50,
                    Stretch = Stretch.Uniform
                };
                listBoxItem.Content = ima;
                this.ListBox.Items.Add(listBoxItem);
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
