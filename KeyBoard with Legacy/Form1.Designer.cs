namespace KeyBoard
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param ///name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtInput = new TextBox();
            label2 = new Label();
            txtOutput = new TextBox();
            btnClear = new Button();
            lable3 = new Label();
            LegOutput = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(95, 25);
            label1.TabIndex = 0;
            label1.Text = "Type Here:";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(12, 37);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(400, 31);
            txtInput.TabIndex = 1;
            txtInput.TextChanged += txtInput_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 71);
            label2.Name = "label2";
            label2.Size = new Size(134, 25);
            label2.TabIndex = 2;
            label2.Text = "Sinhala Output:";
            // 
            // txtOutput
            // 
            txtOutput.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtOutput.Location = new Point(12, 99);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(400, 59);
            txtOutput.TabIndex = 3;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(337, 315);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 34);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // lable3
            // 
            lable3.AutoSize = true;
            lable3.Location = new Point(12, 172);
            lable3.Name = "lable3";
            lable3.Size = new Size(132, 25);
            lable3.TabIndex = 5;
            lable3.Text = "Legacy Output:";
            lable3.Click += label3_Click;
            // 
            // LegOutput
            // 
            LegOutput.Font = new Font("Nirmala UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LegOutput.Location = new Point(12, 210);
            LegOutput.Multiline = true;
            LegOutput.Name = "LegOutput";
            LegOutput.ReadOnly = true;
            LegOutput.ScrollBars = ScrollBars.Vertical;
            LegOutput.Size = new Size(400, 59);
            LegOutput.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 361);
            Controls.Add(LegOutput);
            Controls.Add(lable3);
            Controls.Add(btnClear);
            Controls.Add(txtOutput);
            Controls.Add(label2);
            Controls.Add(txtInput);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sinhala Keyboard";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnClear;
        private Label lable3;
        private TextBox LegOutput;
    }
}