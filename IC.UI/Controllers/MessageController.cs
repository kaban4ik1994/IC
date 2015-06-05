using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IC.Entities.Models;
using IC.Services.Interfaces;
using IC.UI.Filters.AuthorizationFilters;
using IC.UI.Helpers;
using IC.UI.Models;
using Repository.Pattern.UnitOfWork;

namespace IC.UI.Controllers
{
    [CheckRole("Admin,User")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUnitOfWorkAsync unitOfWork, IUserService userService)
        {
            _messageService = messageService;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        // GET: Message
        public ActionResult Inbox()
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var model = _messageService.Query(message => message.ToUserId == currentUser.UserId && message.ShowForSecondUser)
                .Select(message => new MessageViewModel
                {
                    Context = message.Context,
                    DispatchDate = message.DispatchDate,
                    From = message.FromUser.Email,
                    MessageId = message.MessageId,
                    Subject = message.Subject,
                    To = message.ToUser.Email,
                    IsNew = !message.IsViewed
                }).ToList();
            return View(model);
        }

        public ActionResult Sent()
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var model = _messageService.Query(message => message.FromUserId == currentUser.UserId && message.ShowForFirstUser)
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
            }

            var updateMessage = _messageService.Find(id);

            if (!updateMessage.IsViewed)
            {
                updateMessage.IsViewed = true;
                _messageService.Update(updateMessage);
                _unitOfWork.SaveChanges();
            }

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

        public ActionResult Reply(long id)
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            if (_messageService.Query(message => message.MessageId == id
                && (currentUser.UserId == message.FromUserId || currentUser.UserId == message.ToUserId)).Select().Count() == 0)
            {
                return RedirectToAction("Index", "Error");
            }

            var model = _messageService.Query(message => message.MessageId == id).Select(message => new MessageViewModel
            {
                From = currentUser.Email,
                MessageId = message.MessageId,
                Subject = "RE:" + message.Subject,
                To = message.FromUser.Email,
                FromId = currentUser.UserId,
                ToId = message.FromUserId,
            }).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult Reply(MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error");
            }

            var entity = new Message
            {
                Context = model.Context,
                DispatchDate = DateTime.Now,
                ToUserId = model.ToId,
                FromUserId = AuthHelper.GetUser(HttpContext).UserId,
                Subject = model.Subject,
                IsViewed = false,
                ShowForFirstUser = true,
                ShowForSecondUser = true
            };

            _messageService.Insert(entity);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Sent");
        }

        public ActionResult Create()
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var model = new MessageViewModel
            {
                FromId = currentUser.UserId,
                From = currentUser.Email,
            };
            if (!AuthHelper.IsAdministrator(HttpContext))
            {
                var admin = _userService.GetAdministrator();
                model.ToId = admin.UserId;
                model.To = admin.Email;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Error");
            }

            var entity = new Message
            {
                Context = model.Context,
                DispatchDate = DateTime.Now,
                FromUserId = AuthHelper.GetUser(HttpContext).UserId,
                Subject = model.Subject,
                IsViewed = false,
                ShowForFirstUser = true,
                ShowForSecondUser = true
            };

            if (!AuthHelper.IsAdministrator(HttpContext))
            {
                entity.ToUserId = _userService.GetAdministrator().UserId;
            }

            else
            {
                entity.ToUserId = _userService.GetUserByEmail(model.To).UserId;
            }

            _messageService.Insert(entity);
            _unitOfWork.SaveChanges();

            return RedirectToAction("Sent");
        }

        public ActionResult Delete(long id)
        {
            var currentUser = AuthHelper.GetUser(HttpContext);
            var message = _messageService.Find(id);
            var isInbox = false;
            if (message.FromUserId == currentUser.UserId)
            {
                message.ShowForFirstUser = false;
            }
            else if (message.ToUserId == currentUser.UserId)
            {
                message.ShowForSecondUser = false;
                isInbox = true;
            }
            _messageService.Update(message);
            _unitOfWork.SaveChanges();
            return isInbox ? RedirectToAction("Inbox") : RedirectToAction("Sent");
        }
    }
}