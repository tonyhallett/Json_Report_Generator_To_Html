using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var webBrowserWrapper = new WebBrowserWrapper(webBrowser);
            var mainViewModel = new MainViewModel(webBrowserWrapper, webBrowserWrapper, new HtmlProvider(), new ScriptManager(), new Generator(AssemblyHelper.ExecutingDirectory(), 3000), new Toaster());
            this.DataContext = mainViewModel;
        }
        
    }
    
}
