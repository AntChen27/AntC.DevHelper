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
            RefreshCheckedListBoxTemplate();
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
            var tables = _dbInfoProvider.GetTables(_selectedDb.DbName).ToList();
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
                        .Contains(((DbTableInfoModel)checkedListBoxTables.Items[i]).TableName);
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
            var codeGeneratorInfo = GetSelectedTemplate();
            if (codeGeneratorInfo == null)
            {
                codeGeneratorInfo = GetSelectedTemplates().FirstOrDefault();
            }
            var tableName = GetSelectedTableName()?.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = GetSelectedTableNames().FirstOrDefault();
            }
            ShowCodePreview(tableName, codeGeneratorInfo);
        }

        private void buttonCreateCodes_Click(object sender, EventArgs e)
        {
            _selectedTables = GetSelectedTableNames().ToList();

            if (_selectedTables.Count == 0)
            {
                MessageBox.Show("请选择需要生成的表！！！");
                return;
            }

            var outputDir = Path.Combine(textBoxOutputFolder.Text, _selectedDb.DbName);

            if (checkBoxClearDir.Checked)
            {
                ClearDir(new DirectoryInfo(outputDir));
            }

            if (_dbConnectionInfo.SelectTables == null)
            {
                _dbConnectionInfo.SelectTables = new Dictionary<string, List<string>>();
            }

            if (!_dbConnectionInfo.SelectTables.ContainsKey(_selectedDb.DbName))
            {
                _dbConnectionInfo.SelectTables.Add(_selectedDb.DbName, _selectedTables);
            }
            else
            {
                _dbConnectionInfo.SelectTables[_selectedDb.DbName] = _selectedTables;
            }

            var codeGeneratorManager = ServiceManager.CreateGeneratorManager(_dbConnectionInfo);
            // todo 添加模板选择
            //codeGeneratorManager.AddBenchintCodeGenerateImpl();
            GetSelectedTemplates().ToList().ForEach(x =>
            {
                if (x.DbGenerator != null)
                {
                    codeGeneratorManager.AddCodeGenerator(x.DbGenerator);
                }

                if (x.TableGenerator != null)
                {
                    codeGeneratorManager.AddCodeGenerator(x.TableGenerator);
                }
            });
            codeGeneratorManager.AddPropertyTypeConverter(typeof(Plugin.Benchint.Plugin).Assembly);
            codeGeneratorManager.SetCodeWriterType<CodeFileWriter>();

            var codeGenerateInfo = new CodeGenerateInfo()
            {
                OutPutRootPath = outputDir,
                DbName = _selectedDb.DbName,
                CodeGenerateTableInfos = _selectedTables.Select(x => new CodeGenerateTableInfo()
                {
                    TableName = x,
                    GroupName = GetGroupName(x)
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
                p.StartInfo.Arguments = textBoxOutputFolder.Text;
                p.Start();
            }
        }

        private void buttonGroupEdit_Click(object sender, EventArgs e)
        {
            var tableNames = Enumerable.Cast<DbTableInfoModel>(checkedListBoxTables.Items).Select(x => x.TableName);
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

        private void ClearDir(DirectoryInfo directory)
        {
            if (directory != null && directory.Exists)
            {
                directory.Delete(true);
            }
        }

        #region 选表

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

        /// <summary>
        /// 获取选中的表名
        /// </summary>
        /// <returns></returns>
        private DbTableInfoModel GetSelectedTableName()
        {
            return (DbTableInfoModel)checkedListBoxTables.SelectedItem;
        }

        #endregion

        #region 模板操作

        private CodeGeneratorInfo GetSelectedTemplate()
        {
            return (CodeGeneratorInfo)checkedListBoxTemplate.SelectedItem;
        }

        private IEnumerable<CodeGeneratorInfo> GetSelectedTemplates()
        {
            return Enumerable.Cast<CodeGeneratorInfo>(checkedListBoxTemplate.Items)
                .Where((t, i) => checkedListBoxTemplate.GetItemChecked(i))
                .Select(t => t);
        }

        private void RefreshCheckedListBoxTemplate()
        {
            List<CodeGeneratorInfo> codeGenerators = PluginManager.GetPlugins().SelectMany(x =>
            {
                List<CodeGeneratorInfo> generators = new List<CodeGeneratorInfo>();

                var dbGenerators = x.GetDbCodeGenerators().Select(c =>
                {
                    var generator = (IDbCodeGenerator)ServiceManager.ServiceProvider.GetService(c);
                    var codeGeneratorInfo = new CodeGeneratorInfo()
                    {
                        DbGenerator = generator,
                        CodeGeneratorType = c
                    };
                    codeGeneratorInfo.GeneratorInfo = codeGeneratorInfo.DbGenerator?.GeneratorInfo;
                    return codeGeneratorInfo;
                });
                var tableGenerators =
                    x.GetTableCodeGenerators().Select(c =>
                    {
                        var generator = (ITableCodeGenerator)ServiceManager.ServiceProvider.GetService(c);
                        var codeGeneratorInfo = new CodeGeneratorInfo()
                        {
                            TableGenerator = generator,
                            CodeGeneratorType = c
                        };
                        codeGeneratorInfo.GeneratorInfo = codeGeneratorInfo.TableGenerator?.GeneratorInfo;
                        return codeGeneratorInfo;
                    });
                generators.AddRange(dbGenerators);
                generators.AddRange(tableGenerators);
                return generators;
            }).OrderBy(x => x.ToString()).ToList();

            checkedListBoxTemplate.Items.Clear();
            checkedListBoxTemplate.ClearSelected();
            checkedListBoxTemplate.Items.AddRange(codeGenerators.ToArray());
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
            var codeGeneratorInfo = GetSelectedTemplate();
            if (codeGeneratorInfo == null)
            {
                codeGeneratorInfo = GetSelectedTemplates().FirstOrDefault();
            }

            if (codeGeneratorInfo == null)
            {
                textBoxGeneratorDesc.Text = string.Empty;
                return;
            }
            textBoxGeneratorDesc.Text = codeGeneratorInfo.GeneratorInfo?.Desc ?? "无描述";

            #region 代码预览

            var tableName = GetSelectedTableName()?.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = GetSelectedTableNames().FirstOrDefault();
            }
            ShowCodePreview(tableName, codeGeneratorInfo);

            #endregion
        }

        private void ShowCodePreview(string tableName, CodeGeneratorInfo codeGeneratorInfo)
        {
            if (string.IsNullOrEmpty(tableName) || codeGeneratorInfo == null)
            {
                return;
            }
            if (_dbConnectionInfo.SelectTables == null)
            {
                _dbConnectionInfo.SelectTables = new Dictionary<string, List<string>>();
            }

            if (!_dbConnectionInfo.SelectTables.ContainsKey(_selectedDb.DbName))
            {
                _dbConnectionInfo.SelectTables.Add(_selectedDb.DbName, _selectedTables);
            }
            else
            {
                _dbConnectionInfo.SelectTables[_selectedDb.DbName] = _selectedTables;
            }

            var codeGeneratorManager = ServiceManager.CreateGeneratorManager(_dbConnectionInfo);

            if (codeGeneratorInfo.DbGenerator != null)
            {
                codeGeneratorManager.AddCodeGenerator(codeGeneratorInfo.DbGenerator);
            }

            if (codeGeneratorInfo.TableGenerator != null)
            {
                codeGeneratorManager.AddCodeGenerator(codeGeneratorInfo.TableGenerator);
            }
            codeGeneratorManager.AddPropertyTypeConverter(typeof(Plugin.Benchint.Plugin).Assembly);

            codeGeneratorManager.SetCodeWriterType<CustomCodeWriter>();

            codeGeneratorManager.OnCodeWriterCreated += (writer) =>
            {
                if (writer is CustomCodeWriter ccw)
                {
                    ccw.OnAppendContent += CustomCodeWriter_OnAppendContent;
                }
            };

            textBoxCodePreview.Clear();
            var codeGenerateInfo = new CodeGenerateInfo()
            {
                OutPutRootPath = null,
                DbName = _selectedDb.DbName,
                CodeGenerateTableInfos = new List<CodeGenerateTableInfo>()
                {
                    new CodeGenerateTableInfo()
                    {
                        TableName = tableName,
                        GroupName = GetGroupName(tableName)
                    }
                }
            };

            codeGeneratorManager.ExecCodeGenerate(codeGenerateInfo);
        }

        private void CustomCodeWriter_OnAppendContent(string obj)
        {
            textBoxCodePreview.AppendText(obj ?? string.Empty);
        }

        #endregion
    }

    public class CodeGeneratorInfo
    {
        public ITableCodeGenerator TableGenerator { get; set; }

        public IDbCodeGenerator DbGenerator { get; set; }

        public GeneratorInfo GeneratorInfo { get; set; }

        public Type CodeGeneratorType { get; set; }

        public override string ToString()
        {
            if (this.GeneratorInfo == null)
            {
                if (this.CodeGeneratorType == null)
                {
                    return base.ToString();
                }

                return CodeGeneratorType.FullName;
            }

            return GeneratorInfo?.Name ?? base.ToString();
        }
    }
}
