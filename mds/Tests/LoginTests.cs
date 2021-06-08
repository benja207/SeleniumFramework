using NUnit.Framework;
using mds.Framework;

namespace mds.Tests
{
    [TestFixture]
    [Category("Login")]
    class LoginTests : BaseTest
    {
        [Test]
        [Category("TEST001")]
        public void Test001()
        {
            string user = Data.DataValue("user");
            string pass = Data.DataValue("pass");

            BasePage.home.Login(user, pass);
        }

        [Test]
        [Category("TEST002")]
        public void Test002()
        {
            string user = Data.DataValue("user");
            string pass = Data.DataValue("pass");

            BasePage.home.Login(user, pass);
        }
    }
}
