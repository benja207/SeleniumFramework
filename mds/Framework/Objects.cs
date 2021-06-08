using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace mds.Framework
{
    public static class Objects
    {

        public static void ClickElement(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception e)
            {
                //Reporting.Reporter(e.ToString(), Reporting.TestStatus.KO);
                Assert.Fail(e.ToString());
            }
        }

        public static void WriteElement(this IWebElement element, string texto)
        {
            try
            {
                element.SendKeys(texto);
            }
            catch (Exception e)
            {
                //Reporting.Reporter(e.ToString(), Reporting.TestStatus.KO);
                Assert.Fail(e.ToString());
            }
        }

        public static void SelectItemByText(this IWebElement list, string item)
        {
            try
            {
                // var selectElement = new SelectElement(list);
                // selectElement.SelectByText(item);
            }
            catch (Exception e)
            {
                //Reporting.Reporter(e.ToString(), Reporting.TestStatus.KO);
                Assert.Fail(e.ToString());
            }
        }


        public static bool WaitElementVanished(this IWebElement elemento, int tiempoespera)
        {
            int intTiempoEsperado = 0;
            bool blnEncontrado = true;
            do
            {
                blnEncontrado = elemento.Displayed;
            } while (intTiempoEsperado < tiempoespera && blnEncontrado);
            return blnEncontrado;
        }
        public static void ScrollToElement(this IWebElement element)
        {
            // Create a javascript executor
            IJavaScriptExecutor js = BrowserFactory.getDriver as IJavaScriptExecutor;
            try
            {
                js.ExecuteScript("arguments[0].scrollIntoView({ behavior: 'auto', block: 'center', inline: 'center'});", element);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void ClickJS(this IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BrowserFactory.getDriver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public static void Wait(int tiempoespera)
        {
            System.Threading.Thread.Sleep(tiempoespera * 1000);
        }

        //TODO check this method
        //He creado este metodo para comprobar si un elemento esperado se muestra
        public static bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }
    }
}
