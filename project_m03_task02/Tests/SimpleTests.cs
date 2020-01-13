using Allure.Commons;
using NUnit.Allure.Core;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using project_m03_task02.Utils;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace project_m03_task02.Tests
{    
    [TestFixture]
    [AllureNUnit]
    class SimpleTests
    {
        IWebDriver _driver;        
        private string _homeURL = "https://www.onliner.by/";

        [OneTimeSetUp]
        public void OnceSetUp()
        {
            Environment.SetEnvironmentVariable(
                AllureConstants.ALLURE_CONFIG_ENV_VARIABLE,
                Path.Combine(Environment.CurrentDirectory, AllureConstants.CONFIG_FILENAME));

            AllureLifecycle.Instance.CleanupResultDirectory();

            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.option.AddArguments( "--headless","--disable-gpu", "--no-sandbox" );
            _driver.Manage().Window.FullScreen();
            _driver.Navigate().GoToUrl(_homeURL);
        }

        [OneTimeTearDown]
        public void OnceTearDown()
        {
            _driver.Quit();

            CommandLineRunner.ExecuteScript();
        }

        [TearDown]
        public void ScreenShot()
        {
            if(TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                ScreenshotUtils.Snap(_driver);
            }
        }

        [Test]
        public void TitleTest1()
        {
            var expected = "Onliner";
            var actual = _driver.Title;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TitleTest2()
        {
            var expected = "Онлайнер";
            var actual = _driver.Title;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ButtonTest1()
        {
            var tabName = "Вход";
            var tabElements = _driver.FindElements(By.XPath("//div[@class='auth-bar__item auth-bar__item--text']"));
            var tabToSelect = tabElements.FirstOrDefault(el => el.Text.Trim() == tabName);
            Assert.IsNotNull(tabToSelect, $"Can't find tab {tabName}");
        }

        [Test]
        public void ButtonTest2()
        {
            var tabName = "Enter";
            var tabElements = _driver.FindElements(By.XPath("//div[@class='auth-bar__item auth-bar__item--text']"));
            var tabToSelect = tabElements.FirstOrDefault(el => el.Text.Trim() == tabName);
            Assert.IsNotNull(tabToSelect, $"Can't find tab {tabName}");
        }
    }
}
