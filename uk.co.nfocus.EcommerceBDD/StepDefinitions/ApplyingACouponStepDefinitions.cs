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
        private NavBar _navbar;
        public ApplyingACouponStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
        }


        [When(@"and view cart to apply coupon '(.*)'")]
        public void WhenApplyCoupon(string coupon)
        {
            _navbar = (NavBar)_scenarioContext["navbar"];
            _navbar.GoToCart();

            //Entering Coupon and Apply
            _cart = new Cart(_driver);
            _cart.EnterAndApplyCoupon(coupon);
            Console.WriteLine("Successfully applied coupon code");

        }

        [Then(@"I should get '(.*)'% off my selected item")]
        public void ThenIShouldGetOffMySelectedItem(string discount)
        {

            //Test to see if discount works
            try
            {
                Assert.That(_cart.Discount, Is.EqualTo((ConvertToDecimal(discount)/100) * _cart.Price));
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

            //Clean up process. Getting rid of the coupon and item from the cart.
            _cart = new Cart(_driver);
            _cart.RemoveCoupon();
            _cart.RemoveFromCart();
            _cart.CheckCartIsEmpty();
            //_cart.CheckCartIsEmpty2();
        }
    }
}

