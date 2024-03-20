using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
using NUnit.Framework;
using System.Diagnostics.Metrics;

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
            try
            {
                Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/checkout/"));
            }
            catch
            {
                TakeFullPageScreenshot(_driver, "Wrong_URL");
                Console.WriteLine("Cannot proceed to checkout because cart is empty");
            }
        }

        // Locators for finding Elements to fill out the Billing information
        private IWebElement _firstName => StaticWaitForElement(_driver, By.Id("billing_first_name")); 
        private IWebElement _secondName => StaticWaitForElement(_driver, By.Id("billing_last_name"));
        private IWebElement _address => StaticWaitForElement(_driver, By.Id("billing_address_1"));
        private IWebElement _city => StaticWaitForElement(_driver, By.Id("billing_city"));
        private IWebElement _postcode => StaticWaitForElement(_driver, By.Id("billing_postcode"));
        private IWebElement _phoneNo => StaticWaitForElement(_driver, By.Id("billing_phone"));
        private IWebElement _placeOrder => StaticWaitForElement(_driver, By.Id("place_order"));
        private IWebElement _country => StaticWaitForElement(_driver, By.Id("select2-billing_country-container"));
        private IWebElement _checkPayments => WaitForElement(_driver, By.CssSelector("li.wc_payment_method.payment_method_cheque"));
        private IWebElement _cashOnDelivery => WaitForElement(_driver, By.CssSelector("li.wc_payment_method.payment_method_cod"));
        //Service Methods
        //Getters and Setters for all of the billing details fields
        public string FirstName
        {
            get
            {
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
                return _phoneNo.GetAttribute("value");
            }
            set
            {
                _phoneNo.Clear();
                _phoneNo.SendKeys(value);
            }
        }

        public string Country
        {
            get
            {
                return _country.Text;
            }

            set
            {
                SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("billing_country")));
                dropdown.SelectByText(value);
            }
        }


        //Clicks Place Order button to complete transactions
        public void ClickPlaceOrder()
        {

            try
            {
                _placeOrder.Click();
            }
            catch (Exception)
            {
                //Do nothing
            }

        }

        //Checks if the Check Payment Radio Button has been selected
        public void CheckPayment()
        {
            try
            {
               _cashOnDelivery.Click();
            }
            catch
            {

                //Do nothing

            }
        }

    }
}