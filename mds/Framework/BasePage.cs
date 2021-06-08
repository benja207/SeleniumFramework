using mds.PageObjects;

namespace mds.Framework
{
    class BasePage
    {

        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            return page;
        }

        public static void WaitSecs(int time)
        {
            Objects.Wait(time);
        }

        public static void WaitForPage(int time)
        {
            BrowserFactory.WaitPage(time);
        }

        public static HomePage home => GetPage<HomePage>();
        public static ProductsPage products => GetPage<ProductsPage>();

    }
}
