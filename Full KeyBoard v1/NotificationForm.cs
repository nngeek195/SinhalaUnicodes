using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace KeyBoard
{
    public partial class NotificationForm : Form
    {
        private Timer _timer = new Timer();

        public NotificationForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeComponent()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.LightGreen;
            this.Padding = new Padding(10);
            this.Size = new Size(250, 60);

            var label = new Label
            {
                Text = "Text Copied!",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F)
            };

            this.Controls.Add(label);
        }

        private void InitializeTimer()
        {
            _timer.Interval = 1500; // 1.5 seconds
            _timer.Tick += (sender, args) =>
            {
                this.Close();
                _timer.Stop();
            };
        }

        public void ShowMessage(string message)
        {
            this.Text = "";
            this.Controls.Clear();
            var label = new Label
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F),
                AutoSize = false
            };
            this.Controls.Add(label);
            PositionForm();
            _timer.Start();
            this.Show();
        }

        private void PositionForm()
        {
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int x = screen.Right - this.Width - 20;
            int y = screen.Bottom - this.Height - 50;
            this.Location = new Point(x, y);
        }
    }
}