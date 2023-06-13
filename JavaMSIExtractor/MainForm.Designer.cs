namespace JavaMSIExtractor
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wvMain = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.timerLoad = new System.Windows.Forms.Timer(this.components);
            this.btnOpenOutputDirectory = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.wvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(19, 55);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 25);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "提取 MSI 檔案";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Java MSI 檔案提取工具";
            // 
            // wvMain
            // 
            this.wvMain.AllowExternalDrop = true;
            this.wvMain.CreationProperties = null;
            this.wvMain.DefaultBackgroundColor = System.Drawing.Color.White;
            this.wvMain.Location = new System.Drawing.Point(195, 12);
            this.wvMain.Name = "wvMain";
            this.wvMain.Size = new System.Drawing.Size(100, 30);
            this.wvMain.TabIndex = 1;
            this.wvMain.Visible = false;
            this.wvMain.ZoomFactor = 1D;
            // 
            // timerLoad
            // 
            this.timerLoad.Interval = 10000;
            this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);
            // 
            // btnOpenOutputDirectory
            // 
            this.btnOpenOutputDirectory.Location = new System.Drawing.Point(195, 55);
            this.btnOpenOutputDirectory.Name = "btnOpenOutputDirectory";
            this.btnOpenOutputDirectory.Size = new System.Drawing.Size(100, 25);
            this.btnOpenOutputDirectory.TabIndex = 3;
            this.btnOpenOutputDirectory.Text = "開啟輸出資料夾";
            this.btnOpenOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOpenOutputDirectory.Click += new System.EventHandler(this.btnOpenOutputDirectory_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(19, 90);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(280, 450);
            this.txtLog.TabIndex = 4;
            this.txtLog.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 556);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnOpenOutputDirectory);
            this.Controls.Add(this.wvMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Java MSI 檔案提取工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvMain;
        private System.Windows.Forms.Timer timerLoad;
        private System.Windows.Forms.Button btnOpenOutputDirectory;
        private System.Windows.Forms.RichTextBox txtLog;
    }
}

