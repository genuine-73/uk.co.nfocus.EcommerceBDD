using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    //Page Object Model for OrderPage in MyAccount
    //Inherits the methods of the Account Navigation Bar for code duplicity
    internal class LatestOrderPOM : AccountNavBar
    {

        //Initialising steps
        private IWebDriver _driver;
        public LatestOrderPOM(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            Assert.That(driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/my-account/orders/"));
        }

        //Locators - find the most recent order  order
        private IWebElement _latestOrder => _driver.FindElement(By.CssSelector("tr.woocommerce-orders-table__row:nth-child(1) > td:nth-child(1)"));

        //returns the latest order number
        public string ViewLatestOrder()
        {
            StaticWaitForElement(_driver, By.CssSelector("tr.woocommerce-orders-table__row:nth-child(1) > td:nth-child(1)"));
            return _latestOrder.Text;
        }

    }
}