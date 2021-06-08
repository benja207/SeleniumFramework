using System;
using OpenQA.Selenium;
using mds.Framework;
using static mds.Framework.Objects;

namespace mds.PageObjects
{

    class HomePage : BasePage
    {

        IWebDriver driver = BrowserFactory.getDriver;

        protected IWebElement txtUser => driver.FindElement(By.Id("user-name"));
        protected IWebElement txtPass => driver.FindElement(By.Id("password"));
        protected IWebElement btnLogin => driver.FindElement(By.Id("login-button"));
        protected IWebElement loginFail => driver.FindElement(By.ClassName("error-button"));

        public void Login(String user, String pass)
        {
            WaitForPage(10);
            txtUser.WriteElement(user);
            txtPass.WriteElement(pass);
            btnLogin.ClickElement();
            Reporting.Report("Login correcto");
            Reporting.Report("Login correcto 2");
            //WaitSecs(5);
        }

        public void LoginFail(String user, String failPassword)
        {
            txtUser.WriteElement(user);
            txtPass.WriteElement(failPassword);
            btnLogin.ClickElement();
            IsElementVisible(loginFail);
            Reporting.Report("Login fallido, prueba correcta");
        }
    }
}
