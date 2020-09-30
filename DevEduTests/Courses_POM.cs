using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DevEduTests
{
    class Courses_POM
    {
        IWebDriver chrome;
        By _pageTitle = By.XPath("/html/body/div[2]/main/section[1]/div/h1");
        By _dniproCourses = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(1) > a");
        By _kyivCourses = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(2) > a ");
        By _bakuCourses = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(3) > a");
        By _stPeterburhCourses = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(4) > a");
        By _kharkivCourses = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(5) > a");
        By _dniproJavaCourse = By.XPath("/html/body/div[2]/main/section[1]/div/div/ul/li[1]/ul/li[1]/a");
        By _kyivQACourse = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(2) > ul > li:nth-child(1) > a");
        By _BakuBaseCourse = By.CssSelector("body > div.wrapper > main > section.origin-courses > div > div > ul > li:nth-child(3) > ul > li:nth-child(2) > a");

        public Courses_POM(IWebDriver driver)
        {
            this.chrome = driver;
        }

        public Courses_POM ClickOnDniproCourses()
        {
            chrome.FindElement(_dniproCourses).Click();
            return this;
        }
        

        public Courses_POM ClickOnCourse(string key)
        {
            switch (key)
            {
                case "D_Java":
                    chrome.FindElement(_dniproJavaCourse).Click();
                    break;
                case "K_QA":
                    chrome.FindElement(_kyivQACourse).Click();
                    break;
                case "B_Base":
                    chrome.FindElement(_BakuBaseCourse).Click();
                    break;
                default:
                    break;
            }
            return this;
        }
    }
}
