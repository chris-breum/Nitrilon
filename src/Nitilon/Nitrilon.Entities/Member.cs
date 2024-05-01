using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Member
    {
        public readonly DateTime EarliestPossibleEvent = new(2018, 1, 1);
        private int memberId;
        private string name;
        private string phoneNumber;
        private string email;
        private DateTime date;
        private Membership membership;



        public Member(int memberId, string name, string phoneNumber, string email, DateTime date, Membership membership)
        {
            MemberId = memberId;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Date = date;
            Membership = membership;
        }

        public int MemberId { get => memberId; set => memberId = value; }
        public string Name
        {
            get => name; set
            {
                ArgumentOutOfRangeException.ThrowIfNullOrWhiteSpace(value);
                if (name != value)
                {
                    name = value;
                }
            }
        }
        public string PhoneNumber { get => phoneNumber; set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value.Length, 8);
                if (PhoneNumber != value)
                {
                    phoneNumber = value;
                }
            }
        }
        public string Email
        {
            get => email;
            set
            {
                
                if (IsValidEmail(value))
                {
                    if (email != value)
                    {
                        email = value;
                    }
                }
                else
                {
                    throw new ArgumentException("Invalid email address");
                }
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                ArgumentOutOfRangeException.ThrowIfLessThan(value, EarliestPossibleEvent);
                if (date != value)
                {
                    date = value;
                }
            }
        }
        public Membership Membership { get => membership; set => membership = value; }



        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Regular expression for email validation
                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
