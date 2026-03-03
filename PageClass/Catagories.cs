using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RealTimeCsharpSelenium.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RealTimeCsharpSelenium.PageClass
{
    public class catagories : BaseClass
    {
        private IWebDriver driver;
        private WebDriverWait wait;

      
        public catagories(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        private By name = By.XPath("//label//following-sibling::input[@name=\"name\"]");
        private By verifyname = By.XPath("//input[@class=\"ng-untouched ng-pristine ng-valid\"]");
        private By submit = By.XPath("//input[@class=\"btn btn-success\"]");
        private By sucessmesage = By.XPath("//div[@class=\"alert alert-success alert-dismissible\"]");

        public string verifysuccessmesage() {
            driver.FindElement(name).SendKeys("Udit");
            bool name1=driver.FindElement(verifyname).Displayed;
            driver.FindElement(submit).Click();
            IWebElement s = driver.FindElement(sucessmesage);
            return Regex.Replace(s.Text, @"\s", " ").Trim();





        }






    }
}