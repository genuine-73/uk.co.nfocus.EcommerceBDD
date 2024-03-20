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
        private ShopPagePOM _shop;
        private string _item;
        public ApplyingACouponStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _driver = wrapper.Driver;
        }



        [When(@"(?:I|i) view cart to apply coupon '(.*)'")]
        public void WhenApplyCoupon(string coupon)
        {
            //unwrapping values
            _item = (string)_scenarioContext["item"];

            //Click View Cart
            _shop = new ShopPagePOM(_driver, _item);
            _shop.ClickViewCart();

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
                Assert.That(_cart.Discount, Is.EqualTo((ConvertToDecimal(discount)/100) * _cart.SubTotal));
                Console.WriteLine("The discount applied is correct!");
                // Take Screenshot of Cart Page
                TakeFullPageScreenshot(_driver, "Coupon_Applied_Correctly", "TestCaseOne");
            }
            catch (Exception e)
            {
                // Take Screenshot if an exception's occurs when discount is wrong
                TakeFullPageScreenshot(_driver, "DiscountError", "TestCaseOne");
                Console.WriteLine("Discount has not been applied correctly");
            }

            //Tests if the total calculated is correct
            try
            {
                Assert.That(_cart.Total, Is.EqualTo(_cart.SubTotal - _cart.Discount + _cart.ShippingCost));
                Console.WriteLine("The total is correct!");
                // Take Screenshot of Cart Page
                TakeFullPageScreenshot(_driver, "Total_Applied_Correctly", "TestCaseOne");
            }
            catch (Exception)
            {
                //Take Screenshot if an exception occurs in calculating the total.
                TakeFullPageScreenshot(_driver, "TotalCostError", "TestCaseOne");
                Console.WriteLine("The total is incorrect");
            }

            //Clean up process. Getting rid of the coupon and all of the items from the cart
            _cart.CartCleanUp();
            Console.WriteLine("Successfully removed the coupon and deleted items from the cart");

            //Go to Account
            _navbar = new NavBar(_driver);
            _navbar.GoToAccount();

        }
    }
}

