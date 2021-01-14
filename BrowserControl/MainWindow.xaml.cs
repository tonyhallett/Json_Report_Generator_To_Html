using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserControl
{
    public interface IWindowExternalCallbackWriter
    {
        void Received(string mesage);
    }

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindowExternalCallbackWriter
    {
        
        public MainWindow()
        {
            var solnDir = new DirectoryInfo(Assembly.GetExecutingAssembly().Location).Parent.Parent.Parent.Parent;
            var distFile = System.IO.Path.Combine(solnDir.FullName, @"Report\dist\index.html");
            InitializeComponent();
            
            webBrowser.ObjectForScripting = new ScriptManager(this);
            webBrowser.Navigate(distFile);
        }

        public void Received(string message)
        {
            txtWindowExternal.Text = txtWindowExternal.Text + Environment.NewLine + message;
        }
    }
}
