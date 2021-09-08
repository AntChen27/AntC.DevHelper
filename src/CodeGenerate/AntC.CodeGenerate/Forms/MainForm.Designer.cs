
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSelectAllTables = new System.Windows.Forms.CheckBox();
            this.checkBoxOnFinishedOpenDir = new System.Windows.Forms.CheckBox();
            this.buttonCreateCodes = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonOpenBrowserFolderDialog = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCodePreview = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDbNames = new System.Windows.Forms.ComboBox();
            this.checkedListBoxTables = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxTemplate = new System.Windows.Forms.CheckedListBox();
            this.checkBoxClearDir = new System.Windows.Forms.CheckBox();
            this.buttonGroupEdit = new System.Windows.Forms.Button();
            this.textBoxGeneratorDesc = new System.Windows.Forms.TextBox();
            this.checkBoxSelectAllTemplate = new System.Windows.Forms.CheckBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbConnManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTemplateFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择生成表：";
            // 
            // checkBoxSelectAllTables
            // 
            this.checkBoxSelectAllTables.AutoSize = true;
            this.checkBoxSelectAllTables.Location = new System.Drawing.Point(212, 80);
            this.checkBoxSelectAllTables.Name = "checkBoxSelectAllTables";
            this.checkBoxSelectAllTables.Size = new System.Drawing.Size(51, 21);
            this.checkBoxSelectAllTables.TabIndex = 2;
            this.checkBoxSelectAllTables.Text = "全选";
            this.checkBoxSelectAllTables.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllTables.CheckedChanged += new System.EventHandler(this.checkBoxSelectAllTables_CheckedChanged);
            // 
            // checkBoxOnFinishedOpenDir
            // 
            this.checkBoxOnFinishedOpenDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxOnFinishedOpenDir.AutoSize = true;
            this.checkBoxOnFinishedOpenDir.Checked = true;
            this.checkBoxOnFinishedOpenDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxOnFinishedOpenDir.Location = new System.Drawing.Point(16, 494);
            this.checkBoxOnFinishedOpenDir.Name = "checkBoxOnFinishedOpenDir";
            this.checkBoxOnFinishedOpenDir.Size = new System.Drawing.Size(111, 21);
            this.checkBoxOnFinishedOpenDir.TabIndex = 3;
            this.checkBoxOnFinishedOpenDir.Text = "完成后打开目录";
            this.checkBoxOnFinishedOpenDir.UseVisualStyleBackColor = true;
            // 
            // buttonCreateCodes
            // 
            this.buttonCreateCodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreateCodes.Location = new System.Drawing.Point(143, 541);
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
            this.buttonOpenBrowserFolderDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpenBrowserFolderDialog.Location = new System.Drawing.Point(408, 492);
            this.buttonOpenBrowserFolderDialog.Name = "buttonOpenBrowserFolderDialog";
            this.buttonOpenBrowserFolderDialog.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenBrowserFolderDialog.TabIndex = 5;
            this.buttonOpenBrowserFolderDialog.Text = "浏览";
            this.buttonOpenBrowserFolderDialog.UseVisualStyleBackColor = true;
            this.buttonOpenBrowserFolderDialog.Click += new System.EventHandler(this.buttonOpenBrowserFolderDialog_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(15, 466);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(468, 23);
            this.textBoxOutputFolder.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 446);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "输出文件夹：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "数据库：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(269, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "请选择生成模板：";
            // 
            // textBoxCodePreview
            // 
            this.textBoxCodePreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCodePreview.Location = new System.Drawing.Point(544, 66);
            this.textBoxCodePreview.Multiline = true;
            this.textBoxCodePreview.Name = "textBoxCodePreview";
            this.textBoxCodePreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCodePreview.Size = new System.Drawing.Size(355, 449);
            this.textBoxCodePreview.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(544, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "模板代码生成预览：";
            // 
            // comboBoxDbNames
            // 
            this.comboBoxDbNames.FormattingEnabled = true;
            this.comboBoxDbNames.Location = new System.Drawing.Point(78, 40);
            this.comboBoxDbNames.Name = "comboBoxDbNames";
            this.comboBoxDbNames.Size = new System.Drawing.Size(185, 25);
            this.comboBoxDbNames.TabIndex = 14;
            this.comboBoxDbNames.Text = "请选择数据库";
            this.comboBoxDbNames.SelectedValueChanged += new System.EventHandler(this.comboBoxDbNames_SelectedValueChanged);
            // 
            // checkedListBoxTables
            // 
            this.checkedListBoxTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBoxTables.CheckOnClick = true;
            this.checkedListBoxTables.FormattingEnabled = true;
            this.checkedListBoxTables.Location = new System.Drawing.Point(12, 106);
            this.checkedListBoxTables.Name = "checkedListBoxTables";
            this.checkedListBoxTables.Size = new System.Drawing.Size(251, 310);
            this.checkedListBoxTables.TabIndex = 16;
            this.checkedListBoxTables.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxTables_SelectedIndexChanged);
            // 
            // checkedListBoxTemplate
            // 
            this.checkedListBoxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBoxTemplate.CheckOnClick = true;
            this.checkedListBoxTemplate.FormattingEnabled = true;
            this.checkedListBoxTemplate.HorizontalScrollbar = true;
            this.checkedListBoxTemplate.Location = new System.Drawing.Point(269, 107);
            this.checkedListBoxTemplate.Name = "checkedListBoxTemplate";
            this.checkedListBoxTemplate.Size = new System.Drawing.Size(269, 310);
            this.checkedListBoxTemplate.TabIndex = 17;
            this.checkedListBoxTemplate.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxTemplate_SelectedIndexChanged);
            // 
            // checkBoxClearDir
            // 
            this.checkBoxClearDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxClearDir.AutoSize = true;
            this.checkBoxClearDir.Checked = true;
            this.checkBoxClearDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxClearDir.Location = new System.Drawing.Point(133, 494);
            this.checkBoxClearDir.Name = "checkBoxClearDir";
            this.checkBoxClearDir.Size = new System.Drawing.Size(111, 21);
            this.checkBoxClearDir.TabIndex = 18;
            this.checkBoxClearDir.Text = "运行前清空目录";
            this.checkBoxClearDir.UseVisualStyleBackColor = true;
            // 
            // buttonGroupEdit
            // 
            this.buttonGroupEdit.Enabled = false;
            this.buttonGroupEdit.Location = new System.Drawing.Point(269, 40);
            this.buttonGroupEdit.Name = "buttonGroupEdit";
            this.buttonGroupEdit.Size = new System.Drawing.Size(104, 25);
            this.buttonGroupEdit.TabIndex = 19;
            this.buttonGroupEdit.Text = "表分组信息编辑";
            this.buttonGroupEdit.UseVisualStyleBackColor = true;
            this.buttonGroupEdit.Click += new System.EventHandler(this.buttonGroupEdit_Click);
            // 
            // textBoxGeneratorDesc
            // 
            this.textBoxGeneratorDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGeneratorDesc.Location = new System.Drawing.Point(544, 522);
            this.textBoxGeneratorDesc.Multiline = true;
            this.textBoxGeneratorDesc.Name = "textBoxGeneratorDesc";
            this.textBoxGeneratorDesc.Size = new System.Drawing.Size(355, 75);
            this.textBoxGeneratorDesc.TabIndex = 21;
            // 
            // checkBoxSelectAllTemplate
            // 
            this.checkBoxSelectAllTemplate.AutoSize = true;
            this.checkBoxSelectAllTemplate.Location = new System.Drawing.Point(487, 80);
            this.checkBoxSelectAllTemplate.Name = "checkBoxSelectAllTemplate";
            this.checkBoxSelectAllTemplate.Size = new System.Drawing.Size(51, 21);
            this.checkBoxSelectAllTemplate.TabIndex = 22;
            this.checkBoxSelectAllTemplate.Text = "全选";
            this.checkBoxSelectAllTemplate.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllTemplate.CheckedChanged += new System.EventHandler(this.checkBoxSelectAllTemplate_CheckedChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(911, 25);
            this.menuStrip.TabIndex = 23;
            this.menuStrip.TabStop = true;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dbConnManagerToolStripMenuItem,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // dbConnManagerToolStripMenuItem
            // 
            this.dbConnManagerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dbConnManagerToolStripMenuItem.Image")));
            this.dbConnManagerToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dbConnManagerToolStripMenuItem.Name = "dbConnManagerToolStripMenuItem";
            this.dbConnManagerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.dbConnManagerToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.dbConnManagerToolStripMenuItem.Text = "数据库连接管理";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "退出";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templateManagerToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.editToolStripMenuItem.Text = "编辑";
            // 
            // templateManagerToolStripMenuItem
            // 
            this.templateManagerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTemplateFolderToolStripMenuItem,
            this.reloadTemplateToolStripMenuItem});
            this.templateManagerToolStripMenuItem.Name = "templateManagerToolStripMenuItem";
            this.templateManagerToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.templateManagerToolStripMenuItem.Text = "模板管理";
            // 
            // openTemplateFolderToolStripMenuItem
            // 
            this.openTemplateFolderToolStripMenuItem.Name = "openTemplateFolderToolStripMenuItem";
            this.openTemplateFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.openTemplateFolderToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.openTemplateFolderToolStripMenuItem.Text = "打开模板文件夹";
            // 
            // reloadTemplateToolStripMenuItem
            // 
            this.reloadTemplateToolStripMenuItem.Name = "reloadTemplateToolStripMenuItem";
            this.reloadTemplateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.reloadTemplateToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.reloadTemplateToolStripMenuItem.Text = "重新加载所有模板";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.helpToolStripMenuItem.Text = "帮助";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(124, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 604);
            this.Controls.Add(this.checkBoxSelectAllTemplate);
            this.Controls.Add(this.textBoxGeneratorDesc);
            this.Controls.Add(this.buttonGroupEdit);
            this.Controls.Add(this.checkBoxClearDir);
            this.Controls.Add(this.checkedListBoxTemplate);
            this.Controls.Add(this.checkedListBoxTables);
            this.Controls.Add(this.comboBoxDbNames);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxCodePreview);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.buttonOpenBrowserFolderDialog);
            this.Controls.Add(this.buttonCreateCodes);
            this.Controls.Add(this.checkBoxOnFinishedOpenDir);
            this.Controls.Add(this.checkBoxSelectAllTables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(927, 555);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "代码生成器 By AntC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCodePreview;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDbNames;
        private System.Windows.Forms.CheckedListBox checkedListBoxTables;
        private System.Windows.Forms.CheckedListBox checkedListBoxTemplate;
        private System.Windows.Forms.CheckBox checkBoxSelectAllTemplate;
        private System.Windows.Forms.CheckBox checkBoxClearDir;
        private System.Windows.Forms.Button buttonGroupEdit;
        private System.Windows.Forms.TextBox textBoxGeneratorDesc;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dbConnManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem templateManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTemplateFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadTemplateToolStripMenuItem;
    }
}

