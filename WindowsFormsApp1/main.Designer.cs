﻿namespace SleepyWinform
{
    partial class SleepyWinform
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SleepyWinform));
            this.configview = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logcontent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // configview
            // 
            this.configview.Location = new System.Drawing.Point(1216, 68);
            this.configview.Margin = new System.Windows.Forms.Padding(5);
            this.configview.Name = "configview";
            this.configview.Size = new System.Drawing.Size(137, 40);
            this.configview.TabIndex = 1;
            this.configview.Text = "配置";
            this.configview.UseVisualStyleBackColor = true;
            this.configview.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.time,
            this.logcontent});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(84, 210);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1297, 417);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // time
            // 
            this.time.Text = "时间";
            this.time.Width = 171;
            // 
            // logcontent
            // 
            this.logcontent.Text = "日志内容";
            this.logcontent.Width = 559;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sleepy-Winform";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // SleepyWinform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 788);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.configview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SleepyWinform";
            this.Opacity = 0.9D;
            this.Text = "sleepy_winform";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.SleepyWinform_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button configview;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader logcontent;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

