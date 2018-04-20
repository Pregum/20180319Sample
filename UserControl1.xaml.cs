using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace _20180319Sample
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private Canvas _selectedCanvas = null;
        //private DateTime _takeDisplayDate;

        public Canvas SelectedCanvas
        {
            get { return _selectedCanvas; }
        }

        public Food Fudo = null;

        public DateTime SelectedDay { get; private set; }

        public UserControl1()
        {
            InitializeComponent();

        }

        public static readonly RoutedEvent CanvasClickEvent =
            EventManager.RegisterRoutedEvent("CanvasClick", RoutingStrategy.Tunnel, typeof(RoutedEventHandler),
                typeof(UserControl1));

        public event RoutedEventHandler CanvasClick
        {
            add => AddHandler(CanvasClickEvent, value);
            remove => RemoveHandler(CanvasClickEvent, value);
        }

        private void Canvas_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            var canvas = sender as Canvas;
            if (canvas != null)
            {
                var ima = ((Image) canvas.Children[0]);
                BitmapImage bitmap = new BitmapImage(new Uri("Resources/green_apple.png", UriKind.Relative));
                ima.Source = bitmap;
            }
        }

        private void take_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.SelectedDay = this.take.SelectedDate.Value.Day;
            this.SelectedDay = ((DateTime) (e.AddedItems[0]));
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