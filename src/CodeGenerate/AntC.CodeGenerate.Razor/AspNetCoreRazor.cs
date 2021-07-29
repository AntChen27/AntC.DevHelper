namespace AntC.CodeGenerate.Razor
{
    public class AspNetCoreRazor
    {
        //public GeneratorResults Generate(Type modelType, string template)
        //{
        //    //准备临时类名，读取模板文件和Razor代码生成器
        //    string templateType = "YOYO.AspNetCore.ViewEngine.Razor.RazorViewTemplate";
        //    string templateTypeName = modelType != null ? string.Format(templateType + @"<{0}>", modelType.FullName) : templateType;
        //    var class_name = "c" + Guid.NewGuid().ToString("N");
        //    var host = new RazorEngineHost(new CSharpRazorCodeLanguage(), () => new HtmlMarkupParser())
        //    {
        //        DefaultBaseClass = templateTypeName,
        //        DefaultClassName = class_name,
        //        DefaultNamespace = "YOYO.AspNetCore.ViewEngine.Razor",
        //        GeneratedClassContext =
        //                           new GeneratedClassContext("Execute", "Write", "WriteLiteral", "WriteTo",
        //                                                     "WriteLiteralTo",
        //                                                     "RazorViewTemplate.Dynamic", new GeneratedTagHelperContext())
        //    };
        //    host.NamespaceImports.Add("System");
        //    host.NamespaceImports.Add("System.Dynamic");
        //    host.NamespaceImports.Add("System.Linq");
        //    host.NamespaceImports.Add("System.Collections.Generic");
        //    var engine = new RazorTemplateEngine(host);
        //    return engine.GenerateCode(new StringReader(template));
        //}
    }
}
