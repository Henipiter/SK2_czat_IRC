
namespace DayTime
{
    partial class UserControl1
    {
        /// <summary> 
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary> 
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.CanalBox = new System.Windows.Forms.GroupBox();
            this.UserBox = new System.Windows.Forms.GroupBox();
            this.ChatBox = new System.Windows.Forms.GroupBox();
            this.UserWriteButton = new System.Windows.Forms.Button();
            this.UserRefreshButton = new System.Windows.Forms.Button();
            this.ForumRefreshButton = new System.Windows.Forms.Button();
            this.ForumAddButton = new System.Windows.Forms.Button();
            this.ForumDeleteButton = new System.Windows.Forms.Button();
            this.ChatSendButton = new System.Windows.Forms.Button();
            this.ChatWriteBox = new System.Windows.Forms.TextBox();
            this.ForumListBox = new System.Windows.Forms.TextBox();
            this.UserListBox = new System.Windows.Forms.TextBox();
            this.ChatHistoryBox = new System.Windows.Forms.TextBox();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.CanalBox.SuspendLayout();
            this.UserBox.SuspendLayout();
            this.ChatBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CanalBox
            // 
            this.CanalBox.Controls.Add(this.ForumListBox);
            this.CanalBox.Controls.Add(this.ForumDeleteButton);
            this.CanalBox.Controls.Add(this.ForumAddButton);
            this.CanalBox.Controls.Add(this.ForumRefreshButton);
            this.CanalBox.Location = new System.Drawing.Point(39, 24);
            this.CanalBox.Name = "CanalBox";
            this.CanalBox.Size = new System.Drawing.Size(185, 266);
            this.CanalBox.TabIndex = 0;
            this.CanalBox.TabStop = false;
            this.CanalBox.Text = "Lista kanalow";
            // 
            // UserBox
            // 
            this.UserBox.Controls.Add(this.UserListBox);
            this.UserBox.Controls.Add(this.UserRefreshButton);
            this.UserBox.Controls.Add(this.UserWriteButton);
            this.UserBox.Location = new System.Drawing.Point(39, 296);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(185, 266);
            this.UserBox.TabIndex = 0;
            this.UserBox.TabStop = false;
            this.UserBox.Text = "Dostepni uzytkownicy";
            // 
            // ChatBox
            // 
            this.ChatBox.Controls.Add(this.ChatHistoryBox);
            this.ChatBox.Controls.Add(this.ChatWriteBox);
            this.ChatBox.Controls.Add(this.ChatSendButton);
            this.ChatBox.Controls.Add(this.LogoutButton);
            this.ChatBox.Location = new System.Drawing.Point(249, 24);
            this.ChatBox.Name = "ChatBox";
            this.ChatBox.Size = new System.Drawing.Size(771, 538);
            this.ChatBox.TabIndex = 0;
            this.ChatBox.TabStop = false;
            this.ChatBox.Text = "Czat";
            // 
            // UserWriteButton
            // 
            this.UserWriteButton.Location = new System.Drawing.Point(19, 218);
            this.UserWriteButton.Name = "UserWriteButton";
            this.UserWriteButton.Size = new System.Drawing.Size(143, 28);
            this.UserWriteButton.TabIndex = 0;
            this.UserWriteButton.Text = "Napisz";
            this.UserWriteButton.UseVisualStyleBackColor = true;
            // 
            // UserRefreshButton
            // 
            this.UserRefreshButton.Location = new System.Drawing.Point(19, 184);
            this.UserRefreshButton.Name = "UserRefreshButton";
            this.UserRefreshButton.Size = new System.Drawing.Size(143, 28);
            this.UserRefreshButton.TabIndex = 0;
            this.UserRefreshButton.Text = "Refresh";
            this.UserRefreshButton.UseVisualStyleBackColor = true;
            // 
            // ForumRefreshButton
            // 
            this.ForumRefreshButton.Location = new System.Drawing.Point(19, 162);
            this.ForumRefreshButton.Name = "ForumRefreshButton";
            this.ForumRefreshButton.Size = new System.Drawing.Size(143, 28);
            this.ForumRefreshButton.TabIndex = 0;
            this.ForumRefreshButton.Text = "Refresh";
            this.ForumRefreshButton.UseVisualStyleBackColor = true;
            // 
            // ForumAddButton
            // 
            this.ForumAddButton.Location = new System.Drawing.Point(19, 196);
            this.ForumAddButton.Name = "ForumAddButton";
            this.ForumAddButton.Size = new System.Drawing.Size(143, 28);
            this.ForumAddButton.TabIndex = 0;
            this.ForumAddButton.Text = "Add";
            this.ForumAddButton.UseVisualStyleBackColor = true;
            // 
            // ForumDeleteButton
            // 
            this.ForumDeleteButton.Location = new System.Drawing.Point(19, 230);
            this.ForumDeleteButton.Name = "ForumDeleteButton";
            this.ForumDeleteButton.Size = new System.Drawing.Size(143, 28);
            this.ForumDeleteButton.TabIndex = 0;
            this.ForumDeleteButton.Text = "Delete";
            this.ForumDeleteButton.UseVisualStyleBackColor = true;
            // 
            // ChatSendButton
            // 
            this.ChatSendButton.Location = new System.Drawing.Point(603, 456);
            this.ChatSendButton.Name = "ChatSendButton";
            this.ChatSendButton.Size = new System.Drawing.Size(143, 62);
            this.ChatSendButton.TabIndex = 0;
            this.ChatSendButton.Text = "Send";
            this.ChatSendButton.UseVisualStyleBackColor = true;
            // 
            // ChatWriteBox
            // 
            this.ChatWriteBox.Location = new System.Drawing.Point(23, 456);
            this.ChatWriteBox.Multiline = true;
            this.ChatWriteBox.Name = "ChatWriteBox";
            this.ChatWriteBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatWriteBox.Size = new System.Drawing.Size(564, 61);
            this.ChatWriteBox.TabIndex = 1;
            // 
            // ForumListBox
            // 
            this.ForumListBox.Location = new System.Drawing.Point(20, 24);
            this.ForumListBox.Multiline = true;
            this.ForumListBox.Name = "ForumListBox";
            this.ForumListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ForumListBox.Size = new System.Drawing.Size(141, 128);
            this.ForumListBox.TabIndex = 1;
            // 
            // UserListBox
            // 
            this.UserListBox.Location = new System.Drawing.Point(20, 31);
            this.UserListBox.Multiline = true;
            this.UserListBox.Name = "UserListBox";
            this.UserListBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserListBox.Size = new System.Drawing.Size(141, 141);
            this.UserListBox.TabIndex = 1;
            // 
            // ChatHistoryBox
            // 
            this.ChatHistoryBox.Location = new System.Drawing.Point(23, 51);
            this.ChatHistoryBox.Multiline = true;
            this.ChatHistoryBox.Name = "ChatHistoryBox";
            this.ChatHistoryBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ChatHistoryBox.Size = new System.Drawing.Size(722, 392);
            this.ChatHistoryBox.TabIndex = 2;
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(602, 17);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(143, 28);
            this.LogoutButton.TabIndex = 0;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.UseVisualStyleBackColor = true;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChatBox);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.CanalBox);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1063, 601);
            this.CanalBox.ResumeLayout(false);
            this.CanalBox.PerformLayout();
            this.UserBox.ResumeLayout(false);
            this.UserBox.PerformLayout();
            this.ChatBox.ResumeLayout(false);
            this.ChatBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CanalBox;
        private System.Windows.Forms.GroupBox UserBox;
        private System.Windows.Forms.GroupBox ChatBox;
        private System.Windows.Forms.Button ForumDeleteButton;
        private System.Windows.Forms.Button ForumAddButton;
        private System.Windows.Forms.Button ForumRefreshButton;
        private System.Windows.Forms.Button UserRefreshButton;
        private System.Windows.Forms.Button UserWriteButton;
        private System.Windows.Forms.Button ChatSendButton;
        private System.Windows.Forms.TextBox ChatWriteBox;
        private System.Windows.Forms.TextBox ForumListBox;
        private System.Windows.Forms.TextBox UserListBox;
        private System.Windows.Forms.TextBox ChatHistoryBox;
        private System.Windows.Forms.Button LogoutButton;
    }
}
