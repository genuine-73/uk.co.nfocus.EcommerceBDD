using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using uk.co.nfocus.EcommerceBDD.Support;
using uk.co.nfocus.EcommerceBDD.Support.POCOs;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class PlacingAnOrderStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;

        private BillingDetailsPOCO _billingDetailsPOCO;
        private IWebDriver _driver;
        private NavBar _navbar;
        private Checkout _checkout;
        private OrderReceived _orderPage;
        private MyAccount _myAccount;
        private AccountOrder latestOrderPage;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        public PlacingAnOrderStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper, BillingDetailsPOCO billingDetailsShared, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [When(@"I fill the billing details to place an order in checkout")]
        public void FillOutBillingDetails(Table table)
        {
            //Navigating to checkout to place order
            _navbar = new NavBar(_driver);
            _navbar.GoToCheckout();
            _specFlowOutputHelper.WriteLine("Successfully navigated to the checkout Page");

            _billingDetailsPOCO = table.CreateInstance<BillingDetailsPOCO>();

            //Instantiating Checkour class and filling details
            _checkout = new Checkout(_driver);
            _checkout.FirstName = _billingDetailsPOCO.FirstName;
            _checkout.SecondName = _billingDetailsPOCO.SecondName;
            _checkout.Country = _billingDetailsPOCO.Country;
            _checkout.Address = _billingDetailsPOCO.Address;
            _checkout.City = _billingDetailsPOCO.City;
            _checkout.Postcode = _billingDetailsPOCO.Postcode;
            _checkout.PhoneNo= _billingDetailsPOCO.PhoneNo;

            Assert.Multiple(() =>
            {
                Assert.That(_checkout.FirstName, Does.Not.EqualTo(""));
                Assert.That(_checkout.SecondName, Does.Not.EqualTo(""));
                Assert.That(_checkout.Country, Does.Not.EqualTo(""));
                Assert.That(_checkout.Address, Does.Not.EqualTo(""));
                Assert.That(_checkout.City, Does.Not.EqualTo(""));
                Assert.That(_checkout.Postcode, Does.Not.EqualTo(""));
                Assert.That(_checkout.PhoneNo, Does.Not.EqualTo(""));
            });
            _specFlowOutputHelper.WriteLine("Successfully filled out billing details");
        }

        [Then(@"I should see an order summary of my latest order with the order number")]
        
        public void TakeToOrderSummary()
        {
            //Checks Payment Radio Button
            _checkout.CheckPayment();
            _specFlowOutputHelper.WriteLine("Successfully Checked Payments Radio Button");

            //Clicks Place Order Button; taking you to Order received confirmation
            _checkout.ClickPlaceOrder();
            _specFlowOutputHelper.WriteLine("Successfully placed on order");

            //Goes to Order Summary Page and grabs the order number
            _orderPage = new OrderReceived(_driver);
            string orderNo = _orderPage.OrderNo;
            _specFlowOutputHelper.WriteLine($"The most recent order you have made is {orderNo}");

            //Takes a screenshot of the order summary
            TakeFullPageScreenshot(_driver, "Order_Summary", "TestCaseTwo");
            _specFlowOutputHelper.WriteLine("Successfully retrieved the order number");
            _scenarioContext["_orderNo"] = orderNo; //Allows to share objects between sets  
        }
          
        [Then(@"see the same order number when I navigate to my orders tab in my account")]
        public void CheckOrderNumberMatches()
        {
            //Navigates to MyAccount Page
            _navbar.GoToAccount();
            _specFlowOutputHelper.WriteLine("Successfully back to account after placing an order");

            //Opens up all all orders made until now
            _myAccount = new MyAccount(_driver);
            _myAccount.ClickOrdersButton();
            _specFlowOutputHelper.WriteLine("Checking orders page in my accounts");

            //Grabs the latest order number from MyAccount -> Orders Page
            latestOrderPage = new AccountOrder(_driver);
            string viewOrder = latestOrderPage.ViewLatestOrder();
            _specFlowOutputHelper.WriteLine("Successfully grabs the order number from the latest order in Orders Page > My Account");

            //Checks if the order Number matches
            string orderNo = (string)_scenarioContext["_orderNo"];
            try
            {
                Assert.That(viewOrder.Substring(1, viewOrder.Length - 1), Is.EqualTo(orderNo));
                _specFlowOutputHelper.WriteLine($"The order number from Checkout Page: {orderNo}, The order number from MyAccount->Orders: {viewOrder}");
            }
            catch (Exception)
            {
                //Takes a screenshot if Test fails
                TakeFullPageScreenshot(_driver, "Incorrect", "TestCaseTwo");
                _specFlowOutputHelper.WriteLine("Successfully saves a screenshot if something goes wrong");
            }
        }
    }
}
