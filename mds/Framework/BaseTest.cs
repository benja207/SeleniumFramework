using System;
using NUnit.Framework;

namespace mds.Framework
{
    [TestFixture]
    class BaseTest
    {

        [OneTimeSetUp]
        public static void AssemblyInitialize()
        {
            AppDomain.CurrentDomain.FirstChanceException += (s, e) => LastException = e.Exception;
            Reporting.StartReport();
        }

        public static Exception LastException { get; private set; }

        [SetUp]
        public void InitTest()
        {
            Reporting.CreateTestLog();
            Data.GetDataValues();
            BrowserFactory.Init();
            BrowserFactory.Goto("https://www.saucedemo.com/");
        }

        [TearDown]
        public void ShutDown()
        {
            var e = LastException;
            String error = null;
            if (TestContext.CurrentContext.Result.FailCount > 0)
            {
                string SSPath = BrowserFactory.TakeScreenShot();
                //TestContext.CurrentContext.AddResultFile(SSPath);
                //Reporting.Reporter(e.InnerException.ToString(),Reporting.TestStatus.KO);
                error = e.InnerException.ToString();
                //Reporting.Reporter(Exception,Reporting.TestStatus.KO);
            }
            BrowserFactory.CloseDriver();
            Reporting.ClosePDFDoc(error);
        }

    }
}
