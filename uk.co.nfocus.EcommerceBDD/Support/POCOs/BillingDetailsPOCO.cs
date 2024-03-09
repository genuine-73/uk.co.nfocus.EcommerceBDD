using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceBDD.Support.POCOs
{
    internal class BillingDetailsPOCO
    {
        public string postcode { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string lastname { get; set; }

    }
}
