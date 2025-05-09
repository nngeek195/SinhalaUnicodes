using System;
using System.Drawing;
using System.Windows.Forms;

namespace KeyBoard
{
    public partial class Form1 : Form
    {
        private SinhalaConverter _sinhalaConverter;
        private LegacyConverter _legacyConverter;

        public Form1()
        {
            InitializeComponent();

            // Initialize converters
            _sinhalaConverter = new SinhalaConverter();
            _legacyConverter = new LegacyConverter();

            // Set fonts
            txtOutput.Font = new Font("Iskoola Pota", 12F);   // For Sinhala Unicode
            LegOutput.Font = new Font("Kaputa", 12F);         // For Legacy Font (or fallback font)
            txtInput.Font = new Font("Segoe UI", 10F);

            this.Text = "Sinhala Keyboard";
        }

        /// <summary>
        /// Handles real-time conversion from Singlish input to both:
        /// - Sinhala Unicode (in txtOutput)
        /// - Legacy Font (in LegOutput)
        /// </summary>
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            // Step 1: Singlish → Unicode (via SinhalaConverter)
            string sinhalaText = _sinhalaConverter.Convert(txtInput.Text);
            txtOutput.Text = sinhalaText;

            // Step 2: Unicode → Legacy Font (via LegacyConverter)
            string legacyText = _legacyConverter.toisiwara(sinhalaText);
            LegOutput.Text = legacyText;

            // Auto-scroll
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
            LegOutput.SelectionStart = LegOutput.Text.Length;
            LegOutput.ScrollToCaret();
        }

        /// <summary>
        /// Clears all text boxes
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            txtOutput.Clear();
            LegOutput.Clear();
        }

        /// <summary>
        /// Optional load handler
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Startup logic here
        }
        private void label3_Click(object sender, EventArgs e)
        {
            // Handle the click event here
            MessageBox.Show("Label 3 clicked!");
        }
    }
}