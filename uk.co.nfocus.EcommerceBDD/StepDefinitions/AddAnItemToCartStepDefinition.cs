using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using uk.co.nfocus.EcommerceBDD.Support;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
using NUnit.Framework;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class AddAnItemToCartStepDefinition
    {
        //Variable declaration
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private NavBar _navbar;

        //Instantiates an object of the class
        public AddAnItemToCartStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
        }

        [When(@"(?:I|i) add an '(.*)' to my (?i)cart(?-i)")]
        public void WhenIAddAnToMyCart(string item)
        {
            //Intialises shop object
            Shop product = new Shop(_driver);
            try
            {
                //Checks to see if the 'item' passed is in the shop page
                Assert.That(true, Is.EqualTo(product.Containsitem(item)));
                //If it exists, click the clothing item
                product.ClickProduct(item);

                //Creates a clothing item object and adds that object to cart
                ClothingItems clothingItems = new ClothingItems(_driver, item);
                clothingItems.AddItemToCart();
                Console.WriteLine("Successfully added an item to cart");
            }
            catch
            {
                //If an exception is raised in this case where the item does not exist, takes a screenshot
                TakeFullPageScreenshot(_driver, "ItemDoesNotExist");
            }
        }
    }
}


////Dictionary Static Method
//Shop product = new Shop(_driver);
//Shop.FillUpDict2(_driver);
//Shop.ClickProduct2(item);

//ClothingItems beanie = new ClothingItems(_driver, item);
//beanie.AddItemToCart();