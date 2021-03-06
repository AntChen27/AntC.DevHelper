
namespace AntC.CodeGenerate.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSelectAllTables = new System.Windows.Forms.CheckBox();
            this.checkBoxOnFinishedOpenDir = new System.Windows.Forms.CheckBox();
            this.buttonCreateCodes = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOpenBrowserFolderDialog = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDbConnection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCodePreview = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxDbNames = new System.Windows.Forms.ComboBox();
            this.checkedListBoxTables = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxTemplate = new System.Windows.Forms.CheckedListBox();
            this.checkBoxClearDir = new System.Windows.Forms.CheckBox();
            this.buttonGroupEdit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxGeneratorDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择生成表：";
            // 
            // checkBoxSelectAllTables
            // 
            this.checkBoxSelectAllTables.AutoSize = true;
            this.checkBoxSelectAllTables.Location = new System.Drawing.Point(212, 82);
            this.checkBoxSelectAllTables.Name = "checkBoxSelectAllTables";
            this.checkBoxSelectAllTables.Size = new System.Drawing.Size(51, 21);
            this.checkBoxSelectAllTables.TabIndex = 2;
            this.checkBoxSelectAllTables.Text = "全选";
            this.checkBoxSelectAllTables.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllTables.CheckedChanged += new System.EventHandler(this.checkBoxSelectAllTables_CheckedChanged);
            // 
            // checkBoxOnFinishedOpenDir
            // 
            this.checkBoxOnFinishedOpenDir.AutoSize = true;
            this.checkBoxOnFinishedOpenDir.Checked = true;
            this.checkBoxOnFinishedOpenDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOnFinishedOpenDir.Location = new System.Drawing.Point(16, 548);
            this.checkBoxOnFinishedOpenDir.Name = "checkBoxOnFinishedOpenDir";
            this.checkBoxOnFinishedOpenDir.Size = new System.Drawing.Size(111, 21);
            this.checkBoxOnFinishedOpenDir.TabIndex = 3;
            this.checkBoxOnFinishedOpenDir.Text = "完成后打开目录";
            this.checkBoxOnFinishedOpenDir.UseVisualStyleBackColor = true;
            // 
            // buttonCreateCodes
            // 
            this.buttonCreateCodes.Location = new System.Drawing.Point(143, 595);
            this.buttonCreateCodes.Name = "buttonCreateCodes";
            this.buttonCreateCodes.Size = new System.Drawing.Size(248, 51);
            this.buttonCreateCodes.TabIndex = 4;
            this.buttonCreateCodes.Text = "生成代码";
            this.buttonCreateCodes.UseVisualStyleBackColor = true;
            this.buttonCreateCodes.Click += new System.EventHandler(this.buttonCreateCodes_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            // 
            // buttonOpenBrowserFolderDialog
            // 
            this.buttonOpenBrowserFolderDialog.Location = new System.Drawing.Point(408, 546);
            this.buttonOpenBrowserFolderDialog.Name = "buttonOpenBrowserFolderDialog";
            this.buttonOpenBrowserFolderDialog.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenBrowserFolderDialog.TabIndex = 5;
            this.buttonOpenBrowserFolderDialog.Text = "浏览";
            this.buttonOpenBrowserFolderDialog.UseVisualStyleBackColor = true;
            this.buttonOpenBrowserFolderDialog.Click += new System.EventHandler(this.buttonOpenBrowserFolderDialog_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Location = new System.Drawing.Point(15, 520);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(468, 23);
            this.textBoxOutputFolder.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 500);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "输出文件夹：";
            // 
            // comboBoxDbConnection
            // 
            this.comboBoxDbConnection.FormattingEnabled = true;
            this.comboBoxDbConnection.Location = new System.Drawing.Point(84, 13);
            this.comboBoxDbConnection.Name = "comboBoxDbConnection";
            this.comboBoxDbConnection.Size = new System.Drawing.Size(179, 25);
            this.comboBoxDbConnection.TabIndex = 8;
            this.comboBoxDbConnection.Text = "请选择数据库连接";
            this.comboBoxDbConnection.SelectedValueChanged += new System.EventHandler(this.comboBoxDbConnection_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "请选择生成模板：";
            // 
            // textBoxCodePreview
            // 
            this.textBoxCodePreview.Location = new System.Drawing.Point(544, 26);
            this.textBoxCodePreview.Multiline = true;
            this.textBoxCodePreview.Name = "textBoxCodePreview";
            this.textBoxCodePreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCodePreview.Size = new System.Drawing.Size(628, 543);
            this.textBoxCodePreview.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "模板代码预览：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "数据库连接：";
            // 
            // comboBoxDbNames
            // 
            this.comboBoxDbNames.FormattingEnabled = true;
            this.comboBoxDbNames.Location = new System.Drawing.Point(84, 46);
            this.comboBoxDbNames.Name = "comboBoxDbNames";
            this.comboBoxDbNames.Size = new System.Drawing.Size(179, 25);
            this.comboBoxDbNames.TabIndex = 14;
            this.comboBoxDbNames.Text = "请选择数据库";
            this.comboBoxDbNames.SelectedValueChanged += new System.EventHandler(this.comboBoxDbNames_SelectedValueChanged);
            // 
            // checkedListBoxTables
            // 
            this.checkedListBoxTables.FormattingEnabled = true;
            this.checkedListBoxTables.Location = new System.Drawing.Point(12, 106);
            this.checkedListBoxTables.Name = "checkedListBoxTables";
            this.checkedListBoxTables.Size = new System.Drawing.Size(251, 382);
            this.checkedListBoxTables.TabIndex = 16;
            // 
            // checkedListBoxTemplate
            // 
            this.checkedListBoxTemplate.FormattingEnabled = true;
            this.checkedListBoxTemplate.HorizontalScrollbar = true;
            this.checkedListBoxTemplate.Location = new System.Drawing.Point(269, 107);
            this.checkedListBoxTemplate.Name = "checkedListBoxTemplate";
            this.checkedListBoxTemplate.Size = new System.Drawing.Size(269, 382);
            this.checkedListBoxTemplate.TabIndex = 17;
            this.checkedListBoxTemplate.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxTemplate_SelectedIndexChanged);
            // 
            // checkBoxClearDir
            // 
            this.checkBoxClearDir.AutoSize = true;
            this.checkBoxClearDir.Checked = true;
            this.checkBoxClearDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClearDir.Location = new System.Drawing.Point(133, 548);
            this.checkBoxClearDir.Name = "checkBoxClearDir";
            this.checkBoxClearDir.Size = new System.Drawing.Size(111, 21);
            this.checkBoxClearDir.TabIndex = 18;
            this.checkBoxClearDir.Text = "运行前清空目录";
            this.checkBoxClearDir.UseVisualStyleBackColor = true;
            // 
            // buttonGroupEdit
            // 
            this.buttonGroupEdit.Enabled = false;
            this.buttonGroupEdit.Location = new System.Drawing.Point(288, 13);
            this.buttonGroupEdit.Name = "buttonGroupEdit";
            this.buttonGroupEdit.Size = new System.Drawing.Size(103, 23);
            this.buttonGroupEdit.TabIndex = 19;
            this.buttonGroupEdit.Text = "表分组信息编辑";
            this.buttonGroupEdit.UseVisualStyleBackColor = true;
            this.buttonGroupEdit.Click += new System.EventHandler(this.buttonGroupEdit_Click);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(469, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "预览>";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBoxGeneratorDesc
            // 
            this.textBoxGeneratorDesc.Location = new System.Drawing.Point(544, 576);
            this.textBoxGeneratorDesc.Multiline = true;
            this.textBoxGeneratorDesc.Name = "textBoxGeneratorDesc";
            this.textBoxGeneratorDesc.Size = new System.Drawing.Size(628, 75);
            this.textBoxGeneratorDesc.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 658);
            this.Controls.Add(this.textBoxGeneratorDesc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonGroupEdit);
            this.Controls.Add(this.checkBoxClearDir);
            this.Controls.Add(this.checkedListBoxTemplate);
            this.Controls.Add(this.checkedListBoxTables);
            this.Controls.Add(this.comboBoxDbNames);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxCodePreview);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxDbConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.buttonOpenBrowserFolderDialog);
            this.Controls.Add(this.buttonCreateCodes);
            this.Controls.Add(this.checkBoxOnFinishedOpenDir);
            this.Controls.Add(this.checkBoxSelectAllTables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "代码生成器 By AntC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSelectAllTables;
        private System.Windows.Forms.CheckBox checkBoxOnFinishedOpenDir;
        private System.Windows.Forms.Button buttonCreateCodes;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonOpenBrowserFolderDialog;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDbConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCodePreview;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxDbNames;
        private System.Windows.Forms.CheckedListBox checkedListBoxTables;
        private System.Windows.Forms.CheckedListBox checkedListBoxTemplate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxClearDir;
        private System.Windows.Forms.Button buttonGroupEdit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxGeneratorDesc;
    }
}

