using AnimalCrossingFlower.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossingFlower.Model
{
    class ChildCard
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="f">孩子花朵</param>
        /// <param name="probability">生成概率</param>
        public ChildCard(MyFlower f , float p)
        {
            Child = f;
            probability = p;
        }

        public MyFlower Child;

        private float probability;
        public string Probability => probability.ToString("#0.000");

        public string Name => FlowerHelper.FlowerNameShow[Child.GetFlowerType()] + " " + FlowerHelper.ColorNameShow[Child.GetColor()];
        public string Gene => Child.GetGeneName();
        public string ImagePath => Child.GetImagePath();
    }
}
