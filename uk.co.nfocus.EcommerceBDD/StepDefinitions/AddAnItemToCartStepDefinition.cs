using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
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
        private NavBar _navbar;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public AddAnItemToCartStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
            _specFlowOutputHelper = specFlowOutputHelper;

        }
                            
        [When(@"I navigate to the Shop page to add '(.*)' to my (?i)cart(?-i)")]
        public void AddItemToCart(string item)
        {
            //Go To Shop
            _navbar = new NavBar(_driver);
            _navbar.GoToShop();
            _specFlowOutputHelper.WriteLine("Successfully navigated to shop page");

            //Intialises shop object
            ShopPagePOM shopPage = new ShopPagePOM(_driver);

            //If it exists, click the clothing item
            bool value = shopPage.AddItemToCartSuccess(item);
            Assert.That(value, Is.EqualTo(true),$"Item: {item} does not exist");

            shopPage.ClickAddToCart().CheckViewCart();
            _specFlowOutputHelper.WriteLine("Successfully added an item to cart");

            //Sharing value - to be used in applying a coupon step definition
            _scenarioContext["item"] = item;
        }
    }
}
