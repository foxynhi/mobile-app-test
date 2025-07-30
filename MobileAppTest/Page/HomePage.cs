using System.Xml.Linq;

namespace MobileAppTest.Page
{
  public class HomePage : BasePage
  {
    public HomePage(AndroidDriver driver) : base(driver) { }

    private By title = By.Id("com.android.permissioncontroller:id/permission_message");
    private By denyNotiBtn = By.Id("com.android.permissioncontroller:id/permission_deny_button");
    private By denyLocationBtn = By.XPath("//android.widget.Button[@content-desc=\"Not now\"]");
    private By denyLocationText = By.Id("new UiSelector().description(\"Enable location for a better experience\")");
    private By locationConfirmBtn = By.XPath("//android.widget.Button[@content-desc=\"Let’s go\"]");
    private By instruction1 = By.XPath("//android.widget.Button[contains(@content-desc,'Continue')]");
    private By instruction2 = By.XPath("//android.widget.Button[@content-desc=\"Next\"]");
    private By instruction3 = By.XPath("(//android.widget.Button[@content-desc=\"Got it!\"])[2]");
    private By logo = By.XPath("//android.widget.ImageView[@content-desc=\"Logo\"]");

    private By bookFlightBtn = By.XPath("//android.view.View[contains(@content-desc,\"Book Flight\")]");


    private By moreWidget = By.XPath("//android.widget.ImageView[contains(@content-desc,\"go to More\")]");
    private By flightStatWidget = By.XPath("//android.widget.ImageView[contains(@content-desc,\"go to Flight Status\")]");

    public bool GoToHomePage()
    {
      ReportUtility.LogInfo("Clearing pop ups");
      try
      {
        //WaitUtility.TryClick(denyNotiBtn, 2);
        WaitUtility.TryClick(locationConfirmBtn, 5);
        WaitUtility.TryClick(denyLocationBtn, 2);
        WaitUtility.TryClick(instruction1, 2);
        WaitUtility.TryClick(instruction2, 2);
        WaitUtility.TryClick(instruction2, 2);
        WaitUtility.TryClick(instruction3, 2);

        if (WaitUtility.WaitForElementVisible(logo, 2) != null)
          return true;

        return false;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
        return false;
      }
    }
    public BookFlightPage GoToBookFlightPage ()
    {
      ReportUtility.LogInfo("Navigating to BookFlightPage");
      WaitUtility.TryClick(bookFlightBtn, 2);
      return new BookFlightPage(driver);
    }
    public FlightStatusPage GoToFlightStatusPage()
    {
      ReportUtility.LogInfo("Navigating to FlightStatusPage");
      WaitUtility.TryClick(moreWidget);
      WaitUtility.TryClick(flightStatWidget);
      return new FlightStatusPage(driver);
    }
  }
}
