using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace AuthSystem.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Weather()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //instantiate a new MimeMessage
                    var message = new MimeMessage();
                    //Setting the To e-mail address
                    message.To.Add(new MailboxAddress("E-mail Recipient Name", "krzysiek1998@gazeta.pl"));
                    //Setting the From e-mail address
                    message.From.Add(new MailboxAddress(contactViewModel.Email, contactViewModel.Email));
                    //E-mail subject 
                    message.Subject = contactViewModel.Subject;
                    //E-mail message body
                    message.Body = new TextPart(TextFormat.Html)
                    {
                        Text = contactViewModel.Message + "\n Wiadomość wysłana od: " + contactViewModel.Name + "\n E-mail: " + contactViewModel.Email
                    };

                    //Configure the e-mail
                    using (var emailClient = new SmtpClient())
                    {
                        emailClient.Connect("smtp.gmail.com", 587, false);
                        emailClient.Authenticate("choppex1998@gmail.com", "Choppex1");
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
