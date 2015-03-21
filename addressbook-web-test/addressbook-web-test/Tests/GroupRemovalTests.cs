using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupData> OldGroups = app.Groups.GetGroupList();
            GroupData oldData = OldGroups[0];

            app.Groups.Remove(0);

            Assert.AreEqual(OldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            
            OldGroups.RemoveAt(0);
            Assert.AreEqual(OldGroups, newGroups);

            foreach (GroupData group in newGroups) 
            {
                Assert.AreNotEqual(group.Id, oldData.Id);
            }
        } 
    }
}
