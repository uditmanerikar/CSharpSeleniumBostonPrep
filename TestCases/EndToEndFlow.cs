//using NUnit.Framework.Internal.Execution;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using RealTimeCsharpSelenium.PageClass;
using RealTimeCsharpSelenium.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;


namespace RealTimeCsharpSelenium
{
    //[Parallelizable(ParallelScope.Children)]//run all tests in this class parallley
    public class EndToEndFlow :BaseClass
    {

        [Test , TestCaseSource("AddtestDataConfig"),Category("Smoke")]
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshettyacademy", "learning")]//it would run twice with two data sets
        //[Parallelizable(ParallelScope.All)]
        public void Test01(String username,String password,String[] products)
        {
            //String[] products = { "iphone X", "Blackberry" };
           // String[] actualproducts = new string[products.Length];
            Loginpage lp=new Loginpage(getDriver());
            Console.WriteLine("first product is "+products[0]);
            Console.WriteLine("Second product is " + products[1]);

            productpage prod =lp.validlogin(username,password);
            prod.waitforpagedisplay();
            IList<IWebElement> list = prod.cardssearch();
            foreach (IWebElement element in list)
            {
                if (products.Contains(element.Text))
                {
                       
                        element.FindElement(prod.getaddtocart()).Click();
                    //very importatnt to put . before xpath that means it would only enter that product
                }
            }
            Checkoutpage c=prod.checkout();
            IList <IWebElement> checkoutcart = c.checkcart();
            String[] actualproducts = new string[checkoutcart.Count];
            for (int i = 0; i < checkoutcart.Count; i++) {
                actualproducts[i] = checkoutcart[i].Text;
            }
            Thread.Sleep(7000);

            CollectionAssert.AreEqual(products, actualproducts);
            String successtext = c.checkoutandpurcahse("ind");
            StringAssert.Contains("Success", successtext);

        }
        //[TearDown]
        //public void CloseBrowser()
        //{
          //  Thread.Sleep(7000);
            //driver.Value.Quit();
            //driver.Value.Dispose();

        //}

        public static IEnumerable<TestCaseData> AddtestDataConfig() //method which stores test data
        {
           yield return new TestCaseData(getdataParser().extarctdata("username") , 
            getdataParser().extarctdata("password"),
            getdataParser().extarctdataArray("products"));
           // yield return new TestCaseData(getdataParser().extarctdata("username"),
            //getdataParser().extarctdata("password"),
            //getdataParser().extarctdataArray("products1"));

        }

    }
}



