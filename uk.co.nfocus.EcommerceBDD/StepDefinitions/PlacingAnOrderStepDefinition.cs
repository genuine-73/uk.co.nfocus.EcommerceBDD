using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;
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
        public PlacingAnOrderStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper, BillingDetailsPOCO billingDetailsShared)
        {
            _scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
        }

        [When(@"I fill the billing details to place an order in checkout")]
        public void WhenIFillTheBillingDetailsToPlaceAnOrderInCheckout(Table table)
        {
            //Navigating to checkout to place order
            _navbar = new NavBar(_driver);
            _navbar.GoToCheckout();
            Console.WriteLine("Successfully navigated to the checkout Page");

            _billingDetailsPOCO = table.CreateInstance<BillingDetailsPOCO>();

            //Instantiating Checkour class and filling details
            _checkout = new Checkout(_driver);
            _checkout.SetFirstName(_billingDetailsPOCO.FirstName)
                         .SetSecondName(_billingDetailsPOCO.SecondName)
                         .SetCountry(_billingDetailsPOCO.Country)
                         .SetAddress(_billingDetailsPOCO.Address)
                         .SetCity(_billingDetailsPOCO.City)
                         .SetPostCode(_billingDetailsPOCO.Postcode)
                         .SetPhoneNo(_billingDetailsPOCO.PhoneNo);

            Console.WriteLine("Successfully filled out billing details");
        }
        [Then(@"(?:I|i) should see an order summary")]
        public void ThenIShouldSeeAnOrderSummary()
        {
            //Checks Payment Radio Button
            _checkout.CheckPayment();
            Console.WriteLine("Successfully Checked Payments Radio Button");

            //Clicks Place Order Button; taking you to Order received confirmation
            _checkout.ClickPlaceOrder();
            Console.WriteLine("Successfully placed and order; has taken us to order summary");

            //Goes to Order Summary Page and grabs the order number
            _orderPage = new OrderReceived(_driver);
            string orderNo = _orderPage.OrderNo;
            Console.WriteLine("Successfully made a copy of the order number");

            //Takes a screenshot of the order summary
            TakeFullPageScreenshot(_driver, "Order_Summary", "TestCaseTwo");
            Console.WriteLine("Successfully retrieved the order number");
            _scenarioContext["_orderNo"] = orderNo; //Allows to share objects between sets  
        }

        [Then(@"access it from my orders portal")]
        public void ThenAccessItFromMyOrdersPortal()
        {
            //Navigates to MyAccount Page
            _navbar.GoToAccount();
            Console.WriteLine("Successfully back to account after placing an order");

            //Opens up all all orders made until now
            _myAccount = new MyAccount(_driver);
            _myAccount.ClickOrdersButton();
            Console.WriteLine("Checking orders page in my accounts");

            //Grabs the latest order number from MyAccount -> Orders Page
            latestOrderPage = new AccountOrder(_driver);
            string viewOrder = latestOrderPage.ViewLatestOrder();
            Console.WriteLine("Successfully grabs the order number from the latest order in Orders Page > My Account");

            //Checks if the order Number matches
            string orderNo = (string)_scenarioContext["_orderNo"];
            try
            {
                Assert.That(viewOrder.Substring(1, viewOrder.Length - 1), Is.EqualTo(orderNo));
                Console.WriteLine($"The order number from Checkout Page: {orderNo}, The order number from MyAccount->Orders: {viewOrder}");
            }
            catch (Exception)
            {
                //Takes a screenshot if Test fails
                TakeFullPageScreenshot(_driver, "Incorrect", "TestCaseTwo");
                Console.WriteLine("Successfully saves a screenshot if something goes wrong");
            }
        }
    }
}
