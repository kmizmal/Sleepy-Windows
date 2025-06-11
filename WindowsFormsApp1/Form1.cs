using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace SleepyWinform
{
    public partial class SleepyWinform : Form
    {
        public static Config config;
        public static SleepyWinform Instance;
        static readonly string logFile = Path.Combine(Application.StartupPath, "log.txt");
        public static bool usingStatus = true;

        public SleepyWinform()
        {
            InitializeComponent();
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
        }

        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                AddResultToListView("[锁屏]");
                usingStatus = false;
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock){ usingStatus = true;}
        }
        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                AddResultToListView("[休眠]");
                usingStatus = false;
            }else if (e.Mode == PowerModes.Resume){usingStatus = true;}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WriteLog($"======{DateTime.Now:HH:mm:ss}======");
            try
            {
                Instance = this;
                // 加载配置
                config = Config.Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            WindowWatcher.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
            SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
            if (config != null)
            {
                _ = SendPostRequestAsync(config, false, "未使用");
            }
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
            bool skipLogging = string.IsNullOrEmpty(appName);
            using (HttpClient httpClient = new HttpClient())
            {
                UriBuilder uriBuilder = new UriBuilder(cfg.Host)
                {
                    Port = (new Uri(cfg.Host).Port == -1) ? cfg.Port : new Uri(cfg.Host).Port,
                    Path = "/device/set"
                };

                var data = new
                {
                    secret = cfg.Secret,
                    id = cfg.DeviceId,
                    show_name = cfg.Device,
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

                    if (cfg != null && success)
                    {
                        if (!skipLogging)
                        {
                            LogAndUIUpdate($"[{DateTime.Now:HH:mm:ss}]-{appName}", appName);
                        }
                        else
                        {
                            WriteLog($"[状态更新成功] using={isUsing}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = $"[{(skipLogging ? "状态更新" : appName)}请求失败]-{ex.Message}";
                    WriteLog(errorMsg);
                    if (!skipLogging)
                    {
                        Instance?.AddResultToListView($"[请求失败] {ex.Message}");
                    }
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
            if (config != null && config.LogFile)
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
        public string Host { get; set; }
        public int Port { get; set; }
        public string Device { get; set; }
        public string DeviceId { get; set; }
        public string Secret { get; set; }
        public List<string> Blacklists { get; set; }
        public bool LogFile { get; set; }
        public int UpdateCd { get; set; }

        public static Config Load()
        {
            // 使用 Settings 默认值
            var settings = Properties.Settings.Default;
            if (string.IsNullOrEmpty(settings.DeviceId))
            {
                settings.DeviceId = Guid.NewGuid().ToString();
                settings.Save();
            }

            var blacklists = settings.Blacklists?.Split('|')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList() ?? new List<string>();

            return new Config
            {
                Host = settings.Host,
                Port = settings.Port,
                Device = settings.Device,
                DeviceId = settings.DeviceId,
                Secret = settings.Secret,
                Blacklists = blacklists,
                LogFile = settings.LogFile,
                UpdateCd = settings.UpdateCd
            };
        }

        public static void Save(Config config)
        {
            var settings = Properties.Settings.Default;
            settings.Host = config.Host;
            settings.Port = config.Port;
            settings.Device = config.Device;
            settings.DeviceId = config.DeviceId;
            settings.Secret = config.Secret;
            settings.Blacklists = string.Join("|", config.Blacklists);
            settings.LogFile = config.LogFile;
            settings.UpdateCd = config.UpdateCd;

            try
            {
                settings.Save();
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
        private static readonly WinEventDelegate procDelegate = WinEventProc;

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

        private static DateTime lastUpdateTime = DateTime.MinValue;
        private static string lastWindowTitle = string.Empty;

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

        private static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd == IntPtr.Zero) return;

            var currentConfig = SleepyWinform.config;
            if (currentConfig == null) return;

            string windowTitle = GetWindowTitle(hwnd);
            if (string.IsNullOrEmpty(windowTitle)) return;

            if (IsInBlacklist(windowTitle, currentConfig))
            {
                SleepyWinform.WriteLog($"黑名单跳过: {windowTitle}");
                return;
            }

            bool isSameWindow = windowTitle.Equals(lastWindowTitle, StringComparison.OrdinalIgnoreCase);
            bool withinCooldown = (DateTime.Now - lastUpdateTime).TotalSeconds < currentConfig.UpdateCd;

            if (isSameWindow && withinCooldown)
            {
                return;
            }

            lastWindowTitle = windowTitle;
            lastUpdateTime = DateTime.Now;

            _ = SleepyWinform.SendPostRequestAsync(currentConfig,SleepyWinform.usingStatus, windowTitle);
        }

        private static string GetWindowTitle(IntPtr hwnd)
        {
            int length = GetWindowTextLength(hwnd);
            if (length == 0) return string.Empty;

            StringBuilder builder = new StringBuilder(length + 1);
            GetWindowText(hwnd, builder, builder.Capacity);
            return builder.ToString();
        }

        private static bool IsInBlacklist(string title, Config config)
        {
            if (config?.Blacklists == null) return false;

            foreach (var blacklisted in config.Blacklists)
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