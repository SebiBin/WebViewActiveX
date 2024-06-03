namespace WebViewActiveX
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// This interface shows events to javascript
    /// </summary>
    [Guid("E717CE8A-42C9-4AD0-A3C7-AAB23953CF22")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    // [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ControlEvents
    {
        // Add a DispIdAttribute to any members in the source interface
        // to specify the COM DispId.
        [DispId(1)]
        void OnReturnInfo(string returnInfo);
    }
}
