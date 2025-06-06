using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SleepyWinform
{
    public partial class ConfigForm : Form
    {
        public Config Resconfigs = new Config();

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void config_Load(object sender, EventArgs e)
        {
            Resconfigs = Config.Load();

            server_textbox.Text = Resconfigs.Host;
            serverport_box.Text = Resconfigs.Port.ToString();
            device_textbox.Text = Resconfigs.Device;
            secret_textBox.Text = Resconfigs.Secret;
            // 设置密码隐藏
            secret_textBox.PasswordChar = '*';
            button3.Text = "👁️";

            blacklists_box.Text = string.Join("|", Resconfigs.Blacklists);
            logY.Checked = Resconfigs.LogFile;
            UPdatecd.Value = Resconfigs.UpdateCd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Resconfigs = Config.Load();
            server_textbox.Text = Resconfigs.Host;
            serverport_box.Text = Resconfigs.Port.ToString();
            device_textbox.Text = Resconfigs.Device;
            secret_textBox.Text = Resconfigs.Secret;
            // 保持密码隐藏状态
            secret_textBox.PasswordChar = '*';
            button3.Text = "👁️";

            blacklists_box.Text = string.Join("|", Resconfigs.Blacklists);
            logY.Checked = Resconfigs.LogFile;
            UPdatecd.Value = Resconfigs.UpdateCd;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(serverport_box.Text);
                Resconfigs = new Config
                {
                    Host = server_textbox.Text,
                    Port = port,
                    Device = device_textbox.Text,
                    DeviceId = Resconfigs.DeviceId,
                    Secret = secret_textBox.Text,
                    Blacklists = blacklists_box.Text.Split('|')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToList(),
                    LogFile = logY.Checked,
                    UpdateCd = (int)UPdatecd.Value
                };

                Config.Save(Resconfigs);
                this.DialogResult = DialogResult.OK;
            }
            catch (FormatException)
            {
                MessageBox.Show("端口号必须是有效的数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }

        private void TogglePasswordVisibility()
        {
            secret_textBox.PasswordChar = secret_textBox.PasswordChar == '\0' ? '*' : '\0';
            button3.Text = secret_textBox.PasswordChar == '\0' ? "🙈" : "👁️";
        }
    }
}