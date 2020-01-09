using Allure.Commons;
using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;

namespace project_m03_task02.Utils
{
    class ScreenshotUtils
    {
        private static string DirPath = CreateNewDirectory();

        public static void Snap(IWebDriver driver)
        {
            try
            {                
                var shName = CreateFileName();
                var shPath = $"{DirPath}\\{shName}";
                Screenshot sh = ((ITakesScreenshot) driver).GetScreenshot();
                sh.SaveAsFile(shPath, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(shPath, shName);
                AllureLifecycle.Instance.AddAttachment(shPath, shName);                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CREATING OF SCREENSHOT FAILED {ex.Message}");
            }
        }

        private static string CreateNewDirectory()
        {
            string dirLocation;

            try
            {
                dirLocation = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.f")}";
                Directory.CreateDirectory(dirLocation);
                return dirLocation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CREATING OF DIRECTORY FAILED {ex.Message}");
                return $"C\\temp_{new Random().Next(1, 1000)}";
            }
        }

        private static string CreateFileName()
        {
            var ext = "png";

            try
            {                
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss.f");
                var testMethodName = TestContext.CurrentContext.Test.MethodName;                
                return ($"{testMethodName}_{timeStamp}.{ext}");                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CREATING OF FILENAME FAILED {ex.Message}");
                return $"defaultScreenshotName_{new Random().Next(1, 20000)}.{ext}";
            }
        }
    }
}
