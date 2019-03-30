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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//SmokeNote.Client.Controls
namespace SmokeNote.Client.Controls
{
    public class WebBrowserExControl : Canvas
    {
        public System.Windows.Forms.WebBrowser WebBrowser
        {
            get
            {
                return web == null ? null : web.WebBrowser;
            }
        }
        public bool IsShow { get { return show; } }
        private bool show = false;
        private WebViewX web = null;
        private HwndSourceHook hook;
        private HwndSource hwndSource;
        private Window window;
        private const int WM_MOVE = 0x0003;
        private const int WM_MOVING = 0x0216;
        private const int WM_SIZE = 0x0005;
        private const int WM_SIZING = 0x0214;
        private const int WM_ENTERSIZEMOVE = 0x231;
        private const int WM_EXITSIZEMOVE = 0x232;
        private const int WM_PAINT = 0x000F;
        private const int WM_GETMINMAXINFO = 0x0024;//此消息发送给窗口当它将要改变大小或位置
        private const int WM_WINDOWPOSCHANGING = 0x0046;//发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
        private const int WM_NCCALCSIZE = 0x0083;//当某个窗口的客户区域必须被核算时发送此消息
        private const int WM_WINDOWPOSCHANGED = 0x0047;//发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                //case WM_MOVE:
                //case WM_MOVING:
                //case WM_SIZE:
                //case WM_SIZING:
                //case WM_ENTERSIZEMOVE:
                //case WM_EXITSIZEMOVE:
                //case WM_GETMINMAXINFO:
                //case WM_WINDOWPOSCHANGING:
                //case WM_NCCALCSIZE:
                //case WM_WINDOWPOSCHANGED:
                case 49849: //case 0xc2b9: 这是一个很神奇的消息，可能是WPF专用               
                case WM_PAINT:
                    render_web();
                    break;
                default:
                    Console.WriteLine("");
                    break;
            }
            //Console.WriteLine(msg);
            return IntPtr.Zero;
        }
        public WebBrowserExControl()
        {
          
            //this.Text = "WebBrowserExControl";
        }
        ~WebBrowserExControl()
        {
            Hide();
        }
        public void Show()
        {
            if (!show)
            {
                web = new WebViewX();
                window = Window.GetWindow(this);
                hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                if (hwndSource != null)
                {
                    hook = new HwndSourceHook(WndProc);
                    hwndSource.AddHook(hook);
                }
                web.Owner = window;
                web.Show();
                show = true;
                render_web();
            }
        }
        public void Hide()
        {
            if (hwndSource != null)
            {
                hwndSource.RemoveHook(hook);
            }
            if (web != null)
            {
                //try
                //{
                web.WebBrowser.Dispose();
                web.Close();
                //}
                //catch { }
                web = null;
            }
            show = false;
        }
        private void render_web()
        {
            if (show)
            {
                Point point = this.TransformToAncestor(window).Transform(new Point(0, 0));
                //point = this.PointToScreen(point);
                web.Left = window.Left + point.X;// -LeftEx;
                web.Top = window.Top + point.Y;// -TopEx;
                //point = this.PointToScreen(point);
                //web.Left = point.X;// -LeftEx;
                //web.Top = point.Y;// -TopEx;
                web.Width = this.ActualWidth;
                web.Height = this.ActualHeight;
            }
        }
    }
}