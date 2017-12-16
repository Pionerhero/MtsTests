using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Threading;

namespace DnsTests
{
    static class WebExtensions
    {
        public static int timeIntervalmillisec = 5000;
        public static int pollIntervalMillisec = 200;

        public static IWebElement TryFindElement(this IWebElement webElement, By by)
        {
            IWebElement newWebElement = null;
            WaitUntil(() => { newWebElement = webElement.FindElement(by);
                              return newWebElement.Displayed == true; });
            return newWebElement;
        }

        public static bool WaitUntil(Func<bool> condition)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < timeIntervalmillisec)
            {
                if (condition())
                    break;
                Thread.Sleep(pollIntervalMillisec);
            }
            sw.Stop();
            return condition();
        }
    }
}

