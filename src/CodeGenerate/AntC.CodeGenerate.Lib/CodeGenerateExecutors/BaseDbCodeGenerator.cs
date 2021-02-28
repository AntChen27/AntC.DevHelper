using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseDbCodeGenerator : BaseCodeGenerator<CodeGenerateDbContext>, IDbCodeGenerator
    {
    }
}
