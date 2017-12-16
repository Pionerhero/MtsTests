using OpenQA.Selenium;
using System;
using System.Diagnostics.Contracts;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Interactions;

namespace DnsTests
{
    static class Browser
    {
        private static IWebDriver _webDriver;
        public static IWebDriver WebDriver
        {
            get
            {
                return _webDriver;
            }

            set
            {
                _webDriver = value;
            }
        }

        private static string _mainWindowHandler;
        public static string MainWindowHandler
        {
            get
            {
                return _mainWindowHandler;
            }

            set
            {
                _mainWindowHandler = value;
            }
        }

        public static Browsers SelectedBrowser
        {
            get
            {
                return Settings.Default.Browser;
            }
        }

        public static IWebDriver StartWebDriver()
        {
            Contract.Ensures(Contract.Result<IWebDriver>() != null);

            if (WebDriver != null) return WebDriver;

            switch (SelectedBrowser)
            {
                case Browsers.InternetExplorer:
                    WebDriver = StartInternetExplorer();
                    break;
                case Browsers.Firefox:
                    WebDriver = StartFirefox();
                    break;
                case Browsers.Chrome:
                    WebDriver = StartChrome();
                    break;
                default:
                    throw new Exception(string.Format("Unknown browser selected: {0}.", SelectedBrowser));
            }

            WebDriver.Manage().Window.Maximize();
            MainWindowHandler = WebDriver.CurrentWindowHandle;

            return WebDriver;
        }

        public static void Quit()
        {
            if (_webDriver == null) return;

            _webDriver.Quit();
            _webDriver = null;
        }

        public static void scrollToElementAndClick(IWebElement webelement)
        {
            Actions actions = new Actions(_webDriver);
            actions.MoveToElement(webelement).Click();
            var action = actions.Build();
            action.Perform();
        }

        public static void scrollDown()
        {
            ExecuteJavaScript("window.scrollBy(0,document.body.clientHeight)", "");
        }

        public static void scrollUp()
        {
            ExecuteJavaScript("window.scrollBy(0,-document.body.clientHeight)", "");
        }

        public static void WaitReadyStateAndAJAX()
        {
            Contract.Assume(WebDriver != null);
            var ready = WebExtensions.WaitUntil(() => (bool)ExecuteJavaScript("return document.readyState == 'complete'"))
                && WebExtensions.WaitUntil(() => (bool)ExecuteJavaScript("return (typeof($) === 'undefined') ? true : !$.active;"));
            Contract.Assert(ready);
        }

        public static object ExecuteJavaScript(string javaScript, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)WebDriver;

            return javaScriptExecutor.ExecuteScript(javaScript, args);
        }    

        private static InternetExplorerDriver StartInternetExplorer()
        {
            var internetExplorerOptions = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                InitialBrowserUrl = "about:blank",
                EnableNativeEvents = true
            };

            return new InternetExplorerDriver(Directory.GetCurrentDirectory(), internetExplorerOptions);
        }

        private static FirefoxDriver StartFirefox()
        {
            var firefoxProfile = new FirefoxProfile
            {
                AcceptUntrustedCertificates = true,
                EnableNativeEvents = true
            };

            return new FirefoxDriver(firefoxProfile);
        }

        private static ChromeDriver StartChrome()
        {
            var chromeOptions = new ChromeOptions();
            //var defaultDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\..\Local\Google\Chrome\User Data\Default";

            //if (Directory.Exists(defaultDataFolder))
            //{
            //    WaitHelper.Try(() => DirectoryHelper.ForceDelete(defaultDataFolder));
            //}

            return new ChromeDriver(Directory.GetCurrentDirectory(), chromeOptions);
        }
    }
}
