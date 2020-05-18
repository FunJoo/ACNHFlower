using AnimalCrossingFlower.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Media;
using static AnimalCrossingFlower.Model.MyFlower;

namespace AnimalCrossingFlower.Helper
{
    class FlowerHelper
    {
        /// <summary>
        /// 内部存放Json的string，以免多次读取文件
        /// </summary>
        private static string TempJson = "";

        /// <summary>
        /// 获取Json内的所有花朵
        /// </summary>
        /// <returns>花朵列表</returns>
        public static List<MyFlower> GetFlowerAll()
        {
            try
            {
                if (TempJson == "") TempJson = File.ReadAllText("flower.json");

                //这个类需要添加引用：System.Web.Extensions
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var myresult = serializer.Deserialize<List<MyFlower>>(TempJson);
                return myresult;
            }
            catch
            {
                GlobalTool.OpenDialogButton("flower.json 文件读取失败！");
                return new List<MyFlower>();
            }
        }

        /// <summary>
        /// 获取部分花朵
        /// </summary>
        /// <param name="ft">花的种类</param>
        /// <returns>花朵列表</returns>
        public static List<MyFlower> GetFlowerPart(FlowerType ft)
        {
            string ftName = ft.ToString();
            List<MyFlower> rList = new List<MyFlower>();
            foreach (var cd in GetFlowerAll())
            {
                if (cd.Type == ftName) rList.Add(cd);
            }
            return rList;
        }

        /// <summary>
        /// 获取父本对列表
        /// </summary>
        /// <param name="flower">要获取父本的花朵</param>
        /// <returns>父本对列表</returns>
        public static List<MyFlower[]> GetMyParent(MyFlower flower)
        {
            int[] ig = flower.GetIntArray();

            if (ig.Length > 3)
            {
                if (ig == new int[] { 0, 0, 0, 0 }) return new List<MyFlower[]>();
                if ((ig[0] > 0 && ig[0] < 4) || (ig[1] > 0 && ig[1] < 4) || (ig[2] > 0 && ig[2] < 4) || (ig[3] > 0 && ig[3] < 4)) return new List<MyFlower[]>();
            }
            else
            {
                if (ig == new int[] { 0, 0, 0 }) return new List<MyFlower[]>();
                if ((ig[0] > 0 && ig[0] < 4) || (ig[1] > 0 && ig[1] < 4) || (ig[2] > 0 && ig[2] < 4)) return new List<MyFlower[]>();
                ig = new int[] { ig[0], ig[1], ig[2] };
            }

            List<List<int[]>> lli = new List<List<int[]>>();
            for (int i = 0; i < ig.Length; i++)
            {
                List<int[]> li = new List<int[]>();
                switch (ig[i])
                {
                    case 4:
                        li.Add(new int[] { 4, 4 });
                        li.Add(new int[] { 4, 5 });
                        li.Add(new int[] { 5, 5 });
                        break;
                    case 5:
                        li.Add(new int[] { 5, 5 });
                        li.Add(new int[] { 4, 6 });
                        li.Add(new int[] { 5, 6 });
                        break;
                    case 6:
                        li.Add(new int[] { 5, 5 });
                        li.Add(new int[] { 5, 6 });
                        li.Add(new int[] { 6, 6 });
                        break;
                    default:
                        li.Add(new int[] { 4, 4 });
                        li.Add(new int[] { 4, 5 });
                        li.Add(new int[] { 5, 5 });
                        li.Add(new int[] { 5, 6 });
                        li.Add(new int[] { 6, 6 });
                        li.Add(new int[] { 4, 6 });
                        break;
                }
                lli.Add(li);
            }

            List<MyFlower[]> result = new List<MyFlower[]>();
            int[] left, right;

            left = new int[lli.Count];
            right = new int[lli.Count];
            for (int i = 0; i < lli[0].Count; i++)
            {
                left = new int[lli.Count];
                right = new int[lli.Count];
                left[0] = lli[0][i][0];
                right[0] = lli[0][i][1];
                for(int j = 0; j < lli[1].Count; j++)
                {
                    left[1] = lli[1][j][0];
                    right[1] = lli[1][j][1];
                    for(int k = 0; k < lli[2].Count; k++)
                    {
                        left[2] = lli[2][k][0];
                        right[2] = lli[2][k][1];
                        if (lli.Count == 4)
                        {
                            for(int h = 0; h < lli[3].Count; h++)
                            {
                                left[3] = lli[3][h][0];
                                right[3] = lli[3][h][1];
                                MyFlower mfLeft = new MyFlower(
                                flower.GetFlowerType(),
                                (Gene)left[0],
                                (Gene)left[1],
                                (Gene)left[2],
                                (Gene)left[3]);
                                MyFlower mfRight = new MyFlower(
                                    flower.GetFlowerType(),
                                    (Gene)right[0],
                                    (Gene)right[1],
                                    (Gene)right[2],
                                    (Gene)right[3]);
                                MyFlower[] ff = new MyFlower[] { mfLeft, mfRight };
                                if (!result.Contains(ff) || !result.Contains(ff.Reverse())) result.Add(ff);
                            }
                        }
                        else
                        {
                            MyFlower mfLeft = new MyFlower(
                                flower.GetFlowerType(),
                                (Gene)left[0],
                                (Gene)left[1],
                                (Gene)left[2]);
                            MyFlower mfRight = new MyFlower(
                                flower.GetFlowerType(),
                                (Gene)right[0],
                                (Gene)right[1],
                                (Gene)right[2]);
                            MyFlower[] ff = new MyFlower[] { mfLeft, mfRight };
                            if (!result.Contains(ff) || !result.Contains(ff.Reverse())) result.Add(ff);
                        }
                    }
                }
            }
            return result;
        }

        #region Flower Dictionary

        /// <summary>
        /// 花色对应颜色
        /// </summary>
        public static Dictionary<MyColor, Color> ColorShow = new Dictionary<MyColor, Color>()
        {
            { MyColor.Unknown , Colors.Black },
            { MyColor.Blue,Colors.Blue},
            { MyColor.Dark,Colors.Black},
            { MyColor.Green,Colors.Green},
            { MyColor.Orange,Colors.Orange},
            { MyColor.Pink,Colors.HotPink},
            { MyColor.Purple,Colors.Purple},
            { MyColor.Red,Colors.Red},
            { MyColor.White,Colors.Gray},
            { MyColor.Yellow,Colors.Goldenrod},
            { MyColor.YellowRed,Colors.DarkGoldenrod},
            { MyColor.Gold,Colors.Gold},
        };

        /// <summary>
        /// 花色对应中文
        /// </summary>
        public static Dictionary<MyColor, string> ColorNameShow = new Dictionary<MyColor, string>()
        {
            { MyColor.Unknown , "未知" },
            { MyColor.Blue,"蓝色"},
            { MyColor.Dark,"黑色"},
            { MyColor.Green,"绿色"},
            { MyColor.Orange,"橘色"},
            { MyColor.Pink,"粉红色"},
            { MyColor.Purple,"紫色"},
            { MyColor.Red,"红色"},
            { MyColor.White,"白色"},
            { MyColor.Yellow,"黄色"},
            { MyColor.YellowRed,"红黄"},
            { MyColor.Gold,"金色"},
        };

        /// <summary>
        /// 花名对应中文
        /// </summary>
        public static Dictionary<FlowerType, string> FlowerNameShow = new Dictionary<FlowerType, string>()
        {
            {FlowerType.Cosmos,"波斯菊" },
            {FlowerType.Hyacinths,"风信子" },
            {FlowerType.Lilies,"百合" },
            {FlowerType.Mums,"菊花" },
            {FlowerType.Pansies,"三色堇" },
            {FlowerType.Roses,"玫瑰" },
            {FlowerType.Tulips,"郁金香" },
            {FlowerType.Unknown,"未知" },
            {FlowerType.Windflower,"银莲花" },
        };

        /// <summary>
        /// 花种对应配型
        /// </summary>
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

        #endregion
    }
}
