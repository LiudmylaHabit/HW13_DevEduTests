using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEduTests
{
    [TestFixture]
    class NewsPageTests
    {
        IWebDriver chrome;
        Footer_POM f_POM;
        News_POM news_POM;
        NewsArticle_POM newsArticle_POM;
        Header_POM h_POM;
        WebDriverWait wait; 

        [SetUp]
        public void OnOpen()
        {
            chrome = new ChromeDriver(@"D:\Selenium\chromedriver_win32");
            f_POM = new Footer_POM(chrome);
            news_POM = new News_POM(chrome);
            newsArticle_POM = new NewsArticle_POM(chrome);
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

        [Test (Description = "Checking correctnesss of navigation of AboutUs header button")]
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
            chrome.Navigate().GoToUrl("https://deveducation.com/events/");
            int fullscrNavElements = h_POM.CountNavElements();
            h_POM.ClickHamburgerButton("https://deveducation.com/events/");
            int smallScrNavElements = h_POM.CountNavElements();
            Assert.AreEqual(fullscrNavElements, smallScrNavElements);
            chrome.Close();
        }
                
        [Test(Description = "Checking correctness of hamburger button language menu")]
        public void CheckingHamburgerButtonLanguageMenu()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/events/");
            int fullscrNavElements = h_POM.CountNavElements();
            int smallScrNavElements = h_POM.ClickHamburgerButton("https://deveducation.com/events/").CountHamburgerLanguages();
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

        

        [Test(Description = "Checking accordance of news that open to link that user clicked")]
        public void CheckingCorrectnesOfNewsNavigation()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            string expectedTitle = news_POM.ReadFirstNewsTitle();
            news_POM.ClickOnFirstNews();
            string tabId = chrome.WindowHandles.Last();
            chrome.SwitchTo().Window(tabId);
            string openedArticleTitle = newsArticle_POM.ReadNewsTitle();
            Assert.AreEqual(expectedTitle, openedArticleTitle);
            chrome.Close();
        }

        [Test(Description = "Checking work of 'LoadMore' button")]
        public void CheckingLoadMoreBtn()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            int amountOfNewsBlocks = news_POM.CountNewsBlocks(); 
            int newAmountOfBlocks = news_POM.ClickOnLoadMoreBtn().WaitForLoadMoreBtn(wait).CountNewsBlocks();
            Assert.AreEqual(amountOfNewsBlocks, newAmountOfBlocks);
            chrome.Close();
            //Assert.Less(amountOfNewsBlocks, newAmountOfBlocks);
        }

        [Test (Description = "Checking correctnes of email subscription to newsletter")]
        public void SuccesSubscriptionQuery()
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            news_POM.FillEmailInput("test@user.com").ClickSendEmailBtn();
            string tabId = chrome.WindowHandles.Last();
            chrome.SwitchTo().Window(tabId);
            Assert.AreEqual("Подписка на рассылку", chrome.Title);
        }
        
        [TestCase("testtest.com", Description = "Checking email validation if email is without '@' symbol")]
        [TestCase("test@testcom", Description = "Checking email validation if email is without '.' symbol after'@' symbol")]
        [TestCase("@test.com", Description = "Checking email validation if email is without name")]
        [TestCase("", Description = "Checking email validation if email is empty")]
        [TestCase("   ", Description = "Checking email validation if email containes only spaces")]
        [TestCase("test@ test.com", Description = "Checking email validation if email containes spaces")]
        public void CheckingEmailValidationOnSubscription(string email)
        {
            chrome.Navigate().GoToUrl("https://deveducation.com/ua/events/");
            news_POM.FillEmailInput(email).ClickSendEmailBtn();
            bool errorMessage = news_POM.ReadMailErrorMessage();
            Assert.IsTrue(errorMessage);
        }
    }
}
