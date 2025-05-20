namespace KeyBoard
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtInput = new TextBox();
            txtOutput = new TextBox();
            LegOutput = new TextBox();
            cmbConversionMode = new ComboBox();
            btnClear = new Button();
            btnCopySinhala = new Button();
            btnCopyLegacy = new Button();
            btnToggleTranslation = new Button();
            btnToggleWord = new Button();
            label2 = new Label();
            label3 = new Label();
            supportLinkLabel = new Label();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.Font = new Font("Segoe UI", 12F);
            txtInput.Location = new Point(20, 53);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(474, 39);
            txtInput.TabIndex = 0;
            txtInput.TextChanged += txtInput_TextChanged;
            txtInput.KeyDown += txtInput_KeyDown;
            // 
            // txtOutput
            // 
            txtOutput.BackColor = Color.WhiteSmoke;
            txtOutput.Font = new Font("Nirmala UI", 14F);
            txtOutput.Location = new Point(20, 120);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(474, 220);
            txtOutput.TabIndex = 1;
            // 
            // LegOutput
            // 
            LegOutput.BackColor = Color.WhiteSmoke;
            LegOutput.Font = new Font("Microsoft Sans Serif", 14F);
            LegOutput.Location = new Point(20, 402);
            LegOutput.Multiline = true;
            LegOutput.Name = "LegOutput";
            LegOutput.ReadOnly = true;
            LegOutput.ScrollBars = ScrollBars.Vertical;
            LegOutput.Size = new Size(474, 100);
            LegOutput.TabIndex = 2;
            // 
            // cmbConversionMode
            // 
            cmbConversionMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbConversionMode.FormattingEnabled = true;
            cmbConversionMode.Items.AddRange(new object[] { "Singlish to Sinhala", "Singlish to Legacy" });
            cmbConversionMode.Location = new Point(20, 576);
            cmbConversionMode.Name = "cmbConversionMode";
            cmbConversionMode.Size = new Size(210, 36);
            cmbConversionMode.TabIndex = 3;
            cmbConversionMode.SelectedIndexChanged += CmbConversionMode_SelectedIndexChanged;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(255, 87, 34);
            btnClear.Font = new Font("Segoe UI", 10F);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(20, 632);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(474, 50);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear All";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnCopySinhala
            // 
            btnCopySinhala.BackColor = Color.FromArgb(46, 125, 50);
            btnCopySinhala.Font = new Font("Segoe UI", 10F);
            btnCopySinhala.ForeColor = Color.White;
            btnCopySinhala.Location = new Point(294, 346);
            btnCopySinhala.Name = "btnCopySinhala";
            btnCopySinhala.Size = new Size(200, 50);
            btnCopySinhala.TabIndex = 5;
            btnCopySinhala.Text = "Copy Sinhala";
            btnCopySinhala.UseVisualStyleBackColor = false;
            btnCopySinhala.Click += btnCopySinhala_Click;
            // 
            // btnCopyLegacy
            // 
            btnCopyLegacy.BackColor = Color.FromArgb(63, 81, 181);
            btnCopyLegacy.Font = new Font("Segoe UI", 10F);
            btnCopyLegacy.ForeColor = Color.White;
            btnCopyLegacy.Location = new Point(294, 520);
            btnCopyLegacy.Name = "btnCopyLegacy";
            btnCopyLegacy.Size = new Size(200, 50);
            btnCopyLegacy.TabIndex = 6;
            btnCopyLegacy.Text = "Copy Legacy";
            btnCopyLegacy.UseVisualStyleBackColor = false;
            btnCopyLegacy.Click += btnCopyLegacy_Click;
            // 
            // btnToggleTranslation
            // 
            btnToggleTranslation.BackColor = Color.FromArgb(0, 150, 136);
            btnToggleTranslation.Font = new Font("Segoe UI", 10F);
            btnToggleTranslation.ForeColor = Color.White;
            btnToggleTranslation.Location = new Point(294, 576);
            btnToggleTranslation.Name = "btnToggleTranslation";
            btnToggleTranslation.Size = new Size(200, 50);
            btnToggleTranslation.TabIndex = 7;
            btnToggleTranslation.Text = "Translation: ON";
            btnToggleTranslation.UseVisualStyleBackColor = false;
            btnToggleTranslation.Click += btnToggleTranslation_Click;
            // 
            // btnToggleWord
            // 
            btnToggleWord.BackColor = Color.FromArgb(33, 150, 243);
            btnToggleWord.Font = new Font("Segoe UI", 10F);
            btnToggleWord.ForeColor = Color.White;
            btnToggleWord.Location = new Point(20, 520);
            btnToggleWord.Name = "btnToggleWord";
            btnToggleWord.Size = new Size(210, 50);
            btnToggleWord.TabIndex = 8;
            btnToggleWord.Text = "Enable Integration";
            btnToggleWord.UseVisualStyleBackColor = false;
            btnToggleWord.Click += btnToggleWord_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.Location = new Point(20, 95);
            label2.Name = "label2";
            label2.Size = new Size(159, 28);
            label2.TabIndex = 10;
            label2.Text = "Sinhala Output:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.Location = new Point(20, 368);
            label3.Name = "label3";
            label3.Size = new Size(155, 28);
            label3.TabIndex = 9;
            label3.Text = "Legacy Output:";
            label3.Click += label3_Click;
            // 
            // supportLinkLabel
            // 
            supportLinkLabel.BackColor = Color.LightBlue;
            supportLinkLabel.Cursor = Cursors.Hand;
            supportLinkLabel.Dock = DockStyle.Bottom;
            supportLinkLabel.ForeColor = Color.DarkBlue;
            supportLinkLabel.Location = new Point(0, 702);
            supportLinkLabel.Name = "supportLinkLabel";
            supportLinkLabel.Size = new Size(506, 28);
            supportLinkLabel.TabIndex = 0;
            supportLinkLabel.Text = "🌐 Support";
            supportLinkLabel.TextAlign = ContentAlignment.MiddleCenter;
            supportLinkLabel.Click += supportLinkLabel_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(506, 730);
            Controls.Add(supportLinkLabel);
            Controls.Add(btnToggleWord);
            Controls.Add(btnToggleTranslation);
            Controls.Add(btnCopyLegacy);
            Controls.Add(btnCopySinhala);
            Controls.Add(btnClear);
            Controls.Add(LegOutput);
            Controls.Add(label3);
            Controls.Add(txtOutput);
            Controls.Add(label2);
            Controls.Add(txtInput);
            Controls.Add(cmbConversionMode);
            Font = new Font("Segoe UI", 10F);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SinhalaUnicodes";
            Load += Form1_Load_1;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal System.Windows.Forms.TextBox txtInput;
        internal System.Windows.Forms.TextBox txtOutput;
        internal System.Windows.Forms.TextBox LegOutput;
        internal System.Windows.Forms.ComboBox cmbConversionMode;
        internal System.Windows.Forms.Button btnClear;
        internal System.Windows.Forms.Button btnCopySinhala;
        internal System.Windows.Forms.Button btnCopyLegacy;
        internal System.Windows.Forms.Button btnToggleTranslation;
        internal System.Windows.Forms.Button btnToggleWord;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
    }
}