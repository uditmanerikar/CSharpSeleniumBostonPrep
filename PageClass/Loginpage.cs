using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RealTimeCsharpSelenium.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;


//Modern Selenium frameworks prefer By locators with explicit waits because it provides better
//control over synchronization,
//avoids stale element issues caused by Page Factory’s lazy loading, and aligns with Selenium 4 best practices.”
namespace RealTimeCsharpSelenium.PageClass
{
    public  class Loginpage :BaseClass
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Loginpage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));


        }

        private By username = By.Id("username");
        private By password = By.XPath("//input[@id=\"password\"]");
        private By terms=By.XPath("//input[@name=\"terms\"]");
        private By Signin = By.XPath("//input[@name=\"signin\"]");


        public productpage validlogin(String name,String pwd)
        {
            driver.FindElement(username).SendKeys(name);
            driver.FindElement(password).SendKeys(pwd);
            driver.FindElement(terms).Click();
            // driver.FindElement(Signin).Click();
            safeclick(Signin);
            return new productpage(driver);
           

        }

    }
}
