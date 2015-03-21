using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class NewContactCreationTests : AuthTestBase
    {
        [Test]
        public void NewContactCreationTest()
        {
            ContactInfo contact = new ContactInfo("Alex", "Fisher");
            contact.Middlename = "J.";
            contact.Nickname = "Scout";

            List<ContactInfo> OldContacts = app.Contacts.GetContactInfo();

            app.Contacts.CreateContact(contact);

            Assert.AreEqual(OldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactInfo> newContacts = app.Contacts.GetContactInfo();
            OldContacts.Add(contact);
            OldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(OldContacts, newContacts);
        }
    }
}
