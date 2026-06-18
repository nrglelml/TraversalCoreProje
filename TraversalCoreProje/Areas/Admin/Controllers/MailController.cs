using DTOLayer.DTOs.MailDTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;


namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequestDTOs mailRequest)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "yourmailadres");

            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", mailRequest.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequest.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            mimeMessage.Subject = mailRequest.Subject;

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("yourmailadres", "yourmailapppassword");
            client.Send(mimeMessage);
            client.Disconnect(true);
            return View();
        }
    }
}
