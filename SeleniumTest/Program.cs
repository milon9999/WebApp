using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    class Program
    {
        //need to copy chromedriver.exe from solution ChromeDriver folder to Bin folder
        private static IWebDriver _driver;
        static async Task Main(string[] args)
        {
            Thread.Sleep(3000);
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _driver.Navigate().GoToUrl("https://localhost:44332/");
            await TestSite();
            _driver.Close();
        }

        static async Task TestSite()
        {
            Thread.Sleep(500);
            var elem = _driver.FindElement(By.Id("Dates"));
            elem.Click();
            Thread.Sleep(500);
            elem.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            elem.SendKeys(Keys.Enter);
            Thread.Sleep(500);
            await ClickById("SelectDateButton");
            Thread.Sleep(500);
            elem = _driver.FindElement(By.Id("Amount"));
            elem.Click();
            Thread.Sleep(500);
            elem.SendKeys(Keys.Backspace);
            Thread.Sleep(500);
            elem.SendKeys("100");
            Thread.Sleep(500);
            elem = _driver.FindElement(By.Id("From"));
            elem.Click();
            Thread.Sleep(500);
            elem.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            elem.SendKeys(Keys.Enter);
            Thread.Sleep(500); 
            elem = _driver.FindElement(By.Id("To"));
            elem.Click();
            Thread.Sleep(500);
            elem.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            elem.SendKeys(Keys.ArrowDown);
            Thread.Sleep(500);
            elem.SendKeys(Keys.Enter);
            Thread.Sleep(500);
            await ClickById("CalculateButton");
            elem = _driver.FindElement(By.Id("Result"));
            if (elem.Text == "Result: 10501.321440370002 JPY")
                Console.WriteLine("Test is good");
        }

        static async Task ClickById(string idName, string typeText = null)
        {
            var elem = _driver.FindElement(By.Id(idName));
            elem.Click();
            if (!string.IsNullOrEmpty(typeText))
            {
                elem.SendKeys(typeText);
            }
            Thread.Sleep(500);
        }
    }
}
