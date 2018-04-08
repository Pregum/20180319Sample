using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180319Sample
{
    class FoodItem
    {
        /// <summary>
        /// 食品名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 食材画像
        /// </summary>
        public Image FoodIaImage { get; set; }

        /// <summary>
        /// 重量（ｇ）
        /// </summary>
        public float Weight { get; set; }

        public FoodItem(string name, Image foodIaImage, float weight)
        {
            Name = name;
            FoodIaImage = foodIaImage;
            Weight = weight;
        }
    }
}
