
namespace DayTime
{
    partial class Form3
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
            this.PrivateMessageBox = new System.Windows.Forms.GroupBox();
            this.MessageBox = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PrivateMessageBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrivateMessageBox
            // 
            this.PrivateMessageBox.Controls.Add(this.MessageBox);
            this.PrivateMessageBox.Controls.Add(this.MessageLabel);
            this.PrivateMessageBox.Controls.Add(this.UsernameBox);
            this.PrivateMessageBox.Controls.Add(this.sendButton);
            this.PrivateMessageBox.Controls.Add(this.UsernameLabel);
            this.PrivateMessageBox.Location = new System.Drawing.Point(13, 13);
            this.PrivateMessageBox.Margin = new System.Windows.Forms.Padding(4);
            this.PrivateMessageBox.Name = "PrivateMessageBox";
            this.PrivateMessageBox.Padding = new System.Windows.Forms.Padding(4);
            this.PrivateMessageBox.Size = new System.Drawing.Size(483, 205);
            this.PrivateMessageBox.TabIndex = 1;
            this.PrivateMessageBox.TabStop = false;
            this.PrivateMessageBox.Text = "Private Message";
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(124, 60);
            this.MessageBox.Margin = new System.Windows.Forms.Padding(4);
            this.MessageBox.Multiline = true;
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.Size = new System.Drawing.Size(350, 101);
            this.MessageBox.TabIndex = 2;
            this.MessageBox.Text = "Hello";
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(8, 65);
            this.MessageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(69, 17);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "Message:";
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(124, 23);
            this.UsernameBox.Margin = new System.Windows.Forms.Padding(4);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(350, 22);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Text = "1";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(186, 169);
            this.sendButton.Margin = new System.Windows.Forms.Padding(4);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(100, 28);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(8, 28);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(77, 17);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Username:";
            this.UsernameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 228);
            this.Controls.Add(this.PrivateMessageBox);
            this.Name = "Form3";
            this.Text = "Form3";
            this.PrivateMessageBox.ResumeLayout(false);
            this.PrivateMessageBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PrivateMessageBox;
        private System.Windows.Forms.TextBox MessageBox;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label UsernameLabel;
    }
}