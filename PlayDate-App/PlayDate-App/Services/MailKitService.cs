using MailKit.Net.Smtp;
using MimeKit;
using PlayDate_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDate_App.Services
{
    public class MailKitService
    {
    }

    public void SendCustomEmail(Parent parent, string subject, string body)
    {
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("PlayDateApp", APIKeys.ServerEmailAddress));
        message.To.Add(new MailboxAddress(parent.FirstName, parent.EmailAddress));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body 
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 465, true);
            //pls no hacko
            client.Authenticate(APIKeys.ServerEmailAddress, APIKeys.SeverEmailPassword);
            //pls dont hack
            client.Send(message);
            client.Disconnect(true);
        }

        //need await
    }

 

}
