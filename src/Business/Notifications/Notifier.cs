using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Business.Interfaces;

namespace Business.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications;
        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public ReadOnlyCollection<Notification> GetNotifications()
        {
            return new ReadOnlyCollection<Notification>(_notifications);
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
