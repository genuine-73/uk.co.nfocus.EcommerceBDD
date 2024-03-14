using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.StepDefinitions;
using uk.co.nfocus.EcommerceBDD.Support;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.StepDefinitions
{
    [Binding]
    internal class BackgroundStepDefinitions
    {
        //Variable declaration
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private NavBar _navbar;
        private MyAccount _myAccount;

        //BackgroundStepDefinition Constructor
        public BackgroundStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            //Instantiating navigation bar
            _navbar = new NavBar(_driver);
            _scenarioContext["navbar"] = _navbar;

            //Instantiating Dismiss button
            PopUps dismiss = new PopUps(_driver);

            //Dismissing button
            dismiss.ClickDismissButton();
            Console.WriteLine("Successfully dismiss pop-up alert");

            // Go to my Account
            _navbar.GoToAccount();
            Console.WriteLine("Successfully navigated to MyAccount Page");
        }

        [When(@"(?:I|i) have logged in using valid login credentials")]
        public void GivenIHaveLoggedInUsingValidCredentials()
        {

            _myAccount = new MyAccount(_driver);
            //retrieves the username environment variable set in .runsettings
            string username = Environment.GetEnvironmentVariable("SECRET_USERNAME");
            //retrieves the password environment varibale set in .runsettings
            string password = Environment.GetEnvironmentVariable("SECRET_PASSWORD"); 
           
            //Checks if you have successully logged in
            bool loggedIn = _myAccount.LoginExpectSuccess(username, password);
            Assert.That(loggedIn, Is.True, "We did not login");
            Console.WriteLine("Successfully logged in");
            
            //Used to logout during logout process
            _scenarioContext["_myAccount"] = _myAccount;
        }

        [When(@"I navigate to the Shop page")]
        public void WhenINavigateToTheShopPage()
        {
            //Go To Shop
            _navbar.GoToShop();
            Console.WriteLine("Successfully navigated to shop page");
        }

    }
}
