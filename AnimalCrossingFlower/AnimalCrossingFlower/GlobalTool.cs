using AnimalCrossingFlower.Dialog;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        private static void ODP(string msg)
        {
            DialogMessage = msg;
            DHRoot.DialogContent = new DialogProgress();
            DHRoot.IsOpen = true;
        }

        public static void OpenDialogProgress(FrameworkElement page, string msg)
        {
            Action<string> updateAction = new Action<string>(ODP);
            page.Dispatcher.BeginInvoke(updateAction, msg);
        }

        public static void OpenDialogProgress(TaskScheduler ts, string msg)
        {
            Task.Factory.StartNew(() => ODP(msg),
                    new CancellationTokenSource().Token, TaskCreationOptions.None, ts).Wait();
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

        public static void CloseDialog(FrameworkElement page)
        {
            Action updateAction = new Action(CloseDialogProgress);
            page.Dispatcher.BeginInvoke(updateAction);
        }

        public static void CloseDialog(TaskScheduler ts)
        {
            Task.Factory.StartNew(() => CloseDialogProgress(),
                    new CancellationTokenSource().Token, TaskCreationOptions.None, ts).Wait();
        }

        /// <summary>
        /// 打开带按钮的消息框
        /// </summary>
        /// <param name="msg">消息</param>
        private static void ODB(string msg)
        {
            DialogMessage = msg;
            DHRoot.DialogContent = new DialogButton();
            DHRoot.IsOpen = true;
        }

        public static void OpenDialogButton(FrameworkElement page, string msg)
        {
            Action<string> updateAction = new Action<string>(ODB);
            page.Dispatcher.BeginInvoke(updateAction, msg);
        }

        public static void OpenDialogButton(TaskScheduler ts, string msg)
        {
            Task.Factory.StartNew(() => ODB(msg),
                    new CancellationTokenSource().Token, TaskCreationOptions.None, ts).Wait();
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

        #region PageZajiao.xaml

        public static bool BoolColorL;
        public static bool BoolColorR;
        public static bool BoolGeneL;
        public static bool BoolGeneR;
        public static bool BoolSeedL;
        public static bool BoolSeedR;

        public static int IndexColorL;
        public static int IndexColorR;

        public static int IndexSeedL;
        public static int IndexSeedR;

        public static string ItemA1L = "Unknown";
        public static string ItemA2L = "Unknown";
        public static string ItemA3L = "Unknown";
        public static string ItemA4L = "Unknown";

        public static string ItemA1R = "Unknown";
        public static string ItemA2R = "Unknown";
        public static string ItemA3R = "Unknown";
        public static string ItemA4R = "Unknown";

        public static string ButtonNameZajiao;

        public static void ChangeZajiaoComboBox(object sender)
        {
            var s = sender as ComboBox;
            switch (s.Name)
            {
                case "ComboBoxColorL":
                    IndexColorL = s.SelectedIndex;
                    break;
                case "ComboBoxColorR":
                    IndexColorR = s.SelectedIndex;
                    break;
                case "ComboBoxA1L":
                    ItemA1L = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA2L":
                    ItemA2L = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA3L":
                    ItemA3L = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA4L":
                    ItemA4L = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA1R":
                    ItemA1R = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA2R":
                    ItemA2R = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA3R":
                    ItemA3R = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA4R":
                    ItemA4R = s.SelectedItem.ToString();
                    break;
                case "ComboBoxSeedL":
                    IndexSeedL = s.SelectedIndex;
                    break;
                case "ComboBoxSeedR":
                    IndexSeedR = s.SelectedIndex;
                    break;
            }
        }

        public static void ChangeZajiaoCheckBox(object sender)
        {
            var s = sender as CheckBox;
            switch (s.Name)
            {
                case "CheckBoxColorL":
                    BoolColorL = s.IsChecked.Value;
                    break;
                case "CheckBoxColorR":
                    BoolColorR = s.IsChecked.Value;
                    break;
                case "CheckBoxGeneL":
                    BoolGeneL = s.IsChecked.Value;
                    break;
                case "CheckBoxGeneR":
                    BoolGeneR = s.IsChecked.Value;
                    break;
                case "CheckBoxSeedL":
                    BoolSeedL = s.IsChecked.Value;
                    break;
                case "CheckBoxSeedR":
                    BoolSeedR = s.IsChecked.Value;
                    break;
            }
        }

        #endregion

        #region PageParent.xaml

        public static bool BoolColor;
        public static bool BoolGene;

        public static int IndexColor;

        public static string ItemA1 = "Unknown";
        public static string ItemA2 = "Unknown";
        public static string ItemA3 = "Unknown";
        public static string ItemA4 = "Unknown";

        public static string ButtonNameParent;

        public static void ChangeParentComboBox(object sender)
        {
            var s = sender as ComboBox;
            switch (s.Name)
            {
                case "ComboBoxColor":
                    IndexColor = s.SelectedIndex;
                    break;
                case "ComboBoxA1":
                    ItemA1 = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA2":
                    ItemA2 = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA3":
                    ItemA3 = s.SelectedItem.ToString();
                    break;
                case "ComboBoxA4":
                    ItemA4 = s.SelectedItem.ToString();
                    break;
            }
        }

        public static void ChangeParentCheckBox(object sender)
        {
            var s = sender as CheckBox;
            switch (s.Name)
            {
                case "CheckBoxColor":
                    BoolColor = s.IsChecked.Value;
                    break;
                case "CheckBoxGene":
                    BoolGene = s.IsChecked.Value;
                    break;
            }
        }

            #endregion
        }
}
