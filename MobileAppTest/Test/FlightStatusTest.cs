using MobileAppTest.Page;
using Newtonsoft.Json.Linq;

namespace MobileAppTest.Test
{
  public class FlightStatusTest : BaseTest
  {
    [Test]
    public void GetFlightStatusTest()
    {
      ReportUtility.LogInfo($"Starting GetFlightStatus test.");
      homePage.GoToHomePage();
      var flightStatusPage = homePage.GoToFlightStatusPage();
      string rawStatus = flightStatusPage.GetFlightStatus("Kuala Lumpur", "Ho Chi Minh City");
      string status = flightStatusPage.FormatFlightStatus(rawStatus);

      string[] lines = status.Split(new[] { "\n" }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        ReportUtility.LogInfo(line);
      }
    }
  }
}
