using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseDbCodeGenerateExecutor : BaseCodeGenerateExecutor, IDbCodeGenerateExecutor
    {
        public abstract void ExecCodeGenerate(CodeGenerateDbContext context);
    }
}
