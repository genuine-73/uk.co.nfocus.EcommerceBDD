using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    // Page Object Model class for Shop page
    internal class Shop
    {
        // Instantiates Webdriver class
        private IWebDriver _driver;

        // Constructor to initialise instance of IWebDriver
        public Shop(IWebDriver driver)
        {
            this._driver = driver;
            FillUpDict(); // Finds all the clothing item elements
            Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/shop/"));
        }

        //Locators for finding elements Add item to cart and view cart
        private IWebElement _viewCart => _driver.FindElement(By.LinkText("View cart"));
        private IWebElement _addItemToCart => _driver.FindElement(By.LinkText("Add to cart"));

        private IDictionary<string, IWebElement> newElements = new Dictionary<string, IWebElement>(); //Stores clothing items and webelements as key-value pair

        //Service Method
        //Finds all the clothing items and stores them as key-value pairs where the clothing item 
        public void FillUpDict()
        {
            foreach (IWebElement elm in _driver.FindElements(By.TagName("h2")))
            {
                newElements[elm.Text] = elm;
            }
        }
        //Clicks the chosen product
        public void ClickProduct(string product)
        {
            newElements[product].Click();   
        }

        //checks if the item passed in is available in the shop page
        public bool Containsitem(string item)
        {
            if (newElements.ContainsKey(item))
            {
                return true;
            }
            return false;

        }
    }
}


