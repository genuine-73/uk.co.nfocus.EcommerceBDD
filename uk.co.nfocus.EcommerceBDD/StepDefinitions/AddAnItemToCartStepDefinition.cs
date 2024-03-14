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
            Shop shop = new Shop(_driver);
            try
            {
                //Checks to see if the 'item' passed is in the shop page
                Assert.That(true, Is.EqualTo(shop.Containsitem(item)));
                //If it exists, click the clothing item
                shop.ClickProduct(item);
                Console.WriteLine("Item exists and we have successfully opened it");

                //Creates a clothing item object and adds that object to cart
                ClothingItems clothingItems = new ClothingItems(_driver, item);
                clothingItems.AddItemToCart();
                Console.WriteLine("Successfully added an item to cart");

                //Share in Applying a coupon section
                _scenarioContext["item"] = item;
            }
            catch
            {
                //If an exception is raised in this case where the item does not exist, takes a screenshot
                TakeFullPageScreenshot(_driver, "ItemDoesNotExist");
                Console.WriteLine("Item does not exist");
            }
        }
    }
}
