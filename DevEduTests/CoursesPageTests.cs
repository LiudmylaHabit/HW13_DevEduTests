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
    class CoursesPageTests
    {
        IWebDriver chrome;
        Footer_POM f_POM;
        Courses_POM courses_POM;
        Header_POM h_POM;
        WebDriverWait wait;

        [SetUp]
        public void OnOpen()
        {
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32");
            f_POM = new Footer_POM(chrome);
            courses_POM = new Courses_POM(chrome);
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
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            h_POM.ClickButton(buttonKey);
            IWebElement actualH1 = chrome.FindElement(By.TagName("h1"));
            Assert.AreEqual(expectedResult, actualH1.Text);
        }

        [Test(Description = "Checking correctnesss of navigation of AboutUs header button")]
        public void ClickAboutUsButton()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            h_POM.ClickButton("about");
            IWebElement actualH2 = chrome.FindElement(By.TagName("h2"));
            Assert.AreEqual("О нас", actualH2.Text);
        }

        [Test(Description = "Checking correctness of hamburger button")]
        public void CheckingHamburgerButton()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            int fullscrNavElements = h_POM.CountNavElements();
            h_POM.ClickHamburgerButton("https://deveducation.com/courses/");
            int smallScrNavElements = h_POM.CountNavElements();
            Assert.AreEqual(fullscrNavElements, smallScrNavElements);
            chrome.Close();
        }

        [Test(Description = "Checking correctness of hamburger button language menu")]
        public void CheckingHamburgerButtonLanguageMenu()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            int fullscrNavElements = h_POM.CountNavElements();
            int smallScrNavElements = h_POM.ClickHamburgerButton("https://deveducation.com/courses/").CountHamburgerLanguages();
            Assert.AreEqual(4, smallScrNavElements);
            chrome.Close();
            //Assert.AreEqual(fullscrNavElements, smallScrNavElements.Count);
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
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/courses/");
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
            chrome.Navigate().GoToUrl("https://deveducation.com/courses/");
            f_POM.ClickOnLink(key);
            string tabId = chrome.WindowHandles.Last();
            chrome.SwitchTo().Window(tabId);
            Assert.AreEqual(expectedResult, chrome.Title);
            chrome.Close();
        }

        [TestCase("D_Java", "Курси Java - навчання програмування на Java у Дніпрі | DevEducation")]
        [TestCase("K_QA", "Курси QA - курси для тестувальників навчання у Києві | DevEducation")]
        [TestCase("B_Base", "Базовый курс: программирование для начинающих в Баку | DevEducation")]
        public void CheckingNavigationToCourse(string key, string expectedResult)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/courses/");
            courses_POM.ClickOnCourse(key);
            Assert.AreEqual(expectedResult, chrome.Title);
            chrome.Close();
        }
    }
}
