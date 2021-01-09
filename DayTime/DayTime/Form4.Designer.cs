
namespace DayTime
{
    partial class Form4
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
            this.AddBox = new System.Windows.Forms.GroupBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.ForumNameLabel = new System.Windows.Forms.Label();
            this.AddBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddBox
            // 
            this.AddBox.Controls.Add(this.NameBox);
            this.AddBox.Controls.Add(this.AddButton);
            this.AddBox.Controls.Add(this.ForumNameLabel);
            this.AddBox.Location = new System.Drawing.Point(10, 6);
            this.AddBox.Name = "AddBox";
            this.AddBox.Size = new System.Drawing.Size(334, 93);
            this.AddBox.TabIndex = 1;
            this.AddBox.TabStop = false;
            this.AddBox.Text = "Add forum";
            this.AddBox.Enter += new System.EventHandler(this.AddBox_Enter);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(92, 19);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(235, 20);
            this.NameBox.TabIndex = 1;
            this.NameBox.Text = "A";
            this.NameBox.TextChanged += new System.EventHandler(this.textBoxLogin_TextChanged);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(122, 51);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 3;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ForumNameLabel
            // 
            this.ForumNameLabel.AutoSize = true;
            this.ForumNameLabel.Location = new System.Drawing.Point(6, 23);
            this.ForumNameLabel.Name = "ForumNameLabel";
            this.ForumNameLabel.Size = new System.Drawing.Size(68, 13);
            this.ForumNameLabel.TabIndex = 0;
            this.ForumNameLabel.Text = "Forum name:";
            this.ForumNameLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 112);
            this.Controls.Add(this.AddBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form4";
            this.Text = "Form4";
            this.AddBox.ResumeLayout(false);
            this.AddBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox AddBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label ForumNameLabel;
    }
}