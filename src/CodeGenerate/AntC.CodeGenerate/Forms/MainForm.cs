using AntC.CodeGenerate.Core.Contracts;
using AntC.CodeGenerate.Core.Model.Db;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AntC.CodeGenerate.Core.Extension;
using AntC.CodeGenerate.Core.Model.CLR;
using Org.BouncyCastle.Bcpg.OpenPgp;
using RazorEngine;
using Encoding = System.Text.Encoding;

namespace AntC.CodeGenerate.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        private IDbInfoProvider _mysqlDbInfoProvider = new MysqlDbInfoProvider();

        private DatabaseInfo _selectedDb;
        private List<TableInfo> _selectedTables = new List<TableInfo>();
        private List<TableGroupInfo> _tableGroupInfo = new List<TableGroupInfo>();
        private IDbInfoProvider _dbInfoProvider;
        private DbConnectionInfo _dbConnectionInfo;

        private TableGroupForm _tableGroupForm = new TableGroupForm();

        public MainForm()
        {
            var connectionSettingsForm = new ConnectionSettingsForm();
            if (connectionSettingsForm.ShowDialog() == DialogResult.OK)
            {
                InitializeComponent();
            }
            else
            {
                Close();
                return;
            }
            ConfigHelper.OnSave += ConfigHelper_OnSave;
        }

        private void ConfigHelper_OnSave(ConfigHelper.ConfigEntity obj)
        {
            obj.OutputFolder = textBoxOutputFolder.Text;
            obj.IsOnFinishedOpenFolder = checkBoxOnFinishedOpenDir.Checked;
            obj.IsClearFolderBeforeRunning = checkBoxClearDir.Checked;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ConfigHelper.SaveConnectionConfigs(_dbConnectionInfos);
            ConfigHelper.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadFormStatus();
            RegisterMenuEvent();
            RefreshTemplates();
            LoadDbinfo();
        }

        private void LoadFormStatus()
        {
            textBoxOutputFolder.Text = string.IsNullOrWhiteSpace(ConfigHelper.Config.OutputFolder) ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "output")
                : ConfigHelper.Config.OutputFolder;
            checkBoxOnFinishedOpenDir.Checked = ConfigHelper.Config.IsOnFinishedOpenFolder;
            checkBoxClearDir.Checked = ConfigHelper.Config.IsClearFolderBeforeRunning;
        }

        private void LoadDbinfo()
        {
            _dbConnectionInfo = ConfigHelper.CurrentDbConnectionInfo;
            _dbInfoProvider = _mysqlDbInfoProvider;
            _dbInfoProvider.DbConnectionString = _dbConnectionInfo.ToConnectionString();

            _selectedTables.Clear();
            checkedListBoxTables.ClearSelected();
            comboBoxDbNames.DataSource = _dbInfoProvider.GetDataBases();
            checkBoxSelectAllTables.Checked = false;
        }

        private void comboBoxDbNames_SelectedValueChanged(object sender, EventArgs e)
        {
            _selectedDb = comboBoxDbNames.SelectedItem as DatabaseInfo;
            _tableGroupInfo = _dbConnectionInfo.TableGroups.ContainsKey(_selectedDb.DbName) ? _dbConnectionInfo.TableGroups[_selectedDb.DbName] : new List<TableGroupInfo>();
            _selectedTables.Clear();
            checkedListBoxTables.Items.Clear();
            checkedListBoxTables.ClearSelected();
            if (_selectedDb == null)
            {
                buttonGroupEdit.Enabled = false;
                return;
            }

            buttonGroupEdit.Enabled = true;
            var tables = _dbInfoProvider.GetTables(_selectedDb.DbName, true).ToList();
            checkedListBoxTables.Items.AddRange(tables.ToArray());

            var isCheckAll = true;
            if (_dbConnectionInfo.SelectTables != null
                && _dbConnectionInfo.SelectTables.ContainsKey(_selectedDb.DbName)
                && _dbConnectionInfo.SelectTables[_selectedDb.DbName] != null
                && _dbConnectionInfo.SelectTables[_selectedDb.DbName].Count > 0)
            {
                for (var i = 0; i < checkedListBoxTables.Items.Count; i++)
                {
                    var isChecked = _dbConnectionInfo.SelectTables[_selectedDb.DbName]
                        .Contains(((TableInfo)checkedListBoxTables.Items[i]).TableName);
                    isCheckAll &= isChecked;
                    checkedListBoxTables.SetItemChecked(i, isChecked);
                }
            }

            checkBoxSelectAllTables.Checked = isCheckAll;
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

        private void checkedListBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCodePreview();
        }

        private void buttonCreateCodes_Click(object sender, EventArgs e)
        {
            _selectedTables = GetSelectedTables().ToList();

            if (_selectedTables.Count == 0)
            {
                MessageBox.Show("请选择需要生成的表！！！");
                return;
            }

            var outputDir = Path.Combine(textBoxOutputFolder.Text, _selectedDb.DbName);
            CreateDir(outputDir);
            if (checkBoxClearDir.Checked)
            {
                ClearDir(outputDir);
            }

            if (_dbConnectionInfo.SelectTables == null)
            {
                _dbConnectionInfo.SelectTables = new Dictionary<string, List<string>>();
            }

            if (!_dbConnectionInfo.SelectTables.ContainsKey(_selectedDb.DbName))
            {
                _dbConnectionInfo.SelectTables.Add(_selectedDb.DbName, _selectedTables.Select(x => x.TableName).ToList());
            }
            else
            {
                _dbConnectionInfo.SelectTables[_selectedDb.DbName] = _selectedTables.Select(x => x.TableName).ToList();
            }

            var templates = GetSelectedTemplates().ToList();

            var classModels = _selectedTables.Select(x =>
            {
                var classModel = _dbInfoProvider.ToClassModel(x);
                classModel.GroupName = GetGroupName(x.TableName);
                return classModel;
            });


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            foreach (var classModel in classModels)
            {
                foreach (var template in templates)
                {
                    var codeStr = TemplateManager.Run(template, classModel);
                    //报存文件
                    var path = Path.Combine(outputDir, classModel.OutPutFileName);
                    CreateDirectoryAndRemoveFile(path);
                    using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    using var writer = new StreamWriter(stream, Encoding.UTF8);
                    writer.Write(codeStr);
                }
            }

            stopwatch.Stop();

            if (checkBoxOnFinishedOpenDir.Checked)
            {
                OpenFolder(textBoxOutputFolder.Text);
            }
        }

        private void buttonGroupEdit_Click(object sender, EventArgs e)
        {
            var tableNames = Enumerable.Cast<TableInfo>(checkedListBoxTables.Items).Select(x => x.TableName);
            var tableGroupingInfo = new TableGroupingInfo()
            {
                TableNames = tableNames,
                AlreadyGroupTable = tableNames
                    .Select(x => new TableGroupInfo()
                    {
                        TableName = x,
                        GroupName = GetGroupName(x)
                    }).ToList()
            };
            if (_tableGroupInfo == null)
            {
                tableGroupingInfo.GroupNames = new List<string>();
                tableGroupingInfo.AlreadyGroupTable = new List<TableGroupInfo>();
            }
            else
            {
                tableGroupingInfo.GroupNames = _tableGroupInfo.Select(x => x.GroupName).Distinct().OrderBy(x => x).ToList();
                tableGroupingInfo.AlreadyGroupTable = _tableGroupInfo.Select(x => new TableGroupInfo()
                {
                    GroupName = x.GroupName,
                    TableName = x.TableName,
                }).ToList();
            }

            _tableGroupForm.SetData(tableGroupingInfo);
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
                _dbConnectionInfo.TableGroups = new Dictionary<string, List<TableGroupInfo>>();
            }
            _dbConnectionInfo.TableGroups.Remove(_selectedDb.DbName);
            _dbConnectionInfo.TableGroups.Add(_selectedDb.DbName, _tableGroupInfo);
        }

        private string GetGroupName(string tableName)
        {
            return _tableGroupInfo.FirstOrDefault(x => x.TableName == tableName)?.GroupName ?? string.Empty;
        }

        private void ClearDir(string dir)
        {
            if (!string.IsNullOrWhiteSpace(dir) && Directory.Exists(dir))
            {
                Directory.Delete(dir, true);
            }
        }

        private void CreateDir(string dir)
        {
            if (!string.IsNullOrWhiteSpace(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private void CreateDirectoryAndRemoveFile(string filepath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            }

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }

        #region 选表

        /// <summary>
        /// 获取选中的表名
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TableInfo> GetSelectedTables()
        {
            return Enumerable.Cast<TableInfo>(checkedListBoxTables.Items)
                .Where((t, i) => checkedListBoxTables.GetItemChecked(i))
                .Select(t => t).ToList();
        }

        /// <summary>
        /// 获取选中的表名
        /// </summary>
        /// <returns></returns>
        private TableInfo GetSelectedTable()
        {
            return (TableInfo)checkedListBoxTables.SelectedItem;
        }

        #endregion 选表

        #region 模板操作

        private string GetSelectedTemplate()
        {
            return (string)checkedListBoxTemplate.SelectedItem;
        }

        private IEnumerable<string> GetSelectedTemplates()
        {
            return Enumerable.Cast<string>(checkedListBoxTemplate.Items)
                .Where((t, i) => checkedListBoxTemplate.GetItemChecked(i))
                .Select(t => t);
        }

        /// <summary>
        /// 刷新模板
        /// </summary>
        private void RefreshTemplates()
        {
            TemplateManager.LoadTemplates(true);
            checkedListBoxTemplate.Items.Clear();
            checkedListBoxTemplate.ClearSelected();
            checkedListBoxTemplate.Items.AddRange(TemplateManager.TemplateDic.Keys.OrderBy(x => x).ToArray());
        }

        private void checkBoxSelectAllTemplate_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxTemplate.Items.Count; i++)
            {
                checkedListBoxTemplate.SetItemChecked(i, checkBoxSelectAllTemplate.Checked);
            }
        }

        private void checkedListBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 代码预览

            LoadCodePreview();

            #endregion 代码预览
        }

        /// <summary>
        /// 加载代码预览
        /// </summary>
        private void LoadCodePreview()
        {
            var template = GetSelectedTemplate();
            if (template == null)
            {
                template = GetSelectedTemplates().FirstOrDefault();
            }

            if (template == null)
            {
                textBoxGeneratorDesc.Text = string.Empty;
                return;
            }
            textBoxGeneratorDesc.Text = TemplateManager.TemplateDic[template];


            var table = GetSelectedTable();
            if (table == null)
            {
                table = GetSelectedTables().FirstOrDefault();
            }

            textBoxCodePreview.Clear();
            if (table != null && !string.IsNullOrWhiteSpace(template))
            {
                textBoxCodePreview.Text = TemplateManager.Run(template, _dbInfoProvider.ToClassModel(table));
            }
        }

        #endregion 模板操作

        #region 菜单操作        
        /// <summary>
        /// 注册菜单事件
        /// </summary>
        private void RegisterMenuEvent()
        {
            openTemplateFolderToolStripMenuItem.Click += OpenTemplateFolderToolStripMenuItem_Click;
            dbConnManagerToolStripMenuItem.Click += DbConnManagerToolStripMenuItem_Click;
            reloadTemplateToolStripMenuItem.Click += ReloadTemplateToolStripMenuItem_Click;
        }

        private void ReloadTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTemplates();
        }

        private void DbConnManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var connectionSettingsForm = new ConnectionSettingsForm();
            var dialogResult = connectionSettingsForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _dbConnectionInfo = ConfigHelper.CurrentDbConnectionInfo;
                LoadDbinfo();
            }
        }

        private void OpenTemplateFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder(TemplateManager.TemplateRootPath);
        }

        #endregion

        #region 通用操作

        private void OpenFolder(string folder)
        {
            Process p = new Process();
            p.StartInfo.FileName = "explorer.exe";
            p.StartInfo.Arguments = folder;
            p.Start();
        }

        #endregion
    }
}