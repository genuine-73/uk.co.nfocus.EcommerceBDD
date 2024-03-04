using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uk.co.nfocus.EcommerceBDD.Support.POMClasses;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace uk.co.nfocus.EcommerceBDD.Support
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver; //Hackish way to share the browser with other classes
        private readonly ScenarioContext _scenarioContext;
        private readonly WDWrapper _wrapper;
        private NavBar _navbar;
        private MyAccount _myAccount;
        private Cart _cart;
        public Hooks(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _wrapper = wrapper;
        }

        [Before] //Hooks can be made specific to certain tags
        public void SetUpUsingTypeSafeWrapper()
        {
            //_driver = new FirefoxDriver();
            _driver = new EdgeDriver();
            _wrapper.Driver = _driver;
            
            //Brings the page to full screen
            _driver.Manage().Window.Maximize();

            //Navigates to the homepage of E-commerce website
            _driver.Url = "https://www.edgewordstraining.co.uk/demo-site/";
            Console.WriteLine("Successfully loads E-Commerce Page");

        }


        [After]
        public void TearDown()
        {
            //Clean up process. Getting rid of the coupon and item from the cart.
            _cart = new Cart(_driver);
            _cart.RemoveCoupon();
            _cart.RemoveFromCart();
            _cart.CheckCartIsEmpty();

            _navbar = new NavBar(_driver);
            _navbar.GoToAccount();

            //Logs you out
            _myAccount = new MyAccount(_driver);
            bool loggedout = _myAccount.LogoutExpectSuccess();
            Assert.That(loggedout, Is.True, "We did not logout");
            Console.WriteLine("Successfully logsout of the account");

            //Quits browser and DriverServer and disposes of objects (although NUnit Analyser does not know this without further config)
            _driver.Quit(); 
            Console.WriteLine("Successfully closes down the browser");
        }
    }
}
