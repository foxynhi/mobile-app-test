using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppTest.Test
{
  public class HomePageTest : BaseTest
  {
    [Test]
    public void VerifyHomePage()
    {
      var result = homePage.GoToHomePage();
      Assert.That(result, Is.True);
    }
  }
}
