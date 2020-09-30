using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DevEduTests
{
    class News_POM
    {
        IWebDriver chrome;
        By _firstNewsBlockPath = By.CssSelector("body > div.wrapper > main > div > div > div.container.blog-archive > div > ul > li:nth-child(1)");
        By _firstNewsBlockTitle = By.CssSelector("body > div.wrapper > main > div > div > div.container.blog-archive > div > ul > li:nth-child(1) > figure > figcaption > h3 > a");
        By _newsBlocks = By.ClassName("events-list__item");
        By _loadMoreBtn = By.ClassName("button-more-news-blog");
        By _emailInput = By.CssSelector(".validate.form__input");
        By _emailSubmitBtn = By.CssSelector("button[class= 'btn-color btn'][id='blog_btn']");
        By _emailErrorMessage = By.CssSelector("div[data - error= 'Введите правильную почту']");
        By _emailErrorMessageXP = By.XPath("/html/body/div[2]/main/div/div/div[1]/div/div[3]/div/div/form/div");

        public News_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public News_POM ClickOnFirstNews()
        {
            chrome.FindElement(_firstNewsBlockTitle).Click();
            return this;
        }

        public string ReadFirstNewsTitle()
        {
            string title = chrome.FindElement(_firstNewsBlockTitle).Text;
            return title;
        }

        public News_POM ClickOnLoadMoreBtn()
        {
            chrome.FindElement(_loadMoreBtn).Click();
            return this;
        }

        public int CountNewsBlocks()
        {
            List<IWebElement> newsBlocks = new List<IWebElement>(chrome.FindElements(_newsBlocks));
            int amount = newsBlocks.Count;
            return amount;
        }

        public News_POM WaitForLoadMoreBtn(WebDriverWait wait)
        {
            wait.Until(ExpectedConditions.ElementExists(_loadMoreBtn));
            return this;
        }

        public News_POM FillEmailInput(string mail)
        {
            chrome.FindElement(_emailInput).SendKeys(mail);
            return this;
        }

        public News_POM ClickSendEmailBtn()
        {
            chrome.FindElement(_emailSubmitBtn).Click();
            return this;
        }

        public bool ReadMailErrorMessage()
        {
            bool error = chrome.FindElement(_emailErrorMessageXP).Enabled; //GetAttribute("data-error");
            return error;
        }
    }
}
