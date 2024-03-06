using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class ClothingItems
    {
        private IWebDriver _driver;
        public ClothingItems(IWebDriver driver, string item)
        {
            this._driver = driver;
            Assert.That(driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/product/" + item.ToLower() + "/"));
        }
        //Locators
        private IWebElement _addToCart => _driver.FindElement(By.CssSelector(".single_add_to_cart_button"));

        //service methods
        public void AddItemToCart()
        {
            _addToCart.Click();
        }
    }
}

