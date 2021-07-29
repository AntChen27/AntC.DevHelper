@using System
@using AntC.CodeGenerate.Razor
@using AntC.CodeGenerate.Model
@{
    var codeHelper = (CodeHelper)Model;
	var tt = Model.TableName;
	//这可以
	//Model.SetOutputFileName(tt+"_Table");
	//这也行的，但是必须使用变量保存Model中的字段数据
    Model.OutputFileName=tt+"_Table1";
}
public class @codeHelper.TableInfo.TableName
{
    @foreach (var col in codeHelper.TableInfo.Columns)
    {
        @:public @col.DataType @col.ColumnName { get; set; }
        @:
    }
}