using System.IO;
using System.Reflection;

namespace BrowserControl
{
    class HtmlProvider : IHtmlProvider
    {
        public string GetPath()
        {
            var solnDir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent;
            return System.IO.Path.Combine(solnDir.FullName, @"Report\dist\index.html");
        }
    }

    
}
