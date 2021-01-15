using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace BrowserControl
{
    class Toaster : IToaster
    {
        private Notifier notifier;
        public Toaster()
        {
            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
        public void ShowError(string message)
        {
            notifier.ShowError(message);
        }

        public void ShowError(string message, MessageOptions displayOptions)
        {
            notifier.ShowError(message, displayOptions);
        }

        public void ShowInformation(string message)
        {
            notifier.ShowInformation(message);
        }

        public void ShowInformation(string message, MessageOptions displayOptions)
        {
            notifier.ShowInformation(message, displayOptions);
        }

        public void ShowSuccess(string message)
        {
            notifier.ShowSuccess(message);
        }

        public void ShowSuccess(string message, MessageOptions displayOptions)
        {
            notifier.ShowSuccess(message, displayOptions);
        }

        public void ShowWarning(string message)
        {
            notifier.ShowWarning(message);
        }

        public void ShowWarning(string message, MessageOptions displayOptions)
        {
            notifier.ShowWarning(message, displayOptions);
        }
    }
}
