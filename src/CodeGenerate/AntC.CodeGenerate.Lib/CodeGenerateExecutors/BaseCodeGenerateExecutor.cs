using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseCodeGenerateExecutor
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
    }
}
