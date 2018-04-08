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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _20180319Sample
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class Page1 : Page
    {
        private Rectangle _selectedRectangle;

        public Page1()
        {
            InitializeComponent();

            //// 当月の月初めを取得
             var firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            //// 曜日番号の取得
            int dayOfWeek =  (int)firstDay.DayOfWeek;

            //// 月末を取得
            int lastDay = firstDay.AddMonths(1).AddDays(-1).Day;

            //// 1日から月末までループ
            for (int i = 1; i <= lastDay; i++)
            {
                //// セル位置
                int cellIndex = (i - 1) + dayOfWeek;
                //// 列位置
                int columnIndex = cellIndex % 7;
                //// 行位置
                int rowIndex = cellIndex / 7;

                //// 土日は文字色を変更する
                Color color = Colors.Black;
                if (columnIndex == 0) //// 日曜の場合
                {
                    color = Colors.Red;
                }
                else if (columnIndex == 6) //// 土曜の場合
                {
                    color = Colors.Blue;
                }

                //// 日付用コントロールの生成
                var aDayControl = new TextBlock()
                {
                    Text = string.Format($"{i}"),
                    FontSize = 12,
                    Foreground = new SolidColorBrush(color),
                    Padding = new Thickness(0, 10, 10,0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Top,
                };
                this.CalendarGrid.Children.Add(aDayControl);
                aDayControl.SetValue(Grid.ColumnProperty, columnIndex);
                aDayControl.SetValue(Grid.RowProperty, rowIndex + 1);
                aDayControl.SetValue(Panel.ZIndexProperty, 5);

                //// 四角形を生成しグリッドに追加
                var rectangle = new Rectangle
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Fill = Brushes.Transparent,
                    Margin = columnIndex == 6 ? new Thickness(0, -1, 0, 0) : new Thickness(0, -1, -1, 0)
                };
                //// 背景色を設定しないとイベントを検知しないため
                //// 土曜以外はマージンを１多く持つ
                //// イベント設定
                rectangle.MouseLeftButtonDown += day_MouseLeftButtonDown;
                this.CalendarGrid.Children.Add(rectangle);
                rectangle.SetValue(Grid.ColumnProperty, columnIndex);
                rectangle.SetValue(Grid.RowProperty, rowIndex + 1);
            }
        }

        private void day_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //// すでに選択されていれば初期化
            if (this._selectedRectangle != null)
            {
                _selectedRectangle.Fill = Brushes.Transparent;
                _selectedRectangle.StrokeDashArray = null;
                _selectedRectangle.StrokeThickness = 0;
            }

            //// 四角形の背景色に薄ピンクを設定
            Rectangle rec = sender as Rectangle;
            rec.Fill = Brushes.Pink;
            rec.SetValue(Panel.ZIndexProperty, 2);
            this._selectedRectangle = rec;
        }
    }
}
