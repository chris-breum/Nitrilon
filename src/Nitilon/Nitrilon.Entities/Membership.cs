using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Membership
    {
        private int membershipId;
        private string membershipType;
        private string description;

        public Membership(int membershipId, string membershipType, string description)
        {
            MembershipId = membershipId;
            MembershipType = membershipType;
            Description = description;
        }

        public int MembershipId { get => membershipId; set => membershipId = value; }
        public string MembershipType
        {
            get => membershipType; 
            
            set {
                if (value == null)
                {
                    value = "Ikke sat";
                }
                membershipType = value;
            }
        }
        public string Description { get => description; 
            
            set
            {
                if (value == null)
                {
                    value = "Ikke sat";
                }
                description = value;
            }
        }
    }
}
