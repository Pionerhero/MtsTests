using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using System.Diagnostics.Contracts;

namespace DnsTests.Pages
{
    class DnsIndexPage : PageBase
    {
        private readonly string url = @"https://www.dns-shop.ru";
        public override string Url
        {
            get { return url;}
        }

        public DnsIndexPage()
        {
            InitPage(this);
        }

        [FindsBy(How = How.CssSelector, Using = ".btn.btn-additional")]
        private IWebElement cityDefaultBtn;

        [FindsBy(How = How.Id, Using = "menu-catalog")]
        private IWebElement menuCatalog;

        public override void GoToPage()
        {
            Browser.WebDriver.Navigate().GoToUrl(url);
        }

        public void ConfirmDefaultCity()
        {
            Browser.scrollToElementAndClick(cityDefaultBtn);
        }

        public StandardAssortimentPage ChooseCategory(string category, string subcategory1 = "", string subcategory2 = "")
        {
            Contract.Requires(category != null);
            var categoryElement = menuCatalog.TryFindElement(By.XPath($"./li/a//*[text()='{category}']"));
            if (subcategory1 != "")
            {
                new Actions(Browser.WebDriver).MoveToElement(categoryElement).Perform();
                ChooseSubcategories(categoryElement.TryFindElement(By.XPath("./ancestor::li")), subcategory1, subcategory2);
            }
            else
                Browser.scrollToElementAndClick(categoryElement);
            return new StandardAssortimentPage(Browser.WebDriver.Url);
        }

        private void ChooseSubcategories(IWebElement categoryElement, string subcategory1, string subcategory2 = "")
        {
            var subcategory1Element = categoryElement.TryFindElement(By.XPath($"./div/ul//*[text()='{subcategory1}']"));
            if (subcategory2 == "")
            {
                Browser.scrollToElementAndClick(subcategory1Element);
            }
            else
            {
                var subcategory2Element = subcategory1Element
                    .TryFindElement(By.XPath("./ancestor::li[1]"))
                        .TryFindElement(By.XPath($"./ul//*[text()='{subcategory2}']"));
                Browser.scrollToElementAndClick(subcategory2Element);
            }
        }
    }
}
