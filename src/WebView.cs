namespace WebViewActiveX
{
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.Json;
    using System.Windows.Forms;
    using Microsoft.Win32;


    // regasm. /codebase /tlb WebViewActiveX.dll
    //  /codebase /tlb bin\Debug\WebViewActiveX.dll
    // C:\Windows\WinSxS\wow64_regasm_b03f5f7f11d50a3a_4.0.15805.0_none_9be7d950c1f8addd\RegAsm.exe
    // C:\Windows\WinSxS\amd64_regasm_b03f5f7f11d50a3a_4.0.15840.3_none_721495e700b292c6\RegAsm.exe /codebase /tlb bin\Debug\WebViewActiveX.dll
    // C:\Windows\WinSxS\wow64_regasm_b03f5f7f11d50a3a_4.0.15805.0_none_9be7d950c1f8addd\RegAsm.exe /codebase /tlb bin\Debug\WebViewActiveX.dll


    [ProgId("WebViewActiveX")]
    [Guid("8C7F5D39-0997-4804-8853-AF1BAA3574B7")]
    [ComVisible(true)]
    // [ClassInterface(ClassInterfaceType.AutoDual)]
    // [ComSourceInterfaces(typeof(ControlEvents))]
    public partial class WebView : UserControl //, ControlEvents
    {
        public delegate void ControlEventHandler(string returnInfo);


        public WebView()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.ResizeRedraw, true);
            
            InitializeComponent();

            WebViewControl.NavigationCompleted += WebViewControl_NavigationCompleted;
            WebViewControl.NavigationStarting += WebViewControl_NavigationStarting;
            WebViewControl.WebMessageReceived += WebViewControl_WebMessageReceived;
        }
        
        [ComVisible(true)]
        public string Source
        {
            get => this.WebViewControl.Source.ToString();
            set => this.WebViewControl.Source = new System.Uri(value);
        }

        public event ControlEventHandler ReturnInfo;

        public void OnReturnInfo(string returnInfo)
        {
            ReturnInfo?.Invoke(returnInfo);
        }

        private void WebViewControl_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            var returnInfo = Json(new
            {
                Event = "NavigationStarting",
                Args = e
            });

            OnReturnInfo(returnInfo);
        }

        private void WebViewControl_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            var returnInfo = Json(new
            {
                Event = "NavigationCompleted", 
                Args = e
            });

            OnReturnInfo(returnInfo);
        }

        private void WebViewControl_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var returnInfo = Json(new
            {
                Event = "WebMessageReceived",
                Args = e,
                e.WebMessageAsJson
            });

            OnReturnInfo(returnInfo);
        }

        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open the CLSID\{guid} key for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create the 'Control' key - this allows it to show up in 
            // the ActiveX control container 
            RegistryKey ctrl = k?.CreateSubKey("Control");
            ctrl?.Close();

            // Next create the CodeBase entry - needed if not string named and GACced.
            RegistryKey inprocServer32 = k?.OpenSubKey("InprocServer32", true);
            inprocServer32?.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32?.Close();

            // Finally close the main key
            k?.Close();
        }

        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open HKCR\CLSID\{guid} for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Delete the 'Control' key, but don't throw an exception if it does not exist
            k?.DeleteSubKey("Control", false);

            // Next open up InprocServer32
            RegistryKey inprocServer32 = k?.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key, again not throwing if missing 
            //k?.DeleteSubKey("CodeBase", false);
            inprocServer32?.DeleteSubKey("CodeBase", false);

            // Finally close the main key 
            k?.Close();
        }

        #region Json
        private static string Json(object value)
        {
            var result = JsonSerializer.Serialize(value);
            return result;
        }
        #endregion
    }
}
