using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseCodeGenerator
    {
        protected GeneratorInfo DefaultGeneratorInfo;

        protected BaseCodeGenerator()
        {
            DefaultGeneratorInfo = new GeneratorInfo()
            {
                Name = GetType().FullName,
                Desc = ""
            };
        }

        public virtual GeneratorInfo GeneratorInfo => DefaultGeneratorInfo;

        /// <summary>
        /// 代码创建器参数
        /// </summary>
        public virtual GeneratorConfig GeneratorConfig { get; } = new GeneratorConfig();
    }
}
