using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.CodeWriters;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql;
using AntC.CodeGenerate.Plugin.Benchint;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AntC.CodeGenerate
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private IDbInfoProvider _mysqlDbInfoProvider = new MysqlDbInfoProvider();


        private DbInfoModel _selectedDb;
        private List<string> _selectedTables = new List<string>();
        private IDbInfoProvider _dbInfoProvider;
        private DbConnectionInfo _dbConnectionInfo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBoxOutputFolder.Text =
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "output");

            var loadConnectionConfigs = DbConnectionConfig.LoadConnectionConfigs();
            comboBoxDbConnection.DataSource = loadConnectionConfigs;

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
            _selectedDb = (DbInfoModel)comboBoxDbNames.SelectedItem;

            _selectedTables.Clear();
            //checkedListBoxTables.ClearSelected();
            var tables = _dbInfoProvider.GetTables(_selectedDb.DbName).ToList();
            checkedListBoxTables.DisplayMember = nameof(DbTableInfoModel.TableName);
            checkedListBoxTables.ValueMember = nameof(DbTableInfoModel.TableName);
            checkedListBoxTables.Items.Clear();
            checkedListBoxTables.Items.AddRange(tables.ToArray());
            checkedListBoxTables.ClearSelected();
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
            return checkedListBoxTables.Items.Cast<DbTableInfoModel>()
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
    }
}
