﻿using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Models.Emails;

namespace ShopTARge23.Controllers
{
    public class EmailsController : Controller
    {
        private readonly  IEmailServices _emailServices;
        public EmailsController(IEmailServices emailServices)
        { 
            _emailServices = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        //meetod
        [HttpPost]

        public IActionResult SendEmail(EmailViewModel vm)
        {
            var dto = new EmailDto()
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
                Attachments = vm.Attachments
                

            };

            _emailServices.SendEmail(dto);

            return RedirectToAction(nameof(Index));
        }



    }
}
