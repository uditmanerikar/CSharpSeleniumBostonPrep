using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace RealTimeCsharpSelenium.PageClass
{
    public class productpage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        public productpage(IWebDriver driver) { 
        this.driver= driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }
        private By  cards = By.XPath("//h4[@class=\"card-title\"]");
        private By AddCart = By.XPath(".//parent::div//following-sibling::div//button[@class=\"btn btn-info\"]");
        private By Checkout = By.PartialLinkText("Checkout"); 

        public void waitforpagedisplay()
        {
            WebDriverWait w = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            w.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.PartialLinkText("Checkout ")));
        }

        public IList<IWebElement> cardssearch()
        {

            return wait.Until(ExpectedConditions
              .VisibilityOfAllElementsLocatedBy(cards));

        }

        public By getaddtocart() {
            return AddCart;
        }

        public Checkoutpage checkout() 
        {
            driver.FindElement(Checkout).Click();
            return new Checkoutpage(driver);
        }
    }
}
