using System.Xml.Linq;

namespace MobileAppTest.Base
{
  public class BasePage
  {
    protected AndroidDriver driver;

    public BasePage(AndroidDriver driver)
    {
      this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    public AndroidDriver GetDriver() => driver;

    public AppiumElement Find(By locator)
    {
      return driver.FindElement(locator);
    }
    public string GetText(By locator)
    {
      return Find(locator).Text;
    }
    public void Clear(By locator)
    {
      Find(locator).Clear();
    }
    public void Click(By locator)
    {
      try
      {
        Find(locator).Click();
      }
      catch (NoSuchElementException e)
      {
        Console.WriteLine($"{locator} not found. Exception: {e.Message}");
      }
      catch (WebDriverTimeoutException e)
      {
        Console.WriteLine($"{locator} not clickable within timeout. Exception: {e.Message}");
      }
      catch (Exception e)
      {
        Console.WriteLine($"Unexpected error on {locator}: {e.Message}");
      }
    }
    public void SendKeys(By locator, string text)
    {
      Find(locator).SendKeys(text);
    }
  }
}
