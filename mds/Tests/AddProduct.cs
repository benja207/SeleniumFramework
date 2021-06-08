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
    [Category("Add item")]
    class AddProduct : BaseTest
    {
        [Test]
        [Category("Test1")]
        public void Test1()
        {
            string user = "standard_user";
            string pass = "secret_sauce";
            BasePage.home.Login(user, pass);
            BasePage.products.addItem();
            
        }
    }
}
