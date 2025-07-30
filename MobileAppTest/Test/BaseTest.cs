using NUnit.Framework.Interfaces;

namespace MobileAppTest.Test
{
  [TestFixture]
  public class BaseTest
  {
    protected AndroidDriver? driver;
    protected string? userName;
    protected string? accessKey;
    protected string? appId;
    protected HomePage? homePage;

    [SetUp]
    public void SetUp()
    {
      var appiumOptions = new AppiumOptions();

      appiumOptions.DeviceName = "Pixel 9";
      appiumOptions.PlatformName = "Android";
      appiumOptions.PlatformVersion = "16.0";

      appiumOptions.AutomationName = "UiAutomator2";
      appiumOptions.AddAdditionalAppiumOption("appPackage", "aero.sita.lab.resmobileweb.android.mh");
      appiumOptions.AddAdditionalAppiumOption("appActivity", "com.example.mh_app.MainActivity");
      appiumOptions.AddAdditionalAppiumOption("autoGrantPermissions", true);
      appiumOptions.AddAdditionalAppiumOption("adbExecTimeout", 30000);
      appiumOptions.AddAdditionalAppiumOption("enforceXPath1", true);
      appiumOptions.AddAdditionalAppiumOption("chromedriver_autodownload", true);
      appiumOptions.AddAdditionalAppiumOption("chromedriverAutodownload", true);
      appiumOptions.AddAdditionalAppiumOption("chromedriverExecutable", @"C:\Users\khanh\Downloads\chromedriver-win64\chromedriver-win64\chromedriver.exe");

      driver = new AndroidDriver(new Uri("http://127.0.0.1:4723/"), appiumOptions);
      ReportUtility.InitReport();
      ReportUtility.CreateTest(TestContext.CurrentContext.Test.MethodName);
      Utility.SetUtility(driver);
      homePage = new HomePage(driver!);
    }

    [TearDown]
    public void TearDown()
    {
      EndTest();
      ReportUtility.FlushReport();
      if (driver != null)
      {
        driver.Quit();
      }
    }
    private void EndTest()
    {
      var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
      var message = TestContext.CurrentContext.Result.Message;

      switch (testStatus)
      {
        case TestStatus.Passed:
          ReportUtility.LogPass("Test passed");
          break;
        case TestStatus.Failed:
          ReportUtility.LogFail($"Test failed: {message}");
          ReportUtility.LogScreenShot("Screenshot is logged at", ScreenshotUtility.TakeScreenshotAsBase64());
          ScreenshotUtility.TakeScreenshotToFile(TestContext.CurrentContext.Test.MethodName);
          break;
        case TestStatus.Skipped:
          ReportUtility.LogInfo($"Test skipped: {message}");
          break;
        default:
          ReportUtility.LogInfo("Test completed with unknown status");
          break;
      }
      ReportUtility.LogInfo("Test ended");
    }
  }
}
