using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DevEduTests
{
    class NewsArticle_POM
    {
        IWebDriver chrome;
        By _newsTitle = By.ClassName("event-page__title");

        public NewsArticle_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public string ReadNewsTitle()
        {
            string title = chrome.FindElement(_newsTitle).Text;
            return title;
        }
    }
}
