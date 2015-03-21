using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ModifyContactTests : AuthTestBase
    {
        [Test]
        public void ModifyContactTest()
        {
            ContactInfo NewContactData = new ContactInfo("Al", "Fik");
            NewContactData.Middlename = "K.";
            NewContactData.Nickname = "BigD";
            NewContactData.Company = "Galaxy+";
            NewContactData.Mobile = "+380567483421";

            List<ContactInfo> OldContacts = app.Contacts.GetContactInfo();

            app.Contacts.ModifyContact(1, NewContactData);

            Assert.AreEqual(OldContacts.Count, app.Contacts.GetContactCount());

            List<ContactInfo> newContacts = app.Contacts.GetContactInfo();
            OldContacts[1].Firstname = NewContactData.Firstname;
            OldContacts[1].Lastname = NewContactData.Lastname;
            OldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(OldContacts, newContacts);
        }
    }
}
