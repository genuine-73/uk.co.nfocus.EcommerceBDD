using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    //Page Object Model for the Navigation Bar 
    internal class NavBar
    {
        private IWebDriver _driver;

        // Constructor to initialise instance of IWebDriver
        public NavBar(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators to find elements of links to different pages
        private IWebElement _homeLink => _driver.FindElement(By.LinkText("Home"));
        private IWebElement _shopLink => _driver.FindElement(By.LinkText("Shop"));
        private IWebElement _myAccount => _driver.FindElement(By.LinkText("My account"));
        private IWebElement _cart => _driver.FindElement(By.LinkText("Cart"));
        private IWebElement _checkout => _driver.FindElement(By.LinkText("Checkout"));

        //Service methods
        public void GoToShop()//Takes to Shop page
        {
            _shopLink.Click();
        }

        public void GoToAccount()//Takes to Account page
        {
            _myAccount.Click();
        }

        public void GoToCart()//Takes to Cart Page
        {
            _cart.Click();
        }

        public void GoToCheckout()//Takes to Checkout Page
        {
            _checkout.Click();
        }
    }
}