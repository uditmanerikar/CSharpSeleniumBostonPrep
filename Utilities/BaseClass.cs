using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RealTimeCsharpSelenium.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using static WebDriverManager.DriverConfigs.Impl;


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

            String browsername = ConfigurationManager.AppSettings["browser"];
            String executionType = ConfigurationManager.AppSettings["executionType"];

            Init(browsername, executionType);

            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        public void Init(String browsername, String executionType)
        {
            if (executionType.ToLower().Equals("remote"))
            {
                String gridUrl = ConfigurationManager.AppSettings["gridUrl"];

                switch (browsername.ToLower())
                {
                    case "chrome":
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddArgument("--start-maximized");
                        driver.Value = new RemoteWebDriver(new Uri(gridUrl), chromeOptions);
                        break;

                    case "edge":
                        var edgeOptions = new EdgeOptions();
                        driver.Value = new RemoteWebDriver(new Uri(gridUrl), edgeOptions);
                        break;

                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        driver.Value = new RemoteWebDriver(new Uri(gridUrl), firefoxOptions);
                        break;
                }
            }
            else
            {
                switch (browsername.ToLower())
                {
                    case "chrome":
                        driver.Value = new ChromeDriver();
                        break;

                    case "edge":
                        driver.Value = new EdgeDriver();
                        break;

                    case "firefox":
                        driver.Value = new FirefoxDriver();
                        break;
                }
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
            if (driver.Value != null)
            {
                driver.Value.Quit();
                driver.Value.Dispose();
                driver.Value = null;
            }

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