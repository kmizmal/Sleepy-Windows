using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SleepyWinform
{
    public partial class ConfigForm : Form
    {
        private const string ConfigFileName = "config.ini";
        public Config Resconfigs = new Config();

        // 默认配置常量
        static string filePath = Path.Combine(Application.StartupPath, ConfigFileName);
        private const string DefaultHost = "https://expmale.com";
        private const int DefaultPort = 443;
        private const string DefaultDevice = "winform-pc";
        private static string DefaultDeviceId = Guid.NewGuid().ToString();
        private const string DefaultSecret = "114514";
        private const string DefaultBlacklists = "任务切换|开始菜单";
        private const bool DefaultLogFile = false;
        private const int DefaultUpdateCd = 3;

        public ConfigForm()
        {
            InitializeComponent();
        }

        private void config_Load(object sender, EventArgs e)
        {
            initConfig();
        }

        public void initConfig()
        {
            
            Resconfigs = LoadConfig(filePath);

            // 绑定配置到界面
            server_textbox.Text = Resconfigs.Host;
            serverport_box.Text = Resconfigs.Port.ToString();
            device_textbox.Text = Resconfigs.device;
            secret_textBox.Text = Resconfigs.secret;
            blacklists_box.Text = string.Join("|", Resconfigs.blacklists);
            logY.Checked = Resconfigs.logfile;
        }

        public static Config LoadConfig(string filePath)
        {
            // 如果配置文件不存在，创建默认配置
            if (!File.Exists(filePath))
            {
                CreateDefaultConfig(filePath);
                return new Config
                {
                    Host = DefaultHost,
                    Port = DefaultPort,
                    device = DefaultDevice,
                    secret = DefaultSecret,
                    blacklists = DefaultBlacklists.Split('|').ToList(),
                    logfile = DefaultLogFile
                };
            }

            try
            {
                var config = ParseConfigFile(filePath);

                // 配置有效性验证
                if (IsConfigValid(config))
                {
                    return config;
                }

                // 无效配置处理
                HandleInvalidConfig(filePath, config);
                return CreateDefaultConfigAfterInvalid();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"加载配置失败：{ex.Message}");
                return CreateDefaultConfigAfterInvalid();
            }
        }

        private static Config ParseConfigFile(string filePath)
        {
            var config = new Config();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (ShouldSkipLine(line)) continue;

                var (key, value) = ParseConfigLine(line);
                if (key == null) continue;

                ApplyConfigValue(config, key, value);
            }

            return config;
        }

        private static bool ShouldSkipLine(string line)
        {
            var trimmedLine = line.Trim();
            return string.IsNullOrEmpty(trimmedLine) ||
                   trimmedLine.StartsWith("#") ||
                   trimmedLine.StartsWith(";");
        }

        private static (string key, string value) ParseConfigLine(string line)
        {
            var parts = line.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length == 2
                ? (parts[0].Trim(), parts[1].Trim())
                : (null, null);
        }

        private static void ApplyConfigValue(Config config, string key, string value)
        {
            switch (key.ToUpper())
            {
                case "SERVER":
                    config.Host = value;
                    break;
                case "PORT":
                    if (int.TryParse(value, out int port)) config.Port = port;
                    break;
                case "DEVICE_SHOW_NAME":
                    config.device = value;
                    break;
                case "DEVICEID":
                    config.deviceid = value;
                    break;
                case "SECRET":
                    config.secret = value;
                    break;
                case "BLACKLIST":
                    config.blacklists = value.Split('|')
                        .Select(s => s.Trim())
                        .Where(s => !string.IsNullOrEmpty(s))
                        .ToList();
                    break;
                case "LOG_FILE":
                    if (bool.TryParse(value, out bool logFile)) config.logfile = logFile;
                    break;
                case "UPDATECD":
                    if (int.TryParse(value, out int updateCd)) config.updatecd = updateCd;
                    break;
            }
        }

        private static bool IsConfigValid(Config config)
        {
            return !string.IsNullOrEmpty(config.Host) &&
                   config.Port > 1 &&
                   config.Port < 25565 &&
                   !string.IsNullOrEmpty(config.device);
        }

        private static void HandleInvalidConfig(string filePath, Config config)
        {
            string message = "当前配置项无效：\n\n" +
                $"Host: {config.Host}\n" +
                $"Port: {config.Port}\n" +
                $"Device: {config.device}\n\n" +
                "已将配置重置为默认值";

            ShowWarningMessage(message);
            CreateDefaultConfig(filePath);
        }

        private static void CreateDefaultConfig(string filePath)
        {
            Config.SaveConfig(
                DefaultHost,
                DefaultPort,
                DefaultDevice,
                DefaultDeviceId,
                DefaultSecret,
                DefaultBlacklists,
                DefaultLogFile,
                DefaultUpdateCd
            );
        }

        private static Config CreateDefaultConfigAfterInvalid()
        {
            return new Config
            {
                Host = DefaultHost,
                Port = DefaultPort,
                device = DefaultDevice,
                secret = DefaultSecret,
                blacklists = DefaultBlacklists.Split('|').ToList(),
                logfile = DefaultLogFile,
                updatecd = DefaultUpdateCd
            };
        }

        private static void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initConfig();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(serverport_box.Text);
                Config.SaveConfig(
                    server_textbox.Text,
                    port,
                    device_textbox.Text,
                    Resconfigs.deviceid,
                    secret_textBox.Text,
                    blacklists_box.Text,
                    logY.Checked,
                    (int)UPdatecd.Value
                );
                Resconfigs = LoadConfig(filePath);
                this.DialogResult = DialogResult.OK;
            }
            catch (FormatException)
            {
                MessageBox.Show("端口号必须是有效的数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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