using System.Runtime.InteropServices;
using System.Windows.Forms;
using AntC.CodeGenerate.Model;
using Org.BouncyCastle.Math.EC;

namespace AntC.CodeGenerate.Forms
{
    public partial class ConnectionSettingsForm : Form
    {
        #region 属性
        #endregion

        public ConnectionSettingsForm()
        {
            InitializeComponent();
        }

        private void ConnectionSettingsForm_Load(object sender, System.EventArgs e)
        {
            RegisterMenuEvent();
            LoadConnectionSettings();
        }

        private void LoadConnectionSettings()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = ConfigHelper.DbConnectionInfos;
            if (dataGridView1.Columns.Count > 6)
            {
                for (var i = dataGridView1.Columns.Count - 1; i >= 6; i--)
                {
                    dataGridView1.Columns.RemoveAt(i);
                }
            }
        }

        private void EditDbConnectionInfo(OpType opType, DbConnectionInfo dbInfo = null)
        {
            var form = new ConnectingEditForm(opType);
            form.DbConnectionInfo = dbInfo;
            if (DialogResult.OK == form.ShowDialog())
            {
                if (opType == OpType.新建 || opType == OpType.克隆)
                {
                    ConfigHelper.DbConnectionInfos.Add(form.DbConnectionInfo);
                }
                ConfigHelper.SaveAndLoad();
                // 重新加载数据
                LoadConnectionSettings();
            }
        }

        #region 菜单事件控制

        private void RegisterMenuEvent()
        {
            choiseToolStripMenuItem.Click += ChoiseToolStripMenuItem_Click;
            cloneToolStripMenuItem.Click += CloneToolStripMenuItem_Click;
            addToolStripMenuItem.Click += AddToolStripMenuItem_Click;
            editToolStripMenuItem.Click += EditToolStripMenuItem_Click;
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
        }

        private void CloneToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("请选中一条记录后进行操作！！！");
                return;
            }
            var dbConnectionInfo = ConfigHelper.DbConnectionInfos[dataGridView1.CurrentCell.RowIndex];
            EditDbConnectionInfo(OpType.克隆, dbConnectionInfo);
        }

        private void ChoiseToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("请选中一条记录后进行操作！！！");
                return;
            }
            ConfigHelper.CurrentDbConnectionInfo = ConfigHelper.DbConnectionInfos[dataGridView1.CurrentCell.RowIndex];
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DeleteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("请选中一条记录后进行操作！！！");
                return;
            }
            var dbConnectionInfo = ConfigHelper.DbConnectionInfos[dataGridView1.CurrentCell.RowIndex];
            if (DialogResult.OK == MessageBox.Show($"删除后无法恢复，确认删除数据连接[{dbConnectionInfo.Name}]？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
            {
                ConfigHelper.DbConnectionInfos.Remove(dbConnectionInfo);
                ConfigHelper.SaveAndLoad();
                // 重新加载数据
                LoadConnectionSettings();
            }
        }

        private void EditToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("请选中一条记录后进行操作！！！");
                return;
            }

            var dbConnectionInfo = ConfigHelper.DbConnectionInfos[dataGridView1.CurrentCell.RowIndex];
            EditDbConnectionInfo(OpType.编辑, dbConnectionInfo);
        }

        private void AddToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            EditDbConnectionInfo(OpType.新建, null);
        }

        #endregion
    }
}
