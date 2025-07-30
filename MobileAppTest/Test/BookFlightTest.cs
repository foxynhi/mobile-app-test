using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppTest.Test
{
  public class BookFlightTest : BaseTest
  {
    [Test]
    public void BookFlight()
    {
      ReportUtility.LogInfo($"Starting BookFlight test.");
      homePage.GoToHomePage();
      var bookFlightPage = homePage.GoToBookFlightPage();
      Assert.That(bookFlightPage.VerifyBookFlighthPage(), Is.True);
      Console.WriteLine("Is at BookFlightPage.");
      bookFlightPage.FillInput();
    }
  }
}
