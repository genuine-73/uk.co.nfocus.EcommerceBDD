using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class PopUps
    {

        private IWebDriver _driver;

        public PopUps(IWebDriver driver)
        {
            this._driver = driver;
        }

        private IWebElement _dismissButton => _driver.FindElement(By.LinkText("Dismiss"));

        public void ClickDismissButton()
        {
            _dismissButton.Click();
        }
    }
}

