using NUnit.Framework;
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
        private IWebElement _removeItem => _driver.FindElement(By.LinkText("×")); //Finds the 'x' button the delete items from the cart
        private IWebElement _couponCode => _driver.FindElement(By.Id("coupon_code")); // Finds the field to enter coupon code
        private IWebElement _applyButton => _driver.FindElement(By.Name("apply_coupon")); //Finds the apply coupon button 
        private IWebElement _removeCoupon => _driver.FindElement(By.LinkText("[Remove]")); //Finds the [Remove] link to clear coupon 
        private IWebElement _price => _driver.FindElement(By.CssSelector("td.product-subtotal > span:nth-child(1) > bdi:nth-child(1)")); // Cost of the clothing item
        private IWebElement _discount => _driver.FindElement(By.CssSelector(".cart-discount > td:nth-child(2) > span:nth-child(1)")); // Cost of Discount
        private IWebElement _shippingCost => _driver.FindElement(By.CssSelector("#shipping_method > li > label > span")); // Shipping cost
        private IWebElement _total => _driver.FindElement(By.CssSelector(".order-total > td:nth-child(2)")); // The total cost of price + shipping
        private IWebElement _returnToShop => _driver.FindElement(By.LinkText("Return to shop"));
        
        //getters and setters for various fields such as: coupons, price, discount etc.
        public string Coupon
        {
            get
            {
                StaticWaitForElement(_driver, By.Id("coupon_code"));
                return _couponCode.GetAttribute("value");
            }
            set
            {
                _couponCode.Clear();
                _couponCode.SendKeys(value);
            }
        }

        public decimal Price
        {
            get
            {
                StaticWaitForElement(_driver, By.CssSelector("td.product-subtotal > span:nth-child(1) > bdi:nth-child(1)"));
                return ConvertToDecimal(_price.Text);
            }
        }

        public decimal Discount
        {
            get
            {
                StaticWaitForElement(_driver, By.CssSelector(".cart-discount > td:nth-child(2) > span:nth-child(1)"));
                return ConvertToDecimal(_discount.Text);
            }
        }

        public decimal ShippingCost
        {
            get
            {
                StaticWaitForElement(_driver, By.CssSelector("#shipping_method > li:nth-child(1) > label:nth-child(2) > span:nth-child(1) > bdi:nth-child(1)"));
                return ConvertToDecimal(_shippingCost.Text);
            }
        }

        public decimal Total
        {
            get
            {
                StaticWaitForElement(_driver, By.CssSelector(".order-total > td:nth-child(2) > strong:nth-child(1) > span:nth-child(1) > bdi:nth-child(1)"));
                return ConvertToDecimal(_total.Text);
            }
        }


        //Enters and Apply Coupon
        public void EnterAndApplyCoupon(string coupon)
        {
            Coupon = coupon;
            _applyButton.Click();
        }

        //Removes clothing item from cart
        public void RemoveFromCart()
        {
            try
            {
                StaticWaitForElement(_driver, By.LinkText("×"), 7);//Waits to see if the 'x' button is found to delete the item from teh cart
                _removeItem.Click();
            }
            catch
            {
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(3));// Waits to see if the coupon remove link is there
                wait.Until(drv => drv.FindElements(By.LinkText("[Remove]")).Count == 0);// If there arent any, then we can proceed to delete item from cart
                //Thread.Sleep(2000);
                _removeItem.Click();
            }

        }
        //Waits to see if Return to shop button appears as it indicates cart is empty
        public void CheckCartIsEmpty()
        {
            StaticWaitForElement(_driver, By.LinkText("Return to shop"));
            _returnToShop.Click();

        }
        //Removes applied coupon code
        public void RemoveCoupon()
        {
            StaticWaitForElement(_driver, By.LinkText("[Remove]"));
            _removeCoupon.Click();
        }

        //Deletes all item from cart
        public void DeleteItemsFromCart()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));// Waits to see if the coupon remove link is there
            while (wait.Until(drv => drv.FindElements(By.LinkText("×")).Count > 0))
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

        //Cart Clean Up Process
        public void CartCleanUp()
        {
            RemoveCoupon();//Removes coupon
            DeleteItemsFromCart();//Removes item from cart
            CheckCartIsEmpty(); //Check if cart is empty and clicks return to shop
        }
    }
}

