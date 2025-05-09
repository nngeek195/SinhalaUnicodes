using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyBoard
{
    public partial class Form1 : Form
    {
        private SinhalaConverter _converter;

        public Form1()
        {
            InitializeComponent();

            // Initialize the converter
            _converter = new SinhalaConverter();

            // Set font for proper Sinhala rendering
            txtOutput.Font = new Font("Iskoola Pota", 12F);
            txtInput.Font = new Font("Segoe UI", 10F);

            // Optional: Set window title
            this.Text = "Sinhala Keyboard";
        }

        /// <summary>
        /// Handles real-time conversion from English input to Sinhala script
        /// </summary>
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            var converter = new SinhalaConverter();
            string sinhalaText = converter.Convert(txtInput.Text);
            txtOutput.Text = sinhalaText;

            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        /// <summary>
        /// Clears both input and output boxes
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
        }

        /// <summary>
        /// Optional: Load event handler
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Startup logic here
        }
    }
}