namespace WebViewActiveX
{
    public interface AxWebView
    {
        string Source { get; set; }

        void OnReturnInfo(string returnInfo);
    }
}