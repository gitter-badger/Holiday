﻿using System;
using NUnit.Framework;

namespace HolidayTests
{
    [TestFixture]
    public class HolidayTests
    {
        private HolidayRequest request;
        private const string employee = "csaba.trucza@iquestgroup.com";
        private const string manager = "andrei.doibani@iquestgroup.com";

        [Test]
        public void usage()
        {
            CreateHolidayRequest();
            request.Approve();
        }

        [Test]
        public void submitted_request_sends_mail()
        {
            CreateHolidayRequest();

            Assert.AreEqual(employee, MailServer.GetLastSentMail().From);
            Assert.AreEqual(manager, MailServer.GetLastSentMail().To);
        }

        [Test]
        public void approved_request_sends_mail()
        {
            CreateHolidayRequest();

            request.Approve();

            Assert.AreEqual(manager, MailServer.GetLastSentMail().From );
            Assert.AreEqual("hr", MailServer.GetLastSentMail().To);
        }

        private void CreateHolidayRequest()
        {
            request = new HolidayRequest(
                employee,
                manager,
                new DateTime(2014, 11, 11),
                new DateTime(2014, 11, 12),
                "vacation");            
        }
    }

    public static class MailServer
    {
        private static Message lastSentMessage;

        public static Message GetLastSentMail()
        {
            return lastSentMessage;
        }

        public static void Send(Message message)
        {
            lastSentMessage = message;
        }
    }

    public class Message
    {
        public string From;
        public string To;

        public void Send()
        {
            MailServer.Send(this);
        }
    }

    public class HolidayRequest
    {
        private readonly string employee;
        private readonly string manager;

        public HolidayRequest(string employee, string manager, DateTime start, DateTime end, string type)
        {
            this.employee = employee;
            this.manager = manager;
            Submit();
        }

        private void Submit()
        {
            SendMessage(employee, manager);
        }

        public void Approve()
        {
            SendMessage(manager, "hr");
        }

        private static void SendMessage(string from, string to)
        {
            Message message = new Message {From = @from, To = to};
            message.Send();
        }
    }
}
