using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AntC.CodeGenerate.Forms
{
    public partial class EditGroupForm : Form
    {
        public List<string> GroupNames { get; set; }

        public bool IsAdd { get; set; } = true;

        public string GroupName { get; set; }

        public string NewGroupName => textBoxGroupName.Text.Trim();

        public EditGroupForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (textBoxGroupName.Text.Trim().Length == 0)
            {
                MessageBox.Show("分组名前后不能有空格！！！");
                return;
            }

            var groupName = textBoxGroupName.Text.Trim();

            if (IsAdd)
            {
                GroupNames.Add(groupName);
            }
            else
            {
                var indexOf = GroupNames.IndexOf(GroupName);
                GroupNames[indexOf] = groupName;
                //GroupNames.Remove(GroupName);
            }

            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void EditGroupForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupName))
            {
                textBoxGroupName.Text = GroupName;
            }
        }
    }
}
