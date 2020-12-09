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
                client.Authenticate(APIKeys.ServerEmailAddress, APIKeys.ServerEmailPassword);
                //pls dont hack
                client.Send(message);
                client.Disconnect(true);
            }

            //need await
        }

        public void FriendRequestEmail(Parent parentRequester, Parent parentRequestee)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("PlayDateApp", APIKeys.ServerEmailAddress));
            message.To.Add(new MailboxAddress(parentRequestee.FirstName, parentRequestee.EmailAddress));
            message.Subject = $"PlayDateApp - New Friend Request from the {parentRequester.LastName} family";
            //body subject to change based upon performance & requirements - first attempt includes URL to app to unmade FriendRequests screen
            message.Body = new TextPart("html")
            {
                Text = $"<h3>New Friend Request<h3><p>The {parentRequester.LastName} family would like to be friends on the PlayDateApp.</p><p><a href='https://localhost:44398/Parent/FriendRequests'>Please click here to be view your friend requests to respond to this request</a></p>"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(APIKeys.ServerEmailAddress, APIKeys.ServerEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }

            //need await
        }


    }

}
