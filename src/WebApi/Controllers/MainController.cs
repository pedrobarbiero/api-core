using System;
using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        public MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected virtual bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
