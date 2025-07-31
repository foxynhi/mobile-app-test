namespace MobileAppTest.Test
{
  public class FlightStatusTestByCity : BaseTest
  {
    [Test]
    public void GetFlightStatusByCityTest()
    {
      ReportUtility.LogInfo($"Starting Get Flight Status By City test.");
      homePage.GoToHomePage();
      var flightStatusPage = homePage.GoToFlightStatusPage();
      string rawStatus = flightStatusPage.GetFlightStatusByCity("Kuala Lumpur", "Ho Chi Minh City");
      string status = flightStatusPage.FormatFlightStatus(rawStatus);

      string[] lines = status.Split(new[] { "\n" }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        ReportUtility.LogInfo(line);
      }
    }
  }
}
