using AnimalCrossingFlower.Dialog;
using AnimalCrossingFlower.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace AnimalCrossingFlower
{
    class GlobalTool
    {
        #region MainWindow.xaml

        public static Snackbar SBMain;
        public static DialogHost DHRoot;
        public static ContentControl CCMain;
        public static Frame FrameMain;

        public static string DialogMessage = "";

        /// <summary>
        /// Snackbar显示消息
        /// </summary>
        /// <param name="msg">消息</param>
        public static void BarMsg(string msg)
        {
            SBMain.MessageQueue.Enqueue(msg);
        }

        /// <summary>
        /// 打开Progress消息框
        /// </summary>
        /// <param name="msg">要显示的消息</param>
        public static void OpenDialogProgress(string msg)
        {
            DialogMessage = msg;
            DHRoot.DialogContent = new DialogProgress();
            DHRoot.IsOpen = true;
        }

        /// <summary>
        /// 关闭Progress消息框
        /// </summary>
        public static void CloseDialogProgress()
        {
            DialogMessage = "";
            DHRoot.IsOpen = false;
            DHRoot.DialogContent = null;
        }

        /// <summary>
        /// 打开带按钮的消息框
        /// </summary>
        /// <param name="msg">消息</param>
        public static void OpenDialogButton(string msg)
        {
            DialogMessage = msg;
            DHRoot.DialogContent = new DialogButton();
            DHRoot.IsOpen = true;
        }

        /// <summary>
        /// 关闭带按钮的消息框
        /// </summary>
        public static void CloseDialogButton()
        {
            CloseDialogProgress();
        }

        /// <summary>
        /// 关闭所有进程、并关闭程序
        /// </summary>
        public static void CloseApp()
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region Dictionary
        public static Dictionary<BaseModel.MyColor, Color> ColorShow = new Dictionary<BaseModel.MyColor, Color>()
        {
            { BaseModel.MyColor.Unknown , Colors.Black },
            { BaseModel.MyColor.Blue,Colors.Blue},
            { BaseModel.MyColor.Dark,Colors.Black},
            { BaseModel.MyColor.Green,Colors.Green},
            { BaseModel.MyColor.Orange,Colors.Orange},
            { BaseModel.MyColor.Pink,Colors.HotPink},
            { BaseModel.MyColor.Purple,Colors.Purple},
            { BaseModel.MyColor.Red,Colors.Red},
            { BaseModel.MyColor.White,Colors.Gray},
            { BaseModel.MyColor.Yellow,Colors.Goldenrod},
            { BaseModel.MyColor.YellowRed,Colors.DarkGoldenrod},
            { BaseModel.MyColor.Gold,Colors.Gold},
        };

        public static Dictionary<BaseModel.MyColor, string> ColorNameShow = new Dictionary<BaseModel.MyColor, string>()
        {
            { BaseModel.MyColor.Unknown , "未知" },
            { BaseModel.MyColor.Blue,"蓝色"},
            { BaseModel.MyColor.Dark,"黑色"},
            { BaseModel.MyColor.Green,"绿色"},
            { BaseModel.MyColor.Orange,"橘色"},
            { BaseModel.MyColor.Pink,"粉红色"},
            { BaseModel.MyColor.Purple,"紫色"},
            { BaseModel.MyColor.Red,"红色"},
            { BaseModel.MyColor.White,"白色"},
            { BaseModel.MyColor.Yellow,"黄色"},
            { BaseModel.MyColor.YellowRed,"红黄"},
            { BaseModel.MyColor.Gold,"金色"},
        };

        public static Dictionary<BaseModel.FlowerType, string> FlowerNameShow = new Dictionary<BaseModel.FlowerType, string>()
        {
            {BaseModel.FlowerType.Cosmos,"波斯菊" },
            {BaseModel.FlowerType.Hyacinths,"风信子" },
            {BaseModel.FlowerType.Lilies,"百合" },
            {BaseModel.FlowerType.Mums,"菊花" },
            {BaseModel.FlowerType.Pansies,"三色堇" },
            {BaseModel.FlowerType.Roses,"玫瑰" },
            {BaseModel.FlowerType.Tulips,"郁金香" },
            {BaseModel.FlowerType.Unknown,"未知" },
            {BaseModel.FlowerType.Windflower,"银莲花" },
        };

        #endregion


    }
}
