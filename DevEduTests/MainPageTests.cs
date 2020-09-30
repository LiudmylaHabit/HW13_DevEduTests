using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;

namespace DevEduTests
{
    [TestFixture]
    class MainPageTests
    {
        IWebDriver chrome;
        Footer_POM f_POM;
        Header_POM h_POM;
        MainPage_POM mp_POM;

        [SetUp]
        public void OnOpen()
        {
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32");
            f_POM = new Footer_POM(chrome);
            mp_POM = new MainPage_POM(chrome);
            h_POM = new Header_POM(chrome);
        }

        [TearDown]
        public void OnClose()
        {
            chrome.Quit();
        }

        [Test]
        public void GoToDevEducationMainPage()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            chrome.Manage().Window.Maximize();
            IWebElement mainText = chrome.FindElement(By.CssSelector("body > div.wrapper > main > section > div > div.main-home__info > h1"));

            Assert.AreEqual("Міжнародний IT-коледж", mainText.Text);
        }

        [Test]
        public void GoToDevEducationCourses()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            chrome.Manage().Window.Maximize();
            IWebElement h1 = chrome.FindElement(By.TagName("h1"));
            Assert.AreEqual("Наши курсы", h1.Text);
        }

        [TestCase("courses", "Наши курсы")]
        [TestCase("graduates", "Наши выпускники")]
        [TestCase("events", "Новости")]
        [TestCase("blog", "Блог")]
        [TestCase("contacts", "Наши контакты")]
        public void ClickButton(string buttonKey, string expectedResult)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/");
            h_POM.ClickButton(buttonKey);
            IWebElement actualH1 = chrome.FindElement(By.TagName("h1"));
            Assert.AreEqual(expectedResult, actualH1.Text);
        }

        [Test]
        public void ClickAboutUsButton()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/");
            h_POM.ClickButton("about");
            IWebElement actualH2 = chrome.FindElement(By.TagName("h2"));
            Assert.AreEqual("О нас", actualH2.Text);
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
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
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
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            f_POM.ClickOnLink(key);
            string tabId = chrome.WindowHandles.Last();
            chrome.SwitchTo().Window(tabId);
            Assert.AreEqual(expectedResult, chrome.Title);
            chrome.Close();
        }

        [Test(Description = "Checking correctness of hamburger button")]
        public void CheckingHamburgerButton()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            int fullscrNavElements = h_POM.CountNavElements();
            int smallScrNavElements = h_POM.ClickHamburgerButton("https://deveducation.com/ua/").CountNavElements();
            Assert.AreEqual(fullscrNavElements, smallScrNavElements);
            chrome.Close();
        }

        [Test(Description = "Checking correctness of hamburger button language menu")]
        public void CheckingHamburgerButtonLanguageMenu()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/");
            int fullscrNavElements = h_POM.CountNavElements();
            int smallScrNavElements = h_POM.ClickHamburgerButton("https://deveducation.com/ua/").CountHamburgerLanguages();
            Assert.AreEqual(4, smallScrNavElements);
            chrome.Close();
            //Assert.AreEqual(fullscrNavElements, smallScrNavElements.Count);
        }

        [Test(Description = "Checking correctness of navigation to branch site throught map pointers")]
        public void NavigateToPitersBranchPage()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/");
            string title = mp_POM.ClickOnStPitersburhMapPoint().ReadTitle();
            Assert.AreEqual("Санкт-Петербург", title);
            chrome.Quit();
        }

    }
}
