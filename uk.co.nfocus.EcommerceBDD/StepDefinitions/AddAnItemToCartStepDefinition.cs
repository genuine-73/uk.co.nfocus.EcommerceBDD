using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using uk.co.nfocus.EcommerceBDD.Support;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class AddAnItemToCartStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private NavBar _navbar;
        public AddAnItemToCartStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
        }

        [When(@"(?:I|i) add an '(.*)' to my (?i)cart(?-i)")]
        public void WhenIAddAnToMyCart(string item)
        {
            Shop product = new Shop(_driver);
            product.FillUpDict();
            if (product.Containsitem(item))
            {
                product.ClickProduct(item);
                ClothingItems beanie = new ClothingItems(_driver, item);
                beanie.AddItemToCart();
                Console.WriteLine("Successfully added an item to cart");
            }
            else
            {
                TakeFullPageScreenshot(_driver, "ItemSelectedDoesNotExist", "TestCaseTwo");
                Console.WriteLine("Correct clothing item was not passd");
            }
        }
    }
}
//Both belong inside [When(@"(?:I|i) add an '(.*)' to my (?i)cart(?-i)")]

//Shop product = new Shop(_driver);
//product.ClickProduct(item);

//ClothingItems beanie = new ClothingItems(_driver, item);
//beanie.AddItemToCart();



////Dictionary Static Method
//Shop product = new Shop(_driver);
//Shop.FillUpDict2(_driver);
//Shop.ClickProduct2(item);

//ClothingItems beanie = new ClothingItems(_driver, item);
//beanie.AddItemToCart();