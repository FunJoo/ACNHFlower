using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static AnimalCrossingFlower.Model.BaseModel;

namespace AnimalCrossingFlower.Model
{
    class ColorDic
    {
        private int id;
        public string ID
        {
            get { return id.ToString(); }
            set { int.TryParse(value, out id); }
        }
        private int a1;
        public string A1
        {
            get { return a1.ToString(); }
            set { int.TryParse(value, out a1); }
        }
        private int a2;
        public string A2
        {
            get { return a2.ToString(); }
            set { int.TryParse(value, out a2); }
        }
        private int a3;
        public string A3
        {
            get { return a3.ToString(); }
            set { int.TryParse(value, out a3); }
        }
        private int a4;
        public string A4
        {
            get { return a4.ToString(); }
            set { int.TryParse(value, out a4); }
        }
        private int pairs;
        public string Pairs
        {
            get { return pairs.ToString(); }
            set { int.TryParse(value, out pairs); }
        }
        private MyColor color;
        public string Color
        {
            get { return color.ToString(); }
            set { color = (MyColor)Enum.Parse(typeof(MyColor), value); }
        }
        private bool isseed;
        public string IsSeed
        {
            get
            {
                if (isseed) return "True";
                return "False";
            }
            set
            {
                if ("1" == value) isseed = true;
                isseed = false;
            }
        }
        private FlowerType type = FlowerType.Unknown;
        public string Type
        {
            get { return type.ToString(); }
            set
            {
                type = (FlowerType)Enum.Parse(typeof(FlowerType), value);
            }
        }
        public string Note { get; set; }
       
        public bool GetIsSeed()
        {
            return isseed;
        }

        public int[] GetIntArray()
        {
            if (pairs > 3) return new int[] { a1, a2, a3, a4 };
            return new int[] { a1, a2, a3 };
        }

        public Gene[] GetGeneArray()
        {
            if (pairs > 3) return new Gene[] { (Gene)a1, (Gene)a2, (Gene)a3, (Gene)a4 };
            return new Gene[] { (Gene)a1, (Gene)a2, (Gene)a3 };
        }

        public string[] GetGeneNameArray()
        {
            string gt = BaseModel.GeneType[type];
            string[] re;
            if (pairs > 3)
            {
                re = new string[] { ((Gene)a1).ToString(), ((Gene)a2).ToString(), ((Gene)a3).ToString(), ((Gene)a4).ToString() };
            }
            else
            {
                re = new string[] { ((Gene)a1).ToString(), ((Gene)a2).ToString(), ((Gene)a3).ToString() };
            }
            for(int i = 0; i < re.Length; i++)
            {
                re[i] = re[i].Replace('a', char.ToLower(gt[i]));
                re[i] = re[i].Replace('A', char.ToUpper(gt[i]));
            }
            return re;
        }

        public string GetGeneName()
        {
            var a = GetGeneNameArray();
            if (pairs > 3) return a[0] + a[1] + a[2] + a[3];
            return a[0] + a[1] + a[2];
        }

        public static List<ColorDic> GetColorDic()
        {
            try
            {
                string json = File.ReadAllText("flower.json");
                //这个类需要添加引用：System.Web.Extensions
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                var myresult = serializer.Deserialize<List<ColorDic>>(json);
                return myresult;
            }
            catch
            {
                GlobalTool.OpenDialogButton("flower.json 文件读取失败！");
                return new List<ColorDic>();
            }
        }

        public static List<ColorDic> GetColorDic(FlowerType ft)
        {
            string ftName = ft.ToString();
            List<ColorDic> rList = new List<ColorDic>();
            foreach(var cd in GetColorDic())
            {
                if (cd.Type == ftName) rList.Add(cd);
            }
            return rList;
        }
    }
}
