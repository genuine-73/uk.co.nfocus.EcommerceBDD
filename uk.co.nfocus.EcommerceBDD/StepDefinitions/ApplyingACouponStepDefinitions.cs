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
using TechTalk.SpecFlow.Infrastructure;

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
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public ApplyingACouponStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
            this._specFlowOutputHelper = specFlowOutputHelper;
        }



        [When(@"(?:I|i) view cart to apply coupon '(.*)'")]
        public void EnterCouponCode(string coupon)
        {
            //unwrapping values
            _item = (string)_scenarioContext["item"];

            //Click View Cart
            _shop = new ShopPagePOM(_driver);
            _shop.ClickViewCart();

            //Entering Coupon and Apply
            _cart = new Cart(_driver);
            _cart.EnterAndApplyCoupon(coupon);
            _specFlowOutputHelper.WriteLine("Successfully applied coupon code");

        }

        [Then(@"I should get '(.*)'% off my selected item")]
        public void CheckDiscountsApplied(string discount)
        {

            //Test to see if discount works                           
            try
            {
                decimal newDiscount = ConvertToDecimal(discount);
                decimal calculateDiscount = CalculateDiscount(newDiscount, _cart.SubTotal);
                
                Assert.That(_cart.Discount, Is.EqualTo(calculateDiscount), $"Wrong Discount value. Expected: {calculateDiscount} but was {_cart.Discount}");
                _specFlowOutputHelper.WriteLine("The discount applied is correct!");
                
            }      
            catch (Exception e)
            {
                // Take Screenshot if an exception's occurs when discount is wrong
                TakeFullPageScreenshot(_driver, "DiscountError", "TestCaseOne");                               
                _specFlowOutputHelper.WriteLine("Taking a screenshot to show the point of failure when calculating the discount applied");
            }

            //Tests if the total calculated is correct  
            try
            {
                decimal total = _cart.SubTotal - _cart.Discount + _cart.ShippingCost;
                Assert.That(_cart.Total, Is.EqualTo(total), $"Total calculated is incorrect. Expected: {total} but was {_cart.Discount}");
                _specFlowOutputHelper.WriteLine("The total is correct!");
                
                // Take Screenshot of Cart Page
                TakeFullPageScreenshot(_driver, "Total_Applied_Correctly", "TestCaseOne");
            }
            catch (Exception)
            {
                //Take Screenshot if an exception occurs in calculating the total.
                TakeFullPageScreenshot(_driver, "TotalCostError", "TestCaseOne");
                _specFlowOutputHelper.WriteLine("Taking a screenshot to show the point of failure when calculating total cost");
            }


        }
    }
}

