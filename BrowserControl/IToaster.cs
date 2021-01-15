using ToastNotifications.Core;

namespace BrowserControl
{
    public interface IToaster
    {
        void ShowError(string message);
        void ShowError(string message, MessageOptions displayOptions);

        void ShowInformation(string message);
        void ShowInformation(string message, MessageOptions displayOptions);

        void ShowSuccess(string message);
        void ShowSuccess(string message, MessageOptions displayOptions);

        void ShowWarning(string message);
        void ShowWarning(string message, MessageOptions displayOptions);

    }
}
