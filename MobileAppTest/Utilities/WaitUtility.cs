using OpenQA.Selenium.Support.UI;

namespace MobileAppTest.Utilities
{
  internal class WaitUtility : Utility
  {
    public static WebDriverWait Wait(int seconds = 10) => new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
    public static AppiumElement WaitForElementVisible(By locator, int seconds = 10)
    {
      try
      {
        return (AppiumElement)Wait(seconds).Until(ExpectedConditions.ElementToBeClickable(locator));
      }
      catch (WebDriverTimeoutException)
      {
        return null;
      }
      catch (NoSuchElementException)
      {
        return null;
      }
    }
    public static IWebElement WaitForElementClickable(By locator, int seconds = 10)
    {
      try
      {
        return (AppiumElement)Wait(seconds).Until(ExpectedConditions.ElementToBeClickable(locator));
      }
      catch (WebDriverTimeoutException)
      {
        return null;
      }
      catch (NoSuchElementException)
      {
        return null;
      }
    }
    public static void TryClick(By locator, int seconds = 5)
    {
      try
      {
        var element = Wait(seconds).Until(ExpectedConditions.ElementToBeClickable(locator));
        element.Click();
      }
      catch (WebDriverTimeoutException)
      {
        Console.WriteLine($"Element not clickable within {seconds}s: {locator}");
      }
      catch (NoSuchElementException)
      {
        Console.WriteLine($"Element not found: {locator}");
      }
    }
  }
}
