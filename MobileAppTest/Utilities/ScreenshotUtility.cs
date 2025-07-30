using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppTest.Utilities
{
  public class ScreenshotUtility : Utility
  {
    public static string TakeScreenshotAsBase64()
    {
      var img = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
      return img;
    }

    public static string TakeScreenshotToFile(string methodName)
    {
      string timestamp = DateTime.Now.ToString("ddMMyyyy_HHmmss");

      string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "TestResults", "Screenshots\\");
      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      string fileName = Path.Combine(path, $"{methodName}_{timestamp}.png");
      //Console.WriteLine($"Screenshot file path: {fileName}");
      Directory.CreateDirectory(Path.GetDirectoryName(fileName));

      var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
      screenshot.SaveAsFile(fileName);

      ReportUtility.LogInfo($"Screenshot is saved at: {Path.GetFullPath(fileName)}");

      return fileName;
    }
  }
}
