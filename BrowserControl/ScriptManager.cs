using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BrowserControl
{
	[ComVisible(true)]
	public class ScriptManager
	{
        private IWindowExternalCallbackWriter windowExternalCallbackWriter;

        public ScriptManager(IWindowExternalCallbackWriter windowExternalCallbackWriter)
        {
            this.windowExternalCallbackWriter = windowExternalCallbackWriter;
        }
		public void Log(string message)
        {
			Debug.WriteLine(message);
        }

		//commented out in js currently
		public void LogError(string message)
        {
			this.windowExternalCallbackWriter.Received($"Error - {message}");
        }

        public void OpenFile(string assemblyName, string qualifiedClassName)
		{
			this.windowExternalCallbackWriter.Received($"{assemblyName} {qualifiedClassName}");
		}

		public void BuyMeACoffee()
		{
			//System.Diagnostics.Process.Start("https://paypal.me/FortuneNgwenya");
			this.windowExternalCallbackWriter.Received("Buy me a coffee");
		}

		public void LogIssueOrSuggestion()
		{
			//System.Diagnostics.Process.Start("https://github.com/FortuneN/FineCodeCoverage/issues");
			this.windowExternalCallbackWriter.Received("github issues");
		}

		public void RateAndReview()
		{
			this.windowExternalCallbackWriter.Received("rate");
			//System.Diagnostics.Process.Start("https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage&ssr=false#review-details");
		}
	}

}
