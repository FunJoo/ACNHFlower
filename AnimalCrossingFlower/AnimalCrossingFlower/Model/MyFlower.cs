using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalCrossingFlower.Model
{
    class MyFlower : BaseModel
    {
        public MyFlower(FlowerType flower, Gene a1, Gene a2, Gene a3, Gene a4 = Gene.Unknown)
        {
            MyType = flower;
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
        }

        public override List<BaseModel> GetChildren()
        {
            List<BaseModel> mList = new List<BaseModel>();
            if (A4 != Gene.Unknown)
            {
                if ((int)A1 < 4 || (int)A2 < 4 || (int)A3 < 4 || (int)A4 < 4) return mList;

                string[] a_1 = A1.ToString().Split();
                string[] a_2 = A2.ToString().Split();
                string[] a_3 = A3.ToString().Split();
                string[] a_4 = A4.ToString().Split();
                string[,] child = new string[4, 2] {
                { a_1[0],a_1[1]},
                { a_2[0],a_2[1]},
                { a_3[0],a_3[1]},
                { a_4[0],a_4[1]}
            };

                List<string> ls = new List<string>();
                Permutation(ref ls, child, "");
                foreach (var s in ls)
                {
                    string[] ms = s.Split();
                    MyFlower ll = new MyFlower(
                        MyType,
                        (Gene)Enum.Parse(typeof(Gene), ms[0]),
                        (Gene)Enum.Parse(typeof(Gene), ms[1]),
                        (Gene)Enum.Parse(typeof(Gene), ms[2]),
                        (Gene)Enum.Parse(typeof(Gene), ms[3])
                        );
                    mList.Add(ll);
                }
            }
            else
            {
                if ((int)A1 < 4 || (int)A2 < 4 || (int)A3 < 4) return mList;

                string[] a_1 = A1.ToString().Split();
                string[] a_2 = A2.ToString().Split();
                string[] a_3 = A3.ToString().Split();
                string[,] child = new string[3, 2] {
                { a_1[0],a_1[1]},
                { a_2[0],a_2[1]},
                { a_3[0],a_3[1]}
                };

                List<string> ls = new List<string>();
                Permutation(ref ls, child, "");
                foreach (var s in ls)
                {
                    string[] ms = s.Split();
                    MyFlower ll = new MyFlower(
                        MyType,
                        (Gene)Enum.Parse(typeof(Gene), ms[0]),
                        (Gene)Enum.Parse(typeof(Gene), ms[1]),
                        (Gene)Enum.Parse(typeof(Gene), ms[2])
                        );
                    mList.Add(ll);
                }
            }
            return mList;
        }

        public override MyColor GetColor()
        {
            var ml = ColorDic.GetColorDic(MyType);
            for (int i = 0; i < ml.Count; i++)
            {
                if (A4 != Gene.Unknown)
                {
                    if ((int)A1 == Convert.ToInt32(ml[i].A1) && (int)A2 == Convert.ToInt32(ml[i].A2) && (int)A3 == Convert.ToInt32(ml[i].A3) && (int)A4 == Convert.ToInt32(ml[i].A4))
                    {
                        return (MyColor)Enum.Parse(typeof(MyColor), ml[i].Color);
                    }
                }
                else
                {
                    if ((int)A1 == Convert.ToInt32(ml[i].A1) && (int)A2 == Convert.ToInt32(ml[i].A2) && (int)A3 == Convert.ToInt32(ml[i].A3))
                    {
                        return (MyColor)Enum.Parse(typeof(MyColor), ml[i].Color);
                    }
                }
            }
            return MyColor.Unknown;
        }

        public override int[] GetIntArray()
        {
            if (A4 != Gene.Unknown) return new int[] { (int)A1, (int)A2, (int)A3, (int)A4 };
            return new int[] { (int)A1, (int)A2, (int)A3 };
        }

        public override Gene[] GetGeneArray()
        {
            if (A4 != Gene.Unknown) return new Gene[] { A1, A2, A3, A4 };
            return new Gene[] { A1, A2, A3 };
        }

        public override int GetPairsNum()
        {
            if (A4 != Gene.Unknown) return 4;
            return 3;
        }
    }
}
