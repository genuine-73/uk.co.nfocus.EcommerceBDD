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

//TODO:
//POCO -> Add billing information in scenario
//Try and get rid of Thread.Sleep(2000) from delete all from cart and checkpayments method as well
//Write a better way to closedown since in TestCaseTwo goes back to my account and then logouts instead of just logging out
//Add comments and make it more readable
//For Account navbar should I go for abstract or interface
//Scroll down to screenshot webelements
//Do living Doc for reporting and better reporting


namespace uk.co.nfocus.EcommerceBDD.Support
{
    [Binding]
    public class Hooks
    {
        private IWebDriver _driver; 
        private readonly ScenarioContext _scenarioContext;
        private readonly WDWrapper _wrapper;
        private NavBar _navbar;
        private MyAccount _myAccount;
        public Hooks(ScenarioContext scenarioContext, WDWrapper wrapper)
        {
            _scenarioContext = scenarioContext;
            _wrapper = wrapper;
        }

        [Before] //Hooks can be made specific to certain tags
        public void SetUpUsingTypeSafeWrapper()
        {

            //Helps to choose which web browser to open up
            string browser = Environment.GetEnvironmentVariable("BROWSER");
            //string browser = "firefox";
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
                    _driver = new EdgeDriver(); //Sanitation check in case browser is null
                    break;

            }
            Console.WriteLine("Browser set to " + browser);
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
