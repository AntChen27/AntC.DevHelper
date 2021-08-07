using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine.Compilation;
using RazorEngine.Compilation.ReferenceResolver;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// <see cref="RazorEngine.Compilation.ReferenceResolver.UseCurrentAssembliesReferenceResolver"/>
    /// </summary>
    /// <seealso cref="RazorEngine.Compilation.ReferenceResolver.IReferenceResolver" />
    public class MyReferenceResolver : IReferenceResolver
    {
        /// <summary>
        /// <see cref="CompilerServiceBase.DynamicTemplateNamespace"/>
        /// </summary>
        protected internal const string DynamicTemplateNamespace = "CompiledRazorTemplates.Dynamic";
        /// <summary>
        /// See <see cref="IReferenceResolver.GetReferences"/>
        /// </summary>
        /// <param name="context"></param>
        /// <param name="includeAssemblies"></param>
        /// <returns></returns>
        public IEnumerable<CompilerReference> GetReferences(TypeContext context = null, IEnumerable<CompilerReference> includeAssemblies = null)
        {
            return CompilerServicesUtility
                   .GetLoadedAssemblies()
                   .Where(a => !a.IsDynamic && File.Exists(a.Location) && !a.Location.Contains(DynamicTemplateNamespace))
                   .GroupBy(a => a.GetName().Name).Select(grp => grp.First(y => y.GetName().Version == grp.Max(x => x.GetName().Version))) // only select distinct assemblies based on FullName to avoid loading duplicate assemblies
                   .Select(a => CompilerReference.From(a))
                   .Concat(includeAssemblies ?? Enumerable.Empty<CompilerReference>());
        }
    }
}
