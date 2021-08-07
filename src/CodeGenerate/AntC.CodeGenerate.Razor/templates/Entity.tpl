@using System
@using AntC.CodeGenerate.Core.Extension
@using AntC.CodeGenerate.Core.Model.CLR
@using AntC.CodeGenerate.Razor
@{
    var classModel = (ClassModel)Model;
    //这可以
	//Model.SetOutputFileName(tt+"_Table");
	//这也行的，但是必须使用变量保存Model中的字段数据
    classModel.OutPutFileName=$"{classModel.ClassName}Entity.cs";
}
public class @classModel.ClassName
{
@foreach (var prop in classModel.Properties)
{
    if (prop.DbColumnInfo.IsAbpProperty())
    {
        continue;
    }
    @:/// <summary>
    @:/// @prop.Annotation
    @:/// </summary>
    @:public @prop.PropertyTypeName @prop.PropertyName { get; set; }
    @:
}
}