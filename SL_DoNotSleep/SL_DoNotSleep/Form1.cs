using Microsoft.Win32;
using SL_DoNotSleep.Properties;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SL_DoNotSleep
{
    public partial class Form1 : Form
    {
        String appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        String registryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        ToolTip btnToolTipEnableSleep = new ToolTip();
        ToolTip btnToolTipRunStartup = new ToolTip();

        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001,
        }

        internal class NativeMethods
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = appName;

            setBalloonEnableSleep();
            setBalloonRunStartup();

            this.chkRunOnStartup.CheckedChanged -= new System.EventHandler(this.chkRunOnStartup_CheckedChanged);
            chkRunOnStartup.Checked = getStartupKey();
            this.chkRunOnStartup.CheckedChanged += new System.EventHandler(this.chkRunOnStartup_CheckedChanged);

            EnableSleep(false);
        }

        private void EnableSleep(Boolean state)
        {
            EXECUTION_STATE execState;

            if (false == state)
            {
                execState = NativeMethods.SetThreadExecutionState(
                    EXECUTION_STATE.ES_CONTINUOUS
                    | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                    | EXECUTION_STATE.ES_SYSTEM_REQUIRED
                    | EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
            }
            else
            {
                execState = NativeMethods.SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            }

            SetLabelSleepState(execState);
        }

        private void SetLabelSleepState(EXECUTION_STATE state)
        {
            Boolean sleepEnabled = true;
            String status = "Sleep: ";

            if ((state & EXECUTION_STATE.ES_CONTINUOUS) == EXECUTION_STATE.ES_CONTINUOUS)
            {
                status += " Cont";
            }
            if ((state & EXECUTION_STATE.ES_AWAYMODE_REQUIRED) == EXECUTION_STATE.ES_AWAYMODE_REQUIRED)
            {
                status += " + Away";
                sleepEnabled = false;
            }
            if ((state & EXECUTION_STATE.ES_DISPLAY_REQUIRED) == EXECUTION_STATE.ES_DISPLAY_REQUIRED)
            {
                status += " + Display";
                sleepEnabled = false;
            }
            if ((state & EXECUTION_STATE.ES_SYSTEM_REQUIRED) == EXECUTION_STATE.ES_SYSTEM_REQUIRED)
            {
                status += " + System";
                sleepEnabled = false;
            }

            Console.WriteLine(status);

            if (sleepEnabled)
            {
                pctCurrentStatus.Image = Resources.moon.ToBitmap();
            }
            else
            {
                pctCurrentStatus.Image = Resources.sun_3_256.ToBitmap();
            }
        }

        private void chkEnableSleep_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            EnableSleep(chkBox.Checked);
        }

        private void tmrGetStatus_Tick(object sender, EventArgs e)
        {
            EnableSleep(chkEnableSleep.Checked);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                ntfIconTray.Visible = true;
            }
        }

        private void ntfIconTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            EnableSleep(true);
            ntfIconTray.Visible = false;
        }

        private void chkRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            bool success = setStartupKey(chkBox.Checked);

            MessageBox.Show(
                "Change startup result: " + success + (success ? "" : "\nRun as admin if needed"), 
                appName, 
                MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            chkBox.Checked = getStartupKey();
        }

        private bool setStartupKey(bool option)
        {
            bool success;

            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(registryKey, true);

                if (option)
                {
                    rk.SetValue(appName, Application.ExecutablePath);
                    success = true;
                }
                else
                {
                    rk.DeleteValue(appName, false);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set/delete key: " + ex.Message);
                success = false;
            }

            return success;
        }

        private bool getStartupKey()
        {
            bool runOnStartup;

            try
            {
                RegistryKey rk = Registry.CurrentUser.OpenSubKey(registryKey, false);
                String path = (String)(rk.GetValue(appName));
                Console.WriteLine("Startup key: " + path);
                if (path == Application.ExecutablePath)
                {
                    runOnStartup = true;
                }
                else
                {
                    runOnStartup = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Startup key not found: " + ex.Message);
                runOnStartup = false;
            }

            return runOnStartup;
        }

        private void setBalloonEnableSleep()
        {
            ToolTip btnToolTipEnableSleep = new ToolTip();
            btnToolTipEnableSleep.ToolTipTitle = "Enable sleep checkbox";
            btnToolTipEnableSleep.UseFading = true;
            btnToolTipEnableSleep.UseAnimation = true;
            btnToolTipEnableSleep.IsBalloon = true;
            btnToolTipEnableSleep.ShowAlways = true;
            btnToolTipEnableSleep.AutoPopDelay = 10000;
            btnToolTipEnableSleep.InitialDelay = 1000;
            btnToolTipEnableSleep.ReshowDelay = 500;
            btnToolTipEnableSleep.SetToolTip(chkEnableSleep, "If set, the system will sleep with the settings from Windows");
        }

        private void setBalloonRunStartup()
        {
            ToolTip btnToolTipRunStartup = new ToolTip();
            btnToolTipRunStartup.ToolTipTitle = "Run on startup checkbox";
            btnToolTipRunStartup.UseFading = true;
            btnToolTipRunStartup.UseAnimation = true;
            btnToolTipRunStartup.IsBalloon = true;
            btnToolTipRunStartup.ShowAlways = true;
            btnToolTipRunStartup.AutoPopDelay = 10000;
            btnToolTipRunStartup.InitialDelay = 1000;
            btnToolTipRunStartup.ReshowDelay = 500;
            btnToolTipRunStartup.SetToolTip(chkRunOnStartup, "If set, this program will run on Windows startup");
        }

        private void Form1_Resize_1(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
    }
}
