using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeCsharpSelenium.PageClass
{
    public class Checkoutpage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Checkoutpage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        private By checkoutcart = By.XPath("//h4[@class=\"media-heading\"]");
        private By checkout = By.XPath("//button[@class=\"btn btn-success\"]");
        private By country = By.XPath("//input[@id=\"country\"]");
        private By purchase = By.XPath("//input[@value=\"Purchase\"]");
        private By successtext = By.XPath("//div[@class=\"alert alert-success alert-dismissible\"]");


        public IList<IWebElement> checkcart()
        {

            return driver.FindElements(checkoutcart);

        }
        public String checkoutandpurcahse(String countryname) { 
        driver.FindElement(checkout).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(country));
            driver.FindElement(country).SendKeys(countryname);
            driver.FindElement(purchase).Click();
            String Success = driver.FindElement(successtext).Text;
                return Success;
        }

    }
}
