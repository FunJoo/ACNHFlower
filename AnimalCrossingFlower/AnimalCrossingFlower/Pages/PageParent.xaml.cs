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

namespace AnimalCrossingFlower.Pages
{
    /// <summary>
    /// PageParent.xaml 的交互逻辑
    /// </summary>
    public partial class PageParent : Page
    {
        public PageParent()
        {
            InitializeComponent();
            BindFlower();
            BindGene();
        }

        private BaseModel.FlowerType SelectedFlower;
        private BaseModel.MyColor SelectedColor;

        private List<ColorDic> ListColorDic;
        private List<ColorDic> SelectedColorDic;
        private List<string> ListColor;
        private List<string> ListColorName;

        private void BindFlower()
        {
            List<string> Flowers = new List<string>();
            foreach (int i in Enum.GetValues(typeof(BaseModel.FlowerType)))
            {
                if (i == 0) continue;
                Flowers.Add(GlobalTool.FlowerNameShow[(BaseModel.FlowerType)i]);
            }
            ComboBoxChoose.ItemsSource = Flowers;
            ComboBoxChoose.SelectionChanged += GroupChanged;
            ComboBoxChoose.SelectedIndex = 0;

            CheckBoxColor.IsChecked = true;
        }

        private void BindGene()
        {
            List<string> Genes = new List<string>();
            foreach(int i in Enum.GetValues(typeof(BaseModel.Gene)))
            {
                if (i ==0 || i > 3) Genes.Add(((BaseModel.Gene)i).ToString());
            }
            ComboBoxA1.ItemsSource = Genes;
            ComboBoxA2.ItemsSource = Genes;
            ComboBoxA3.ItemsSource = Genes;
            ComboBoxA4.ItemsSource = Genes;
            ComboBoxA1.SelectedIndex = 0;
            ComboBoxA2.SelectedIndex = 0;
            ComboBoxA3.SelectedIndex = 0;
            ComboBoxA4.SelectedIndex = 0;
            ComboBoxA1.SelectionChanged += GroupChanged;
            ComboBoxA2.SelectionChanged += GroupChanged;
            ComboBoxA3.SelectionChanged += GroupChanged;
            ComboBoxA4.SelectionChanged += GroupChanged;
        }

        private void GroupChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            string senderName = cb.Name;
            switch (senderName)
            {
                case "ComboBoxChoose":
                    {
                        SelectedFlower = (BaseModel.FlowerType)cb.SelectedIndex + 1;

                        TextBlockType.Text = GlobalTool.FlowerNameShow[SelectedFlower];

                        ListColorDic = new List<ColorDic>();
                        ListColorDic = ColorDic.GetColorDic(SelectedFlower);

                        ListColor = new List<string>();
                        ListColorName = new List<string>();
                        ListColor.Add("");
                        ListColorName.Add("无");
                        foreach(var a in ListColorDic)
                        {
                            if (!ListColor.Contains(a.Color) && a.Color != "Unknown" )
                            {
                                ListColor.Add(a.Color);
                                BaseModel.MyColor mc = (BaseModel.MyColor)Enum.Parse(typeof(BaseModel.MyColor), a.Color);
                                ListColorName.Add(GlobalTool.ColorNameShow[mc]);
                            }
                        }

                        ComboBoxColor.SelectionChanged -= GroupChanged;
                        ComboBoxColor.ItemsSource = ListColorName;
                        ComboBoxColor.SelectedIndex = 0;
                        ComboBoxColor.SelectionChanged += GroupChanged;
                        ComboBoxColor.SelectedIndex = 1;

                        CheckBoxColor.IsChecked = true;
                    }
                    break;
                case "ComboBoxColor":
                    {
                        if (cb.SelectedIndex == 0) return;

                        SelectedColor = (BaseModel.MyColor)Enum.Parse(typeof(BaseModel.MyColor), ListColor[cb.SelectedIndex]);

                        TextBlockColor.Text = GlobalTool.ColorNameShow[SelectedColor];
                        TextBlockColor.Foreground = new SolidColorBrush(GlobalTool.ColorShow[SelectedColor]);
                        ImageFlower.Source = new BitmapImage(new Uri("/Assets/" + SelectedFlower.ToString() + SelectedColor.ToString() + ".png", UriKind.Relative));

                        string s = "";
                        SelectedColorDic = new List<ColorDic>();
                        foreach(var a in ListColorDic)
                        {
                            if (a.Color == ListColor[cb.SelectedIndex])
                            {
                                SelectedColorDic.Add(a);
                                string n = a.GetGeneName();
                                if (!s.Contains(n)) s += n + " ";
                            }
                        }
                        TextBlockGene.Text = s;
                    }
                    break;
                case "ComboBoxA1":
                case "ComboBoxA2":
                case "ComboBoxA3":
                case "ComboBoxA4":
                    {
                        if (CheckBoxGene.IsChecked == false) return; 

                        int a1 = ComboBoxA1.SelectedIndex;
                        int a2 = ComboBoxA2.SelectedIndex;
                        int a3 = ComboBoxA3.SelectedIndex;
                        int a4 = ComboBoxA4.SelectedIndex;
                        int aa1 = (int)(BaseModel.Gene)Enum.Parse(typeof(BaseModel.Gene), ComboBoxA1.SelectedItem.ToString());
                        int aa2 = (int)(BaseModel.Gene)Enum.Parse(typeof(BaseModel.Gene), ComboBoxA2.SelectedItem.ToString());
                        int aa3 = (int)(BaseModel.Gene)Enum.Parse(typeof(BaseModel.Gene), ComboBoxA3.SelectedItem.ToString());
                        int aa4 = (int)(BaseModel.Gene)Enum.Parse(typeof(BaseModel.Gene), ComboBoxA4.SelectedItem.ToString());

                        if (
                            (a1 > 0 && a2 > 0 && a3 > 0 && SelectedFlower != BaseModel.FlowerType.Roses)
                            ||
                            (a1 > 0 && a2 > 0 && a3 > 0 && a4>0)
                            )
                        {
                            ColorDic cd = new ColorDic();
                            foreach (var a in ListColorDic)
                            {
                                if (SelectedFlower == BaseModel.FlowerType.Roses)
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
                            TextBlockGene.Text = cd.GetGeneName();
                            TextBlockColor.Text = GlobalTool.ColorNameShow[(BaseModel.MyColor)Enum.Parse(typeof(BaseModel.MyColor), cd.Color)];
                            TextBlockColor.Foreground = new SolidColorBrush(GlobalTool.ColorShow[(BaseModel.MyColor)Enum.Parse(typeof(BaseModel.MyColor), cd.Color)]);

                            string path =
                                cd.Color == "Unknown" ?
                                "/Assets/" + cd.Color + ".png" :
                                "/Assets/" + cd.Type + cd.Color + ".png";
                            ImageFlower.Source = new BitmapImage(new Uri(path, UriKind.Relative));
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
                case "ButtonSearch":
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.IsChecked == false) return;
            string senderName = cb.Name;
            switch (senderName)
            {
                case "CheckBoxColor":
                    CheckBoxGene.IsChecked = false;
                    ComboBoxA1.SelectedIndex = 0;
                    ComboBoxA2.SelectedIndex = 0;
                    ComboBoxA3.SelectedIndex = 0;
                    ComboBoxA4.SelectedIndex = 0;
                    break;
                case "CheckBoxGene":
                    CheckBoxColor.IsChecked = false;
                    ComboBoxColor.SelectedIndex = 0;
                    if (SelectedFlower != BaseModel.FlowerType.Roses) ComboBoxA4.IsEnabled = false;
                    break;
            }
        }
    }
}
