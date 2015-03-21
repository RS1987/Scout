using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
     [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
         [Test]
         public void GroupModificationTest()
         {
             GroupData newData = new GroupData("Crazy Group");
             newData.Header = null;
             newData.Footer = null;

             List<GroupData> OldGroups = app.Groups.GetGroupList();
             GroupData oldData = OldGroups[0];

             app.Groups.Modify(0, newData);

             Assert.AreEqual(OldGroups.Count, app.Groups.GetGroupCount());

             List<GroupData> newGroups = app.Groups.GetGroupList();
             OldGroups[0].Name = newData.Name;
             OldGroups.Sort();
             newGroups.Sort();
             Assert.AreEqual(OldGroups, newGroups);

             foreach (GroupData group in newGroups) 
             {
                 if (group.Id == oldData.Id) 
                 {
                     Assert.AreEqual(newData.Name, group.Name);
                 }
             }

         }
    }
}
