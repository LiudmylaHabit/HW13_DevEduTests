using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEduTests
{
    class Contacts_POM
    {
        IWebDriver chrome;

        public Contacts_POM(IWebDriver driver)
        {
            this.chrome = driver;

        }

        public Contacts_POM ClickButton(string key)
        {

            string cssSelector = String.Format("button[data-type='{0}']", key);
            By citySelection = By.CssSelector(cssSelector);
            chrome.Manage().Window.Maximize();
            IWebElement city = chrome.FindElement(citySelection);
            city.Click();
            return this;
        }

        public string GetEnteredContactsCity()
        {
            string cssSelector = "div[class='contacts-list__item entered']";
            By citySelection = By.CssSelector(cssSelector);
            chrome.Manage().Window.Maximize();
            IWebElement contacts = chrome.FindElement(citySelection);
            return contacts.GetAttribute("data-type");
        }

    }
}
