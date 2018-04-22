using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace _20180319Sample
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        /// <summary>
        /// 選択されている日付
        /// </summary>
        public DateTime SelectedDay { get; private set; }

        public UserControl1()
        {
            InitializeComponent();
        }

        public static readonly RoutedEvent DayClickEvent =
            EventManager.RegisterRoutedEvent("DayClick", RoutingStrategy.Tunnel, typeof(RoutedEventHandler),
                typeof(UserControl1));

        public event RoutedEventHandler DayClick
        {
            add => AddHandler(DayClickEvent, value);
            remove => RemoveHandler(DayClickEvent, value);
        }

        private void take_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //this.SelectedDay = this.take.SelectedDate.Value.Day;
            this.SelectedDay = ((DateTime) (e.AddedItems[0]));
            RaiseEvent(new CanvasEventArgs(UserControl1.DayClickEvent));
        }
    }

    public class CanvasEventArgs : RoutedEventArgs
    {
        public CanvasEventArgs(RoutedEvent e) : base(e)
        {
        }
    }
}