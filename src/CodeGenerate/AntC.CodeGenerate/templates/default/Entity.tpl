@using System
@using AntC.CodeGenerate.Core.Extension
@using AntC.CodeGenerate.Core.Model.CLR
@using AntC.CodeGenerate
@{
    var classModel = (ClassModel)Model;
    //设置输出文件名及相对路径
    classModel.OutPutFileName = $"{(string.IsNullOrWhiteSpace(classModel.GroupName) ? "" : classModel.GroupName+"")}{classModel.ClassName}Entity.cs";
}
/// <summary>
/// @classModel.Annotation
/// </summary>
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