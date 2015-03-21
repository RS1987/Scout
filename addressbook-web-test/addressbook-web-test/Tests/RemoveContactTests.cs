using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemoveContactTests : AuthTestBase
    {
        [Test]
        public void RemoveContactTest()
        {
            List<ContactInfo> OldContacts = app.Contacts.GetContactInfo();

            app.Contacts.Remove(1);

            Assert.AreEqual(OldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactInfo> newContacts = app.Contacts.GetContactInfo();
            OldContacts.RemoveAt(1);
            OldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(OldContacts, newContacts);
        }
    }
}
