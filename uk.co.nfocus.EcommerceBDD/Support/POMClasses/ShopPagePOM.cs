using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;
namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class ShopPagePOM
    {
        private IWebDriver _driver;
        private string _item;
        public ShopPagePOM(IWebDriver driver, string item)
        {
            this._driver = driver;
            this._item = item;
        }

        //Locators
        private IWebElement _addToCart => StaticWaitForElement(_driver, By.CssSelector($"a[aria-label=\"Add “{_item}” to your cart\"]"));
        private IWebElement _viewCart => StaticWaitForElement(_driver, By.LinkText("View cart"));

        public ShopPagePOM ClickAddToCart()
        {
            _addToCart.Click();
            return this;
        }

        public void ClickViewCart()
        {
            _viewCart.Click();
        }

        public bool CheckViewCart()
        {
            return _viewCart.Displayed;

        }
    }
}
