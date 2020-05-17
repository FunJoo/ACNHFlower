using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossingFlower.Model
{
    abstract class BaseModel
    {
        public FlowerType MyType;
        public Gene A1;
        public Gene A2;
        public Gene A3;
        public Gene A4;

        abstract public int GetPairsNum();
        abstract public List<BaseModel> GetChildren();
        abstract public MyColor GetColor();
        abstract public int[] GetIntArray();
        abstract public Gene[] GetGeneArray();

        public enum Gene
        {
            Unknown = 0,
            a = 2,
            A = 3,
            aa = 4,
            Aa = 5,
            AA = 6
        }

        public enum MyColor
        {
            Unknown,
            White,
            Yellow,
            Red,
            Pink,
            Dark,
            Orange,
            Blue,
            Purple,
            Green,
            YellowRed,
            Gold
        }

        public enum FlowerType
        {
            Unknown,
            Cosmos,
            Hyacinths,
            Lilies,
            Mums,
            Pansies,
            Roses,
            Tulips,
            Windflower
        }

        public static Dictionary<FlowerType, string> GeneType = new Dictionary<FlowerType, string>()
        {
            {FlowerType.Unknown,"" },
            {FlowerType.Cosmos,"RYS" },
            {FlowerType.Hyacinths,"RYW" },
            {FlowerType.Lilies,"RYS" },
            {FlowerType.Mums,"RYW" },
            {FlowerType.Pansies,"RYW" },
            {FlowerType.Roses,"RYWS" },
            {FlowerType.Tulips,"RYS" },
            { FlowerType.Windflower,"ROW"}
        };

        /// <summary>
        /// 获得子配型专用排列组合
        /// </summary>
        /// <param name="ls">存放配型的列表</param>
        /// <param name="arr">二维数组</param>
        /// <param name="rs">传递的字符</param>
        /// <param name="_m">处于第几行的指示</param>
        protected void Permutation(ref List<string> ls, string[,] arr, string rs = "", int _m = 0)
        {
            string s = rs;
            if (_m < arr.GetLength(0))
            {
                rs += arr[_m, 0];
                Permutation(ref ls, arr, rs, _m + 1);
                s += arr[_m, 1];
                Permutation(ref ls, arr, s, _m + 1);
            }
            else
            {
                if (ls.Contains(rs)) ls.Add(rs);
            }
        }
    }
}
