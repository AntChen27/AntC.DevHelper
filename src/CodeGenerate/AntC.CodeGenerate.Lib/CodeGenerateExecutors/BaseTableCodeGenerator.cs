using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseTableCodeGenerator : BaseCodeGenerator, ITableCodeGenerator
    {
        public abstract void ExecCodeGenerate(CodeGenerateTableContext context);
    }
}
