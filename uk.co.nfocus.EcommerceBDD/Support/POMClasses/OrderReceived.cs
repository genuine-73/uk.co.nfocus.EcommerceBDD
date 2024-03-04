using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static uk.co.nfocus.EcommerceBDD.Support.StaticHelperLib;

namespace uk.co.nfocus.EcommerceBDD.Support.POMClasses
{
    internal class OrderReceived
    {
        private IWebDriver _driver;

        public OrderReceived(IWebDriver driver)
        {
            this._driver = driver;
        }

        private IWebElement _orderNo => _driver.FindElement(By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));

        public string OrderNo
        {
            get
            {
                StaticWaitForElement(_driver, By.CssSelector("#post-6 > div > div > div > ul > li.woocommerce-order-overview__order.order > strong"));
                return _orderNo.Text;
            }
        }
    }
}
