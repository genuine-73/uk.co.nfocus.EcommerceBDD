using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class AddAnItemToCartStepDefinition
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        public AddAnItemToCartStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;

        }
                            
        [When(@"I add '(.*)' to my (?i)cart(?-i)")] 
        public void WhenIAddAnToMyCart(string item)
        {
            //Intialises shop object
            ShopPagePOM shopPage = new ShopPagePOM(_driver, item);

            //If it exists, click the clothing item
            shopPage.ClickAddToCart().CheckViewCart();
            Console.WriteLine("Successfully added an item to cart");

            //Sharing value - to be used in applying a coupon step definition
            _scenarioContext["item"] = item;
        }
    }
}
