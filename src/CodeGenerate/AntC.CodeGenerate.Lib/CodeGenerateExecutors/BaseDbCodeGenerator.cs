using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseDbCodeGenerator : BaseCodeGenerator, IDbCodeGenerator
    {
        public abstract void ExecCodeGenerate(CodeGenerateDbContext context);
    }
}
