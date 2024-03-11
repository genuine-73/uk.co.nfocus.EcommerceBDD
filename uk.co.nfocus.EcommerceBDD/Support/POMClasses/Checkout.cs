using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
using NUnit.Framework;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{

    // Page Object Model class for Checkout 
    internal class Checkout
    {
        private IWebDriver _driver;

        //Creates an instance of the Checkout class
        //Initialises the driver variable when constructed
        public Checkout(IWebDriver driver)
        {
            _driver = driver;
            Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/checkout/"));
        }

        // Locators for finding Elements to fill out the Billing information
        private IWebElement _firstName => _driver.FindElement(By.Id("billing_first_name")); 
        private IWebElement _secondName => _driver.FindElement(By.Id("billing_last_name"));
        private IWebElement _address => _driver.FindElement(By.Id("billing_address_1"));
        private IWebElement _city => _driver.FindElement(By.Id("billing_city"));
        private IWebElement _postcode => _driver.FindElement(By.Id("billing_postcode"));
        private IWebElement _phoneNo => _driver.FindElement(By.Id("billing_phone"));
        private IWebElement _placeOrder => _driver.FindElement(By.Id("place_order"));
        private IWebElement _country => _driver.FindElement(By.Id("select2-billing_country-container"));
        private IWebElement _checkPayments => _driver.FindElement(By.Id("payment_method_cheque"));
        private IWebElement _cashOnDelivery => _driver.FindElement(By.Id("payment_method_cod"));
        
        //Service Methods
        //Getters and Setters for all of the billing details fields
        public string FirstName
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_first_name"));
                return _firstName.GetAttribute("value");
            }
            set
            {
                _firstName.Clear(); 
                _firstName.SendKeys(value);
            }
        }

        public string SecondName
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_last_name"));
                return _secondName.GetAttribute("value");
            }
            set
            {
                _secondName.Clear();
                _secondName.SendKeys(value);
            }
        }

        public string Address
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_address_1"));
                return _address.GetAttribute("value");
            }
            set
            {
                _address.Clear();
                _address.SendKeys(value);
            }
        }

        public string City
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_city"));
                return _city.GetAttribute("value");
            }
            set
            {
                _city.Clear();
                _city.SendKeys(value);
            }
        }

        public string Postcode
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_postcode"));
                return _postcode.GetAttribute("value");
            }
            set
            {
                _postcode.Clear();
                _postcode.SendKeys(value);
            }
        }

        public string PhoneNo
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("billing_phone"));
                return _phoneNo.GetAttribute("value");
            }
            set
            {
                _phoneNo.Clear();
                _phoneNo.SendKeys(value);
            }
        }



        //Service Methods
        //Fills the billing details needed to check payment and place an order

        //Sets the first name
        public Checkout SetFirstName(string firstname)
        {
            FirstName = firstname;
            return this;
        }
        //Sets the surname
        public Checkout SetSecondName(string secondName)
        {
            SecondName = secondName;
            return this;
        }
        //Sets the address
        public Checkout SetAddress(string address)
        {
            Address = address;
            return this;
        }
        //Sets the city
        public Checkout SetCity(string city)
        {
            City = city;
            return this;
        }

        //Sets the Postcode

        public Checkout SetPostCode(string postcode)
        {
            Postcode = postcode;
            return this;
        }

        //Sets the Phone number
        public Checkout SetPhoneNo(string phone)
        {
            PhoneNo = phone;
            return this;
        }

        //Chooses the country from the dropdown list
        public Checkout SetCountry(string country)
        {
            StaticWaitForElement(_driver, By.Id("billing_country"));
            SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("billing_country")));
            dropdown.SelectByText(country);
            return this;
        }
        
        //Clicks Place Order button to complete transaction
        public void ClickPlaceOrder()
        {

            try
            {
                //try to search for element and click it when found
                StaticWaitForElement(_driver, By.Id("place_order"), 7);
                _placeOrder.Click();
            }
            catch (Exception)
            {
                ClickElementInView(_driver, _placeOrder);
            }

        }

        //Checks if the Check Payment Radio Button has been selected
        public void CheckPayment()
        {
            try
            {
                StaticWaitForElement(_driver, By.CssSelector("li.wc_payment_method:nth-child(1)"));
                _checkPayments.Click();
            }
            catch
            {
                ScrollElementIntoView(_driver, _checkPayments);
                ClickElementInView(_driver, _checkPayments);
               
            }
        }

    }
}