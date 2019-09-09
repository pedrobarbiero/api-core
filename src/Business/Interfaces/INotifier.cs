using System.Collections.ObjectModel;
using Business.Notifications;

namespace Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        ReadOnlyCollection<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
