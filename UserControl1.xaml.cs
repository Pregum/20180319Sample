using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using _20180319Sample.Properties;

namespace _20180319Sample
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private Canvas _selectedCanvas = null;
        public Canvas SelectedCanvas
        {
            get { return _selectedCanvas; }
        }

        public int SelectedDay { get; private set; }

        public static readonly RoutedEvent CanvasClickEvent =
            EventManager.RegisterRoutedEvent("CanvasClick", RoutingStrategy.Tunnel, typeof(RoutedEventHandler),
                typeof(UserControl1));
        //typeof(Canvas));

        public event RoutedEventHandler CanvasClick
        {
            add => AddHandler(CanvasClickEvent, value);
            remove => RemoveHandler(CanvasClickEvent, value);
        }

        public UserControl1()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas != null)
            {
                var ima = ((Image)canvas.Children[0]);
                BitmapImage bitmap = new BitmapImage(new Uri("Resources/green_apple.png", UriKind.Relative));
                ima.Source = bitmap;
            }
            //// 赤くなる
            //canvas.Background = Brushes.Coral;

            //if (this._selectedCanvas != null)
            //{
            //this._selectedCanvas.Background = Brushes.AliceBlue;
            //}

            //this._selectedCanvas = canvas;

            //BitmapImage bitmap = new BitmapImage(new Uri("Resources/meat.png", UriKind.Relative));
            //Image ima = new Image() { Source = bitmap };
            //canvas?.Children.Add(ima);
            //var takeSelectedDate = this.take.SelectedDate;
            //if (takeSelectedDate != null) this.SelectedDay = takeSelectedDate.Value.Day;

            //e.Handled = true;
            //RaiseEvent(new CanvasEventArgs(UserControl1.CanvasClickEvent));
        }

        private void take_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.SelectedDay = this.take.SelectedDate.Value.Day;
            this.SelectedDay = ((DateTime)(e.AddedItems[0])).Day;
            RaiseEvent(new CanvasEventArgs(UserControl1.CanvasClickEvent));
        }
    }

    public class CanvasEventArgs : RoutedEventArgs
    {
        public CanvasEventArgs(RoutedEvent e) : base(e)
        {
        }
    }
}