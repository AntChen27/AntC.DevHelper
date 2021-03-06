using System;
using System.IO;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    public abstract class BaseCodeGenerator<TContext> : ICodeGenerator<TContext>
        where TContext : CodeGenerateContext
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
        public virtual GeneratorConfig GeneratorConfig { get; set; }

        protected virtual GeneratorConfig GetDefaultConfig(TContext context)
        {
            return new GeneratorConfig();
        }

        public virtual void ExecCodeGenerate(TContext context)
        {
            PreExecCodeGenerate(context);
            ExecutingCodeGenerate(context);
            ExecutedCodeGenerate(context);
        }

        public virtual void PreExecCodeGenerate(TContext context)
        {
            var generatorConfig = GeneratorConfig ?? GetDefaultConfig(context);
            var outPutPath = Path.Combine(context.OutPutRootPath, generatorConfig.FileRelativePath.Replace('\\', '/').TrimStart('/'));
            SetOutPutFilePath(context.CodeWriter as ICodeFileWriter, outPutPath);
        }

        public abstract void ExecutingCodeGenerate(TContext context);

        public virtual void ExecutedCodeGenerate(TContext context)
        {

        }

        private void SetOutPutFilePath(ICodeFileWriter codeFileWriter, string filePath)
        {
            if (codeFileWriter != null)
            {
                codeFileWriter.FilePath = filePath;
            }
        }
    }
}
