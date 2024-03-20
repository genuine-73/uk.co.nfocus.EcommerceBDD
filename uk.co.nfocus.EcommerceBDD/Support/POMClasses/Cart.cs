﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;



//Page Object Model class for Cart page
namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class Cart
    {
        private IWebDriver _driver;

        //Constructor
        public Cart(IWebDriver driver)
        {
            this._driver = driver;
            Assert.That(_driver.Url, Is.EqualTo("https://www.edgewordstraining.co.uk/demo-site/cart/"));

        }

        //Locators for finding various elements on cart page
        private IWebElement _removeItem => StaticWaitForElement(_driver, By.LinkText("×")); //Finds the 'x' button the delete items from the cart
        private IWebElement _couponCode => StaticWaitForElement(_driver, By.Id("coupon_code")); // Finds the field to enter coupon code
        private IWebElement _applyButton => StaticWaitForElement( _driver, By.Name("apply_coupon")); //Finds the apply coupon button 
        private IWebElement _removeCoupon => StaticWaitForElement(_driver, By.LinkText("[Remove]")); //Finds the [Remove] link to clear coupon 
        private IWebElement _subtotal => StaticWaitForElement(_driver, By.CssSelector("td.product-subtotal > span:nth-child(1) > bdi:nth-child(1)")); // Cost of the clothing item
        private IWebElement _discount => StaticWaitForElement(_driver, By.CssSelector(".cart-discount > td:nth-child(2) > span:nth-child(1)")); // Cost of Discount
        private IWebElement _shippingCost => StaticWaitForElement(_driver, By.CssSelector("#shipping_method > li > label > span")); // Shipping cost
        private IWebElement _total => StaticWaitForElement(_driver, By.CssSelector(".order-total > td:nth-child(2)")); // The total cost of price + shipping
        private IWebElement _returnToShop => StaticWaitForElement(_driver, By.LinkText("Return to shop"));

        //getters and setters for various fields such as: coupons, price, discount etc.
        public string Coupon
        {
            get
            {
                return _couponCode.GetAttribute("value");
            }
            set
            {
                _couponCode.Clear();
                _couponCode.SendKeys(value);
            }
        }

        public decimal SubTotal
        {
            get
            {
                return ConvertToDecimal(_subtotal.Text);
            }
        }

        public decimal Discount
        {
            get
            {
                return ConvertToDecimal(_discount.Text);
            }
        }

        public decimal ShippingCost
        {
            get
            {
                return ConvertToDecimal(_shippingCost.Text);
            }
        }

        public decimal Total
        {
            get
            {
                return ConvertToDecimal(_total.Text);
            }
        }
        

        //Enters and Apply Coupon
        public void EnterAndApplyCoupon(string coupon)
        {
            Coupon = coupon;
            _applyButton.Click();
        }

        //Waits to see if Return to shop button appears as it indicates cart is empty
        public void ReturnToShop()
        {
            if (CheckIfCartIsEmpty())
            {
                _returnToShop.Click();
            }
           
        }
        //Removes applied coupon code
        public void RemoveCoupon()
        {
            _removeCoupon.Click();
        }

        //Deletes all item from cart
        public void DeleteItemsFromCart()
        {
            while(_driver.FindElements(By.LinkText("×")).Count > 0)
            {
                try
                {
                    ClickElementInView(_driver, _removeItem);
                }
                catch
                {
                    break;
                }
            }
        }
        //Check if cart is empty
        public bool CheckIfCartIsEmpty()
        {

            return _returnToShop.Displayed;
        }

        //Cleans after the code runs
        public void CartCleanUp()
        {
            RemoveCoupon();
            DeleteItemsFromCart();
            ReturnToShop(); 
        }
    }
}

  //finds the element
  //clicks the x button, 
  //waits until the the circle thing is gone and I get a banner saying its gone.
  //From here I can either press x if there anymore
  // or click return to shop if there is not any