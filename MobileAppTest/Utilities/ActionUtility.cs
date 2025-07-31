
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
           .MoveByOffset(0, (int)(screenH * 0.5))
           .Pause(TimeSpan.FromMilliseconds(500))
           .Release()
           .Perform();
    }
    public static void SwipeUp()
    {
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
    public static void SwipeToElementJS(By locator)
    {
      ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(locator));
    }
    public static void ClickJS(By locator)
    {
      ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(locator));
    }
  }
}
