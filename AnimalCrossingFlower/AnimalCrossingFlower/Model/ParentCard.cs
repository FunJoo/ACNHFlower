using AnimalCrossingFlower.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossingFlower.Model
{
    class ParentCard
    {
        public ParentCard(MyFlower[] parent)
        {
            List<MyFlower> Flowers = FlowerHelper.GetFlowerPart(parent[0].GetFlowerType());
            foreach (var f in Flowers)
            {
                if (f.A1 == parent[0].A1 && f.A2 == parent[0].A2 && f.A3 == parent[0].A3 && f.A4 == parent[0].A4) FlowerLeft = f;
                if (f.A1 == parent[1].A1 && f.A2 == parent[1].A2 && f.A3 == parent[1].A3 && f.A4 == parent[1].A4) FlowerRight = f;
            }
        }

        private MyFlower FlowerLeft = new MyFlower();
        private MyFlower FlowerRight = new MyFlower();

        public string ImagePathLeft => FlowerLeft.GetImagePath();

        public string ImagePathRight => FlowerRight.GetImagePath();

        public string TextNameLeft => FlowerHelper.FlowerNameShow[FlowerLeft.GetFlowerType()] + " " + FlowerHelper.ColorNameShow[FlowerLeft.GetColor()] ;

        public string TextNameRight => FlowerHelper.FlowerNameShow[FlowerRight.GetFlowerType()] + " " + FlowerHelper.ColorNameShow[FlowerRight.GetColor()] ;

        public string TextGeneLeft => FlowerLeft.GetGeneName() + (FlowerLeft.GetIsSeed() ? " 种子" : "");

        public string TextGeneRight => FlowerRight.GetGeneName() + (FlowerRight.GetIsSeed() ? " 种子" : "");

        /// <summary>
        /// 显示所有孩子的基因型，因为放在界面上有点鸡肋，PageParent里还是去掉了，功能暂时保留
        /// </summary>
        public string TextChildren
        {
            get
            {
                string s = "";
                var list = FlowerHelper.GetOurChildren(FlowerLeft, FlowerRight);
                foreach(var a in list)
                {
                    s += a.GetGeneName() + " ";
                }
                return s;
            }
        }
    }
}
