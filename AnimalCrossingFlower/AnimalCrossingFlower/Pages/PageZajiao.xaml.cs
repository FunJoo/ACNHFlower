using AnimalCrossingFlower.Helper;
using AnimalCrossingFlower.Model;
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
using static AnimalCrossingFlower.Model.MyFlower;

namespace AnimalCrossingFlower.Pages
{
    /// <summary>
    /// PageZajiao.xaml 的交互逻辑
    /// </summary>
    public partial class PageZajiao : Page
    {
        public PageZajiao()
        {
            InitializeComponent();
            BindFlower();
            BindGene();
        }

        /// <summary>
        /// 设置了哪种花朵
        /// </summary>
        private FlowerType SelectedFlower;
        /// <summary>
        /// 设置了哪种花色
        /// </summary>
        private MyColor SelectedColor;
        /// <summary>
        /// 所设置花朵的所有ColorDic
        /// </summary>
        private List<MyFlower> ListColorDic;

        private List<MyFlower> SelectedColorDic;
        private List<string> ListColor;
        private List<string> ListColorName;

        /// <summary>
        /// 种子列表
        /// </summary>
        private List<MyFlower> ListSeed;

        private void BindFlower()
        {
            List<string> Flowers = new List<string>();
            foreach (int i in Enum.GetValues(typeof(FlowerType)))
            {
                if (i == 0) continue;
                Flowers.Add(FlowerHelper.FlowerNameShow[(FlowerType)i]);
            }
            ComboBoxChoose.ItemsSource = Flowers;
            ComboBoxChoose.SelectionChanged += GroupChanged;
            ComboBoxChoose.SelectedIndex = 0;

            CheckBoxColorL.IsChecked = true;
            CheckBoxColorR.IsChecked = true;
        }

        private void BindSeed()
        {
            ListSeed = new List<MyFlower>();
            foreach(var a in ListColorDic)
            {
                if (a.GetIsSeed()) ListSeed.Add(a);
            }
            List<string> ListSeedName = new List<string>();
            ListSeedName.Add("无");
            foreach(var a in ListSeed)
            {
                string s = FlowerHelper.FlowerNameShow[a.GetFlowerType()] + " " + FlowerHelper.ColorNameShow[a.GetColor()] + " " + a.GetGeneName();
                ListSeedName.Add(s);
            }
            ComboBoxSeedL.ItemsSource = ListSeedName;
            ComboBoxSeedR.ItemsSource = ListSeedName;
            ComboBoxSeedL.SelectedIndex = 0;
            ComboBoxSeedR.SelectedIndex = 0;
            ComboBoxSeedL.SelectionChanged += GroupChanged;
            ComboBoxSeedR.SelectionChanged += GroupChanged;
        }

        private void BindGene()
        {
            List<string> Genes = new List<string>();
            foreach (int i in Enum.GetValues(typeof(Gene)))
            {
                if (i == 0 || i > 3) Genes.Add(((Gene)i).ToString());
            }
            ComboBoxA1L.ItemsSource = Genes;
            ComboBoxA2L.ItemsSource = Genes;
            ComboBoxA3L.ItemsSource = Genes;
            ComboBoxA4L.ItemsSource = Genes;
            ComboBoxA1L.SelectedIndex = 0;
            ComboBoxA2L.SelectedIndex = 0;
            ComboBoxA3L.SelectedIndex = 0;
            ComboBoxA4L.SelectedIndex = 0;
            ComboBoxA1L.SelectionChanged += GroupChanged;
            ComboBoxA2L.SelectionChanged += GroupChanged;
            ComboBoxA3L.SelectionChanged += GroupChanged;
            ComboBoxA4L.SelectionChanged += GroupChanged;

            ComboBoxA1R.ItemsSource = Genes;
            ComboBoxA2R.ItemsSource = Genes;
            ComboBoxA3R.ItemsSource = Genes;
            ComboBoxA4R.ItemsSource = Genes;
            ComboBoxA1R.SelectedIndex = 0;
            ComboBoxA2R.SelectedIndex = 0;
            ComboBoxA3R.SelectedIndex = 0;
            ComboBoxA4R.SelectedIndex = 0;
            ComboBoxA1R.SelectionChanged += GroupChanged;
            ComboBoxA2R.SelectionChanged += GroupChanged;
            ComboBoxA3R.SelectionChanged += GroupChanged;
            ComboBoxA4R.SelectionChanged += GroupChanged;
        }

        private void GroupChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            string senderName = cb.Name;
            switch (senderName)
            {
                case "ComboBoxChoose":
                    {
                        SelectedFlower = (FlowerType)cb.SelectedIndex + 1;

                        TextBlockTypeL.Text = FlowerHelper.FlowerNameShow[SelectedFlower];
                        TextBlockTypeR.Text = TextBlockTypeL.Text;

                        ListColorDic = new List<MyFlower>();
                        ListColorDic = FlowerHelper.GetFlowerPart(SelectedFlower);

                        ListColor = new List<string>();
                        ListColorName = new List<string>();
                        ListColor.Add("");
                        ListColorName.Add("无");
                        foreach (var a in ListColorDic)
                        {
                            if (!ListColor.Contains(a.Color) && a.Color != "Unknown")
                            {
                                ListColor.Add(a.Color);
                                MyColor mc = (MyColor)Enum.Parse(typeof(MyColor), a.Color);
                                ListColorName.Add(FlowerHelper.ColorNameShow[mc]);
                            }
                        }

                        ComboBoxColorL.SelectionChanged -= GroupChanged;
                        ComboBoxColorR.SelectionChanged -= GroupChanged;
                        ComboBoxColorL.ItemsSource = ListColorName;
                        ComboBoxColorR.ItemsSource = ListColorName;
                        ComboBoxColorL.SelectedIndex = 0;
                        ComboBoxColorR.SelectedIndex = 0;
                        ComboBoxColorL.SelectionChanged += GroupChanged;
                        ComboBoxColorR.SelectionChanged += GroupChanged;
                        ComboBoxColorL.SelectedIndex = 1;
                        ComboBoxColorR.SelectedIndex = 1;

                        CheckBoxColorL.IsChecked = true;
                        CheckBoxColorR.IsChecked = true;

                        //需要在选定花朵之后绑定种子
                        BindSeed();
                    }
                    break;
                case "ComboBoxColorL":
                    {
                        if (cb.SelectedIndex == 0) return;

                        SelectedColor = (MyColor)Enum.Parse(typeof(MyColor), ListColor[cb.SelectedIndex]);

                        TextBlockColorL.Text = FlowerHelper.ColorNameShow[SelectedColor];
                        TextBlockColorL.Foreground = new SolidColorBrush(FlowerHelper.ColorShow[SelectedColor]);
                        ImageFlowerL.Source = new BitmapImage(new Uri("/Assets/" + SelectedFlower.ToString() + SelectedColor.ToString() + ".png", UriKind.Relative));

                        string s = "";
                        SelectedColorDic = new List<MyFlower>();
                        foreach (var a in ListColorDic)
                        {
                            if (a.Color == ListColor[cb.SelectedIndex])
                            {
                                SelectedColorDic.Add(a);
                                string n = a.GetGeneName();
                                if (!s.Contains(n)) s += n + " ";
                            }
                        }
                        TextBlockGeneL.Text = s;
                    }
                    break;
                case "ComboBoxColorR":
                    {
                        if (cb.SelectedIndex == 0) return;

                        SelectedColor = (MyColor)Enum.Parse(typeof(MyColor), ListColor[cb.SelectedIndex]);

                        TextBlockColorR.Text = FlowerHelper.ColorNameShow[SelectedColor];
                        TextBlockColorR.Foreground = new SolidColorBrush(FlowerHelper.ColorShow[SelectedColor]);
                        ImageFlowerR.Source = new BitmapImage(new Uri("/Assets/" + SelectedFlower.ToString() + SelectedColor.ToString() + ".png", UriKind.Relative));

                        string s = "";
                        SelectedColorDic = new List<MyFlower>();
                        foreach (var a in ListColorDic)
                        {
                            if (a.Color == ListColor[cb.SelectedIndex])
                            {
                                SelectedColorDic.Add(a);
                                string n = a.GetGeneName();
                                if (!s.Contains(n)) s += n + " ";
                            }
                        }
                        TextBlockGeneR.Text = s;
                    }
                    break;
                case "ComboBoxA1L":
                case "ComboBoxA2L":
                case "ComboBoxA3L":
                case "ComboBoxA4L":
                    {
                        if (CheckBoxGeneL.IsChecked == false) return;

                        int a1 = ComboBoxA1L.SelectedIndex;
                        int a2 = ComboBoxA2L.SelectedIndex;
                        int a3 = ComboBoxA3L.SelectedIndex;
                        int a4 = ComboBoxA4L.SelectedIndex;
                        int aa1 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA1L.SelectedItem.ToString());
                        int aa2 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA2L.SelectedItem.ToString());
                        int aa3 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA3L.SelectedItem.ToString());
                        int aa4 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA4L.SelectedItem.ToString());

                        if (
                            (a1 > 0 && a2 > 0 && a3 > 0 && SelectedFlower != FlowerType.Roses)
                            ||
                            (a1 > 0 && a2 > 0 && a3 > 0 && a4 > 0)
                            )
                        {
                            MyFlower cd = new MyFlower();
                            foreach (var a in ListColorDic)
                            {
                                if (SelectedFlower == FlowerType.Roses)
                                {
                                    if (a.A1 == aa1.ToString() && a.A2 == aa2.ToString() && a.A3 == aa3.ToString() && a.A4 == aa4.ToString())
                                    {
                                        cd = a;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (a.A1 == aa1.ToString() && a.A2 == aa2.ToString() && a.A3 == aa3.ToString())
                                    {
                                        cd = a;
                                        break;
                                    }
                                }

                            }
                            TextBlockGeneL.Text = cd.GetGeneName();
                            TextBlockColorL.Text = FlowerHelper.ColorNameShow[(MyColor)Enum.Parse(typeof(MyColor), cd.Color)];
                            TextBlockColorL.Foreground = new SolidColorBrush(FlowerHelper.ColorShow[(MyColor)Enum.Parse(typeof(MyColor), cd.Color)]);

                            string path = cd.GetImagePath();
                            ImageFlowerL.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                        }
                    }
                    break;
                case "ComboBoxA1R":
                case "ComboBoxA2R":
                case "ComboBoxA3R":
                case "ComboBoxA4R":
                    {
                        if (CheckBoxGeneR.IsChecked == false) return;

                        int a1 = ComboBoxA1R.SelectedIndex;
                        int a2 = ComboBoxA2R.SelectedIndex;
                        int a3 = ComboBoxA3R.SelectedIndex;
                        int a4 = ComboBoxA4R.SelectedIndex;
                        int aa1 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA1R.SelectedItem.ToString());
                        int aa2 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA2R.SelectedItem.ToString());
                        int aa3 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA3R.SelectedItem.ToString());
                        int aa4 = (int)(Gene)Enum.Parse(typeof(Gene), ComboBoxA4R.SelectedItem.ToString());

                        if (
                            (a1 > 0 && a2 > 0 && a3 > 0 && SelectedFlower != FlowerType.Roses)
                            ||
                            (a1 > 0 && a2 > 0 && a3 > 0 && a4 > 0)
                            )
                        {
                            MyFlower cd = new MyFlower();
                            foreach (var a in ListColorDic)
                            {
                                if (SelectedFlower == FlowerType.Roses)
                                {
                                    if (a.A1 == aa1.ToString() && a.A2 == aa2.ToString() && a.A3 == aa3.ToString() && a.A4 == aa4.ToString())
                                    {
                                        cd = a;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (a.A1 == aa1.ToString() && a.A2 == aa2.ToString() && a.A3 == aa3.ToString())
                                    {
                                        cd = a;
                                        break;
                                    }
                                }
                            }
                            TextBlockGeneR.Text = cd.GetGeneName();
                            TextBlockColorR.Text = FlowerHelper.ColorNameShow[(MyColor)Enum.Parse(typeof(MyColor), cd.Color)];
                            TextBlockColorR.Foreground = new SolidColorBrush(FlowerHelper.ColorShow[(MyColor)Enum.Parse(typeof(MyColor), cd.Color)]);

                            string path = cd.GetImagePath();
                            ImageFlowerR.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                        }
                    }
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string senderName = (sender as Button).Name;
            switch (senderName)
            {
                /*
                case "ButtonSearch":
                    {
                        if (CheckBoxColor.IsChecked == true)
                        {//按颜色查父本
                            if (ComboBoxColor.SelectedIndex == 0)
                            {
                                GlobalTool.OpenDialogButton("没有选择颜色");
                                return;
                            }
                            if (SelectedColorDic == null || SelectedColorDic.Count == 0) return;
                            ObservableCollection<ParentCard> reCard = new ObservableCollection<ParentCard>();
                            foreach (var everyflower in SelectedColorDic)
                            {
                                var parent = FlowerHelper.GetMyParent(everyflower);
                                foreach (var a in parent)
                                {
                                    var aa = new ParentCard(a);
                                    if (reCard.Count == 0)
                                    {
                                        reCard.Add(aa);
                                    }
                                    else
                                    {
                                        bool inResult = false;
                                        foreach (var b in reCard)
                                        {
                                            if (b.TextGeneLeft == aa.TextGeneLeft && b.TextGeneRight == aa.TextGeneRight) inResult = true;
                                        }
                                        if (!inResult) reCard.Add(aa);
                                    }
                                }
                            }
                            ListViewParent.ItemsSource = reCard;
                            ListViewParent.ScrollIntoView(ListViewParent.Items[0]);
                        }
                        if (CheckBoxGene.IsChecked == true)
                        {//按基因型查父本
                            Gene a1 = (Gene)Enum.Parse(typeof(Gene), ComboBoxA1.SelectedItem.ToString());
                            Gene a2 = (Gene)Enum.Parse(typeof(Gene), ComboBoxA2.SelectedItem.ToString());
                            Gene a3 = (Gene)Enum.Parse(typeof(Gene), ComboBoxA3.SelectedItem.ToString());
                            Gene a4 = (Gene)Enum.Parse(typeof(Gene), ComboBoxA4.SelectedItem.ToString());
                            MyFlower f = new MyFlower(SelectedFlower, a1, a2, a3, a4);
                            var re = FlowerHelper.GetMyParent(f);
                            ObservableCollection<ParentCard> reCard = new ObservableCollection<ParentCard>();
                            foreach (var a in re)
                            {
                                reCard.Add(new ParentCard(a));
                            }
                            ListViewParent.ItemsSource = reCard;
                            ListViewParent.ScrollIntoView(ListViewParent.Items[0]);
                        }
                    }
                    break;
                    */
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == false) return;
            string senderName = cb.Name;
            switch (senderName)
            {
                case "CheckBoxColorL":
                    CheckBoxGeneL.IsChecked = false;
                    CheckBoxSeedL.IsChecked = false;
                    ComboBoxA1L.SelectedIndex = 0;
                    ComboBoxA2L.SelectedIndex = 0;
                    ComboBoxA3L.SelectedIndex = 0;
                    ComboBoxA4L.SelectedIndex = 0;
                    ComboBoxSeedL.SelectedIndex = 0;
                    break;
                case "CheckBoxColorR":
                    CheckBoxGeneR.IsChecked = false;
                    CheckBoxSeedR.IsChecked = false;
                    ComboBoxA1R.SelectedIndex = 0;
                    ComboBoxA2R.SelectedIndex = 0;
                    ComboBoxA3R.SelectedIndex = 0;
                    ComboBoxA4R.SelectedIndex = 0;
                    ComboBoxSeedR.SelectedIndex = 0;
                    break;
                case "CheckBoxGeneL":
                    CheckBoxColorL.IsChecked = false;
                    CheckBoxSeedL.IsChecked = false;
                    ComboBoxColorL.SelectedIndex = 0;
                    ComboBoxSeedL.SelectedIndex = 0;
                    break;
                case "CheckBoxGeneR":
                    CheckBoxColorR.IsChecked = false;
                    CheckBoxSeedR.IsChecked = false;
                    ComboBoxColorR.SelectedIndex = 0;
                    ComboBoxSeedR.SelectedIndex = 0;
                    break;
                case "CheckBoxSeedL":
                    CheckBoxGeneL.IsChecked = false;
                    CheckBoxColorL.IsChecked = false;
                    ComboBoxColorL.SelectedIndex = 0;
                    ComboBoxA1L.SelectedIndex = 0;
                    ComboBoxA2L.SelectedIndex = 0;
                    ComboBoxA3L.SelectedIndex = 0;
                    ComboBoxA4L.SelectedIndex = 0;
                    break;
                case "CheckBoxSeedR":
                    CheckBoxGeneR.IsChecked = false;
                    CheckBoxColorR.IsChecked = false;
                    ComboBoxColorR.SelectedIndex = 0;
                    ComboBoxA1R.SelectedIndex = 0;
                    ComboBoxA2R.SelectedIndex = 0;
                    ComboBoxA3R.SelectedIndex = 0;
                    ComboBoxA4R.SelectedIndex = 0;
                    break;
            }
        }


    }
}
