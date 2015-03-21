using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactInfo : IEquatable<ContactInfo>, IComparable<ContactInfo>
    {
        public  string allPhones;

        public ContactInfo(string  firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactInfo other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Firstname == other.Firstname;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode(); 
        }

        public override string ToString()
        {
            return "firstname=" + Firstname;
        }

        public int CompareTo(ContactInfo other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname);
        }


        public string Firstname { get;set; }

        public string Middlename { get;set; }

        public string Lastname { get;set; }

        public string Nickname { get;set; }

        public string Company { get;set; }
        
        public string Mobile { get;set; }

        public string Id { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string EmailAddress { get; set; }

        public string AllPhones 
        { 
            get 
            {
                if (allPhones != null) 
                {
                    return allPhones;
                }
                else 
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone)).Trim();
                }
            } 
            set 
            {
                allPhones = value;
            } 
        }

        public string CleanUp(string phone)
            {
                if (phone == null || phone == "") 
                {
                    return "";
                }
                return Regex.Replace(phone, "[ -()]", "");// phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            }
    }
}
