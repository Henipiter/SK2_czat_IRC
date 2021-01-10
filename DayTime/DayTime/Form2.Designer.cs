
using System;

namespace DayTime
{
    partial class Form2
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
            this.ForumRefreshButton = new System.Windows.Forms.Button();
            this.ForumAddButton = new System.Windows.Forms.Button();
            this.ForumListBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.UserWriteButton = new System.Windows.Forms.Button();
            this.UserRefreshButton = new System.Windows.Forms.Button();
            this.UserListBox = new System.Windows.Forms.TextBox();
            this.ForumBox = new System.Windows.Forms.GroupBox();
            this.ForumChangeButton = new System.Windows.Forms.Button();
            this.ForumDeleteButton = new System.Windows.Forms.Button();
            this.userBox = new System.Windows.Forms.GroupBox();
            this.ChatBox = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.RichTextBox();
            this.ForumBox.SuspendLayout();
            this.userBox.SuspendLayout();
            this.ChatBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ForumRefreshButton
            // 
            this.ForumRefreshButton.Location = new System.Drawing.Point(21, 158);
            this.ForumRefreshButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumRefreshButton.Name = "ForumRefreshButton";
            this.ForumRefreshButton.Size = new System.Drawing.Size(143, 28);
            this.ForumRefreshButton.TabIndex = 0;
            this.ForumRefreshButton.Text = "Refresh";
            this.ForumRefreshButton.UseVisualStyleBackColor = true;
            this.ForumRefreshButton.Click += new System.EventHandler(this.ForumRefreshButton_Click);
            // 
            // ForumAddButton
            // 
            this.ForumAddButton.Location = new System.Drawing.Point(21, 191);
            this.ForumAddButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumAddButton.Name = "ForumAddButton";
            this.ForumAddButton.Size = new System.Drawing.Size(143, 28);
            this.ForumAddButton.TabIndex = 0;
            this.ForumAddButton.Text = "Add";
            this.ForumAddButton.UseVisualStyleBackColor = true;
            this.ForumAddButton.Click += new System.EventHandler(this.ForumAddButton_Click);
            // 
            // ForumListBox
            // 
            this.ForumListBox.Location = new System.Drawing.Point(23, 18);
            this.ForumListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumListBox.Multiline = true;
            this.ForumListBox.Name = "ForumListBox";
            this.ForumListBox.ReadOnly = true;
            this.ForumListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ForumListBox.Size = new System.Drawing.Size(141, 128);
            this.ForumListBox.TabIndex = 1;
            this.ForumListBox.TextChanged += new System.EventHandler(this.ForumListBox_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(595, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(596, 476);
            this.SendButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(143, 62);
            this.SendButton.TabIndex = 0;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(16, 476);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(564, 61);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(92, 28);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(120, 22);
            this.textBox3.TabIndex = 4;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Forumname";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(331, 26);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(120, 22);
            this.textBox4.TabIndex = 4;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // UserWriteButton
            // 
            this.UserWriteButton.Location = new System.Drawing.Point(11, 230);
            this.UserWriteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserWriteButton.Name = "UserWriteButton";
            this.UserWriteButton.Size = new System.Drawing.Size(143, 28);
            this.UserWriteButton.TabIndex = 0;
            this.UserWriteButton.Text = "Napisz";
            this.UserWriteButton.UseVisualStyleBackColor = true;
            this.UserWriteButton.Click += new System.EventHandler(this.UserWriteButton_Click);
            // 
            // UserRefreshButton
            // 
            this.UserRefreshButton.Location = new System.Drawing.Point(11, 196);
            this.UserRefreshButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserRefreshButton.Name = "UserRefreshButton";
            this.UserRefreshButton.Size = new System.Drawing.Size(143, 28);
            this.UserRefreshButton.TabIndex = 0;
            this.UserRefreshButton.Text = "Refresh";
            this.UserRefreshButton.UseVisualStyleBackColor = true;
            this.UserRefreshButton.Click += new System.EventHandler(this.UserRefreshButton_Click);
            // 
            // UserListBox
            // 
            this.UserListBox.Location = new System.Drawing.Point(11, 43);
            this.UserListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserListBox.Multiline = true;
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.ReadOnly = true;
            this.UserListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserListBox.Size = new System.Drawing.Size(141, 141);
            this.UserListBox.TabIndex = 1;
            this.UserListBox.TextChanged += new System.EventHandler(this.UserListBox_TextChanged);
            // 
            // ForumBox
            // 
            this.ForumBox.Controls.Add(this.ForumChangeButton);
            this.ForumBox.Controls.Add(this.ForumListBox);
            this.ForumBox.Controls.Add(this.ForumDeleteButton);
            this.ForumBox.Controls.Add(this.ForumRefreshButton);
            this.ForumBox.Controls.Add(this.ForumAddButton);
            this.ForumBox.Location = new System.Drawing.Point(0, 12);
            this.ForumBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumBox.Name = "ForumBox";
            this.ForumBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumBox.Size = new System.Drawing.Size(184, 299);
            this.ForumBox.TabIndex = 5;
            this.ForumBox.TabStop = false;
            this.ForumBox.Text = "Forum List";
            // 
            // ForumChangeButton
            // 
            this.ForumChangeButton.Location = new System.Drawing.Point(21, 224);
            this.ForumChangeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumChangeButton.Name = "ForumChangeButton";
            this.ForumChangeButton.Size = new System.Drawing.Size(143, 28);
            this.ForumChangeButton.TabIndex = 2;
            this.ForumChangeButton.Text = "Change";
            this.ForumChangeButton.UseVisualStyleBackColor = true;
            this.ForumChangeButton.Click += new System.EventHandler(this.ForumChangeButton_Click);
            // 
            // ForumDeleteButton
            // 
            this.ForumDeleteButton.Location = new System.Drawing.Point(21, 257);
            this.ForumDeleteButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ForumDeleteButton.Name = "ForumDeleteButton";
            this.ForumDeleteButton.Size = new System.Drawing.Size(143, 28);
            this.ForumDeleteButton.TabIndex = 0;
            this.ForumDeleteButton.Text = "Delete";
            this.ForumDeleteButton.UseVisualStyleBackColor = true;
            this.ForumDeleteButton.Click += new System.EventHandler(this.ForumDeleteButton_Click);
            // 
            // userBox
            // 
            this.userBox.Controls.Add(this.UserListBox);
            this.userBox.Controls.Add(this.UserRefreshButton);
            this.userBox.Controls.Add(this.UserWriteButton);
            this.userBox.Location = new System.Drawing.Point(12, 316);
            this.userBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.userBox.Name = "userBox";
            this.userBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.userBox.Size = new System.Drawing.Size(172, 261);
            this.userBox.TabIndex = 6;
            this.userBox.TabStop = false;
            this.userBox.Text = "User List";
            // 
            // ChatBox
            // 
            this.ChatBox.Controls.Add(this.textBox1);
            this.ChatBox.Controls.Add(this.textBox4);
            this.ChatBox.Controls.Add(this.label2);
            this.ChatBox.Controls.Add(this.textBox3);
            this.ChatBox.Controls.Add(this.label1);
            this.ChatBox.Controls.Add(this.textBox2);
            this.ChatBox.Controls.Add(this.button1);
            this.ChatBox.Controls.Add(this.SendButton);
            this.ChatBox.Location = new System.Drawing.Point(191, 2);
            this.ChatBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChatBox.Size = new System.Drawing.Size(755, 572);
            this.ChatBox.TabIndex = 7;
            this.ChatBox.TabStop = false;
            this.ChatBox.Text = "Chat";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(36, 80);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(640, 334);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 638);
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.ForumBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ForumBox.ResumeLayout(false);
            this.ForumBox.PerformLayout();
            this.userBox.ResumeLayout(false);
            this.userBox.PerformLayout();
            this.ChatBox.ResumeLayout(false);
            this.ChatBox.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Button ForumRefreshButton;
        private System.Windows.Forms.Button ForumAddButton;
        private System.Windows.Forms.TextBox ForumListBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button UserWriteButton;
        private System.Windows.Forms.Button UserRefreshButton;
        private System.Windows.Forms.TextBox UserListBox;
        private System.Windows.Forms.GroupBox ForumBox;
        private System.Windows.Forms.GroupBox userBox;
        private System.Windows.Forms.GroupBox ChatBox;
        private System.Windows.Forms.Button ForumDeleteButton;
        private System.Windows.Forms.Button ForumChangeButton;
        private System.Windows.Forms.RichTextBox textBox1;
    }
}