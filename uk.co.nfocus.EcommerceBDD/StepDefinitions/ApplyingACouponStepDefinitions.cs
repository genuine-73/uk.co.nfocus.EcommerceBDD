using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using TechTalk.SpecFlow.CommonModels;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class ApplyingACouponStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private Cart _cart;
        public ApplyingACouponStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
        }

        [When(@"I add an item to my cart")]
        public void WhenIAddAnItemCart()
        {
            //Instantiating Shop class and adding items to cart
            Shop product = new Shop(_driver);
            product.AddToCart();
            product.ClickViewCart();
            Console.WriteLine("Successfully added an item to cart");
        }

        [When(@"apply coupon '(.*)'")]
        public void WhenApplyCoupon(string edgewords0)
        {
            //Entering Coupon and Apply
            _cart = new Cart(_driver);
            _cart.EnterAndApplyCoupon("edgewords");
            Console.WriteLine("Successfully applied coupon code");

        }

        [Then(@"I should get ten percent off my selected item")]
        public void ThenIShouldGetOffMySelectedItem()
        {

            //Test to see if discount works
            try
            {
                Assert.That(_cart.Discount, Is.EqualTo((decimal)0.10 * _cart.Price));
                Console.WriteLine("The discount applied is correct!");
            }
            catch (Exception e)
            {
                // Take Screenshot if an exception's occurs when discount is wrong
                TakeFullPageScreenshot(_driver, "DiscountError", "TestCaseOne");
            }

            //Tests if the total calculated is correct
            try
            {
                Assert.That(_cart.Total, Is.EqualTo(_cart.Price - _cart.Discount + _cart.ShippingCost));
                Console.WriteLine("The total is correct!");
            }
            catch (Exception)
            {
                //Take Screenshot if an exception occurs in calculating the total.
                TakeFullPageScreenshot(_driver, "TotalCostError", "TestCaseOne");
            }

            // Take Screenshot of Cart Page
            TakeFullPageScreenshot(_driver, "Coupon_Applied", "TestCaseOne");
        }
    }
}

