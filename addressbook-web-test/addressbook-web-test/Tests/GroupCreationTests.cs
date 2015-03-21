using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("Best Group");
            group.Header = "First";
            group.Footer = "Last";

            List<GroupData> OldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(OldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            OldGroups.Add(group);
            OldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(OldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> OldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(OldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            OldGroups.Add(group);
            OldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(OldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> OldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(OldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            OldGroups.Add(group);
            OldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(OldGroups, newGroups);
        }
    }
}
