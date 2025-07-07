namespace SleepyWinform
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.server_textbox = new System.Windows.Forms.TextBox();
            this.serverport_box = new System.Windows.Forms.TextBox();
            this.device_textbox = new System.Windows.Forms.TextBox();
            this.secret_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ReloadCfg = new System.Windows.Forms.Button();
            this.Savecfg_button = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.blacklists_box = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.logY = new System.Windows.Forms.RadioButton();
            this.logN = new System.Windows.Forms.RadioButton();
            this.UPdatecd = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.deviceIdtxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.UPdatecd)).BeginInit();
            this.SuspendLayout();
            // 
            // server_textbox
            // 
            this.server_textbox.Location = new System.Drawing.Point(286, 41);
            this.server_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.server_textbox.Name = "server_textbox";
            this.server_textbox.Size = new System.Drawing.Size(381, 28);
            this.server_textbox.TabIndex = 0;
            // 
            // serverport_box
            // 
            this.serverport_box.Location = new System.Drawing.Point(286, 85);
            this.serverport_box.Margin = new System.Windows.Forms.Padding(4);
            this.serverport_box.Name = "serverport_box";
            this.serverport_box.Size = new System.Drawing.Size(148, 28);
            this.serverport_box.TabIndex = 1;
            // 
            // device_textbox
            // 
            this.device_textbox.Location = new System.Drawing.Point(286, 147);
            this.device_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.device_textbox.Name = "device_textbox";
            this.device_textbox.Size = new System.Drawing.Size(148, 28);
            this.device_textbox.TabIndex = 2;
            // 
            // secret_textBox
            // 
            this.secret_textBox.Location = new System.Drawing.Point(286, 235);
            this.secret_textBox.Margin = new System.Windows.Forms.Padding(4);
            this.secret_textBox.Name = "secret_textBox";
            this.secret_textBox.PasswordChar = '*';
            this.secret_textBox.Size = new System.Drawing.Size(148, 28);
            this.secret_textBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "ServerPort";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "外显设备名称";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 239);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "secret";
            // 
            // ReloadCfg
            // 
            this.ReloadCfg.Location = new System.Drawing.Point(859, 147);
            this.ReloadCfg.Margin = new System.Windows.Forms.Padding(4);
            this.ReloadCfg.Name = "ReloadCfg";
            this.ReloadCfg.Size = new System.Drawing.Size(112, 34);
            this.ReloadCfg.TabIndex = 8;
            this.ReloadCfg.Text = "重载配置";
            this.ReloadCfg.UseVisualStyleBackColor = true;
            this.ReloadCfg.Click += new System.EventHandler(this.button1_Click);
            // 
            // Savecfg_button
            // 
            this.Savecfg_button.Location = new System.Drawing.Point(859, 324);
            this.Savecfg_button.Margin = new System.Windows.Forms.Padding(4);
            this.Savecfg_button.Name = "Savecfg_button";
            this.Savecfg_button.Size = new System.Drawing.Size(112, 34);
            this.Savecfg_button.TabIndex = 9;
            this.Savecfg_button.Text = "保存配置";
            this.Savecfg_button.UseVisualStyleBackColor = true;
            this.Savecfg_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(441, 239);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(23, 24);
            this.button3.TabIndex = 10;
            this.button3.Text = "👁️";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 407);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "黑名单列表（|分隔）";
            // 
            // blacklists_box
            // 
            this.blacklists_box.Location = new System.Drawing.Point(264, 407);
            this.blacklists_box.Margin = new System.Windows.Forms.Padding(4);
            this.blacklists_box.Multiline = true;
            this.blacklists_box.Name = "blacklists_box";
            this.blacklists_box.Size = new System.Drawing.Size(388, 187);
            this.blacklists_box.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(71, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "是否创建日志文件";
            // 
            // logY
            // 
            this.logY.AutoSize = true;
            this.logY.Location = new System.Drawing.Point(307, 293);
            this.logY.Name = "logY";
            this.logY.Size = new System.Drawing.Size(51, 22);
            this.logY.TabIndex = 14;
            this.logY.TabStop = true;
            this.logY.Text = "是";
            this.logY.UseVisualStyleBackColor = true;
            // 
            // logN
            // 
            this.logN.AutoSize = true;
            this.logN.Checked = true;
            this.logN.Location = new System.Drawing.Point(383, 293);
            this.logN.Name = "logN";
            this.logN.Size = new System.Drawing.Size(51, 22);
            this.logN.TabIndex = 15;
            this.logN.TabStop = true;
            this.logN.Text = "否";
            this.logN.UseVisualStyleBackColor = true;
            // 
            // UPdatecd
            // 
            this.UPdatecd.Location = new System.Drawing.Point(286, 341);
            this.UPdatecd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UPdatecd.Name = "UPdatecd";
            this.UPdatecd.Size = new System.Drawing.Size(120, 28);
            this.UPdatecd.TabIndex = 16;
            this.UPdatecd.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(92, 343);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 18);
            this.label7.TabIndex = 18;
            this.label7.Text = "最短更新间隔";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(426, 351);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 18);
            this.label8.TabIndex = 19;
            this.label8.Text = "s";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(92, 202);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 18);
            this.label9.TabIndex = 21;
            this.label9.Text = "设备id";
            // 
            // deviceIdtxt
            // 
            this.deviceIdtxt.Location = new System.Drawing.Point(286, 199);
            this.deviceIdtxt.Margin = new System.Windows.Forms.Padding(4);
            this.deviceIdtxt.Name = "deviceIdtxt";
            this.deviceIdtxt.Size = new System.Drawing.Size(148, 28);
            this.deviceIdtxt.TabIndex = 20;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.deviceIdtxt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.UPdatecd);
            this.Controls.Add(this.logN);
            this.Controls.Add(this.logY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.blacklists_box);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Savecfg_button);
            this.Controls.Add(this.ReloadCfg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secret_textBox);
            this.Controls.Add(this.device_textbox);
            this.Controls.Add(this.serverport_box);
            this.Controls.Add(this.server_textbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigForm";
            this.Text = "config";
            this.Load += new System.EventHandler(this.config_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UPdatecd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox server_textbox;
        private System.Windows.Forms.TextBox serverport_box;
        private System.Windows.Forms.TextBox device_textbox;
        private System.Windows.Forms.TextBox secret_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ReloadCfg;
        private System.Windows.Forms.Button Savecfg_button;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox blacklists_box;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton logY;
        private System.Windows.Forms.RadioButton logN;
        private System.Windows.Forms.NumericUpDown UPdatecd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox deviceIdtxt;
    }
}