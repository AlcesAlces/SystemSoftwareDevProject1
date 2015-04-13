namespace SystemSoftwareDevProject1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbFromDateLabel = new System.Windows.Forms.Label();
            this.lbToDateLabel = new System.Windows.Forms.Label();
            this.gbLocality = new System.Windows.Forms.GroupBox();
            this.rbView = new System.Windows.Forms.RadioButton();
            this.rbDownload = new System.Windows.Forms.RadioButton();
            this.dpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dpToDate = new System.Windows.Forms.DateTimePicker();
            this.gbResolution = new System.Windows.Forms.GroupBox();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.rbWeek = new System.Windows.Forms.RadioButton();
            this.rbDay = new System.Windows.Forms.RadioButton();
            this.btnGo = new System.Windows.Forms.Button();
            this.cbStocks = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbLocality.SuspendLayout();
            this.gbResolution.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFromDateLabel
            // 
            this.lbFromDateLabel.AutoSize = true;
            this.lbFromDateLabel.Location = new System.Drawing.Point(9, 23);
            this.lbFromDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbFromDateLabel.Name = "lbFromDateLabel";
            this.lbFromDateLabel.Size = new System.Drawing.Size(59, 13);
            this.lbFromDateLabel.TabIndex = 3;
            this.lbFromDateLabel.Text = "From Date:";
            // 
            // lbToDateLabel
            // 
            this.lbToDateLabel.AutoSize = true;
            this.lbToDateLabel.Location = new System.Drawing.Point(16, 42);
            this.lbToDateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbToDateLabel.Name = "lbToDateLabel";
            this.lbToDateLabel.Size = new System.Drawing.Size(49, 13);
            this.lbToDateLabel.TabIndex = 5;
            this.lbToDateLabel.Text = "To Date:";
            // 
            // gbLocality
            // 
            this.gbLocality.Controls.Add(this.rbView);
            this.gbLocality.Controls.Add(this.rbDownload);
            this.gbLocality.Location = new System.Drawing.Point(89, 85);
            this.gbLocality.Margin = new System.Windows.Forms.Padding(2);
            this.gbLocality.Name = "gbLocality";
            this.gbLocality.Padding = new System.Windows.Forms.Padding(2);
            this.gbLocality.Size = new System.Drawing.Size(100, 61);
            this.gbLocality.TabIndex = 7;
            this.gbLocality.TabStop = false;
            // 
            // rbView
            // 
            this.rbView.AutoSize = true;
            this.rbView.Location = new System.Drawing.Point(3, 34);
            this.rbView.Margin = new System.Windows.Forms.Padding(2);
            this.rbView.Name = "rbView";
            this.rbView.Size = new System.Drawing.Size(48, 17);
            this.rbView.TabIndex = 1;
            this.rbView.TabStop = true;
            this.rbView.Text = "View";
            this.rbView.UseVisualStyleBackColor = true;
            // 
            // rbDownload
            // 
            this.rbDownload.AutoSize = true;
            this.rbDownload.Location = new System.Drawing.Point(3, 16);
            this.rbDownload.Margin = new System.Windows.Forms.Padding(2);
            this.rbDownload.Name = "rbDownload";
            this.rbDownload.Size = new System.Drawing.Size(73, 17);
            this.rbDownload.TabIndex = 0;
            this.rbDownload.TabStop = true;
            this.rbDownload.Text = "Download";
            this.rbDownload.UseVisualStyleBackColor = true;
            // 
            // dpFromDate
            // 
            this.dpFromDate.Location = new System.Drawing.Point(71, 23);
            this.dpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dpFromDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dpFromDate.Name = "dpFromDate";
            this.dpFromDate.Size = new System.Drawing.Size(190, 20);
            this.dpFromDate.TabIndex = 8;
            // 
            // dpToDate
            // 
            this.dpToDate.Location = new System.Drawing.Point(71, 42);
            this.dpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dpToDate.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dpToDate.Name = "dpToDate";
            this.dpToDate.Size = new System.Drawing.Size(190, 20);
            this.dpToDate.TabIndex = 9;
            // 
            // gbResolution
            // 
            this.gbResolution.Controls.Add(this.rbMonth);
            this.gbResolution.Controls.Add(this.rbWeek);
            this.gbResolution.Controls.Add(this.rbDay);
            this.gbResolution.Location = new System.Drawing.Point(12, 79);
            this.gbResolution.Margin = new System.Windows.Forms.Padding(2);
            this.gbResolution.Name = "gbResolution";
            this.gbResolution.Padding = new System.Windows.Forms.Padding(2);
            this.gbResolution.Size = new System.Drawing.Size(64, 68);
            this.gbResolution.TabIndex = 10;
            this.gbResolution.TabStop = false;
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Location = new System.Drawing.Point(4, 50);
            this.rbMonth.Margin = new System.Windows.Forms.Padding(2);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(55, 17);
            this.rbMonth.TabIndex = 2;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            // 
            // rbWeek
            // 
            this.rbWeek.AutoSize = true;
            this.rbWeek.Location = new System.Drawing.Point(3, 34);
            this.rbWeek.Margin = new System.Windows.Forms.Padding(2);
            this.rbWeek.Name = "rbWeek";
            this.rbWeek.Size = new System.Drawing.Size(54, 17);
            this.rbWeek.TabIndex = 1;
            this.rbWeek.TabStop = true;
            this.rbWeek.Text = "Week";
            this.rbWeek.UseVisualStyleBackColor = true;
            // 
            // rbDay
            // 
            this.rbDay.AutoSize = true;
            this.rbDay.Location = new System.Drawing.Point(3, 16);
            this.rbDay.Margin = new System.Windows.Forms.Padding(2);
            this.rbDay.Name = "rbDay";
            this.rbDay.Size = new System.Drawing.Size(44, 17);
            this.rbDay.TabIndex = 0;
            this.rbDay.TabStop = true;
            this.rbDay.Text = "Day";
            this.rbDay.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(12, 200);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 21;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cbStocks
            // 
            this.cbStocks.FormattingEnabled = true;
            this.cbStocks.Location = new System.Drawing.Point(12, 173);
            this.cbStocks.Name = "cbStocks";
            this.cbStocks.Size = new System.Drawing.Size(121, 21);
            this.cbStocks.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Stock";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMain});
            this.statusStrip1.Location = new System.Drawing.Point(0, 223);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(275, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslMain
            // 
            this.tsslMain.Name = "tsslMain";
            this.tsslMain.Size = new System.Drawing.Size(16, 17);
            this.tsslMain.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 245);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbStocks);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.gbResolution);
            this.Controls.Add(this.dpToDate);
            this.Controls.Add(this.dpFromDate);
            this.Controls.Add(this.gbLocality);
            this.Controls.Add(this.lbToDateLabel);
            this.Controls.Add(this.lbFromDateLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbLocality.ResumeLayout(false);
            this.gbLocality.PerformLayout();
            this.gbResolution.ResumeLayout(false);
            this.gbResolution.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFromDateLabel;
        private System.Windows.Forms.Label lbToDateLabel;
        private System.Windows.Forms.GroupBox gbLocality;
        private System.Windows.Forms.RadioButton rbDownload;
        private System.Windows.Forms.RadioButton rbView;
        private System.Windows.Forms.DateTimePicker dpFromDate;
        private System.Windows.Forms.DateTimePicker dpToDate;
        private System.Windows.Forms.GroupBox gbResolution;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.RadioButton rbWeek;
        private System.Windows.Forms.RadioButton rbDay;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cbStocks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMain;
    }
}
