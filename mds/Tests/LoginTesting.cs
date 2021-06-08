using mds.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mds.Tests
{
    [TestFixture]
    [Category("TestLogin")]
    class LoginTesting : BaseTest
    {
        [Test]
        [Category("Casca")]
        public void LoginFail()
        {
            string user = Data.DataValue("user");
            string failPassword = Data.DataValue("password");

            BasePage.home.LoginFail(user, failPassword);
        }
    }
}
