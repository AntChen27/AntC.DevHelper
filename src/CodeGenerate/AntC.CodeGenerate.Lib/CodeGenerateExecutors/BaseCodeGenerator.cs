using System;
using System.IO;
using AntC.CodeGenerate.Interfaces;
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

        protected virtual void SetRelativePath(CodeGenerateContext context, string fileRelativePath)
        {
            var outPutPath = Path.Combine(context.OutPutRootPath, fileRelativePath.Replace('\\', '/').TrimStart('/'));
            SetOutPutFilePath(context.CodeWriter as ICodeFileWriter, outPutPath);
        }

        protected virtual void SetOutPutFilePath(ICodeFileWriter codeFileWriter, string filePath)
        {
            if (codeFileWriter != null)
            {
                codeFileWriter.FilePath = filePath;
            }
        }
    }
}
