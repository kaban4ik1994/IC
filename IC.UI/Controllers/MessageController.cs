using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;

namespace IC.UI.Controllers
{
    [CheckRole("Admin,User")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: Message
        public ActionResult Inbox()
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var model = _messageService.Query(message => message.ToUserId == currentUser.UserId)
                .Select(message => new MessageViewModel
                {
                    Context = message.Context,
                    DispatchDate = message.DispatchDate,
                    From = message.FromUser.Email,
                    MessageId = message.MessageId,
                    Subject = message.Subject,
                    To = message.ToUser.Email
                }).ToList();
            return View(model);
        }

        public ActionResult Sent()
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var model = _messageService.Query(message => message.FromUserId == currentUser.UserId)
                .Select(message => new MessageViewModel
                {
                    Context = message.Context,
                    DispatchDate = message.DispatchDate,
                    From = message.FromUser.Email,
                    MessageId = message.MessageId,
                    Subject = message.Subject,
                    To = message.ToUser.Email
                }).ToList();

            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            if (_messageService.Query(message => message.MessageId == id
                && (currentUser.UserId == message.FromUserId || currentUser.UserId == message.ToUserId)).Select().Count() == 0)
            {
                return RedirectToAction("Index", "Error");
            };
            var model = _messageService.Query(message => message.MessageId == id).Select(message => new MessageViewModel
            {
                Context = message.Context,
                DispatchDate = message.DispatchDate,
                From = message.FromUser.Email,
                MessageId = message.MessageId,
                Subject = message.Subject,
                To = message.ToUser.Email
            }).FirstOrDefault();

            if (model == null)
            {
                return RedirectToAction("Index", "Error");
            }

            return View(model);
        }
    }
}