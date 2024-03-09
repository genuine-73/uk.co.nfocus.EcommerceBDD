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
    internal class MyAccount : AccountNavBar
    {
        private IWebDriver _driver;

        public MyAccount(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
            Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/my-account/"));
        }

        //Locators - finding elements on the page
        private IWebElement _loginUsername => _driver.FindElement(By.Id("username"));
        private IWebElement _loginPassword => _driver.FindElement(By.Id("password"));
        private IWebElement _loginButton => _driver.FindElement(By.Name("login"));


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

        //Checks to see if login button is displayed
        //Used to find if we have successfully logged out
        public bool CheckLoginButton()
        {
            StaticWaitForElement(_driver, By.Name("login"));
            return _loginButton.Displayed;
        }

        //returns true or false if we have successfully logged in
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