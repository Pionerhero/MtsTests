using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;

namespace DnsTests.Pages
{
    abstract class PageBase
    {
        abstract public string Url { get; }

        public static void InitPage<T>(T pageClass) where T : PageBase
        {
            PageFactory.InitElements(Browser.WebDriver, pageClass);
        }

        public abstract void GoToPage();
    }
}
