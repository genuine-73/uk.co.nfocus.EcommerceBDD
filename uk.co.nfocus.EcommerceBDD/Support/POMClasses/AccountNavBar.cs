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
    //Abstract method for Navigation bar in the MyAccount page - sets common functionalities
    internal abstract class AccountNavBar
    {
        
        //Initialising process
        private IWebDriver _driver;
        public AccountNavBar(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators - finds the button to navigate within the MyAccount class
        private IWebElement _dashboard => StaticWaitForElement(_driver, By.LinkText("Dashboard"));
        private IWebElement _ordersButton => StaticWaitForElement(_driver, By.LinkText("Orders"));
        private IWebElement _logoutButton => StaticWaitForElement(_driver, By.LinkText("Logout"));

        //Service methods
        //Navigates to the dashboard
        public void ClickDashboard()
        {
            _dashboard.Click();
        }
        //Navigates to the Orders Button tab
        public void ClickOrdersButton()
        {
            _ordersButton.Click();
        }

        //Clicks Logout Button
        public void ClickLogoutButton()
        {
            _logoutButton.Click();
        }

        //Check if the Logout Button is visible
        public bool CheckLogoutButton()
        {
            return _logoutButton.Displayed;
        }


    }
}
