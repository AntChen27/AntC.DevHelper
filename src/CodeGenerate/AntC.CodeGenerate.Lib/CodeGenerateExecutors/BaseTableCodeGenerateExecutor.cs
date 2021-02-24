using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseTableCodeGenerateExecutor : BaseCodeGenerateExecutor, ITableCodeGenerateExecutor
    {
        public abstract void ExecCodeGenerate(CodeGenerateTableContext context);
    }
}
