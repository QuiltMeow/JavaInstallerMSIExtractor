using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace JavaMSIExtractor
{
    public partial class MainForm : Form
    {
        private const int EXIT_FAILURE = 1;
        private const string DOWNLOAD_FILE_NAME = "JREInstaller.exe";
        private static readonly string javaMSIPath = Path.Combine(GUIDPath.getLocalLowPath(), "Oracle", "Java");

        private readonly string outputDirectory;
        private readonly string outputFullPath;

        public MainForm()
        {
            outputDirectory = createOutputDirectory();
            outputFullPath = Path.Combine(Environment.CurrentDirectory, outputDirectory);
            InitializeComponent();
        }

        private static string createOutputDirectory()
        {
            try
            {
                const string OUTPUT_DIRECTORY = "Output";
                return Directory.CreateDirectory(OUTPUT_DIRECTORY).FullName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"建立輸出資料夾時發生例外狀況 : {ex.Message}");
                Environment.Exit(EXIT_FAILURE);
            }
            return null;
        }

        public static async Task killProcessAsync(string processName, int waitTime = 10000)
        {
            await Task.Run(() =>
            {
                using (Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "taskkill",
                        Arguments = $"/F /IM \"{processName}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                })
                {
                    process.Start();
                    process.WaitForExit(waitTime);
                }
            });
        }

        public static async Task<string> getJavaLinkAsync(string html)
        {
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = await parser.ParseDocumentAsync(html);
            return document.QuerySelector("div.jvc0w1.clearfix").QuerySelectorAll("span")[4].QuerySelectorAll("tr")[4].QuerySelector("a").GetAttribute("href");
        }

        private void cleanUpJavaMSIDirectory()
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(javaMSIPath);
                foreach (FileInfo file in info.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo directory in info.GetDirectories())
                {
                    directory.Delete(true);
                }
            }
            catch (Exception ex)
            {
                appendLog($"清除檔案時發生例外狀況 : {ex.Message}", Color.DarkOrange);
            }
        }

        public void appendLog(string data, Color color)
        {
            txtLog.SelectionColor = color;
            txtLog.AppendText($"{data}{Environment.NewLine}");
        }

        public void appendLog(string data)
        {
            appendLog(data, Color.Black);
        }

        public static async Task downloadFileAsync(string url, string fileName)
        {
            await Task.Run(() =>
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), fileName);
                }
            });
        }

        public static async Task<string> getWebViewHTMLAsync(WebView2 webView)
        {
            string html = await webView.CoreWebView2.ExecuteScriptAsync("document.body.outerHTML");
            return Json.Decode(html);
        }

        public static async Task sleepAsync(int ms)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(ms);
            });
        }

        private async void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Stop();

            try
            {
                appendLog("正在讀取網頁內容 ...");
                string html = await getWebViewHTMLAsync(wvMain);

                appendLog("正在取得下載網址 ...");
                string downloadLink = await getJavaLinkAsync(html);
                appendLog($"已取得下載網址 : {downloadLink}", Color.Green);

                appendLog("正在下載 Java 安裝檔案 ...");
                await downloadFileAsync(downloadLink, DOWNLOAD_FILE_NAME);
                appendLog("檔案下載完成", Color.Green);

                appendLog("清除舊檔案 ...");
                cleanUpJavaMSIDirectory();

                appendLog("正在啟動 Java 安裝程式 ...");
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = DOWNLOAD_FILE_NAME,
                        UseShellExecute = false
                    }
                };
                process.Start();

                const int WAIT_TIME = 20000;
                await sleepAsync(WAIT_TIME);

                appendLog("正在提取 MSI 檔案 ...");
                bool detect = false;
                string[] msiFile = Directory.GetFiles(javaMSIPath, "*.msi", SearchOption.AllDirectories);
                foreach (string msi in msiFile)
                {
                    detect = true;
                    string fileName = Path.GetFileName(msi);
                    File.Copy(msi, Path.Combine(outputDirectory, fileName), true);
                }
                if (detect)
                {
                    appendLog("MSI 檔案提取成功", Color.Green);
                }
                else
                {
                    appendLog("找不到 MSI 檔案，請檢查安裝程式是否正常運作", Color.DarkOrange);
                }

                appendLog("關閉安裝程式");
                await killProcessAsync(DOWNLOAD_FILE_NAME);

                appendLog("清理暫存資料");
                cleanUpAll();
                appendLog("程式處理完成", Color.Blue);
            }
            catch (Exception ex)
            {
                appendLog($"發生例外狀況 : {ex.Message}", Color.Red);
                appendLog("程式已中止", Color.Red);
            }
            btnStart.Enabled = true;
        }

        private void cleanUpAll()
        {
            cleanUpJavaMSIDirectory();
            try
            {
                File.Delete(DOWNLOAD_FILE_NAME);
            }
            catch (Exception ex)
            {
                appendLog($"清除檔案時發生例外狀況 : {ex.Message}", Color.DarkOrange);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            clearLog();
            appendLog("爬蟲開始執行，正在爬取目標網頁 ...");
            wvMain.Source = new Uri("https://www.java.com/zh-TW/download/manual.jsp");
            timerLoad.Start();
        }

        private void clearLog()
        {
            txtLog.Text = string.Empty;
            appendLog($"Java MSI 路徑 : {javaMSIPath}", Color.Green);
            appendLog($"程式輸出路徑 : {outputFullPath}", Color.Green);
        }

        private void btnOpenOutputDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                using (Process.Start(outputFullPath))
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟輸出資料夾時發生例外狀況 : {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            clearLog();
        }
    }
}