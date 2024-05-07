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
                
                if(value.Length < 8)
                {
                    phoneNumber = "no phone number provided";
                }

               else if (PhoneNumber != value)
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
                if (value == null)
                {
                    email = "Ingen email på denne bruger.";
                    return;
                }

                if (!value.Contains('@'))
                {
                    email = "";
                }

                if (value != email )
                {
                    email = value;
                }
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                if (value < EarliestPossibleEvent)
                {
                    value = DateTime.Today;
                }

                if (date != value)
                {
                    date = value;
                }
            }
        }
        public Membership Membership { get => membership; 
            set 
            { 

                if (value == null || value.MembershipId == null) {
                    value = new Membership(1, "Aktiv", "Basic membership");
                }

                if (membership != value)
                {
                    membership = value;
                }
            } 
        }
    }
}
