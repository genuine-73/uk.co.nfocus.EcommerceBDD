using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceBDD.Support.POCOs
{
    //Represents the share data to be injected in checkout billing details
    internal class BillingDetailsPOCO
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string PhoneNo { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
