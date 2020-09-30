using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DevEduTests
{
    class MainPage_POM
    {
        IWebDriver chrome;
        By _stPitersburhMapPoint = By.CssSelector(".map-pointer.piter-point");
        By _stPitersburhTitle = By.LinkText("Санкт-Петербург");


        public MainPage_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public MainPage_POM ClickOnStPitersburhMapPoint()
        {
            chrome.FindElement(_stPitersburhMapPoint).Click();
            return this;
        }

        public string ReadTitle()
        {
            string title = chrome.FindElement(_stPitersburhTitle).Text;
            return title;
        }
    }
}