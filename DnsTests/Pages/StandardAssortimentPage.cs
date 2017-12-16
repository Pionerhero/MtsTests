using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Diagnostics.Contracts;


namespace DnsTests.Pages
{
    class StandardAssortimentPage : PageBase
    {
        public enum OrderMode
        {
            Ascending = 1,
            Descending,
            ByName,
            Discussed,
            ByRating
        }

        public StandardAssortimentPage(string baseUrl)
        {
            this.baseUrl = baseUrl;
            InitPage(this);
        }

        [FindsBy(How = How.CssSelector, Using = 
            "#catalog-items-page > div.page-content-container > div.catalog-category-wrapper > div.catalog-items > div.catalog-items-list.view-list")]
        IWebElement pageItemList;

        [FindsBy(How = How.XPath,Using = "//div[@class='pagination']/span[text()='В конец']")]
        IWebElement inEndPageBtn;

        readonly string baseUrl;
        OrderMode currentOrder = OrderMode.Ascending; //default order
        int currentPage = 1; //default page

        public override string Url
        {
            get { return baseUrl + getCurrentUrlPostfix;}
        }

        public override void GoToPage()
        {
            Browser.WebDriver.Navigate().GoToUrl(Url);
        }

        public void GoToSubPage(int pageNumb)
        {
            Browser.WebDriver.Navigate().GoToUrl(baseUrl + getUrlPostfix(pageNumb,currentOrder));
            currentPage = pageNumb;
        }

        public void GoToLastPage()
        {
            IWebElement inEndElement = inEndPageBtn;
            Browser.scrollDown();
            Browser.scrollToElementAndClick(inEndElement);
            Browser.WaitReadyStateAndAJAX();
        }

        public void Sort(OrderMode order)
        {
            Browser.WebDriver.Navigate().GoToUrl(baseUrl + getUrlPostfix(1, order));
            currentOrder = order;
        }

        public string GetItemText(int itemNumb)
        {
            var item = GetItemElement(itemNumb);
            return item.Text;
        }

        public string GetLastItemText()
        {
            var item = GetLastItemElement();
            return item.Text;
        }

        #region Helper methods

        private string getUrlOrderPostfix(OrderMode order)
        {
            return $"order={(int)order}";
        }
        private string getUrlPagePostfix(int pageNumb)
        {
            return $"p={pageNumb}";
        }
        private string getUrlPostfix(int pageNumb, OrderMode order)
        {
            return $"?{getUrlPagePostfix(pageNumb)}&{getUrlOrderPostfix(order)}";
        }

        private string getCurrentUrlPostfix
        {
            get { return getUrlPostfix(currentPage, currentOrder); }
        }

        public IWebElement GetItemElement(int itemNumb)
        {
            Contract.Requires(itemNumb > 0);

            string itemSelector = $"./div[@data-position-index='{itemNumb}']";
            var item = pageItemList.TryFindElement(By.XPath(itemSelector));
            return item;
        }

        public IWebElement GetLastItemElement()
        {
            string itemSelector = $"./div[last()]";
            var item = pageItemList.TryFindElement(By.XPath(itemSelector));
            return item;
        }

        #endregion
    }
}
