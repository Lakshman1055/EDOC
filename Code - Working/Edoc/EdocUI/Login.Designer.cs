namespace EdocUI
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.loginContainerTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.LoginStatusBar = new System.Windows.Forms.StatusStrip();
            this.LoginStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.userFirstName = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.userLastNameInput = new System.Windows.Forms.TextBox();
            this.userLastName = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userFirstNameInput = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.loginContainerTable.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.LoginStatusBar.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // loginContainerTable
            // 
            this.loginContainerTable.AutoSize = true;
            this.loginContainerTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.loginContainerTable.ColumnCount = 3;
            this.loginContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.06206F));
            this.loginContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.87977F));
            this.loginContainerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.99354F));
            this.loginContainerTable.Controls.Add(this.panel1, 0, 0);
            this.loginContainerTable.Controls.Add(this.LoginStatusBar, 0, 3);
            this.loginContainerTable.Controls.Add(this.flowLayoutPanel1, 1, 2);
            this.loginContainerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginContainerTable.Location = new System.Drawing.Point(0, 0);
            this.loginContainerTable.Name = "loginContainerTable";
            this.loginContainerTable.RowCount = 4;
            this.loginContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.997781F));
            this.loginContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.45079F));
            this.loginContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.39611F));
            this.loginContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.00074F));
            this.loginContainerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.loginContainerTable.Size = new System.Drawing.Size(1547, 823);
            this.loginContainerTable.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.loginContainerTable.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1547, 57);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.Image = global::EdocUI.Properties.Resources.edoc_logo;
            this.pictureBox4.Location = new System.Drawing.Point(36, 3);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(129, 59);
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // LoginStatusBar
            // 
            this.LoginStatusBar.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.loginContainerTable.SetColumnSpan(this.LoginStatusBar, 3);
            this.LoginStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginStatusLabel});
            this.LoginStatusBar.Location = new System.Drawing.Point(0, 798);
            this.LoginStatusBar.MinimumSize = new System.Drawing.Size(1547, 25);
            this.LoginStatusBar.Name = "LoginStatusBar";
            this.LoginStatusBar.Size = new System.Drawing.Size(1547, 25);
            this.LoginStatusBar.SizingGrip = false;
            this.LoginStatusBar.TabIndex = 9;
            this.LoginStatusBar.Text = "statusStrip1";
            // 
            // LoginStatusLabel
            // 
            this.LoginStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginStatusLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LoginStatusLabel.Image = global::EdocUI.Properties.Resources.icon_done;
            this.LoginStatusLabel.Name = "LoginStatusLabel";
            this.LoginStatusLabel.Size = new System.Drawing.Size(53, 20);
            this.LoginStatusLabel.Text = "Done";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.flowLayoutPanel1.Controls.Add(this.pictureBox3);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Controls.Add(this.pictureBox5);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(503, 250);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(572, 316);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImage = global::EdocUI.Properties.Resources.lft_tileBg;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(15, 288);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.tableLayoutPanel1.BackgroundImage = global::EdocUI.Properties.Resources.cnt_tileBg;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.76596F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.24371F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.87805F));
            this.tableLayoutPanel1.Controls.Add(this.userFirstName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.userLastNameInput, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.userLastName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.applyButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.userFirstNameInput, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.70616F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(482, 288);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // userFirstName
            // 
            this.userFirstName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.userFirstName.BackColor = System.Drawing.Color.Transparent;
            this.userFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userFirstName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.userFirstName.Location = new System.Drawing.Point(64, 150);
            this.userFirstName.Name = "userFirstName";
            this.userFirstName.Size = new System.Drawing.Size(101, 22);
            this.userFirstName.TabIndex = 0;
            this.userFirstName.Text = "PASSWORD";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox2.Image = global::EdocUI.Properties.Resources.icn_lck;
            this.pictureBox2.Location = new System.Drawing.Point(36, 151);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 22);
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // userLastNameInput
            // 
            this.userLastNameInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.userLastNameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLastNameInput.Location = new System.Drawing.Point(171, 72);
            this.userLastNameInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.userLastNameInput.MinimumSize = new System.Drawing.Size(230, 24);
            this.userLastNameInput.Name = "userLastNameInput";
            this.userLastNameInput.Size = new System.Drawing.Size(230, 24);
            this.userLastNameInput.TabIndex = 1;
            // 
            // userLastName
            // 
            this.userLastName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.userLastName.BackColor = System.Drawing.Color.Transparent;
            this.userLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userLastName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.userLastName.Location = new System.Drawing.Point(64, 75);
            this.userLastName.Name = "userLastName";
            this.userLastName.Size = new System.Drawing.Size(101, 19);
            this.userLastName.TabIndex = 0;
            this.userLastName.Text = "USERNAME";
            // 
            // applyButton
            // 
            this.applyButton.BackColor = System.Drawing.Color.Transparent;
            this.applyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.applyButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.applyButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(142)))), ((int)(((byte)(168)))));
            this.applyButton.FlatAppearance.BorderSize = 0;
            this.applyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.applyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Image = global::EdocUI.Properties.Resources.btn_login;
            this.applyButton.Location = new System.Drawing.Point(171, 207);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(113, 45);
            this.applyButton.TabIndex = 3;
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::EdocUI.Properties.Resources.icn_user;
            this.pictureBox1.Location = new System.Drawing.Point(35, 76);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 20);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // userFirstNameInput
            // 
            this.userFirstNameInput.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.userFirstNameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userFirstNameInput.Location = new System.Drawing.Point(171, 149);
            this.userFirstNameInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.userFirstNameInput.MinimumSize = new System.Drawing.Size(230, 24);
            this.userFirstNameInput.Name = "userFirstNameInput";
            this.userFirstNameInput.PasswordChar = '*';
            this.userFirstNameInput.Size = new System.Drawing.Size(230, 24);
            this.userFirstNameInput.TabIndex = 2;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.pictureBox5.BackgroundImage = global::EdocUI.Properties.Resources.rgt_tileBg;
            this.pictureBox5.Location = new System.Drawing.Point(497, 0);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(15, 288);
            this.pictureBox5.TabIndex = 11;
            this.pictureBox5.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(1547, 823);
            this.Controls.Add(this.loginContainerTable);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.Text = "Admin Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.loginContainerTable.ResumeLayout(false);
            this.loginContainerTable.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.LoginStatusBar.ResumeLayout(false);
            this.LoginStatusBar.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

     
    

        #endregion

        private System.Windows.Forms.TableLayoutPanel loginContainerTable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.StatusStrip LoginStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel LoginStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label userLastName;
        private System.Windows.Forms.Label userFirstName;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox userLastNameInput;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.TextBox userFirstNameInput;
    }
}