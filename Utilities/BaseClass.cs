using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using RealTimeCsharpSelenium.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using static WebDriverManager.DriverConfigs.Impl;
using SeleniumExtras.WaitHelpers;


namespace RealTimeCsharpSelenium.Utilities
{
    public class BaseClass
    {
        //public IWebDriver driver;
        ExtentReports extent;
        ExtentTest test;
        [OneTimeSetUp]
        public void report()
        {
            var spark = new ExtentSparkReporter("ExtentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(spark);
            extent.AddSystemInfo(" Host Name", "Local Host ");
            extent.AddSystemInfo(" Environment", "QA");

        }
        public static ThreadLocal<IWebDriver> driver = new();
        [SetUp]

        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            String Browsername = ConfigurationManager.AppSettings["browser"];
            Init(Browsername);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = ("https://rahulshettyacademy.com/loginpagePractise/");

        }
        public void Init(String browsername)
        {
            switch (browsername)
            {

                case "Chrome":
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    //   new WebDriverManager.DriverManager().SetUpDriver(new WebDriverManager.DriverConfigs.Impl.EdgeConfig());
                    driver.Value = new EdgeDriver();
                    // new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    //driver = new EdgeDriver();
                    break;
                case "Firefox":
                    driver.Value = new FirefoxDriver();
                    break;
                case "Opera":
                    driver.Value = new EdgeDriver();
                    break;
            }
        }
        public IWebDriver getDriver()
        {
            return driver.Value;
        }
        public static jsonReader getdataParser()
        {
            return new jsonReader();

        }

        [TearDown]
        public void tearDown()
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                test.Fail(
                    "Test Failed",
                    takescreenst(TestContext.CurrentContext.Test.Name)
                );
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
            driver.Value.Quit();
            driver.Value.Dispose();
            driver.Value = null;

        }
        public Media takescreenst(String screenshotname)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver.Value;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotname).Build();

        }

        public void safeclick(By locator)
        {
            WebDriverWait w = new WebDriverWait(driver.Value,TimeSpan.FromMicroseconds(5));
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    IWebElement e = w.Until(ExpectedConditions.ElementToBeClickable(locator));
                    e.Click();
                    return;
                }
                catch (StaleElementReferenceException)
                {
                    attempts++;
                }
                catch (ElementClickInterceptedException) {
                    attempts++;
                }
            }
        }
        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }

    

    } }