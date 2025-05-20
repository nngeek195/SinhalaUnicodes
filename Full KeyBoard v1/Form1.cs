using Gma.System.MouseKeyHook;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace KeyBoard
{
    public partial class Form1 : Form
    {
        private IKeyboardMouseEvents _globalHook;
        private string currentWord = "";
        private int caretPosition = 0;
        private bool isTranslationEnabled = true;
        private bool isWordIntegrationEnabled = false;
        private readonly SinhalaConverter _sinhalaConverter = new SinhalaConverter();
        private readonly LegacyConverter _legacyConverter = new LegacyConverter();
        private System.Windows.Forms.Timer clipboardMonitorTimer;
        private Label supportLinkLabel; 

        private NotifyIcon notifyIcon;

        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.txtInput.ReadOnly = true;
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.FormClosing += Form1_FormClosing;
            this.Text = "SinhalaUnicodes - Developed by Niranga Nayanajith";
            AddToStartup();



            this.txtInput.TextChanged -= new System.EventHandler(this.txtInput_TextChanged);
            this.txtInput.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            SubscribeGlobalHook();
            InitializeControls();
            this.btnCopySinhala.Click += new System.EventHandler(this.btnCopySinhala_Click);
            this.btnCopyLegacy.Click += new System.EventHandler(this.btnCopyLegacy_Click);

            InitializeNotifyIcon();
            this.Icon = new Icon("app_icon.ico");
            notifyIcon.Text = "SinhalaUnicodes\nDeveloped by Niranga Nayanajith";
            notifyIcon.DoubleClick += (s, e) => ShowWindow();

            // Start minimized in system tray
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle Ctrl + V (Paste)
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsText())
                {
                    string pastedText = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(pastedText))
                    {
                        // Insert pasted text at current caret position
                        if (caretPosition < currentWord.Length)
                            currentWord = currentWord.Insert(caretPosition, pastedText);
                        else
                            currentWord += pastedText;

                        caretPosition += pastedText.Length;
                        UpdateTextBoxWithCaret();

                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }

            // Optional: Handle Backspace or Spacebar if needed
        }
        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                string pastedText = Clipboard.GetText();
                if (!string.IsNullOrEmpty(pastedText))
                {
                    if (caretPosition < currentWord.Length)
                        currentWord = currentWord.Insert(caretPosition, pastedText);
                    else
                        currentWord += pastedText;

                    caretPosition += pastedText.Length;
                    UpdateTextBoxWithCaret();
                }
            }
        }
        private void btnCopySinhala_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOutput.Text))
                Clipboard.SetText(txtOutput.Text);
            ShowNotification("Sinhala copied to clipboard");
        }

        private void supportLinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                // Opens default browser with your site
                System.Diagnostics.Process.Start(new ProcessStartInfo("https://sinhalaunicodes.vercel.app ")
                {
                    UseShellExecute = true,
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open website. Please check your internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSupportSite_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://sinhalaunicodes.vercel.app ");
            }
            catch
            {
                MessageBox.Show("Failed to open the website. Please check your internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCopyLegacy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LegOutput.Text))
                Clipboard.SetText(LegOutput.Text);
            ShowNotification("Legacy copied to clipboard");
        }
        private void InitializeNotifyIcon()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Show", null, (s, e) => ShowWindow());
            contextMenu.Items.Add("Exit", null, (s, e) => ExitApplication());

            notifyIcon = new NotifyIcon()
            {
                Text = "SinhalaUnicodes\nDeveloped by Niranga Nayanajith",
                Icon = new Icon("app_icon.ico"),
                ContextMenuStrip = contextMenu,
                Visible = true
            };

            notifyIcon.DoubleClick += (s, e) => ShowWindow();
        }

        public void ShowWindow()
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            this.Show();
            this.BringToFront();
            this.Activate();
            this.Focus();
        }
        private void ExitApplication()
        {
            // Clean up global hook
            UnsubscribeGlobalHook();

            // Stop clipboard monitor
            if (clipboardMonitorTimer != null)
            {
                clipboardMonitorTimer.Stop();
                clipboardMonitorTimer.Dispose();
            }

            // Remove notify icon
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }

            // Exit application
            Application.Exit();
        }
        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
                // Prevent showing initially
                base.SetVisibleCore(false);
            }
            else
            {
                base.SetVisibleCore(value);
            }
        }

        private void InitializeControls()
        {
            cmbConversionMode.Items.AddRange(new object[] { "Singlish to Sinhala", "Singlish to Legacy" });
            cmbConversionMode.SelectedIndex = 0;
            cmbConversionMode.SelectedIndexChanged += CmbConversionMode_SelectedIndexChanged;

            btnToggleTranslation.Text = isTranslationEnabled ? "Translation: ON" : "Translation: OFF";
            btnToggleTranslation.Click += (s, e) =>
            {
                isTranslationEnabled = !isTranslationEnabled;
                btnToggleTranslation.Text = isTranslationEnabled ? "Translation: ON" : "Translation: OFF";
                UpdateTextBoxWithCaret();
            };

            btnCopySinhala.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(txtOutput.Text))
                    Clipboard.SetText(txtOutput.Text);
                ShowNotification("Sinhala copied to clipboard");
            };

            btnCopyLegacy.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(LegOutput.Text))
                    Clipboard.SetText(LegOutput.Text);
                ShowNotification("Legacy copied to clipboard");
            };

            btnClear.Click += (s, e) =>
            {
                txtInput.Clear();
                txtOutput.Clear();
                LegOutput.Clear();
                currentWord = "";
                caretPosition = 0;
                UpdateTextBoxWithCaret();
            };

            clipboardMonitorTimer = new Timer { Interval = 500 };
            clipboardMonitorTimer.Tick += ClipboardMonitorTimer_Tick;
        }

        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += OnKeyDown;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prevent full shutdown
            e.Cancel = true;

            // Just hide the form
            this.Hide();
        }
        private void UnsubscribeGlobalHook()
        {
            if (_globalHook != null)
            {
                _globalHook.Dispose();
                _globalHook = null;
            }
        }
        
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                return;

            byte[] keyboardState = new byte[255];
            if (!GetKeyboardState(keyboardState))
                return;

            var sb = new StringBuilder(640);
            uint virtualKeyCode = (uint)e.KeyCode;
            int result = ToUnicode(virtualKeyCode, MapVirtualKey(virtualKeyCode, 0), keyboardState, sb, sb.Capacity, 0);

            bool needsUpdate = true;

            if (e.KeyCode == Keys.Space)
            {
                currentWord += " ";
                caretPosition++;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (caretPosition > 0 && currentWord.Length > 0)
                {
                    currentWord = currentWord.Remove(caretPosition - 1, 1);
                    caretPosition--;
                }
                else if (currentWord.Length > 0)
                {
                    currentWord = currentWord.Substring(1);
                    caretPosition = 0;
                }
                else
                {
                    needsUpdate = false;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                currentWord += "\r\n";
                caretPosition = currentWord.Length;
            }
            else if (result > 0)
            {
                string charTyped = sb.ToString();
                if (charTyped.Length > 0)
                {
                    if (caretPosition < currentWord.Length)
                        currentWord = currentWord.Insert(caretPosition, charTyped);
                    else
                        currentWord += charTyped;

                    caretPosition++;
                }
            }
            else if (e.KeyCode == Keys.Left && caretPosition > 0)
            {
                caretPosition--;
                needsUpdate = false;
            }
            else if (e.KeyCode == Keys.Right && caretPosition < currentWord.Length)
            {
                caretPosition++;
                needsUpdate = false;
            }
            else
            {
                needsUpdate = false;
            }

            if (needsUpdate)
            {
                UpdateTextBoxWithCaret(); // This updates txtInput, txtOutput, LegOutput
            }
        }

        private void UpdateTextBoxWithCaret()
        {
            if (!isTranslationEnabled)
            {
                txtOutput.Clear();
                LegOutput.Clear();
                return;
            }

            string sinhalaText = _sinhalaConverter.Convert(currentWord);
            string legacyText = _legacyConverter.toisiwara(sinhalaText);

            txtInput.Text = currentWord;
            txtInput.SelectionStart = caretPosition;
            txtInput.ScrollToCaret();

            // Update Sinhala output box and auto-scroll
            txtOutput.Text = sinhalaText;
            txtOutput.SelectionStart = txtOutput.Text.Length; // Move cursor to end
            txtOutput.ScrollToCaret(); // Scroll to show latest text

            // Update Legacy output box and auto-scroll
            LegOutput.Text = legacyText;
            LegOutput.SelectionStart = LegOutput.Text.Length;
            LegOutput.ScrollToCaret();
        }

        private void CmbConversionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextBoxWithCaret();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                ProcessLastWord();
                e.SuppressKeyPress = true;
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            int pos = txtInput.SelectionStart;

            // Only update if currentWord doesn't match (to avoid loop)
            if (txtInput.Text != currentWord)
            {
                txtInput.Text = currentWord; // Reset to match currentWord
                txtInput.SelectionStart = caretPosition;
            }
        }

        private void ProcessLastWord()
        {
            if (txtInput == null || cmbConversionMode == null) return;
            string input = txtInput.Text;
            int caretPos = txtInput.SelectionStart;
            int lastSpace = -1;
            for (int i = caretPos - 1; i >= 0; i--)
            {
                if (input[i] == ' ')
                {
                    lastSpace = i;
                    break;
                }
            }
            string wordToConvert = input.Substring(lastSpace + 1).Trim();
            string converted = "";
            if (cmbConversionMode.SelectedItem.ToString() == "Singlish to Sinhala")
            {
                converted = _sinhalaConverter.Convert(wordToConvert);
            }
            else
            {
                string sinhala = _sinhalaConverter.Convert(wordToConvert);
                converted = _legacyConverter.toisiwara(sinhala);
            }
            UpdateTextBoxWithCaret();
        }

        private void ClipboardMonitorTimer_Tick(object sender, EventArgs e)
        {
            if (!isWordIntegrationEnabled || !isTranslationEnabled)
                return;

            try
            {
                if (Clipboard.ContainsText())
                {
                    string originalText = Clipboard.GetText().Trim();
                    if (originalText.StartsWith("w") || originalText.StartsWith("අ"))
                        return;

                    string convertedText = "";
                    if (cmbConversionMode.SelectedItem.ToString() == "Singlish to Sinhala")
                    {
                        convertedText = _sinhalaConverter.Convert(originalText);
                    }
                    else
                    {
                        string sinhala = _sinhalaConverter.Convert(originalText);
                        convertedText = _legacyConverter.toisiwara(sinhala);
                    }
                    Clipboard.SetText(convertedText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error accessing clipboard: " + ex.Message);
            }
        }

        private void btnToggleWord_Click(object sender, EventArgs e)
        {
            isWordIntegrationEnabled = !isWordIntegrationEnabled;
            btnToggleWord.Text = isWordIntegrationEnabled ? "Disable Integration" : "Enable Integration";

            if (isWordIntegrationEnabled)
                clipboardMonitorTimer.Start();
            else
                clipboardMonitorTimer.Stop();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
            LegOutput.Clear();
            currentWord = "";
            caretPosition = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Optional logic
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Legacy Output label clicked!");
        }
        private void AddToStartup()
        {
            try
            {
                string appName = "SinhalaUnicodes";
                string appPath = $"\"{Application.ExecutablePath}\"";

                using (var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
                {
                    key.SetValue(appName, appPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add app to startup: " + ex.Message);
            }
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            // Startup logic here
        }

        private void ShowNotification(string message)
        {
            var notification = new NotificationForm();
            notification.ShowMessage(message);
        }

        #region Native Methods
        [DllImport("user32.dll")]
        private static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder receivingBuffer, int bufferSize, uint flags);
        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] pbKeyState);
        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint uCode, uint uMapType);
        #endregion

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Don't call base.OnFormClosed(e); ← Avoids Application.Exit()
            this.Hide();

            // Keep global hook alive
            if (_globalHook != null)
            {
                _globalHook.Dispose(); // Optional: keep this only if needed
            }
        }

        private void LegOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnToggleTranslation_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}