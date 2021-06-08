using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.IO;

namespace mds.Framework
{
    class BrowserFactory
    {
        private static IWebDriver webDriver;
        private static string browser = "chrome";

        public static void Init()
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    //TODO habilitar esta linea
                    //webDriver = new ChromeDriver();
                     webDriver = new ChromeDriver("C:/Users/bsanmigu/Desktop/Auto");
                    break;
                case "ie":
                    var internetExplorerDriverService = InternetExplorerDriverService.CreateDefaultService();
                    var internetExplorerOptions = new InternetExplorerOptions();
                    internetExplorerOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    internetExplorerOptions.EnableNativeEvents = false;
                    internetExplorerOptions.IgnoreZoomLevel = true;
                    webDriver = new InternetExplorerDriver(internetExplorerDriverService, internetExplorerOptions);
                    break;
                case "firefox":
                    webDriver = new FirefoxDriver();
                    break;
            }

            webDriver.Manage().Window.Maximize();
        }
        public static IWebDriver getDriver
        {
            get { return webDriver; }
        }
        public static void Goto(string url)
        {
            webDriver.Navigate().GoToUrl(url);
        }
        public static void Close()
        {
            webDriver.Quit();
        }
        public static void CloseDriver()
        {
            if (webDriver != null)
            {
                webDriver.Close();
                webDriver.Quit();
            }
        }
        public static string TakeScreenShot()
        {
            string currentDir = Environment.CurrentDirectory;
            string formattedDate = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day;
            string formattedTime = DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            string screenShotPath = Path.GetFullPath(Path.Combine(currentDir, @"Output\ScreenShot_" + formattedDate + "_" + formattedTime + ".png"));
            //TODO Crear carpeta output si no existe
            Screenshot ss = ((ITakesScreenshot)webDriver).GetScreenshot();
            ss.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);
            return screenShotPath;
        }


        public static void WaitPage(int time)
        {
            var wait = new WebDriverWait(getDriver, TimeSpan.FromMilliseconds(30));
            // Wait for the page to load
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
    }
}
