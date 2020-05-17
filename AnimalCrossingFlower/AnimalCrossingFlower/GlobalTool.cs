using AnimalCrossingFlower.Dialog;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
