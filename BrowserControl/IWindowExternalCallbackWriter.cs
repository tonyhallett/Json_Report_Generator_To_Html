namespace BrowserControl
{
    public interface IWindowExternalCallbackWriter
    {
        void Received(string message);
        void ReceivedError(string message);
    }

    
}
