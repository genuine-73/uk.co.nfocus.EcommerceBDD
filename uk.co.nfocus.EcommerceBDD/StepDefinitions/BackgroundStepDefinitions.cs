﻿using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;
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
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
        private IWebDriver _driver;
        private NavBar _navbar;
        private MyAccount _myAccount;

        //BackgroundStepDefinition Constructor
        public BackgroundStepDefinitions(ScenarioContext scenarioContext, WDWrapper wrapper, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _scenarioContext = scenarioContext;
            this._driver = wrapper.Driver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [Given(@"I am on the login page")]
        public void GoToLoginPage()
        {
            //Instantiating navigation bar
            _navbar = new NavBar(_driver);

            //Instantiating Dismiss button
            PopUps dismiss = new PopUps(_driver);

            //Dismissing button
            dismiss.ClickDismissButton();
            _specFlowOutputHelper.WriteLine("Successfully dismiss pop-up alert");

            // Go to my Account
            _navbar.GoToAccount();
            _specFlowOutputHelper.WriteLine("Successfully navigated to MyAccount Page");
        }

        [Given(@"(?:I|i) have logged in using valid login credentials")]
        public void LoginToMyAccount()
        {

            _myAccount = new MyAccount(_driver);
            //retrieves the username environment variable set in .runsettings
            string username = Environment.GetEnvironmentVariable("SECRET_USERNAME");

            if (username == null)
            {
                username = "hellogen@edgewords.co.uk";
                _specFlowOutputHelper.WriteLine("USERNAME env not set: Setting username ...");
            }

            //retrieves the password environment varibale set in .runsettings
            string password = Environment.GetEnvironmentVariable("SECRET_PASSWORD");

            if (password == null)
            {
                password = "HelloEdgewords!23";
                _specFlowOutputHelper.WriteLine("Password env not set: Setting password ...");
            }

            //Checks if you have successully logged in
            bool loggedIn = _myAccount.LoginExpectSuccess(username, password);
            Assert.That(loggedIn, Is.True, "We did not login");
            _specFlowOutputHelper.WriteLine("Successfully logged in");
            
        }

    }
}
