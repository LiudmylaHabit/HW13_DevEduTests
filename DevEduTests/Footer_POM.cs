using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEduTests
{
    class Footer_POM
    {
        IWebDriver chrome;
        By _instagramLink = By.XPath("/html/body/div[2]/footer/div/ul/li[3]/a/img");
        By _youTubeLink = By.XPath("/html/body/div[2]/footer/div/ul/li[5]/a/img");
        By _linkedInLink = By.XPath("/html/body/div[2]/footer/div/ul/li[6]/a/img");
        By _facebookLink = By.XPath("/html/body/div[2]/footer/div/ul/li[2]/a/img");

        public Footer_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public Footer_POM ClickOnLink(string key)
        {
            switch (key)
            {
                case "inst":
                    chrome.FindElement(_instagramLink).Click();
                    break;
                case "youTube":
                    chrome.FindElement(_youTubeLink).Click();
                    break;
                case "linkedIn":
                    chrome.FindElement(_linkedInLink).Click();
                    break;
                case "facebook":
                    chrome.FindElement(_facebookLink).Click();
                    break;
                default:
                    break;
            }
            return this;
        }


        //public Footer_POM ClickOnInstagramLink()
        //{
        //    WebDriverWait wait = new WebDriverWait(chrome, TimeSpan.FromSeconds(10));
        //    wait.Until(ExpectedConditions.ElementExists(_instagramLink));
        //    chrome.FindElement(_instagramLink).Click();
        //    return this;
        //}

        //public Footer_POM ClickOnYouTubeLink()
        //{
        //    WebDriverWait wait = new WebDriverWait(chrome, TimeSpan.FromSeconds(10));
        //    wait.Until(ExpectedConditions.ElementExists(_youTubeLink));
        //    chrome.FindElement(_youTubeLink).Click();
        //    return this;
        //}

        //public Footer_POM ClickOnLinkedInLink()
        //{
        //    chrome.FindElement(_linkedInLink).Click();
        //    return this;
        //}

        //public Footer_POM ClickOnFacebookLink()
        //{
        //    chrome.FindElement(_facebookLink).Click();
        //    return this;
        //}
    }
}
