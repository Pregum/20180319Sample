using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using _20180319Sample.Annotations;

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
    public class Food : INotifyPropertyChanged
    {
        private string _name;

        /// <summary>
        /// 食品名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// 食材画像
        /// </summary>
        //public BitmapImage FoodImage { get; set; }
        private BitmapImage _foodImage;

        /// <summary>
        /// 食材画像
        /// </summary>
        public BitmapImage FoodImage
        {
            get { return _foodImage; }
            set
            {
                _foodImage = value;
                OnPropertyChanged(nameof(FoodImage));
            }
        }

        /// <summary>
        /// 重量（ｇ）
        /// </summary>
        //public float Weight { get; set; }
        private float _weight;

        public float Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }


        /// <summary>
        /// 購入日
        /// </summary>
        //public DateTime BoughtDate { get; set; }
        private DateTime _boughtDate;

        public DateTime BoughtDate
        {
            get { return _boughtDate; }
            set
            {
                _boughtDate = value;
                OnPropertyChanged(nameof(BoughtDate));
            }
        }

        /// <summary>
        /// 期限日
        /// </summary>
        //public DateTime LimitDate { get; set; }
        private DateTime _limitDate;

        public DateTime LimitDate
        {
            get { return _limitDate; }
            set
            {
                _limitDate = value;
                OnPropertyChanged(nameof(LimitDate));
            }
        }


        public Food(string name, BitmapImage foodImage, float weight, DateTime boughtDate, DateTime limitDate)
        {
            Name = name;
            FoodImage = foodImage;
            Weight = weight;
            this.BoughtDate = boughtDate;
            this.LimitDate = limitDate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}