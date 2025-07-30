
namespace MobileAppTest.Utilities
{
  internal class ActionUtility : Utility
  {
    private static int screenW = driver.Manage().Window.Size.Width;
    private static int screenH = driver.Manage().Window.Size.Height;
    private static Actions actions()
    {
      return new Actions(driver);
    }

    public static void SwipeDown()
    {
      actions().MoveToLocation((int)(screenW * 0.5), (int)(screenH * 0.2))
           .ClickAndHold() 
           .Pause(TimeSpan.FromMilliseconds(200))
           .MoveByOffset(0, (int)(screenH * 0.8))
           .Pause(TimeSpan.FromMilliseconds(500))
           .Release()
           .Perform();
    }
    public static void SwipeUp()
    {
      //actions().MoveToLocation((int)(screenW * 0.5), (int)(screenH * 0.45))    // Move to starting point
      //     .ClickAndHold()                  // Simulate pointer down
      //     .Pause(TimeSpan.FromMilliseconds(200)) // Pause from the image
      //     .MoveByOffset(0, -(int)(screenH * 0.3))  // Move upward (negative Y offset)
      //     .Pause(TimeSpan.FromMilliseconds(200)) // Move duration
      //     .Release()                       // Simulate pointer up
      //     .Perform();
      new Actions(driver).MoveToLocation((int)(screenW * 0.5), (int)(screenH * 0.6))
             .ClickAndHold()
             .Pause(TimeSpan.FromMilliseconds(200))
             .MoveByOffset(0, -(int)(screenH * 0.5))
             .Pause(TimeSpan.FromMilliseconds(500))
             .Release()
             .Perform();
    }
    public static void SwipeToElement(By locator, int maxSwipes = 5)
    {
      for (int i = 0; i < maxSwipes; i++)
      {
        if (IsElementDisplayed(locator))
          return;

        ReportUtility.LogInfo("Swipe to element: " + locator);
        SwipeUp();
        Thread.Sleep(500);
      }

      throw new Exception("Element not found after swiping.");
    }
    public static void SwipeUntilClickable(By locator, int maxSwipes = 5, int swipeDelayMs = 500)
    {
      for (int i = 0; i < maxSwipes; i++)
      {
        try
        {
          var element = WaitUtility.WaitForElementClickable(locator, 1);
          element.Click();
          Console.WriteLine($"Element clicked after {i} swipe(s).");
          return;
        }
        catch (WebDriverTimeoutException)
        {
          Console.WriteLine($"Swipe {i + 1}: element not clickable yet, swiping up...");
          SwipeUp();
          Thread.Sleep(swipeDelayMs);
        }
        catch (Exception e)
        {
          Console.WriteLine($"Unexpected error: {e.Message}");
        }
      }
      throw new Exception("Failed to find and click the element after swiping.");
    }
  }
}
