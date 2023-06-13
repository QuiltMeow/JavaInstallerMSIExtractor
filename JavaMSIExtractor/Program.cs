using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace JavaMSIExtractor
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!WebViewChecker.isWebViewInstall())
            {
                if (MessageBox.Show("Web View 2 執行階段尚未安裝 按下確定後進入下載頁面", "執行階段", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start("https://developer.microsoft.com/zh-tw/microsoft-edge/webview2");
                }
                return;
            }
            Application.Run(new MainForm());
        }
    }
}