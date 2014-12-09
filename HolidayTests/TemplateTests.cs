﻿using Holiday;
using NUnit.Framework;

namespace HolidayTests
{
    
    [TestFixture]
    public class TemplateTests
    {
        [Test]
        public void Template_has_subject_and_body()
        {
            Template t = new Template();
            Assert.IsNotNull(t.Subject);
            Assert.IsNotNull(t.Body);
        }

        [Test]
        public void Template_has_parameters()
        {
            const string name = "EmployeeName";
            const string value = "Csaba Trucza";

            Template t = new Template();
            t.SetParameter(name, value);

            var actual = t.GetParameter(name);

            Assert.AreEqual(value, actual);
        }

        [Test]
        public void Template_replaces_placeholders_with_parameters()
        {
            Template t = new Template();
            t.SetParameter("EmployeeName", "Csaba Trucza");
            t.BodyTemplate = "Hello {EmployeeName}!";

            Assert.AreEqual("Hello Csaba Trucza!", t.Body);
        }
    }
}