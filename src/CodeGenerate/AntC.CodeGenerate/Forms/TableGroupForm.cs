using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Forms
{
    public partial class TableGroupForm : Form
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        private List<string> _groupNames = new List<string>();

        /// <summary>
        /// 表分组信息
        /// </summary>
        private List<TableGroupInfo> _tableGroupInfo = new List<TableGroupInfo>();

        /// <summary>
        /// 所有表信息
        /// </summary>
        private List<string> _allTables = new List<string>();

        /// <summary>
        /// 未分组表信息
        /// </summary>
        private List<string> _noGroupTables = new List<string>();

        public TableGroupForm()
        {
            InitializeComponent();
        }

        public void SetData(TableGroupingInfo tableGroupingInfo)
        {
            _groupNames.Clear();
            _groupNames.AddRange(tableGroupingInfo.GroupNames);

            _tableGroupInfo.Clear();
            _tableGroupInfo.AddRange(tableGroupingInfo.
                AlreadyGroupTable
                .Where(x => !string.IsNullOrEmpty(x.GroupName)));

            _allTables.Clear();
            _allTables.AddRange(tableGroupingInfo.TableNames);

            _noGroupTables.Clear();
            _noGroupTables.AddRange(_allTables
                .Where(x => _tableGroupInfo.All(o => !string.IsNullOrEmpty(o.GroupName) && o.TableName != x))
               .ToList());
        }

        /// <summary>
        /// 表-分组信息
        /// </summary>
        /// <returns></returns>
        public List<TableGroupInfo> GetTableGroupData()
        {
            List<TableGroupInfo> groupInfos = new List<TableGroupInfo>();
            groupInfos.AddRange(_tableGroupInfo);
            groupInfos.AddRange(_noGroupTables
                .Select(x => new TableGroupInfo()
                {
                    TableName = x,
                    GroupName = string.Empty
                }));
            return groupInfos;
        }

        private void TableGroupForm_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            RefreshCheckedListBoxGroup();
            RefreshCheckedListBoxNoGroupTables();
            RefreshCheckedListBoxGroupTables();
        }

        private void RefreshCheckedListBoxGroup()
        {
            var selectedItem = comboBoxGroupName.SelectedItem;
            comboBoxGroupName.SelectedItem = null;
            comboBoxGroupName.Items.Clear();
            foreach (var groupName in _groupNames.OrderBy(x => x))
            {
                comboBoxGroupName.Items.Add(groupName);
            }

            if (_groupNames.Contains<object>(selectedItem))
            {
                comboBoxGroupName.SelectedItem = selectedItem;
            }
        }

        private void RefreshCheckedListBoxGroupTables()
        {
            checkedListBoxGroupTables.ClearSelected();
            checkedListBoxGroupTables.Items.Clear();
            var tableGroupInfos = _tableGroupInfo.OrderBy(x => x.GroupName).ThenBy(x => x.TableName);

            foreach (var groupInfo in tableGroupInfos)
            {
                checkedListBoxGroupTables.Items.Add(groupInfo);
            }
        }

        private void RefreshCheckedListBoxNoGroupTables()
        {
            checkedListBoxNoGroupTables.ClearSelected();
            checkedListBoxNoGroupTables.Items.Clear();
            foreach (var tableName in _noGroupTables.OrderBy(x => x))
            {
                checkedListBoxNoGroupTables.Items.Add(tableName);
            }
        }

        /// <summary>
        /// 获取选中的表名
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetSelectedTableNames()
        {
            return Enumerable.Cast<string>(checkedListBoxNoGroupTables.Items)
                .Where((t, i) => checkedListBoxNoGroupTables.GetItemChecked(i))
                .Select(t => t).ToList();
        }

        private void buttonAddTableGroup_Click(object sender, EventArgs e)
        {
            var groupName = comboBoxGroupName.SelectedItem?.ToString();
            var tableNames = GetSelectedTableNames();
            if (string.IsNullOrEmpty(groupName) || tableNames == null || !tableNames.Any())
            {
                return;
            }

            foreach (var tableName in tableNames)
            {
                _noGroupTables.Remove(tableName);
                _tableGroupInfo.Add(new TableGroupInfo()
                {
                    TableName = tableName,
                    GroupName = groupName,
                });
            }
            Refresh();
        }

        /// <summary>
        /// 获取选中的分组表信息
        /// </summary>
        /// <returns></returns>
        private IEnumerable<TableGroupInfo> GetSelectedGroupInfos()
        {
            return Enumerable.Cast<TableGroupInfo>(checkedListBoxGroupTables.Items)
                .Where((t, i) => checkedListBoxGroupTables.GetItemChecked(i))
                .Select(t => t).ToList();
        }

        private void buttonRemoveTableGroup_Click(object sender, EventArgs e)
        {
            var groupInfos = GetSelectedGroupInfos();
            if (groupInfos == null || !groupInfos.Any())
            {
                return;
            }

            foreach (var groupInfo in groupInfos)
            {
                _noGroupTables.Add(groupInfo.TableName);
                _tableGroupInfo.Remove(groupInfo);
            }
            Refresh();
        }

        private void buttonEditGroup_Click(object sender, EventArgs e)
        {
            if (comboBoxGroupName.SelectedItem == null ||
                string.IsNullOrEmpty(comboBoxGroupName.SelectedItem.ToString()))
            {
                MessageBox.Show("请选择分组后进行修改");
                return;
            }

            var editGroupForm = new EditGroupForm
            {
                GroupNames = _groupNames,
                IsAdd = false,
                GroupName = comboBoxGroupName.SelectedItem.ToString()
            };
            if (DialogResult.OK == editGroupForm.ShowDialog())
            {
                //_groupNames.Remove(editGroupForm.GroupName);
                //_groupNames.Add(editGroupForm.NewGroupName);

                RefreshCheckedListBoxGroup();
                // 调整分组表
                _tableGroupInfo = _tableGroupInfo.Select(x =>
                {
                    if (x.GroupName == editGroupForm.GroupName)
                    {
                        x.GroupName = editGroupForm.NewGroupName;
                    }
                    return x;
                }).ToList();
                RefreshCheckedListBoxGroupTables();
            }
        }

        private void buttonDeleteGroup_Click(object sender, EventArgs e)
        {
            var groupName = comboBoxGroupName.SelectedItem?.ToString();
            if (comboBoxGroupName.SelectedItem == null ||
                string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("请选择分组后进行修改");
                return;
            }

            if (DialogResult.Yes != MessageBox.Show("删除后已分组的表将移动到未分组的列表中,是否确定删除？", "警告", MessageBoxButtons.YesNo))
            {
                return;
            }
            //移除分组信息
            _groupNames.Remove(groupName);
            //添加到未分组表信息中
            _noGroupTables.AddRange(_tableGroupInfo.Where(x => x.GroupName == groupName).Select(x => x.TableName));
            //移除已分组表信息
            _tableGroupInfo = _tableGroupInfo.Where(x => x.GroupName != groupName).ToList();
            //刷新界面数据
            Refresh();
        }

        private void buttonAddGroup_Click(object sender, EventArgs e)
        {
            var editGroupForm = new EditGroupForm
            {
                GroupNames = _groupNames,
                IsAdd = true,
                GroupName = string.Empty
            };
            if (DialogResult.OK == editGroupForm.ShowDialog())
            {
                //_groupNames.Add(editGroupForm.NewGroupName);
                RefreshCheckedListBoxGroup();
            }
        }

        private void comboBoxGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCheckedListBoxGroupTables();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TableGroupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }

    public class TableGroupingInfo
    {
        public IEnumerable<string> TableNames { get; set; }
        public IEnumerable<string> GroupNames { get; set; }
        public List<TableGroupInfo> AlreadyGroupTable { get; set; }
    }
}
