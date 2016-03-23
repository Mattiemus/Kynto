namespace KyntoServerTester.DebugClient
{
    partial class DebugClientForm
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
            this.InBoundListBox = new System.Windows.Forms.ListBox();
            this.OutBoundListBox = new System.Windows.Forms.ListBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ToSendBox = new System.Windows.Forms.TextBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InBoundListBox
            // 
            this.InBoundListBox.FormattingEnabled = true;
            this.InBoundListBox.Location = new System.Drawing.Point(12, 12);
            this.InBoundListBox.Name = "InBoundListBox";
            this.InBoundListBox.Size = new System.Drawing.Size(301, 381);
            this.InBoundListBox.TabIndex = 0;
            // 
            // OutBoundListBox
            // 
            this.OutBoundListBox.FormattingEnabled = true;
            this.OutBoundListBox.Location = new System.Drawing.Point(319, 12);
            this.OutBoundListBox.Name = "OutBoundListBox";
            this.OutBoundListBox.Size = new System.Drawing.Size(301, 381);
            this.OutBoundListBox.TabIndex = 1;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(545, 398);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ToSendBox
            // 
            this.ToSendBox.Location = new System.Drawing.Point(12, 400);
            this.ToSendBox.Multiline = true;
            this.ToSendBox.Name = "ToSendBox";
            this.ToSendBox.Size = new System.Drawing.Size(446, 50);
            this.ToSendBox.TabIndex = 3;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(545, 427);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 4;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(464, 398);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 52);
            this.SendButton.TabIndex = 5;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // DebugClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 465);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ToSendBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.OutBoundListBox);
            this.Controls.Add(this.InBoundListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DebugClientForm";
            this.Text = "DebugClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox InBoundListBox;
        private System.Windows.Forms.ListBox OutBoundListBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox ToSendBox;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Button SendButton;
    }
}