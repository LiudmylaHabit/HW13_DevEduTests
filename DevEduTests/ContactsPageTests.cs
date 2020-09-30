using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevEduTests
{
    [TestFixture]
    class ContactsPageTests
    {
        IWebDriver chrome;
        Footer_POM f_POM;
        Contacts_POM c_POM;
        Header_POM h_POM;
        WebDriverWait wait;

        [SetUp]
        public void OnOpen()
        {
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32");
            f_POM = new Footer_POM(chrome);
            c_POM = new Contacts_POM(chrome);
            h_POM = new Header_POM(chrome);
            wait = new WebDriverWait(chrome, TimeSpan.FromSeconds(20));
        }

        [TearDown]
        public void OnClose()
        {
            chrome.Quit();
        }

        // Checking correctnesss of navigation of header 

        // Checking correctnesss of navigation of header navbar
        [TestCase("courses", "Наши курсы")]
        [TestCase("graduates", "Наши выпускники")]
        [TestCase("events", "Новости")]
        [TestCase("blog", "Блог")]
        [TestCase("contacts", "Наши контакты")]
        public void ClickButton(string buttonKey, string expectedResult)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/events/");
            h_POM.ClickButton(buttonKey);
            IWebElement actualH1 = chrome.FindElement(By.TagName("h1"));
            Assert.AreEqual(expectedResult, actualH1.Text);
        }

        [Test(Description = "Checking correctnesss of navigation of AboutUs header button")]
        public void ClickAboutUsButton()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/events/");
            h_POM.ClickButton("about");
            IWebElement actualH2 = chrome.FindElement(By.TagName("h2"));
            Assert.AreEqual("О нас", actualH2.Text);
        }

        [Test(Description = "Checking correctness of hamburger button")]
        public void CheckingHamburgerButton()
        {
            chrome.Quit();
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--window-size=500,500");
            chrome = new ChromeDriver(option);
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            IWebElement hamburgerBtn = chrome.FindElement(By.ClassName("_header__burger"));
            hamburgerBtn.Click();
            List<IWebElement> eleCount = new List<IWebElement>(chrome.FindElements(By.CssSelector("._nav__link")));
            Assert.AreEqual(6, eleCount.Count);
        }

        [Test(Description = "Checking correctness of hamburger button language menu")]
        public void CheckingHamburgerButtonLanguageMenu()
        {
            chrome.Quit();
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--window-size=500,500");
            chrome = new ChromeDriver(option);
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            IWebElement hamburgerBtn = chrome.FindElement(By.ClassName("_header__burger"));
            hamburgerBtn.Click();
            List<IWebElement> eleCount = new List<IWebElement>(chrome.FindElements(By.CssSelector(".lang-switcher-header__link")));
            Assert.AreEqual(4, eleCount.Count);
        }              

        [TestCase("inst",
            Description = "Checking correctness of openning instagram link")]
        [TestCase("youTube",
            Description = "Checking correctness of openning YouTube link")]
        [TestCase("linkedIn",
            Description = "Checking correctness of openning LinkedIn link")]
        [TestCase("facebook",
            Description = "Checking correctness of openning Facebook link")]
        public void SocialMediaLinksNewTabOpenning(string key)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            f_POM.ClickOnLink(key);
            Assert.AreEqual(2, chrome.WindowHandles.Count);
            chrome.Close();
        }

        [TestCase("inst", "Международный IT колледж (@dev.education) • Фото и видео в Instagram",
            Description = "Checking correctness of instagram link")]
        [TestCase("youTube", "DevEducation - YouTube",
            Description = "Checking correctness of YouTube link")]
        [TestCase("linkedIn", "Зарегистрироваться | LinkedIn",
            Description = "Checking correctness of LinkedIn link")]
        [TestCase("facebook", "Dev.education - Главная | Facebook",
            Description = "Checking correctness of Facebook link")]
        public void CheckFooterLinks(string key, string expectedResult)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            f_POM.ClickOnLink(key);
            string tabId = chrome.WindowHandles.Last();
            chrome.SwitchTo().Window(tabId);
            Assert.AreEqual(expectedResult, chrome.Title);
            chrome.Close();
        }

        [TestCase("kyiv", "kyiv")]
        [TestCase("dnipro", "dnipro")]
        [TestCase("baku", "baku")]
        [TestCase("spb", "spb")]
        [TestCase("kharkiv", "kharkiv")]
        public void SelectCity(string clickButtonCitySelect, string expectedCity)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/contacts/");
            c_POM.ClickButton(clickButtonCitySelect);
            string actualCity = c_POM.GetEnteredContactsCity();            
            Assert.AreEqual(expectedCity, actualCity);
        }
    }
}
