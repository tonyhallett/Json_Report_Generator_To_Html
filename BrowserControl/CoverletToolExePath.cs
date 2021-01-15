using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{
	static class CoverletToolExePath
	{
		public static string Get(string name)
		{
			var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			return Path.Combine(localAppData, $@"FineCodeCoverage\{name}\{name}.exe");
		}
	}

}
