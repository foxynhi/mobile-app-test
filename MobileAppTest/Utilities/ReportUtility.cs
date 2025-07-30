using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace MobileAppTest.Utilities
{
  public class ReportUtility : Utility
  {
    private static ExtentReports extent;
    private static ExtentSparkReporter spark;
    private static ExtentTest test;

    public static void InitReport()
    {
      string path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..", "..", "..", ".." + "\\TestResults\\"));
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      string fileName = Path.Combine(path, $"{TestContext.CurrentContext.Test.MethodName}_{DateTime.Now:ddMMyyyy_HHmmss}.html");
      extent = new ExtentReports();
      spark = new ExtentSparkReporter(fileName);
      extent.AttachReporter(spark);
    }
    public static void CreateTest(string testName)
    {
      if (extent == null) InitReport();
      test = extent.CreateTest(testName);
    }
    public static void FlushReport()
    {
      extent?.Flush();
    }
    public static void LogInfo(string message)
    {
      test?.Info(message);
    }
    public static void LogFail(string message)
    {
      test?.Fail(message);
    }
    public static void LogPass(string message)
    {
      test?.Pass(message);
    }
    public static void LogScreenShot(string message, string img)
    {
      test?.Info(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(img).Build());
    }
  }
}
