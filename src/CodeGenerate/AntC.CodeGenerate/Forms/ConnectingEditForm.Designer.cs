
namespace AntC.CodeGenerate.Forms
{
    partial class ConnectingEditForm
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDbType = new System.Windows.Forms.ComboBox();
            this.tbConnName = new System.Windows.Forms.TextBox();
            this.tbDbHost = new System.Windows.Forms.TextBox();
            this.tbDbPost = new System.Windows.Forms.TextBox();
            this.tbDbUserName = new System.Windows.Forms.TextBox();
            this.tbDbPassword = new System.Windows.Forms.TextBox();
            this.buttonTestConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(194, 305);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(86, 23);
            this.buttonOk.TabIndex = 11;
            this.buttonOk.Text = "保存";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(284, 305);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(39, 35);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(68, 17);
            this.label.TabIndex = 2;
            this.label.Text = "连接名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "数据库类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Host：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "端口：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "密码：";
            // 
            // cbDbType
            // 
            this.cbDbType.FormattingEnabled = true;
            this.cbDbType.Location = new System.Drawing.Point(122, 75);
            this.cbDbType.Name = "cbDbType";
            this.cbDbType.Size = new System.Drawing.Size(207, 25);
            this.cbDbType.TabIndex = 2;
            // 
            // tbConnName
            // 
            this.tbConnName.Location = new System.Drawing.Point(122, 30);
            this.tbConnName.Name = "tbConnName";
            this.tbConnName.Size = new System.Drawing.Size(207, 23);
            this.tbConnName.TabIndex = 1;
            this.tbConnName.TextChanged += new System.EventHandler(this.tbConnName_TextChanged);
            // 
            // tbDbHost
            // 
            this.tbDbHost.Location = new System.Drawing.Point(122, 122);
            this.tbDbHost.Name = "tbDbHost";
            this.tbDbHost.Size = new System.Drawing.Size(207, 23);
            this.tbDbHost.TabIndex = 3;
            this.tbDbHost.TextChanged += new System.EventHandler(this.tbDbHost_TextChanged);
            // 
            // tbDbPost
            // 
            this.tbDbPost.Location = new System.Drawing.Point(122, 167);
            this.tbDbPost.MaxLength = 5;
            this.tbDbPost.Name = "tbDbPost";
            this.tbDbPost.Size = new System.Drawing.Size(207, 23);
            this.tbDbPost.TabIndex = 4;
            this.tbDbPost.TextChanged += new System.EventHandler(this.tbDbPost_TextChanged);
            // 
            // tbDbUserName
            // 
            this.tbDbUserName.Location = new System.Drawing.Point(122, 212);
            this.tbDbUserName.Name = "tbDbUserName";
            this.tbDbUserName.Size = new System.Drawing.Size(207, 23);
            this.tbDbUserName.TabIndex = 5;
            this.tbDbUserName.TextChanged += new System.EventHandler(this.tbDbUserName_TextChanged);
            // 
            // tbDbPassword
            // 
            this.tbDbPassword.Location = new System.Drawing.Point(122, 257);
            this.tbDbPassword.Name = "tbDbPassword";
            this.tbDbPassword.Size = new System.Drawing.Size(207, 23);
            this.tbDbPassword.TabIndex = 6;
            this.tbDbPassword.TextChanged += new System.EventHandler(this.tbDbPassword_TextChanged);
            // 
            // buttonTestConnect
            // 
            this.buttonTestConnect.Location = new System.Drawing.Point(26, 305);
            this.buttonTestConnect.Name = "buttonTestConnect";
            this.buttonTestConnect.Size = new System.Drawing.Size(105, 23);
            this.buttonTestConnect.TabIndex = 10;
            this.buttonTestConnect.Text = "测试";
            this.buttonTestConnect.UseVisualStyleBackColor = true;
            this.buttonTestConnect.Click += new System.EventHandler(this.buttonTestConnect_Click);
            // 
            // ConnectingEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 348);
            this.Controls.Add(this.buttonTestConnect);
            this.Controls.Add(this.tbDbPassword);
            this.Controls.Add(this.tbDbUserName);
            this.Controls.Add(this.tbDbPost);
            this.Controls.Add(this.tbDbHost);
            this.Controls.Add(this.tbConnName);
            this.Controls.Add(this.cbDbType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Name = "ConnectingEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加/编辑连接";
            this.Load += new System.EventHandler(this.ConnectingEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbDbType;
        private System.Windows.Forms.TextBox tbConnName;
        private System.Windows.Forms.TextBox tbDbHost;
        private System.Windows.Forms.TextBox tbDbPost;
        private System.Windows.Forms.TextBox tbDbUserName;
        private System.Windows.Forms.TextBox tbDbPassword;
        private System.Windows.Forms.Button buttonTestConnect;
    }
}