using MimeKit;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;


namespace ShopTARge23.ApplicationServices.Services
{
    public class EmailServices : IEmailServices
    {

        private readonly IConfiguration _config;
        public EmailServices ( IConfiguration config )
        {
            _config = config;
        }
        

        public void SendEmail(EmailDto dto)
        {
            var bodyBuilder = new BodyBuilder
            {
                TextBody = dto.Body,
            };
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
                email.To.Add(MailboxAddress.Parse(dto.To));
                email.Subject = dto.Subject;

            email.Body = bodyBuilder.ToMessageBody();

            if (dto.Attachments != null)
            {
                foreach (var file in dto.Attachments)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        bodyBuilder.Attachments.Add(file.FileName, memoryStream.ToArray());
                    }
                }
            }
            email.Body = bodyBuilder.ToMessageBody();
            //  email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
               // {
                //    Text = dto.Body
               // };
                // mailkit.net.smtp



                using var smtpClient = new SmtpClient();
            // tuleb valida õige port ja kasutada sourceresocketoptionit
            //autentita
            //saada email
            //vabastada resurss


            using var smtp = new SmtpClient();
            { 
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }


        }
    }
}
