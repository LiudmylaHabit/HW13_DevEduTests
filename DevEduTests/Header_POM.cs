using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEduTests
{
    class Header_POM
    {
        IWebDriver chrome;
        By _buttonCourses = By.CssSelector("a[class='_nav__link'][href='/courses/']");
        By _buttonGraduates = By.CssSelector("a[class='_nav__link'][href='/graduates/']");
        By _buttonNews = By.CssSelector("a[class='_nav__link'][href='/events/']");
        By _buttonBlog = By.CssSelector("a[class='_nav__link'][href='/blog/']");
        By _buttonAboutUs = By.CssSelector("a[class='_nav__link'][href='/about/']");
        By _buttonContacts = By.CssSelector("a[class='_nav__link'][href='/contacts/']");
        By _navElements = By.CssSelector("._nav__link");
        By _hamburgerBtn = By.ClassName("_header__burger");
        By _hamburgerLanguages = By.ClassName("lang-switcher-header__link");

        public Header_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public Header_POM ClickButton(string key)
        {
            string cssSelector = String.Format("a[class='_nav__link'][href='/{0}/']", key);
            By buttonBy = By.CssSelector(cssSelector);
            chrome.Manage().Window.Maximize();
            IWebElement button = chrome.FindElement(buttonBy);
            button.Click();
            return this;
        }

        public Header_POM ClickAboutUsButton()
        {
            chrome.Manage().Window.Maximize();
            chrome.FindElement(_buttonAboutUs).Click();
            return this;
        }

        public int CountNavElements()
        {
            List<IWebElement> navElements = new List<IWebElement>(chrome.FindElements(_navElements));
            return navElements.Count;
        }

        public Header_POM ClickHamburgerButton(string url)
        {
            chrome.Quit();
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--window-size=500,500");
            chrome = new ChromeDriver(option);
            chrome.Navigate().GoToUrl(url);
            chrome.FindElement(_hamburgerBtn).Click();
            return this;
        }

        public int CountHamburgerLanguages()
        {
            List<IWebElement> lengugeElements = new List<IWebElement>(chrome.FindElements(_hamburgerLanguages));
            return lengugeElements.Count;
        }
    }
}
