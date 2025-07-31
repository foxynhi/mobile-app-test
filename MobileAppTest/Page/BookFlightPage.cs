using OpenQA.Selenium.Appium.MultiTouch;

namespace MobileAppTest.Page
{
  public class BookFlightPage : BasePage
  {
    private By title = By.XPath("(//android.view.View[@content-desc=\"Book Flight\"])[1]");
    private By oneWayBtn = By.XPath("//android.view.View[contains(@content-desc,\"One Way\")]");
    private By depBtn = By.XPath("//android.view.View[contains(@hint, 'Input Depart')]");
    private By textInp = By.XPath("//android.widget.EditText");
    private By depCity = By.XPath("(//android.widget.Button[contains(@content-desc,\"Chiang Mai, Thailand\")])[1]");

    private By arrBtn = By.XPath("//android.view.View[contains(@hint, 'Input Arrival')]");
    private By arrCity = By.XPath("(//android.widget.Button[contains(@content-desc,\"Ho Chi Minh City, Vietnam\")])[1]");

    private By datePicker = By.XPath("//android.view.View[@content-desc=\"Departure date\"]");
    private By monthYear = By.XPath("//android.view.View[@content-desc="+ DateTime.Now.ToString("MMMM yyyy") + "]");
    private By day(string d)
    {
      int dayNumber;
      if (!int.TryParse(d, out dayNumber))
        throw new ArgumentException("Invalid day format: " + d);

      int today = DateTime.Today.Day;

      string xpath;
      if (dayNumber < today)
      {
        xpath = $"(//android.view.View[@content-desc=\"{d}\"])[2]";
      }
      else
      {
        xpath = $"//android.view.View[@content-desc=\"{d}\"]";
      }

      return By.XPath(xpath);
    } 
    private By dateSubmit = By.XPath("//android.widget.Button[@content-desc=\"Done\"]");
    private By searchBtn = By.XPath("//android.widget.Button[@content-desc=\"Search Flight\"]");


    public BookFlightPage(AndroidDriver driver) : base(driver) { }

    public bool VerifyBookFlighthPage()
    {
      ReportUtility.LogInfo("Verifying is on BookFlighthPage");
      return WaitUtility.WaitForElementVisible(title).Displayed;
    }
    public void FillInput()
    {
      ReportUtility.LogInfo("Filling input to book flight");

      WaitUtility.TryClick(oneWayBtn);
      ReportUtility.LogInfo("Choosing departure city");
      Click(depBtn);
      WaitUtility.TryClick(textInp);
      SendKeys(textInp, "Chiang Mai");
      try
      {
        WaitUtility.TryClick(depCity);
      }
      catch (StaleElementReferenceException e)
      {
        Console.WriteLine($"Element went stale, retrying. Message: {e.Message}");
        WaitUtility.TryClick(depCity);
      }

      ReportUtility.LogInfo("Choosing arrival city");
      Click(arrBtn);
      WaitUtility.TryClick(textInp);
      SendKeys(textInp, "Ho Chi Minh");
      WaitUtility.TryClick(arrCity);

      ReportUtility.LogInfo("Choosing date");
      WaitUtility.TryClick(datePicker);
      WaitUtility.TryClick(day(DateTime.Now.AddDays(1).ToString("dd")));

      WaitUtility.TryClick(dateSubmit);
      WaitUtility.TryClick(searchBtn);

      Thread.Sleep(6000);

      var contexts = driver.Contexts; 
      var webviewContext = contexts.FirstOrDefault(c => c.Contains("WEBVIEW"));
      if (webviewContext != null)
      {
        driver.Context = webviewContext;
        ReportUtility.LogInfo("Switched to: " + webviewContext + " driver context");

        By selectFlight = By.XPath("(//div[@class='basic-flight-card-layout-right-section-container'])[1]");
        By confirmBtn = By.XPath("//button[@class='continue']");
        ReportUtility.LogInfo("Selecting flight");
        WaitUtility.WaitForElementVisible(By.XPath("//span[text()='Select your departure']"), 20);
        WaitUtility.TryClick(selectFlight);
        WaitUtility.TryClick(confirmBtn);

        ReportUtility.LogInfo("Selecting guest title");
        By title = By.CssSelector("mat-select[formcontrolname='title']");
        WaitUtility.WaitForElementVisible(title, 30);
        ActionUtility.SwipeToElementJS(title);
        WaitUtility.TryClick(title, 10);
        WaitUtility.TryClick(By.XPath("//mat-option//span[text()='Mr']"));

        var firstName = By.CssSelector("input[formcontrolname='firstName'][aria-required=\"true\"]");
        ActionUtility.SwipeToElementJS(firstName);
        WaitUtility.TryClick(firstName);
        SendKeys(firstName, "John");

        var lastName = By.CssSelector("input[formcontrolname='lastName'][aria-required=\"true\"]");
        ActionUtility.SwipeToElementJS(lastName);
        WaitUtility.TryClick(lastName);
        SendKeys(lastName, "Adam");

        var DOB = By.CssSelector("input[formcontrolname='dob'][aria-required=\"true\"]");
        ActionUtility.SwipeToElementJS(DOB);
        WaitUtility.TryClick(DOB);
        SendKeys(DOB, "01012000");

        var maleBtn = By.CssSelector("input[type='radio'][value='male']");
        ActionUtility.SwipeToElementJS(maleBtn);
        WaitUtility.TryClick(maleBtn);

        var nationality = By.CssSelector("input[placeholder='Nationality']");
        ActionUtility.SwipeToElementJS(nationality);
        WaitUtility.TryClick(nationality);
        SendKeys(nationality, "Viet");
        WaitUtility.TryClick(By.XPath("//mat-option//span[text()='Vietnamese']"), 5);

        var docNumber = By.XPath("input[placeholder='Your document number']");
        ActionUtility.SwipeToElementJS(docNumber);
        WaitUtility.TryClick(docNumber);
        SendKeys(docNumber, "C3481043");

        var expDate = By.XPath("input[placeholder='Select the expiry date']");
        ActionUtility.SwipeToElement(expDate);
        WaitUtility.TryClick(expDate);
        SendKeys(expDate, "01012027");

        var docCountry = By.XPath("input[placeholder='Document issued by']");
        ActionUtility.SwipeToElementJS(docCountry);
        WaitUtility.TryClick(docCountry);
        SendKeys(docCountry, "Viet");
        WaitUtility.TryClick(By.XPath("//mat-option//span[contains(text(),'Viet')]"), 5);

        var emailInp = By.XPath("//label[contains(., 'Email') and not(contains(., 'Confirm'))]/ancestor::div[contains(@class,'mat-mdc-form-field-flex')]//input[@type='email']");
        ActionUtility.SwipeToElementJS(emailInp);
        WaitUtility.TryClick(emailInp);
        SendKeys(emailInp, "dinh71234@gmail.com");

        var confirmEmailInp = By.XPath("//label[contains(., 'Confirm email')]/ancestor::div[contains(@class,'mat-mdc-form-field-flex')]//input[@type='email']");
        ActionUtility.SwipeToElementJS(confirmEmailInp);
        WaitUtility.TryClick(confirmEmailInp);
        SendKeys(confirmEmailInp, "dinkhoi71234@gmail.com");

        var code = By.XPath("input[placeholder='Your country calling code']");
        ActionUtility.SwipeToElementJS(code);
        WaitUtility.TryClick(code);
        SendKeys(code, "Viet");
        WaitUtility.TryClick(By.XPath("//mat-option//span[contains(text(),'Viet')]"), 5);

        var phoneNumber = By.XPath("input[placeholder='Your mobile phone']");
        ActionUtility.SwipeToElementJS(phoneNumber);
        WaitUtility.TryClick(phoneNumber);
        SendKeys(phoneNumber, "0982136343");
      }
      else
      {
        Console.WriteLine("No WebView context found");
      }
    }
  }
}
