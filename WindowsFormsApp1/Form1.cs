using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.Json;
using System.IO;

namespace SleepyWinform
{
    public partial class SleepyWinform : Form
    {
        public static Config config;
        public static SleepyWinform Instance;
        static readonly string filePath = Path.Combine(Application.StartupPath, "config.ini");
        static readonly string logFile = Path.Combine(Application.StartupPath, "log.txt");

        public SleepyWinform()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteLog($"======{DateTime.Now:HH:mm:ss}======");
            try
            {
                Instance = this;
                config = ConfigForm.LoadConfig(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            WindowWatcher.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ = SendPostRequestAsync(config, false, "");
            WindowWatcher.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm();
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                config = configForm.Resconfigs;
            }
            else
            {
                MessageBox.Show("配置未保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AddResultToListView(string result)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => AddResultToListView(result)));
                return;
            }

            ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
            item.SubItems.Add(result);

            if (result.Contains("失败") || result.Contains("错误"))
                item.ForeColor = Color.Red;
            else if (result.Contains("成功"))
                item.ForeColor = Color.Green;

            listView1.Items.Add(item);
            listView1.EnsureVisible(listView1.Items.Count - 1);
        }

        public static async Task SendPostRequestAsync(Config cfg, bool isUsing, string appName)
        {
            // 当appName为空时不记录日志（减少不必要日志）
            bool skipLogging = string.IsNullOrEmpty(appName);

            HttpClient httpClient = new HttpClient();

            UriBuilder uriBuilder = new UriBuilder(cfg.Host)
            {
                Port = (new Uri(cfg.Host).Port == -1) ? cfg.Port : new Uri(cfg.Host).Port,
                Path = "/device/set"
            };

            var data = new
            {
                secret = cfg.secret,
                id = cfg.deviceid,
                show_name = cfg.device,
                @using = isUsing,
                app_name = appName
            };

            string json = JsonSerializer.Serialize(data);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(uriBuilder.Uri, content).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                bool success = JsonDocument.Parse(result).RootElement.GetProperty("success").GetBoolean();

                if (cfg != null && success && !skipLogging)
                {
                    LogAndUIUpdate($"[{DateTime.Now:HH:mm:ss}]-{appName}", appName);
                }
            }
            catch (Exception ex)
            {
                if (!skipLogging)
                {
                    string errorMsg = $"[{appName}请求失败]-{ex.Message}";
                    LogAndUIUpdate($"[请求失败] {ex.Message}", errorMsg);
                }
            }
        }

        private static void LogAndUIUpdate(string logMsg, string uiMsg)
        {
            WriteLog(logMsg);
            Instance?.AddResultToListView(uiMsg);
        }

        public static void WriteLog(string message)
        {
            Console.WriteLine(message);
            if (config != null && config.logfile)
            {
                try
                {
                    File.AppendAllText(logFile, $"[{DateTime.Now:HH:mm:ss}]{message}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"写入日志失败: {ex.Message}");
                }
            }
        }

        private void SleepyWinform_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }

    public class Config
    {
        public string Host { get; set; } = "https://init.space";
        public int Port { get; set; } = 443;
        public string device { get; set; } = "zmal-pc";
        public string deviceid { get; set; } = Guid.NewGuid().ToString();
        public string secret { get; set; } = "114514";
        public List<string> blacklists { get; set; } = new List<string>();
        public bool logfile { get; set; } = false;
        public int updatecd { get; set; } = 3; // 最短更新间隔，单位为秒

        public static string filePath { get; set; } = Path.Combine(Application.StartupPath, "config.ini");

        public static void SaveConfig(string host, int port, string device,string deviceid, string secret, string blacklists, bool logfile,int updatecd)
        {
            var configLines = new List<string>
            {
                "# 服务端地址",
                $"SERVER={host}",
                "# 服务端端口",
                $"Port={port}",
                "# 显示名称",
                $"DEVICE_SHOW_NAME={device}",
                "# 设备id（随机生成）",
                $"deviceid={deviceid}",
                "# 服务端密钥",
                $"SECRET={secret}",
                "# 黑名单列表",
                $"BLACKLIST={blacklists}",
                "#是否写入日志",
                $"LOG_FILE={logfile}",
                $"UPDATECD={updatecd}",

            };

            try
            {
                File.WriteAllLines(filePath, configLines);
                MessageBox.Show("配置保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存配置失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class WindowWatcher
    {
        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        private static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        private const uint EVENT_SYSTEM_FOREGROUND = 0x0003;
        private const uint WINEVENT_OUTOFCONTEXT = 0;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        private static IntPtr _hook;

        public static void Start()
        {
            _hook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, procDelegate, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        public static void Stop()
        {
            if (_hook != IntPtr.Zero)
            {
                UnhookWinEvent(_hook);
            }
        }

        private static DateTime lastUpdateTime = DateTime.MinValue;
        private static string lastWindowTitle = string.Empty;

        private static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd == IntPtr.Zero) return;

            string windowTitle = GetWindowTitle(hwnd);
            if (string.IsNullOrEmpty(windowTitle)) return;

            // 检查时间间隔和窗口变化
            bool isSameWindow = windowTitle.Equals(lastWindowTitle, StringComparison.OrdinalIgnoreCase);
            bool withinCooldown = (DateTime.Now - lastUpdateTime).TotalSeconds < SleepyWinform.config.updatecd;

            if (isSameWindow && withinCooldown)
            {
                // 相同窗口且在冷却期内，跳过更新
                return;
            }

            // 检查黑名单
            if (IsInBlacklist(windowTitle))
            {
                SleepyWinform.WriteLog($"黑名单跳过: {windowTitle}");
                return;
            }

            // 更新状态并发送请求
            lastWindowTitle = windowTitle;
            lastUpdateTime = DateTime.Now;

            _ = SleepyWinform.SendPostRequestAsync(SleepyWinform.config, true, windowTitle);
        }

        private static string GetWindowTitle(IntPtr hwnd)
        {
            int length = GetWindowTextLength(hwnd);
            StringBuilder builder = new StringBuilder(length + 1);
            GetWindowText(hwnd, builder, builder.Capacity);
            return builder.ToString();
        }

        private static bool IsInBlacklist(string title)
        {
            if (SleepyWinform.config?.blacklists == null) return false;

            foreach (var blacklisted in SleepyWinform.config.blacklists)
            {
                if (!string.IsNullOrEmpty(blacklisted) && title.IndexOf(blacklisted, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
