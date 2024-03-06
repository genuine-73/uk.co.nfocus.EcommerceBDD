using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.HeadlessExperimental;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

// Page Object Model for Account Class
namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class MyAccount
    {
        private IWebDriver _driver;

        public MyAccount(IWebDriver driver)
        {
            this._driver = driver;
            Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/my-account/"));
        }

        //Locators - finding elements on the page
        private IWebElement _loginUsername => _driver.FindElement(By.Id("username"));
        private IWebElement _loginPassword => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));
        private IWebElement _logoutButton => _driver.FindElement(By.LinkText("Logout"));
        private IWebElement _OrdersButton => _driver.FindElement(By.LinkText("Orders"));


        //Service methods

        public string Username
        {
            get //getters for Username in C# style
            {
                StaticWaitForElement(_driver, By.Id("password"));
                return _loginUsername.GetAttribute("value");
            }
            set //setters for Username in C# style
            {
                _loginUsername.Clear();
                _loginUsername.SendKeys(value);
            }
        }

        public string Password
        {
            get //getters for Password in C# style
            {
                StaticWaitForElement(_driver, By.Id("password"));
                return _loginPassword.GetAttribute("value");
            }
            set //setters for Password in C# style
            {
                _loginPassword.Clear();
                _loginPassword.SendKeys(value);
            }
        }

        // Clicks login button to go to your account
        public void ClickLoginButton()
        {
            _loginButton.Click();
        }

        // Clicks logout button to leave your account
        public void ClickLogoutButton()
        {
            _logoutButton.Click();
        }

        // Clicks Orders tab within My account to view all of the Orders made
        public void ClickOrdersButton()
        {
            StaticWaitForElement(_driver, By.LinkText("Orders"));
            _OrdersButton.Click();
        }

        //Checks to see if login button is displayed
        //Used to find if we have successfully logged out
        public bool CheckLoginButton()
        {
            StaticWaitForElement(_driver, By.Name("login"));
            return _loginButton.Displayed;
        }

        //Checks to see if we logout button is displayed
        //Used to find if we have successfully logged in
        public bool CheckLogoutButton()
        {
            return _logoutButton.Displayed;
        }

        //returns true or false to check that we have successfully logged in
        public bool LoginExpectSuccess(string username, string password)
        {
            Username = username;
            Password = password;
            ClickLoginButton();

            try
            {
                return CheckLogoutButton();

            }
            catch (Exception)
            {
                return false;
            }
        }

        //returns true or false to check that we have successfully logged out
        public bool LogoutExpectSuccess()
        {
            ClickLogoutButton();
            try
            {
                return CheckLoginButton();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}