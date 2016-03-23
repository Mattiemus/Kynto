namespace KyntoDatabaseCodeGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.GenerateButton = new System.Windows.Forms.Button();
            this.InputCode = new System.Windows.Forms.TextBox();
            this.OutputCode = new System.Windows.Forms.TextBox();
            this.DatabaseNameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GenerateButton
            // 
            this.GenerateButton.AccessibleDescription = "";
            this.GenerateButton.Location = new System.Drawing.Point(12, 543);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(75, 23);
            this.GenerateButton.TabIndex = 0;
            this.GenerateButton.Text = "Generate";
            this.GenerateButton.UseVisualStyleBackColor = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // InputCode
            // 
            this.InputCode.Location = new System.Drawing.Point(12, 38);
            this.InputCode.Multiline = true;
            this.InputCode.Name = "InputCode";
            this.InputCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InputCode.Size = new System.Drawing.Size(358, 499);
            this.InputCode.TabIndex = 1;
            // 
            // OutputCode
            // 
            this.OutputCode.Location = new System.Drawing.Point(376, 12);
            this.OutputCode.Multiline = true;
            this.OutputCode.Name = "OutputCode";
            this.OutputCode.ReadOnly = true;
            this.OutputCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputCode.Size = new System.Drawing.Size(563, 525);
            this.OutputCode.TabIndex = 2;
            // 
            // DatabaseNameInput
            // 
            this.DatabaseNameInput.Location = new System.Drawing.Point(81, 12);
            this.DatabaseNameInput.Name = "DatabaseNameInput";
            this.DatabaseNameInput.Size = new System.Drawing.Size(289, 20);
            this.DatabaseNameInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Database:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 578);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DatabaseNameInput);
            this.Controls.Add(this.OutputCode);
            this.Controls.Add(this.InputCode);
            this.Controls.Add(this.GenerateButton);
            this.Name = "MainForm";
            this.Text = "Database Code Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateButton;
        private System.Windows.Forms.TextBox InputCode;
        private System.Windows.Forms.TextBox OutputCode;
        private System.Windows.Forms.TextBox DatabaseNameInput;
        private System.Windows.Forms.Label label1;
    }
}

