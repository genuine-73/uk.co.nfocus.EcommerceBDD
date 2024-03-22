﻿using OpenQA.Selenium;
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
        public ShopPagePOM(IWebDriver driver)
        {
            this._driver = driver;
        }

        //Locators
        private IWebElement _addToCart => StaticWaitForElement(_driver, By.CssSelector($"a[aria-label=\"Add “{_item}” to your cart\"]"));
        private IWebElement _viewCart => StaticWaitForElement(_driver, By.LinkText("View cart"));

        //Adds the chosen item to cart
        public ShopPagePOM ClickAddToCart()
        {
            _addToCart.Click();
            return this;
        }

        //Checks if the item passed in exists.
        public bool AddItemToCartSuccess(string item)
        {
            _item = item;
            try
            {
                return _addToCart.Displayed;
            }
            catch
            {
                return false;
            }
        }
        //Takes to the cart page
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
