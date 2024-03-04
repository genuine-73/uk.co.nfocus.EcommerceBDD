using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class PlacingAnOrderStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        public PlacingAnOrderStepDefinition(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
        }
        [When(@"fill out the billing details to place an order in checkout")]
        public void WhenFillOutTheBillingDetailsToPlaceAnOrderInCheckout()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I should see an order summary")]
        public void ThenIShouldSeeAnOrderSummary()
        {
            _scenarioContext.Pending();
        }

        [Then(@"access it from my orders portal")]
        public void ThenAccessItFromMyOrdersPortal()
        {
            _scenarioContext.Pending();
        }
    }
}
