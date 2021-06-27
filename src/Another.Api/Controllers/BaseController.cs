using Another.Business.Interfaces;
using Another.Business.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace Another.Api.Controllers
{

    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public abstract class BaseController : ControllerBase
    {
        private readonly INotificator _notificator;

        public BaseController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyErrorsModelState(modelState);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (isValid())
            {
                return Ok(new
                {
                    Success = true,
                    Data = result
                });
            }

            return BadRequest(new
            {
                Success = false,
                Errors = _notificator.GetNotifications().Select(n => n.Message)
            });
        }

        protected bool isValid()
        {
            return !_notificator.HasNotification();
        }

        protected void NotifyErrorsModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var item in errors)
            {
                var errorMsg = item.Exception == null ? item.ErrorMessage : item.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string message)
        {
            _notificator.Handle(new Notification(message));
        }
    }
}
