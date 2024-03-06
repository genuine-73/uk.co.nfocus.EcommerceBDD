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

namespace uk.co.nfocus.EcommerceBDD.Support
{
    internal class StaticHelperLib
    {
        //Helper method to wait for element to load.
        public static void StaticWaitForElement(IWebDriver driver, By locator, int timeoutInSeconds = 5)
        {
            WebDriverWait myWait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            myWait.Until(drv => drv.FindElement(locator).Enabled);
        }

        public static void TakeFullPageScreenshot(IWebDriver driver, string filename, string subfolderName = null)
        {
            //Stores the current time stamp of test
            string dateTime = (DateTime.UtcNow).ToString("dd-MMM-yyyy_HH-mm-ss");
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
            var style = NumberStyles.Currency | NumberStyles.AllowCurrencySymbol;
            var provider = new CultureInfo("en-GB");
            return decimal.Parse(price, style, provider);
        }

        //Scroll down to chosen element
        public static void ScrollElementIntoView(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor? jsdriver = driver as IJavaScriptExecutor;
            jsdriver?.ExecuteScript("arguments[0].scrollIntoView()", element);
        }

    }
}

