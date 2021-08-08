
namespace AntC.CodeGenerate.Forms
{
    partial class ConnectionSettingsForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridColumnDbType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridColumnHost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridColumnPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridColumnUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridColumnPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.choiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridColumnName,
            this.dataGridColumnDbType,
            this.dataGridColumnHost,
            this.dataGridColumnPort,
            this.dataGridColumnUserName,
            this.dataGridColumnPassword});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(645, 317);
            this.dataGridView1.TabIndex = 0;
            // 
            // dataGridColumnName
            // 
            this.dataGridColumnName.DataPropertyName = "Name";
            this.dataGridColumnName.Frozen = true;
            this.dataGridColumnName.HeaderText = "连接名称";
            this.dataGridColumnName.Name = "dataGridColumnName";
            this.dataGridColumnName.ReadOnly = true;
            // 
            // dataGridColumnDbType
            // 
            this.dataGridColumnDbType.DataPropertyName = "DbType";
            this.dataGridColumnDbType.HeaderText = "数据库类型";
            this.dataGridColumnDbType.Name = "dataGridColumnDbType";
            this.dataGridColumnDbType.ReadOnly = true;
            // 
            // dataGridColumnHost
            // 
            this.dataGridColumnHost.DataPropertyName = "Host";
            this.dataGridColumnHost.HeaderText = "Host";
            this.dataGridColumnHost.Name = "dataGridColumnHost";
            this.dataGridColumnHost.ReadOnly = true;
            // 
            // dataGridColumnPort
            // 
            this.dataGridColumnPort.DataPropertyName = "Port";
            this.dataGridColumnPort.HeaderText = "端口";
            this.dataGridColumnPort.Name = "dataGridColumnPort";
            this.dataGridColumnPort.ReadOnly = true;
            // 
            // dataGridColumnUserName
            // 
            this.dataGridColumnUserName.DataPropertyName = "UserName";
            this.dataGridColumnUserName.HeaderText = "用户名";
            this.dataGridColumnUserName.Name = "dataGridColumnUserName";
            this.dataGridColumnUserName.ReadOnly = true;
            // 
            // dataGridColumnPassword
            // 
            this.dataGridColumnPassword.DataPropertyName = "Password";
            this.dataGridColumnPassword.HeaderText = "密码";
            this.dataGridColumnPassword.Name = "dataGridColumnPassword";
            this.dataGridColumnPassword.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.choiseToolStripMenuItem,
            this.cloneToolStripMenuItem,
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(645, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // choiseToolStripMenuItem
            // 
            this.choiseToolStripMenuItem.Name = "choiseToolStripMenuItem";
            this.choiseToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.choiseToolStripMenuItem.Text = "选择";
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.cloneToolStripMenuItem.Text = "克隆";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.addToolStripMenuItem.Text = "添加";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.editToolStripMenuItem.Text = "编辑";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.deleteToolStripMenuItem.Text = "删除";
            // 
            // ConnectionSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 342);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConnectionSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库连接配置";
            this.Load += new System.EventHandler(this.ConnectionSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnDbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnHost;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridColumnPassword;
        private System.Windows.Forms.ToolStripMenuItem choiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
    }
}