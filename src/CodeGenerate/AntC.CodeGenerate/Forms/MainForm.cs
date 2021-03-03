using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AntC.CodeGenerate.CodeWriters;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql;
using AntC.CodeGenerate.Plugin.Benchint;

namespace AntC.CodeGenerate.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private IDbInfoProvider _mysqlDbInfoProvider = new MysqlDbInfoProvider();


        private DbInfoModel _selectedDb;
        private List<string> _selectedTables = new List<string>();
        private List<TableGroupInfo> _tableGroupInfo = new List<TableGroupInfo>();
        private IDbInfoProvider _dbInfoProvider;
        private DbConnectionInfo _dbConnectionInfo;
        private IEnumerable<DbConnectionInfo> _dbConnectionInfos;

        private TableGroupForm _tableGroupForm = new TableGroupForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigHelper.SaveConnectionConfigs(_dbConnectionInfos);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBoxOutputFolder.Text =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "output");

            _dbConnectionInfos = ConfigHelper.LoadConnectionConfigs();
            comboBoxDbConnection.DataSource = _dbConnectionInfos;

            PluginManager.AddPlugin(new Plugin.Benchint.Plugin());
        }

        private void comboBoxDbConnection_SelectedValueChanged(object sender, EventArgs e)
        {
            _dbConnectionInfo = (DbConnectionInfo)comboBoxDbConnection.SelectedItem;
            _dbInfoProvider = _mysqlDbInfoProvider;
            _dbInfoProvider.DbConnectionString = _dbConnectionInfo.ToConnectionString();

            _selectedTables.Clear();
            checkedListBoxTables.ClearSelected();
            comboBoxDbNames.DataSource = _dbInfoProvider.GetDataBases();
            checkBoxSelectAllTables.Checked = false;
        }

        private void comboBoxDbNames_SelectedValueChanged(object sender, EventArgs e)
        {
            _selectedDb = comboBoxDbNames.SelectedItem as DbInfoModel;
            _selectedTables.Clear();
            checkedListBoxTables.Items.Clear();
            checkedListBoxTables.ClearSelected();
            if (_selectedDb == null)
            {
                buttonGroupEdit.Enabled = false;
                return;
            }

            buttonGroupEdit.Enabled = true;
            //checkedListBoxTables.ClearSelected();
            var tables = _dbInfoProvider.GetTables(_selectedDb.DbName).ToList();
            //checkedListBoxTables.DisplayMember = nameof(DbTableInfoModel.TableName);
            //checkedListBoxTables.ValueMember = nameof(DbTableInfoModel.TableName);
            checkedListBoxTables.Items.AddRange(tables.ToArray());

            checkBoxSelectAllTables.Checked = false;
        }

        private void checkBoxSelectAllTables_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTables.Items.Count; i++)
            {
                checkedListBoxTables.SetItemChecked(i, checkBoxSelectAllTables.Checked);
            }
        }

        private void buttonOpenBrowserFolderDialog_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = textBoxOutputFolder.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 获取选中的表名
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetSelectedTableNames()
        {
            return Enumerable.Cast<DbTableInfoModel>(checkedListBoxTables.Items)
                .Where((t, i) => checkedListBoxTables.GetItemChecked(i))
                .Select(t => t.TableName).ToList();
        }

        private void buttonCreateCodes_Click(object sender, EventArgs e)
        {
            _selectedTables = GetSelectedTableNames().ToList();

            if (_selectedTables.Count == 0)
            {
                MessageBox.Show("请选择需要生成的表！！！");
                return;
            }

            if (checkBoxClearDir.Checked)
            {
                ClearDir(new DirectoryInfo(textBoxOutputFolder.Text));
            }

            var codeGeneratorManager = ServiceManager.CreateGeneratorManager(_dbConnectionInfo);
            // todo 添加模板选择
            codeGeneratorManager.UseBenchintCodeGenerateImpl();
            codeGeneratorManager.SetCodeWriterType<CodeFileWriter>();

            var codeGenerateInfo = new CodeGenerateInfo()
            {
                OutPutRootPath = textBoxOutputFolder.Text,
                DbName = _selectedDb.DbName,
                CodeGenerateTableInfos = _selectedTables.Select(x => new CodeGenerateTableInfo()
                {
                    TableName = x,
                    //GroupName = GetGroupName(x)
                    // todo 添加分组功能
                })
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            codeGeneratorManager.ExecCodeGenerate(codeGenerateInfo);
            stopwatch.Stop();

            if (checkBoxOnFinishedOpenDir.Checked)
            {
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = codeGenerateInfo.OutPutRootPath;
                p.Start();
            }
        }

        private void ClearDir(DirectoryInfo directory)
        {
            //foreach (var directoryInfo in directory.GetDirectories())
            //{
            //    ClearDir(directoryInfo);
            //}

            //foreach (var fileInfo in directory.GetFiles())
            //{
            //    fileInfo.Delete();
            //}
            if (directory != null && directory.Exists)
            {
                directory.Delete(true);
            }
        }

        private void buttonGroupEdit_Click(object sender, EventArgs e)
        {
            var tableNames = Enumerable.Cast<DbTableInfoModel>(checkedListBoxTables.Items).Select(x => x.TableName);
            _tableGroupForm.SetData(new TableGroupingInfo()
            {
                GroupNames = new List<string>()
                {
                    "Stat",
                    "Run",
                    "Define",
                    "Basic",
                },
                TableNames = tableNames,
                AlreadyGroupTable = tableNames
                    .Select(x => new TableGroupInfo()
                    {
                        TableName = x,
                        GroupName = GetGroupName(x)
                    }).ToList()
            });
            if (DialogResult.OK == _tableGroupForm.ShowDialog())
            {
                _tableGroupInfo = _tableGroupForm.GetTableGroupData();
                UpdateTableGroupInfo(_selectedDb.DbName, _tableGroupInfo);
            }
        }

        private void UpdateTableGroupInfo(string dbName, IEnumerable<TableGroupInfo> tableGroupInfos)
        {
            if (_dbConnectionInfo.TableGroups == null)
            {
                _dbConnectionInfo.TableGroups = new Dictionary<string, IEnumerable<TableGroupInfo>>();
            }
            _dbConnectionInfo.TableGroups.Remove(_selectedDb.DbName);
            _dbConnectionInfo.TableGroups.Add(_selectedDb.DbName, _tableGroupInfo);
        }

        private string GetGroupName(string tableName)
        {
            if (tableName.StartsWith("kpi_stat"))
            {
                return "Stat";
            }
            if (tableName.StartsWith("kpi_run"))
            {
                return "Run";
            }
            if (tableName.StartsWith("kpi_define"))
            {
                return "Define";
            }

            return "Basic";
        }
    }
}
