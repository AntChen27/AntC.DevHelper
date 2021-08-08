using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AntC.CodeGenerate.Model;
using Google.Protobuf.WellKnownTypes;
using Enum = System.Enum;

namespace AntC.CodeGenerate.Forms
{
    public partial class ConnectingEditForm : Form
    {
        #region 属性

        private string thisText;
        private string oldPort;
        private string oldName;

        private OpType _opType;

        public OpType OpType
        {
            get { return _opType; }
            set
            {
                _opType = value;
                this.Text = $"{_opType}数据库连接";
                thisText = Text;
            }
        }

        public DbConnectionInfo DbConnectionInfo { get; set; }

        #endregion

        public ConnectingEditForm(OpType opType)
        {
            InitializeComponent();
            OpType = opType;
        }

        #region 控件数据

        private void LoadDbType()
        {
            cbDbType.DataSource = Enum.GetValues(typeof(DbType));
        }
        private void LoadDbInfo()
        {
            if (DbConnectionInfo == null)
            {
                DbConnectionInfo = new DbConnectionInfo();
                return;
            }

            oldName = DbConnectionInfo.Name;
            tbConnName.Text = DbConnectionInfo.Name;
            tbDbHost.Text = DbConnectionInfo.Host;
            tbDbPost.Text = DbConnectionInfo.Port.ToString();
            tbDbUserName.Text = DbConnectionInfo.Username;
            tbDbPassword.Text = DbConnectionInfo.Password;
            cbDbType.SelectedItem = DbConnectionInfo.DbType;
            if (OpType == OpType.克隆)
            {
                tbConnName.Text += "_clone";
            }
        }

        #endregion

        #region 事件

        private void ConnectingEditForm_Load(object sender, System.EventArgs e)
        {
            LoadDbType();
            LoadDbInfo();
        }

        private void buttonTestConnect_Click(object sender, System.EventArgs e)
        {
            buttonTestConnect.Enabled = false;
            this.Text = $"{thisText}[测试中...]";

            var dbConnectionInfo = ToDbConnectionInfo();
            var result = dbConnectionInfo.TestConnect();
            this.Text = $"{thisText}[{result}]";
            buttonTestConnect.Enabled = true;
            buttonOk.Enabled = result == "连接成功";
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            if (!CheckName())
            {
                return;
            }
            DbConnectionInfo = ToDbConnectionInfo(false);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        /// <summary>
        /// 检查当前连接命名是否重复
        /// </summary>
        /// <returns></returns>
        private bool CheckName()
        {
            var name = tbConnName.Text;
            if (OpType == OpType.克隆 || OpType == OpType.新建)
            {
                // 不能在现有的列表中存在
                if (ConfigHelper.DbConnectionInfos.Any(x => x.Name == name))
                {
                    MessageBox.Show("连接名称不能重复！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else if (OpType == OpType.编辑)
            {
                // 不能在现有的列表中存在,排除当前连接
                if (ConfigHelper.DbConnectionInfos.Where(x => x.Name != DbConnectionInfo.Name).Any(x => x.Name == name))
                {
                    MessageBox.Show("连接名称不能重复！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private DbConnectionInfo ToDbConnectionInfo(bool createNew = true)
        {
            var dbConnectionInfo = ConfigHelper.DbConnectionInfos.FirstOrDefault(x => x.Name == oldName);

            var dbInfo = createNew ? new DbConnectionInfo() : dbConnectionInfo;
            dbInfo ??= new DbConnectionInfo();

            dbInfo.Name = tbConnName.Text;
            dbInfo.Host = tbDbHost.Text;
            dbInfo.Port = ushort.Parse(tbDbPost.Text);
            dbInfo.Username = tbDbUserName.Text;
            dbInfo.Password = tbDbPassword.Text;
            dbInfo.DbType = (DbType)cbDbType.SelectedItem;

            dbInfo.SelectTables = dbConnectionInfo?.SelectTables ?? new Dictionary<string, List<string>>();
            dbInfo.TableGroups = dbConnectionInfo?.TableGroups ?? new Dictionary<string, List<TableGroupInfo>>();

            return dbInfo;
        }

        private void tbConnName_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void tbDbHost_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void tbDbUserName_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void tbDbPassword_TextChanged(object sender, System.EventArgs e)
        {
            buttonOk.Enabled = false;
        }

        private void tbDbPost_TextChanged(object sender, System.EventArgs e)
        {
            var txt = string.Join("", tbDbPost.Text.Where(x => x >= '0' && x <= '9'));
            if (txt != tbDbPost.Text)
            {
                buttonOk.Enabled = txt != oldPort;
                tbDbPost.Text = txt;
                // 设置光标位置到文本最后
                tbDbPost.SelectionStart = txt.Length;
            }
            oldPort = tbDbPost.Text;
        }
    }
}
