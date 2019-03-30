using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmokeNote.Client.Controls
{
    /// <summary>
    /// WebViewX.xaml 的交互逻辑
    /// </summary>
    public partial class WebViewX : Window
    {
        public System.Windows.Forms.WebBrowser WebBrowser { get { return web_browser; } }
        private System.Windows.Forms.Integration.WindowsFormsHost forms_host;
        private System.Windows.Forms.WebBrowser web_browser;
        public WebViewX()
        {
            InitializeComponent();
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.ShowInTaskbar = false;
            forms_host = new System.Windows.Forms.Integration.WindowsFormsHost();
            web_browser = new System.Windows.Forms.WebBrowser();
            forms_host.Child = web_browser;
            this.grid.Children.Add(forms_host);
          
        }
    }
}
