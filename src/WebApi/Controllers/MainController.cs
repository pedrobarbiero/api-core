using System;
using System.Linq;
using Business.Interfaces;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Controllers
{

    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        private readonly IUser _appUser;

        public MainController(INotifier notifier, IUser appUser)
        {
            _notifier = notifier;
            _appUser = appUser;
        }

        protected bool IsAuthenticated => _appUser.IsAuthenticated;
        protected Guid UserId => _appUser.UserID;

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModelState(modelState);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
                return Ok(new
                {
                    success = true,
                    data = result
                });

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected void NotifyInvalidModelState(ModelStateDictionary modelState)
        {
            var errors = ModelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected void NotifyError(string errorMessage)
        {
            _notifier.Handle(new Notification(errorMessage));
        }

        protected virtual bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }
    }
}
