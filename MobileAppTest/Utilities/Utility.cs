namespace MobileAppTest.Utilities
{
  public class Utility
  {
    protected static AndroidDriver driver;

    public static void SetUtility(AndroidDriver androidDriver)
    {
      if (androidDriver == null)
      {
        throw new ArgumentNullException(nameof(androidDriver), "Driver cannot be null");
      }
      driver = androidDriver;
    }
    public static bool IsElementDisplayed(By locator)
    {
      try
      {
        return driver.FindElement(locator).Displayed;
      }
      catch (NoSuchElementException)
      {
        return false;
      }
    }
  }
}
