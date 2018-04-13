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
using Image = System.Drawing.Image;
using MessageBox = System.Windows.Forms.MessageBox;

namespace _20180319Sample
{
    /// <summary>
    /// FoodEditWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class FoodEditWindow : Window
    {
        public Food EditFood { get; set; }

        public FoodEditWindow()
        {
            InitializeComponent();

            BitmapImage ima = new BitmapImage(new Uri("Resources/green_apple.png", UriKind.Relative));

            string foodName = "みーと";

            DateTime boughtDate = DateTime.Today;

            DateTime limitDate = DateTime.Today.AddDays(7);

            this.EditFood = new Food(foodName, ima, 0, boughtDate, limitDate);
            this.DataContext = this.EditFood;

            //this.BoughtDate.SelectedDate = DateTime.Today;
            //this.LimitDate.SelectedDate = DateTime.Today.AddDays(7);

        }

        #region UserEvent

        public delegate void FoodCreatedEventHandler(object sender, FoodCreatedArgs e);

        public static readonly RoutedEvent FoodCreatedEvent =
            EventManager.RegisterRoutedEvent("FoodCreated", RoutingStrategy.Bubble, typeof(FoodCreatedEventHandler),
                typeof(FoodEditWindow));

        public event FoodCreatedEventHandler FoodCreated
        {
            add => AddHandler(FoodCreatedEvent, value);
            remove => RemoveHandler(FoodCreatedEvent, value);
        }

        #endregion


        /// <summary>
        /// アイコンが押されたときに発生するイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Icon_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(Properties.Resources.notImplementMessage);
        }

        /// <summary>
        /// 決定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            //// ここで食材を送る処理を追加
            //string foodName = this.FoodName.Text;
            //BitmapImage ima = this.FoodImage.Source;
            //ImageSource ima = this.FoodImage.Source;
            DateTime? boughtDateSelectedDate = this.BoughtDate.SelectedDate;
            DateTime boughtDate;
            if (boughtDateSelectedDate != null)
            {
                boughtDate = boughtDateSelectedDate.Value;
            }
            else
            {
                MessageBox.Show(Properties.Resources.boughtDateInvalid);
                return;
            }

            DateTime? limitDateSelectedDate = this.LimitDate.SelectedDate;
            DateTime limitDate;
            if (limitDateSelectedDate != null)
            {
                limitDate = limitDateSelectedDate.Value;
            }
            else
            {
                MessageBox.Show(Properties.Resources.limitDateInvalid);
                return;
            }

            if (boughtDate > limitDate)
            {
                MessageBox.Show(Properties.Resources.dateRelationWrong);
            }

            //BitmapImage toImage = ima.;

            //var food = new Food(foodName, toImage, 0, boughtDate, limitDate);

            var args = new FoodCreatedArgs(FoodEditWindow.FoodCreatedEvent, this.EditFood);
            RaiseEvent(args);

            //this.Dispatcher.InvokeShutdown();
            this.Close();
        }

        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            //this.DispatcheRoutedEventr.InvokeShutdown();
            this.Close();
        }
    }

    public class FoodCreatedArgs : RoutedEventArgs
    {
        public Food FoodInfo { get; set; }

        public FoodCreatedArgs(RoutedEvent e, Food food) : base(e)
        {
            this.FoodInfo = food;
        }

    }
}