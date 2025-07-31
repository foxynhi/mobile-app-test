namespace MobileAppTest.Test
{
  public class FlightStatusTestByFlightNo : BaseTest
  {
    [Test]
    public void GetFlightStatusByFlightNoTest()
    {
      ReportUtility.LogInfo($"Starting Get Flight Status By Flight Number test.");
      homePage.GoToHomePage();
      var flightStatusPage = homePage.GoToFlightStatusPage();
      string rawStatus = flightStatusPage.GetFlightStatusByFlightNo("MH750");
      string status = flightStatusPage.FormatFlightStatus(rawStatus);

      string[] lines = status.Split(new[] { "\n" }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        ReportUtility.LogInfo(line);
      }
    }
  }
}
