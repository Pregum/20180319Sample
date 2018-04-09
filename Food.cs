using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180319Sample
{
    /// <summary>
    /// カレンダーにバインドさせるクラス
    /// </summary>
    public class CalendarFood
    {
        /// <summary>
        /// 各日にちのコレクション
        /// </summary>
        public IList<DayFood> DayFoods { get; set; }

        public CalendarFood()
        {
            this.DayFoods = new ObservableCollection<DayFood>();
        }
    }

    /// <summary>
    /// 1日の食材を格納しているクラス
    /// </summary>
    public class DayFood
    {
        /// <summary>
        /// 食材のコレクション
        /// </summary>
        public IList<Food> Foods { get; set; }

        /// <summary>
        /// 現在の日にち
        /// </summary>
        public DateTime CurrentDate { get; set; }

        public DayFood(DateTime currentDate)
        {
            this.Foods = new ObservableCollection<Food>();
            this.CurrentDate = currentDate;
        }
    }

    /// <summary>
    /// 1食材を表すクラス
    /// </summary>
    public class Food 
    {
        /// <summary>
        /// 食品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 食材画像
        /// </summary>
        public System.Windows.Controls.Image FoodIaImage { get; set; }

        /// <summary>
        /// 重量（ｇ）
        /// </summary>
        public float Weight { get; set; }

        /// <summary>
        /// 購入日
        /// </summary>
        public DateTime BoughtDate { get; set; }

        /// <summary>
        /// 期限日
        /// </summary>
        public DateTime LimitDate { get; set; }

        public Food(string name, System.Windows.Controls.Image foodIaImage, float weight, DateTime boughtDate, DateTime limitDate)
        {
            Name = name;
            FoodIaImage = foodIaImage;
            Weight = weight;
            this.BoughtDate = boughtDate;
            this.LimitDate = limitDate;
        }
    }
}
