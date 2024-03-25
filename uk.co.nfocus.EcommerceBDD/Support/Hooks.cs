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
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V120.Browser;
using OpenQA.Selenium.DevTools.V120.FedCm;      
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;



namespace uk.co.nfocus.EcommerceBDD.Support
{
    [Binding]
    public class Hooks
    {
        //variable declaration
        private IWebDriver _driver; 
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

        [Before] //Runs before every scenario    
        public void SetUp()
        {

            //Retrieves the enviornment variable browser set on .runsettings file
            string browser = Environment.GetEnvironmentVariable("BROWSER");

            //Sanitation check in case the browser is null
            if (browser == null)
            {
                browser = "edge"; // creates edge as default
                Console.WriteLine("BROWSER env not set: Setting to Edge");
            }


            //picks a browser from the options below
            switch (browser)
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;

                case "edge":
                    _driver = new EdgeDriver();
                    break;

                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                default:
                    _driver = new FirefoxDriver();
                    break;

            }
            Console.WriteLine("Browser set to " + browser);
            _wrapper.Driver = _driver;

            //Brings the page to full screen
            _driver.Manage().Window.Maximize();

            //Navigates to the homepage of E-commerce website
            string startURL = TestContext.Parameters["WebAppURL"];
            
            //Sanitation check in case startURL is null
            if(startURL == null)
            {
                startURL = "https://www.edgewordstraining.co.uk/demo-site/";
                Console.WriteLine("URL has not been properly set \n default string has been passed to the URL");
            }

            //Opens up the browser to the ecommerce site
            _driver.Url = startURL;
            Console.WriteLine("Successfully loads E-Commerce Page");

        }


        [After] // Runs after every scenario
        public void TearDown()
        {
            _navbar = new NavBar(_driver);

            try
            {
                _navbar.GoToCart();

                //Clean up process. Getting rid of the coupon and all of the items from the cart      
                _cart = new Cart(_driver);
                _cart.CartCleanUp();
                Console.WriteLine("Successfully removed the coupon and deleted items from the cart");
            }
            catch
            {
                //Takes screenshot in case anything goes wrong during cleanup
                TakeFullPageScreenshot(_driver, "CartCleanUpGoneWrong");
                Console.WriteLine("Cart Clean Up Gone Wrong");
            }
            finally
            {
                //Go to Account
                _navbar.GoToAccount();

                _myAccount = new MyAccount(_driver);
                //Logs the user out of the account
                bool loggedout = _myAccount.LogoutExpectSuccess();
                Assert.That(loggedout, Is.True, "Logout failure");
                Console.WriteLine("Successfully logsout of the account");

                // Closes the browser correctly
                _driver.Quit();
                Console.WriteLine("Successfully closes down the browser");
            }
        }
    }
}

