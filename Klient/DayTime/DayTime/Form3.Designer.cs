
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
            this.MessageBox1 = new System.Windows.Forms.TextBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PrivateMessageBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PrivateMessageBox
            // 
            this.PrivateMessageBox.Controls.Add(this.MessageBox1);
            this.PrivateMessageBox.Controls.Add(this.MessageLabel);
            this.PrivateMessageBox.Controls.Add(this.UsernameBox);
            this.PrivateMessageBox.Controls.Add(this.sendButton);
            this.PrivateMessageBox.Controls.Add(this.UsernameLabel);
            this.PrivateMessageBox.Location = new System.Drawing.Point(10, 11);
            this.PrivateMessageBox.Name = "PrivateMessageBox";
            this.PrivateMessageBox.Size = new System.Drawing.Size(362, 167);
            this.PrivateMessageBox.TabIndex = 1;
            this.PrivateMessageBox.TabStop = false;
            this.PrivateMessageBox.Text = "Private Message";
            this.PrivateMessageBox.Enter += new System.EventHandler(this.PrivateMessageBox_Enter);
            // 
            // MessageBox1
            // 
            this.MessageBox1.Location = new System.Drawing.Point(93, 49);
            this.MessageBox1.Multiline = true;
            this.MessageBox1.Name = "MessageBox1";
            this.MessageBox1.Size = new System.Drawing.Size(264, 83);
            this.MessageBox1.TabIndex = 2;
            this.MessageBox1.Text = "Hello";
            this.MessageBox1.TextChanged += new System.EventHandler(this.MessageBox_TextChanged);
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoSize = true;
            this.MessageLabel.Location = new System.Drawing.Point(6, 53);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(53, 13);
            this.MessageLabel.TabIndex = 0;
            this.MessageLabel.Text = "Message:";
            this.MessageLabel.Click += new System.EventHandler(this.MessageLabel_Click);
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(93, 19);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(264, 20);
            this.UsernameBox.TabIndex = 1;
            this.UsernameBox.Text = "1";
            this.UsernameBox.TextChanged += new System.EventHandler(this.UsernameBox_TextChanged);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(140, 137);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 3;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(6, 23);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Username:";
            this.UsernameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 185);
            this.Controls.Add(this.PrivateMessageBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form3";
            this.Text = "Form3";
            this.PrivateMessageBox.ResumeLayout(false);
            this.PrivateMessageBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox PrivateMessageBox;
        private System.Windows.Forms.TextBox MessageBox1;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label UsernameLabel;
    }
}