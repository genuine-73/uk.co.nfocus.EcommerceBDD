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

//TODO:
/*
- Double check and rewrite my scenarios
- Currently clean up is in the applying coupon step definition
- Improve on WaitForElement as well as DeleteFromCart, CheckPayments etc in Static Helper Library. Utilise Scroll Element into View
- Add enums for payment methods
- Rewrite some of the method names
- Add in the outerhelp so I can write out the reporting in HTML form
- Look for ways to decouple code
- Cart Cleanup -> think of if there is no coupon, Items in cart etc. best to seperate them out!  
- Add Item to cart from ShopPOM make it better

 Teardown

Your TearDown clean up is *very* specific to the two tests. I.e. if you don’t end up on the cart page then no attempt is made to clear the cart. Both tests should log out at the end, and you’ve repeated that code. Why not just have the teardown clear the cart and log out for both tests?

Put the whole thing in a try/catch/finally with finally containing driver.Quit(); -this way the browser will always be quit even if something should go wrong with the cleanup.
 

Line 54 (as part of an assertion try/catch, you create a variable ‘e’ that holds the caught exception details but ‘e’ is never used afterwards. Either remove this unused variable or output it in the catch. Also, rather than catching the general Exception, consider catching the more specific AssertionException


ThenAccessItFromMyOrdersPortal()    

The final assert and try/catch has a few issues:              +

If the assertion fails the following console.writeline will not execute so the message will not be logged. Use the third parameter of Assert.That() to write out fail messages:
                
Assert.That(viewOrder.Substring(1, viewOrder.Length - 1), Is.EqualTo(orderNo), $"The order number from Checkout Page: {orderNo}, The order number from MyAccount->Orders: {viewOrder}");

 

As this is the last assert of the test if it fails, rethrow the assertion in the catch block.:
            catch (Exception)

            {

                //Takes a screenshot if Test fails

                TakeFullPageScreenshot(_driver, "Incorrect", "TestCaseTwo");

                Console.WriteLine("Successfully saves a screenshot if something goes wrong");

                throw;

            }

 

By doing this the test will definitely report a fail. If you are using Specflow LivingDoc for reporting, catching all failed AssertionExceptions will result in a test reporting a pass.

  

Further to this and more generally with try/catch and assertions – if you are always catching assertion errors and allowing the test to continue you can waste time. Try adding a non existent item to the cart (e.g. ‘Boo’) then place an order. Your test attempts to run far further than it can – it will e.g. navigate to the cart. When the requested item isn’t found execution should stop there for that test, and move on to the next.

 

Also general feedback – run through your code base and try and fix any compiler warnings (green underlines).
*/

namespace uk.co.nfocus.EcommerceBDD.Support
{
    [Binding]
    public class Hooks
    {
        //declaring variables for all the classes that will be used
        private IWebDriver _driver; 
        private readonly ScenarioContext _scenarioContext;
        private readonly WDWrapper _wrapper;
        private NavBar _navbar;
        private MyAccount _myAccount;
        private Cart _cart;

        //Initialises the variables when called 
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
            
            if(startURL == null)
            {
                startURL = "https://www.edgewordstraining.co.uk/demo-site/";
                Console.WriteLine("URL has not been properly set \n default string has been passed to the URL");
            }

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

