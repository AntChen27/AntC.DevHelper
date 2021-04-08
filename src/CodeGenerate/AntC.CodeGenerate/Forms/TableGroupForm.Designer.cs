
namespace AntC.CodeGenerate.Forms
{
    partial class TableGroupForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.checkedListBoxNoGroupTables = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxGroupTables = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonEditGroup = new System.Windows.Forms.Button();
            this.buttonDeleteGroup = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonAddTableGroup = new System.Windows.Forms.Button();
            this.buttonRemoveTableGroup = new System.Windows.Forms.Button();
            this.comboBoxGroupName = new System.Windows.Forms.ComboBox();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "分组：";
            // 
            // checkedListBoxNoGroupTables
            // 
            this.checkedListBoxNoGroupTables.CheckOnClick = true;
            this.checkedListBoxNoGroupTables.FormattingEnabled = true;
            this.checkedListBoxNoGroupTables.Location = new System.Drawing.Point(10, 69);
            this.checkedListBoxNoGroupTables.Name = "checkedListBoxNoGroupTables";
            this.checkedListBoxNoGroupTables.Size = new System.Drawing.Size(211, 274);
            this.checkedListBoxNoGroupTables.TabIndex = 2;
            // 
            // checkedListBoxGroupTables
            // 
            this.checkedListBoxGroupTables.CheckOnClick = true;
            this.checkedListBoxGroupTables.FormattingEnabled = true;
            this.checkedListBoxGroupTables.Location = new System.Drawing.Point(287, 69);
            this.checkedListBoxGroupTables.Name = "checkedListBoxGroupTables";
            this.checkedListBoxGroupTables.Size = new System.Drawing.Size(310, 274);
            this.checkedListBoxGroupTables.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "未分组的表";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "当前分组的表：";
            // 
            // buttonEditGroup
            // 
            this.buttonEditGroup.Location = new System.Drawing.Point(322, 12);
            this.buttonEditGroup.Name = "buttonEditGroup";
            this.buttonEditGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonEditGroup.TabIndex = 6;
            this.buttonEditGroup.Text = "编辑分组";
            this.buttonEditGroup.UseVisualStyleBackColor = true;
            this.buttonEditGroup.Click += new System.EventHandler(this.buttonEditGroup_Click);
            // 
            // buttonDeleteGroup
            // 
            this.buttonDeleteGroup.Location = new System.Drawing.Point(408, 12);
            this.buttonDeleteGroup.Name = "buttonDeleteGroup";
            this.buttonDeleteGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteGroup.TabIndex = 7;
            this.buttonDeleteGroup.Text = "删除分组";
            this.buttonDeleteGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteGroup.Click += new System.EventHandler(this.buttonDeleteGroup_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(441, 355);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 9;
            this.buttonConfirm.Text = "确认";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(522, 355);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonAddTableGroup
            // 
            this.buttonAddTableGroup.Location = new System.Drawing.Point(236, 129);
            this.buttonAddTableGroup.Name = "buttonAddTableGroup";
            this.buttonAddTableGroup.Size = new System.Drawing.Size(35, 35);
            this.buttonAddTableGroup.TabIndex = 11;
            this.buttonAddTableGroup.Text = ">";
            this.buttonAddTableGroup.UseVisualStyleBackColor = true;
            this.buttonAddTableGroup.Click += new System.EventHandler(this.buttonAddTableGroup_Click);
            // 
            // buttonRemoveTableGroup
            // 
            this.buttonRemoveTableGroup.Location = new System.Drawing.Point(236, 170);
            this.buttonRemoveTableGroup.Name = "buttonRemoveTableGroup";
            this.buttonRemoveTableGroup.Size = new System.Drawing.Size(35, 35);
            this.buttonRemoveTableGroup.TabIndex = 12;
            this.buttonRemoveTableGroup.Text = "<";
            this.buttonRemoveTableGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveTableGroup.Click += new System.EventHandler(this.buttonRemoveTableGroup_Click);
            // 
            // comboBoxGroupName
            // 
            this.comboBoxGroupName.FormattingEnabled = true;
            this.comboBoxGroupName.Location = new System.Drawing.Point(62, 12);
            this.comboBoxGroupName.Name = "comboBoxGroupName";
            this.comboBoxGroupName.Size = new System.Drawing.Size(161, 25);
            this.comboBoxGroupName.TabIndex = 13;
            this.comboBoxGroupName.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroupName_SelectedIndexChanged);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(236, 12);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(75, 23);
            this.buttonAddGroup.TabIndex = 7;
            this.buttonAddGroup.Text = "新增分组";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // TableGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 390);
            this.Controls.Add(this.comboBoxGroupName);
            this.Controls.Add(this.buttonRemoveTableGroup);
            this.Controls.Add(this.buttonAddTableGroup);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonAddGroup);
            this.Controls.Add(this.buttonDeleteGroup);
            this.Controls.Add(this.buttonEditGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkedListBoxGroupTables);
            this.Controls.Add(this.checkedListBoxNoGroupTables);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(625, 429);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 429);
            this.Name = "TableGroupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "表分组";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TableGroupForm_FormClosing);
            this.Load += new System.EventHandler(this.TableGroupForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox checkedListBoxNoGroupTables;
        private System.Windows.Forms.CheckedListBox checkedListBoxGroupTables;
        private System.Windows.Forms.Button buttonEditGroup;
        private System.Windows.Forms.Button buttonDeleteGroup;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonAddTableGroup;
        private System.Windows.Forms.Button buttonAddTableToGroup;
        private System.Windows.Forms.Button buttonRemoveTableGroup;
        private System.Windows.Forms.ComboBox comboBoxGroupName;
        private System.Windows.Forms.Button buttonAddGroup;
    }
}