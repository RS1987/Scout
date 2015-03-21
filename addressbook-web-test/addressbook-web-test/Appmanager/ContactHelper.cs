using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
         public ContactHelper(ApplicationManager manager) : base(manager) { }

         public ContactHelper CreateContact(ContactInfo contact) 
         {
             manager.Navigator.GoToAddNewContactsPage();
             FillContactInfo(contact);
             SubmitContactCreation();
             manager.Navigator.ReturnToHomePage();
             return this;
         }
         public ContactHelper ModifyContact(int p, ContactInfo NewContactData)
         {
             if (IsElementPresent(By.CssSelector("img[alt=\"Edit\"]")))
             {
                 SelectContact(p);
                 InitContactModifying(0);
                 FillContactInfo(NewContactData);
                 SubmitContactModification();
                 manager.Navigator.ReturnToHomePage();
                 return this;
             }
             else
             {
                 manager.Navigator.GoToAddNewContactsPage();
                 SubmitContactCreation();
                 manager.Navigator.ReturnToHomePage();
                 SelectContact(p);
                 InitContactModifying(0);
                 FillContactInfo(NewContactData);
                 SubmitContactModification();
                 manager.Navigator.ReturnToHomePage();
                 return this;
             }
         }
         public ContactHelper Remove(int p)
         {
             if (IsElementPresent(By.CssSelector("img[alt=\"Edit\"]")))
             {
                 SelectContact(p);
                 InitContactModifying(0);
                 RemoveContact();
                 manager.Navigator.ReturnToHomePage();
                 return this;
             }
             else
             {
                 manager.Navigator.GoToAddNewContactsPage();
                 SubmitContactCreation();
                 manager.Navigator.ReturnToHomePage();
                 SelectContact(p);
                 InitContactModifying(0);
                 RemoveContact();
                 manager.Navigator.ReturnToHomePage();
                 return this;
             }
         }

         public ContactHelper InitContactModifying(int index)
         {
             driver.FindElements(By.Name("entry"))[index]
                 .FindElements(By.TagName("td"))[7]
                 .FindElement(By.TagName("a")).Click();
             return this;
         }

         public ContactHelper SubmitContactModification()
         {
             driver.FindElement(By.Name("update")).Click();
             contactCache = null;
             return this;
         }

         public ContactHelper SubmitContactCreation()
         {
             driver.FindElement(By.Name("submit")).Click();
             contactCache = null;
             return this;
         }

         public ContactHelper FillContactInfo(ContactInfo contact)
         {
             Type(By.Name("firstname"), contact.Firstname);
             Type(By.Name("middlename"), contact.Middlename);
             Type(By.Name("lastname"), contact.Lastname);
             Type(By.Name("nickname"), contact.Nickname);
             Type(By.Name("company"), contact.Company);
             Type(By.Name("mobile"), contact.Mobile);
             return this;
         }
         public ContactHelper SelectContact(int index)
         {
             driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
             return this;
         }
         public ContactHelper RemoveContact()
         {
             driver.FindElement(By.XPath("(//input[@name='update'])[3]")).Click();
             contactCache = null;
             return this;
         }

         private List<ContactInfo> contactCache = null;

         public List<ContactInfo> GetContactInfo()
         {
             if (contactCache == null)
             {
                 contactCache = new List<ContactInfo>();
                 manager.Navigator.GoToHomePage();
                 ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                 foreach (IWebElement element in elements)
                 {
                     contactCache.Add(new ContactInfo(element.FindElement(By.XPath("td[3]")).Text, element.FindElement(By.XPath("td[2]")).Text)
                     {
                         Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                     });
                     //contactCache.Add(new ContactInfo(element.FindElement(By.XPath("td[3]")).Text, element.FindElement(By.XPath("td[2]")).Text));
                 }
             }
             return new List<ContactInfo>(contactCache);
         }

         public int GetContactCount()
         {
             return driver.FindElements(By.Name("entry")).Count;
         }

         public ContactInfo GetContactInformationFromEditForm(int index)
         {
             manager.Navigator.GoToHomePage();
             InitContactModifying(0);
             string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
             string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
             string address = driver.FindElement(By.Name("address")).GetAttribute("value");

             string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
             string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
             string emailAddress = driver.FindElement(By.Name("email")).GetAttribute("value");

             return new ContactInfo(firstName, lastName)
             {
                 Address = address,
                 HomePhone = homePhone,
                 MobilePhone = mobilePhone,
                 EmailAddress = emailAddress
             };
         }

         public ContactInfo GetContactInformationFromTable(int index)
         {
             manager.Navigator.GoToHomePage();
             IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                 .FindElements(By.TagName("td"));
             string lastName = cells[1].Text;
             string firstName = cells[2].Text;
             string address = cells[3].Text;
             string emailAddress = cells[4].Text;
             string allPhones = cells[5].Text;

             return new ContactInfo(firstName, lastName)
             {
                 Address = address,
                 EmailAddress = emailAddress,
                 AllPhones = allPhones
             };
         }

         public int GetNumberOfSearchResults() 
         {
             manager.Navigator.GoToHomePage();
             string text = driver.FindElement(By.Id("search_count")).Text;
             //Match m = new Regex(@"\d+").Match(text);
             return Int32.Parse(text);
         }
    }
}
