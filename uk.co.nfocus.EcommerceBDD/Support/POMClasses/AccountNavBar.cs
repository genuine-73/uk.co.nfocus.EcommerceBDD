using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal abstract class AccountNavBar
    {

        private IWebDriver _driver;

        public AccountNavBar(IWebDriver driver)
        {
            this._driver = driver;
        }
        //Locators
        private IWebElement _dashboard => _driver.FindElement(By.LinkText("Dashboard"));
        private IWebElement _orders => _driver.FindElement(By.LinkText("Orders"));
        private IWebElement _logout => _driver.FindElement(By.LinkText("Logout"));

        //Service methods
        public void ClickDashboard()
        {
            StaticWaitForElement(_driver, By.LinkText("Dashboard"));
            _dashboard.Click();
        }
        public void ClickOrders()
        {
            StaticWaitForElement(_driver, By.LinkText("Orders"));
            _orders.Click();
        }

        public void ClickLogoutButton()
        {
            StaticWaitForElement(_driver, By.LinkText("Logout"));
            _logout.Click();
        }

        public bool CheckLogoutButton()
        {
            return _logout.Displayed;
        }

    }
}
