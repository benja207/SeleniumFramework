using mds.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mds.PageObjects
{
    class ProductsPage : BasePage
    {
        IWebDriver driver = BrowserFactory.getDriver;
        protected IWebElement backPackAdd => driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
        protected IWebElement bikeLightAdd => driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
        protected IWebElement tshirtAdd => driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
        protected IWebElement jacketAdd => driver.FindElement(By.Id("add-to-cart-sauce-labs-fleece-jacket"));
        protected IWebElement onesieAdd => driver.FindElement(By.Id("add-to-cart-sauce-labs-onesie"));
        protected IWebElement testAllTshirtAdd => driver.FindElement(By.Id("add-to-cart-test.allthethings()-t-shirt-(red)"));
        //protected IWebElement listOfAll => driver.FindElements(By.ClassName("inventory_item"));
        public void addItem()
        {
            Console.WriteLine(listOfAll);
        }
    }
}