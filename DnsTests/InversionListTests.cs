using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using DnsTests.Pages;

namespace DnsTests
{
    [TestClass]
    public class InversionListTests
    {
        /// <summary>
        /// Categories data for tests
        /// </summary>
        //public Dictionary<int, string> menuCatalogTextCategories = new Dictionary<int, string>
        //{
        //    {0, "Ноутбуки и планшеты" },
        //    {1, "Компьютеры и периферия" },
        //    {2, "Комплектующие для ПК" },
        //    {3, "Смартфоны и смарт-часы" },
        //    {4, "Телевизоры и медиа" },
        //    {5, "Игры и приставки" },
        //    {6, "Аудиотехника" },
        //    {7, "Фото-видеоаппаратура" },
        //    {8, "Офисная техника и мебель" },
        //    {9, "Сетевое оборудование" },
        //    {10," Автотовары" },
        //    {11, "Крупная бытовая техника" },
        //    {12, "Товары для кухни" },
        //    {13, "Красота и здоровье" },
        //    {14, "Товары для дома" },
        //    {15, "Инструменты" },
        //    {16, "Услуги" },
        //    {17, "Собери свой компьютер" },
        //    {18, "Акции" },
        //    {19, "Уцененные товары" }
        //};

        IWebDriver CurrentBrowser;

        [TestInitialize]
        public void TestInit()
        {
            Browser.StartWebDriver();
            CurrentBrowser = Browser.WebDriver;
        }

        [TestMethod]
        public void InversionListLaptopTest()
        {
            #region TestData
            string lowerPriceProductModel1 = "";
            string lowerPriceProductModel2 = "";
            #endregion

            DnsIndexPage dnsIndexPage = new DnsIndexPage();
            dnsIndexPage.GoToPage();
            dnsIndexPage.ConfirmDefaultCity();
            var assortimentPage = dnsIndexPage.ChooseCategory("Ноутбуки и планшеты", "Ноутбуки"); //("Ноутбуки и планшеты", "Ноутбуки", "Нетбуки")
            assortimentPage.Sort(StandardAssortimentPage.OrderMode.Ascending);

            lowerPriceProductModel1 = GetModelFromItemText(assortimentPage.GetItemText(1));

            assortimentPage.Sort(StandardAssortimentPage.OrderMode.Descending);
            assortimentPage.GoToLastPage();

            lowerPriceProductModel2 = GetModelFromItemText(assortimentPage.GetLastItemText());

            Assert.AreEqual(lowerPriceProductModel1, lowerPriceProductModel2);
        }

        [TestCleanup]
        public void TestClean()
        {
            Browser.Quit();
        }

        private static string GetModelFromItemText(string itemText)
        {
            return itemText.Substring(0, itemText.IndexOf(Environment.NewLine));
        }
    }
}
