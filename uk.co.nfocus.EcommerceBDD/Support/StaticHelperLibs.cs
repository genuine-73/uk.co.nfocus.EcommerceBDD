using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.Extensions;
using System.Globalization;
using System.Reflection;
using NUnit.Framework;
using System.Data;
using SeleniumExtras.WaitHelpers;

namespace uk.co.nfocus.EcommerceBDD.Support
{

    internal class StaticHelperLib
    {
        private static IJavaScriptExecutor? jsdriver;


        //Helper method to wait for element to load.
        public static IWebElement StaticWaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            try
            {
                return myWait.Until(drv => drv.FindElement(locator));

            }
            catch
            {
                return null;
            }
        }

            //Helper method to wait for elements to be clickable when blockUI leaves
            public static IWebElement? WaitForBlockUIToDisappear(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            //returns element if the locator can be found once blockUI is gone else returns null
            try
            {
                WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                By blockUI = By.CssSelector(".blockUI.blockOverlay");
                myWait.Until(ExpectedConditions.InvisibilityOfElementLocated(blockUI));
                return driver.FindElement(locator);
            }
            catch
            {
                return null;
            }
        }

        public static void TakeFullPageScreenshot(IWebDriver driver, string filename, string? subfolderName = null)
        {
            //Stores the current time stamp of test
            string dateTime = DateTime.UtcNow.ToString("dd-MMM-yyyy_HH-mm-ss");
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string filepath = path + $"\\Screenshot\\{subfolderName}\\{filename}_{dateTime}.png";

            //Takes a screenshot of the order page
            Screenshot screenshot = driver.TakeScreenshot();
            screenshot.SaveAsFile(filepath);
            
            //Adds attachment 
            TestContext.AddTestAttachment(filepath, $"{filename}_{dateTime}.png");
            
        }

       
        // Calculates the discount
        public static decimal ConvertToDecimal(string price)           
        {
            NumberStyles style = NumberStyles.Currency | NumberStyles.AllowCurrencySymbol;
            CultureInfo provider = new CultureInfo("en-GB");
            return decimal.Parse(price, style, provider);
        }

        //Scroll down to chosen element
        public static void ScrollElementIntoView(IWebDriver driver, IWebElement element)
        {
            jsdriver = driver as IJavaScriptExecutor;
            jsdriver?.ExecuteScript("arguments[0].scrollIntoView()", element);
        }

        // Calculates the discount of subtotal
        public static decimal CalculateDiscount(decimal discountPercent, decimal amount)
        {
            return (discountPercent / 100) * amount;
        }

        //Calculates the total cost of cart after discount
        public static decimal CalculateTotal(decimal subTotal, decimal discount, decimal shippingCost)
        {
            return (subTotal - discount) + shippingCost;
        }

    }
}

