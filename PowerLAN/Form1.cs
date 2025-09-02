using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerLAN
{
    public partial class Form1 : Form
    {
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref uint PhyAddrLen);

        private readonly string _configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PowerLAN");
        private readonly string _configFile;

        private TextBox editBox = new TextBox();
        private ListViewItem editedItem;
        private int editedSubItemIndex;

        public Form1()
        {
            InitializeComponent();
            _configFile = Path.Combine(_configPath, "computers.csv");
            btnStartupSelected.Enabled = false; // Disable startup button initially
            this.Load += Form1_Load;
            this.FormClosing += Form1_FormClosing;

            // Setup for inline editing
            editBox.Visible = false;
            this.listViewComputers.Controls.Add(editBox);
            editBox.Leave += EditBox_Leave;
            editBox.KeyPress += EditBox_KeyPress;
            this.listViewComputers.MouseDoubleClick += ListViewComputers_MouseDoubleClick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComputers();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveComputers();
        }

        private void SaveComputers()
        {
            try
            {
                Directory.CreateDirectory(_configPath);
                var lines = new List<string>();
                foreach (ListViewItem item in listViewComputers.Items)
                {
                    var ip = item.SubItems[0].Text;
                    var mac = item.SubItems[1].Text;
                    var user = item.SubItems[2].Text;
                    var pass = item.Tag as string ?? "";
                    
                    lines.Add(string.Join(",", new[] { ip, mac, user, pass }));
                }
                File.WriteAllLines(_configFile, lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存计算机列表失败: {ex.Message}");
            }
        }

        private void LoadComputers()
        {
            if (!File.Exists(_configFile))
            {
                return;
            }

            try
            {
                var lines = File.ReadAllLines(_configFile);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        ListViewItem item = new ListViewItem(parts[0]); // IP
                        item.SubItems.Add(parts[1]); // MAC
                        item.SubItems.Add(parts[2]); // Username
                        item.Tag = parts[3]; // Password
                        item.SubItems.Add("未知"); // Status column
                        listViewComputers.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载计算机列表失败: {ex.Message}");
            }
        }

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            string ip = $"{txtIp1.Text}.{txtIp2.Text}.{txtIp3.Text}.{txtIp4.Text}";
            string macAddress = txtMacAddress.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (!IPAddress.TryParse(ip, out _))
            {
                MessageBox.Show("无效的IP地址格式。");
                return;
            }

            // If MAC is provided, validate it.
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                if (!IsValidMacAddress(macAddress))
                {
                    MessageBox.Show("提供的MAC地址格式无效。");
                    return;
                }
            }
            else // If MAC is not provided, try to get it.
            {
                this.Cursor = Cursors.WaitCursor;
                macAddress = await Task.Run(() => GetMacAddressFromIp(ip));
                this.Cursor = Cursors.Default;
                if (string.IsNullOrWhiteSpace(macAddress))
                {
                    MessageBox.Show($"未能自动获取IP地址 {ip} 的MAC地址。该计算机将无法被远程开机。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ListViewItem item = new ListViewItem(ip);
            item.SubItems.Add(macAddress);
            item.SubItems.Add(username);
            item.Tag = password; // Password is not stored in the list view for security
            item.SubItems.Add("未知"); // Status column
            listViewComputers.Items.Add(item);
        }

        private async void BtnShutdownSelected_Click(object sender, EventArgs e)
        {
            if (listViewComputers.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一台计算机。");
                return;
            }

            foreach (ListViewItem item in listViewComputers.SelectedItems)
            {
                await ShutdownComputer(item);
            }
        }

        private void BtnStartupSelected_Click(object sender, EventArgs e)
        {
            if (listViewComputers.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一台计算机。");
                return;
            }
            foreach (ListViewItem item in listViewComputers.SelectedItems)
            {
                StartupComputer(item);
            }
        }

        private async void BtnShutdownAll_Click(object sender, EventArgs e)
        {
            if (listViewComputers.Items.Count == 0)
            {
                MessageBox.Show("列表中没有计算机。");
                return;
            }
            foreach (ListViewItem item in listViewComputers.Items)
            {
                await ShutdownComputer(item);
            }
        }

        private void BtnStartupAll_Click(object sender, EventArgs e)
        {
            if (listViewComputers.Items.Count == 0)
            {
                MessageBox.Show("列表中没有计算机。");
                return;
            }
            foreach (ListViewItem item in listViewComputers.Items)
            {
                StartupComputer(item);
            }
        }

        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (listViewComputers.SelectedItems.Count == 0)
            {
                MessageBox.Show("请至少选择一台计算机。");
                return;
            }
            foreach (ListViewItem item in listViewComputers.SelectedItems)
            {
                listViewComputers.Items.Remove(item);
            }
        }

        private void BtnClearList_Click(object sender, EventArgs e)
        {
            listViewComputers.Items.Clear();
        }

        private async Task ShutdownComputer(ListViewItem item)
        {
            string ip = item.SubItems[0].Text;
            string username = item.SubItems[2].Text;
            string password = item.Tag as string;

            // Helper to update UI from any thread
            void SetStatus(string text)
            {
                if (item.ListView.InvokeRequired)
                {
                    item.ListView.Invoke(new Action(() => item.SubItems[3].Text = text));
                }
                else
                {
                    item.SubItems[3].Text = text;
                }
            }

            try
            {
                SetStatus("正在检查...");
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(ip, 1000); // 1-second timeout
                    if (reply.Status != IPStatus.Success)
                    {
                        SetStatus("主机不可达");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(username))
                {
                    SetStatus("正在验证...");
                    string passwordArg = string.IsNullOrEmpty(password) ? "\"\"" : $"\"{password}\"";
                    bool success = await ExecuteCommandAsync($"net use \\\\{ip}\\ipc$ {passwordArg} /user:{username}", 5000); // 5-second timeout
                    Console.WriteLine($"net use \\\\{ip}\\ipc$ {passwordArg} /user:{username}");
                    if (!success)
                    {
                        SetStatus("验证失败");
                        return;
                    }
                }

                SetStatus("正在关机...");
                bool shutdownSuccess = await ExecuteCommandAsync($"shutdown /s /m \\\\{ip} /t 0", 5000);
                Console.WriteLine($"shutdown /s /m \\\\{ip} /t 0");
                if (shutdownSuccess)
                {
                    SetStatus("已关机");
                }
                else
                {
                    SetStatus("关机命令失败");
                }
            }
            catch (Exception ex)
            {
                SetStatus("关机失败");
                MessageBox.Show($"关闭计算机 {ip} 失败: {ex.Message}");
            }
        }

        private void StartupComputer(ListViewItem item)
        {
            string macAddress = item.SubItems[1].Text;
            if (string.IsNullOrWhiteSpace(macAddress))
            {
                item.SubItems[3].Text = "开机失败(无MAC)";
                return; // Can't start without a MAC address
            }
            try
            {
                WakeOnLan(macAddress);
                item.SubItems[3].Text = "开机指令已发送";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动计算机 {macAddress} 失败: {ex.Message}");
                item.SubItems[3].Text = "开机失败";
            }
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                var arpOutput = await Task.Run(() => ExecuteCommandWithOutput("arp -a"));
                var arpMap = ParseArpOutput(arpOutput);

                int updatedCount = 0;
                foreach (ListViewItem item in listViewComputers.Items)
                {
                    string mac = item.SubItems[1].Text;
                    if (string.IsNullOrWhiteSpace(mac)) continue;

                    string normalizedMac = mac.Replace(':', '-').ToLower();

                    if (arpMap.TryGetValue(normalizedMac, out string newIp))
                    {
                        if (item.SubItems[0].Text != newIp)
                        {
                            item.SubItems[0].Text = newIp;
                            updatedCount++;
                        }
                    }
                }
                MessageBox.Show($"刷新完成。更新了 {updatedCount} 个IP地址。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private Dictionary<string, string> ParseArpOutput(string arpOutput)
        {
            var map = new Dictionary<string, string>();
            var regex = new Regex(@"\s*([0-9]+\.[0-9]+\.[0-9]+\.[0-9]+)\s+([0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2}-[0-9a-fA-F]{2})\s+.*");
            var lines = arpOutput.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                if (match.Success)
                {
                    string ip = match.Groups[1].Value;
                    string mac = match.Groups[2].Value.ToLower();
                    if (!map.ContainsKey(mac))
                    {
                        map.Add(mac, ip);
                    }
                }
            }
            return map;
        }

        private string ExecuteCommandWithOutput(string command)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            using (Process proc = new Process())
            {
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return result;
            }
        }

        private Task<bool> ExecuteCommandAsync(string command, int timeout)
        {
            return Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process proc = new Process { StartInfo = procStartInfo })
                    {
                        proc.Start();
                        bool exited = proc.WaitForExit(timeout);

                        if (!exited)
                        {
                            proc.Kill();
                            return false; // Timed out
                        }
                        
                        if (proc.ExitCode != 0)
                        {
                            return false;
                        }

                        return true; // Success
                    }
                }
                catch
                {
                    return false; // Exception occurred
                }
            });
        }

        private void WakeOnLan(string macAddress)
        {
            macAddress = macAddress.Replace(":", "").Replace("-", "");
            byte[] macBytes = new byte[6];
            for (int i = 0; i < 6; i++)
            {
                macBytes[i] = byte.Parse(macAddress.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            List<byte> packet = new List<byte>();
            for (int i = 0; i < 6; i++)
            {
                packet.Add(0xFF);
            }

            for (int i = 0; i < 16; i++)
            {
                packet.AddRange(macBytes);
            }

            using (UdpClient client = new UdpClient())
            {
                client.Connect(IPAddress.Broadcast, 9);
                client.Send(packet.ToArray(), packet.Count);
            }
        }

        private void ListViewComputers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewComputers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewComputers.SelectedItems[0];
                string macAddress = selectedItem.SubItems[1].Text;
                btnStartupSelected.Enabled = !string.IsNullOrWhiteSpace(macAddress);
            }
            else
            {
                btnStartupSelected.Enabled = false;
            }
        }

        private void TxtIp_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox currentTxt = sender as TextBox;
            if (e.KeyChar == '.')
            {
                e.Handled = true; // Suppress the dot character
                if (currentTxt == txtIp1)
                {
                    txtIp2.Focus();
                }
                else if (currentTxt == txtIp2)
                {
                    txtIp3.Focus();
                }
                else if (currentTxt == txtIp3)
                {
                    txtIp4.Focus();
                }
            }
        }

        private bool IsValidMacAddress(string mac)
        {
            // This regex validates MAC address with or without separators (hyphen or colon)
            return Regex.IsMatch(mac, @"^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$|^([0-9A-Fa-f]{12})$");
        }

        private string GetMacAddressFromIp(string ipAddress)
        {
            try
            {
                IPAddress destIP = IPAddress.Parse(ipAddress);
                byte[] macAddr = new byte[6];
                uint macAddrLen = (uint)macAddr.Length;

                // Ping the IP first to ensure it's in the ARP cache. This is not always necessary but can help.
                using (Ping ping = new Ping())
                {
                    ping.Send(destIP, 100); // 100ms timeout
                }

                if (SendARP((int)destIP.Address, 0, macAddr, ref macAddrLen) != 0)
                {
                    return string.Empty; // SendARP failed
                }

                string[] macAddrStrs = new string[(int)macAddrLen];
                for (int i = 0; i < (int)macAddrLen; i++)
                {
                    macAddrStrs[i] = macAddr[i].ToString("X2");
                }

                return string.Join(":", macAddrStrs);
            }
            catch
            {
                return string.Empty; // Error occurred (e.g., host unreachable, parsing failed)
            }
        }

        private void ListViewComputers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = listViewComputers.GetItemAt(e.X, e.Y);
            if (item == null) return;

            // Manually determine the column index
            int left = item.Bounds.Left;
            int columnIndex = -1;
            for (int i = 0; i < listViewComputers.Columns.Count; i++)
            {
                if (e.X < left + listViewComputers.Columns[i].Width)
                {
                    columnIndex = i;
                    break;
                }
                left += listViewComputers.Columns[i].Width;
            }

            if (columnIndex == 0 || columnIndex == 2) // IP or Username column
            {
                editedItem = item;
                editedSubItemIndex = columnIndex;

                // 'left' from the loop is the correct left boundary for the cell
                System.Drawing.Rectangle cellBounds = new System.Drawing.Rectangle(
                    left,
                    item.Bounds.Top,
                    listViewComputers.Columns[columnIndex].Width,
                    item.Bounds.Height
                );

                editBox.Bounds = cellBounds;
                editBox.Text = item.SubItems[columnIndex].Text;
                editBox.Visible = true;
                editBox.Focus();
                editBox.SelectAll();
            }
        }

        private void EditBox_Leave(object sender, EventArgs e)
        {
            if (editedItem != null)
            {
                if (editedSubItemIndex == 0) // IP column validation
                {
                    if (!IPAddress.TryParse(editBox.Text, out _))
                    {
                        MessageBox.Show("无效的IP地址格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        editBox.Focus(); // Keep focus to force correction
                        return;
                    }
                }

                editedItem.SubItems[editedSubItemIndex].Text = editBox.Text;

                // Hide and reset
                editBox.Visible = false;
                editedItem = null;
                editedSubItemIndex = -1;
            }
        }

        private void EditBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                listViewComputers.Focus(); // Trigger the Leave event to save
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                e.Handled = true;
                // Just hide and reset, don't save
                editBox.Visible = false;
                editedItem = null;
                editedSubItemIndex = -1;
            }
        }
    }
}