using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseCodeGenerateExecutor : ICodeGenerateExecutor
    {
        protected ExecutorInfo DefaultExecutorInfo;

        protected BaseCodeGenerateExecutor()
        {
            DefaultExecutorInfo = new ExecutorInfo()
            {
                Name = GetType().FullName,
                Desc = ""
            };
        }
        
        public virtual ExecutorInfo ExecutorInfo => DefaultExecutorInfo;

        public abstract void ExecCodeGenerate(CodeGenerateContext context);
    }
}
