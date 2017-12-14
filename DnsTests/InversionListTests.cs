using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Linq;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace DnsTests
{
    [TestClass]
    public class InversionListTests
    {
        IWebDriver CurrentBrowser;
        string Url;

        [TestInitialize]
        public void TestInit()
        {
            Browser.StartWebDriver();
            CurrentBrowser = Browser.WebDriver;
            Url = "http://www.dns-shop.ru";
        }

        [TestMethod]
        public void InversionListLaptopTest()
        {
            CurrentBrowser.Navigate().GoToUrl(Url);

            Thread.Sleep(5000);
            var cityBtn = CurrentBrowser.FindElement(By.CssSelector(".btn.btn-additional"));
            Browser.scrollToElementAndClick(cityBtn);

            Thread.Sleep(700);
            var lapAndTabs = CurrentBrowser.FindElement(By.CssSelector("#menu-catalog > li:nth-child(1) > a"));
            new Actions(CurrentBrowser).MoveToElement(lapAndTabs).Perform();

            Thread.Sleep(700);
            var lap = CurrentBrowser.FindElement(By.CssSelector("#menu-catalog > li:nth-child(1) > div > ul > li:nth-child(1) > div > a"));
            Browser.scrollToElementAndClick(lap);

            AssertEquals();
        }

        [TestMethod]
        public void InversionListSmartphonesTest()
        {
            CurrentBrowser.Navigate().GoToUrl(Url);

            Thread.Sleep(5000);
            var cityBtn = CurrentBrowser.FindElement(By.CssSelector(".btn.btn-additional"));
            Browser.scrollToElementAndClick(cityBtn);

            Thread.Sleep(1000);
            var phonesAndWatches = CurrentBrowser.FindElement(By.CssSelector("#menu-catalog > li:nth-child(4) > a"));
            new Actions(CurrentBrowser).MoveToElement(phonesAndWatches).Perform();
            

            Thread.Sleep(1000);
            var phones = CurrentBrowser.FindElement(By.CssSelector("#menu-catalog > li:nth-child(4) > div > ul > li:nth-child(1) > div > a"));
            Browser.scrollToElementAndClick(phones);


            AssertEquals();
        }

        [TestCleanup]
        public void TestClean()
        {
            Browser.Quit();
        }

        private void AssertEquals()
        {
            Thread.Sleep(5000);
            string priseOrderSelector = @"#sort-filter > div:nth-child(1) > div > button > span.title";
            string lowerPriceSelector = @"#sort-filter > div:nth-child(1) > div > ul > li:nth-child(2) > a"; //<
            string higherPriceSelector = @"#sort-filter > div:nth-child(1) > div > ul > li.disabled > a";//>

            var priseOrder = CurrentBrowser.FindElement(By.CssSelector(priseOrderSelector));
            var higherPrice = CurrentBrowser.FindElement(By.CssSelector(higherPriceSelector));
            var lowerPrice = CurrentBrowser.FindElement(By.CssSelector(lowerPriceSelector));
            Browser.scrollToElementAndClick(priseOrder);
            Browser.scrollToElementAndClick(higherPrice);

            Thread.Sleep(5000);
            string firstIndexSelector = @"//div[@data-position-index='1']";
            var firstItem = CurrentBrowser.FindElement(By.XPath(firstIndexSelector));

            string productName1 = firstItem.Text.Substring(0, firstItem.Text.IndexOf(Environment.NewLine));

            Browser.scrollToElementAndClick(priseOrder);
            Browser.scrollToElementAndClick(lowerPrice);

            Thread.Sleep(5000);
            string inEndSelector = @"#catalog-items-page > div.page-content-container > div.catalog-category-wrapper > div.catalog-items > div.pagination-container > div > span:nth-child(10)";
            var inEnd = CurrentBrowser.FindElement(By.CssSelector(inEndSelector));
            Browser.scrollDown();
            Browser.scrollToElementAndClick(inEnd);

            Thread.Sleep(5000);
            Browser.scrollDown();

            Thread.Sleep(500);
            string itemListSelector = @"#catalog-items-page > div.page-content-container > div.catalog-category-wrapper > div.catalog-items > div.catalog-items-list.view-list";
            var itemList = CurrentBrowser.FindElement(By.CssSelector(itemListSelector));
            var lastItem = itemList.FindElement(By.XPath("./div[last()]"));

            string productName2 = lastItem.Text.Substring(0, lastItem.Text.IndexOf(Environment.NewLine));
            Assert.AreEqual(productName1, productName2);
        }
    }
}
