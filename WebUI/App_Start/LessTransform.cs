using dotless.Core;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Parser;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class LessTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse bundle)
        {
            var pathResolver = new ImportedFilePathResolver(context.HttpContext.Server);
            var lessParser = new Parser();
            var lessEngine = new LessEngine(lessParser);
            (lessParser.Importer as Importer).FileReader = new FileReader(pathResolver);
            var content = new StringBuilder(bundle.Content.Length);
            foreach (var bundleFile in bundle.Files)
            {
                pathResolver.SetCurrentDirectory(bundleFile.IncludedVirtualPath);
                using (var stream = new StreamReader(bundleFile.VirtualFile.Open()))
                {
                    content.Append(lessEngine.TransformToCss(stream.ReadToEnd(), bundleFile.IncludedVirtualPath));
                }
                content.AppendLine();
            }
            bundle.ContentType = "text/css";
            bundle.Content = content.ToString();
        }
    }

    public class ImportedFilePathResolver : IPathResolver
    {
        private HttpServerUtilityBase server { get; set; }
        private string currentDirectory { get; set; }

        public ImportedFilePathResolver(HttpServerUtilityBase server)
        {
            this.server = server;
        }

        public void SetCurrentDirectory(string fileLocation)
        {
            currentDirectory = Path.GetDirectoryName(fileLocation);
        }

        public string GetFullPath(string filePath)
        {
            var baseDirectory = server.MapPath(currentDirectory);
            return Path.GetFullPath(Path.Combine(baseDirectory, filePath));
        }
    }
}