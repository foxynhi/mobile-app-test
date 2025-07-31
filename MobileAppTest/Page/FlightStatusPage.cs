using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppTest.Page
{
  public class FlightStatusPage : BasePage
  {
    public FlightStatusPage(AndroidDriver driver) : base(driver) { }
    private By day(string d)
    {
      return By.XPath("//android.view.View[@content-desc=" + d + "]");
    }
    public string GetFlightStatusyCity(string depCity, string arrCity)
    {
      ReportUtility.LogInfo("Choosing date");
      By Date = By.XPath("//android.view.View[@content-desc=\"Departure date\"]");
      WaitUtility.TryClick(Date);
      WaitUtility.TryClick(day(DateTime.Now.AddDays(1).ToString("dd")));
      WaitUtility.TryClick(By.XPath("//android.widget.Button[@content-desc=\"Done\"]"));

      ReportUtility.LogInfo("Choosing departure city");
      Click(By.XPath("//android.view.View[contains(@hint, 'Input Depart')]"));
      By textInp = By.XPath("//android.widget.EditText");
      WaitUtility.TryClick(textInp);
      SendKeys(textInp, depCity);
      By depInp = By.XPath("(//android.widget.Button[contains(@content-desc,'"+depCity+"')])[1]");
      try
      {
        WaitUtility.TryClick(depInp);
      }
      catch (StaleElementReferenceException e)
      {
        Console.WriteLine($"Element went stale, retrying. Message: {e.Message}");
        WaitUtility.TryClick(depInp);
      }

      ReportUtility.LogInfo("Choosing arrival city");
      Click(By.XPath("//android.view.View[contains(@hint, 'Input Arrival')]"));
      WaitUtility.TryClick(textInp);
      SendKeys(textInp, arrCity);
      By arrInp = By.XPath("(//android.widget.Button[contains(@content-desc,'"+arrCity+"')])[1]");
      try
      {
        WaitUtility.TryClick(arrInp);
      }
      catch (StaleElementReferenceException e)
      {
        Console.WriteLine($"Element went stale, retrying. Message: {e.Message}");
        WaitUtility.TryClick(arrInp);
      }

      WaitUtility.TryClick(By.XPath("//android.widget.Button[@content-desc=\"View Flight Status\"]"));
      ReportUtility.LogInfo("Loading flight status");
      var flightStatus = WaitUtility.WaitForElementVisible(By.XPath("//android.widget.ScrollView/android.view.View[1]//android.widget.ImageView"));
      var flightStatusText = flightStatus.GetAttribute("content-desc");
      return flightStatusText;
    }

    public string GetFlightStatusByFlightNo(string flightNo)
    {
      WaitUtility.TryClick(By.XPath("//android.view.View[contains(@content-desc,\"Flight Number\")]"));

      ReportUtility.LogInfo("Choosing date");
      By Date = By.XPath("//android.view.View[@content-desc=\"Departure date\"]");
      WaitUtility.TryClick(Date);
      WaitUtility.TryClick(day(DateTime.Now.AddDays(1).ToString("dd")));
      WaitUtility.TryClick(By.XPath("//android.widget.Button[@content-desc=\"Done\"]"));

      ReportUtility.LogInfo("Entering flight number");
      By textInp = By.XPath("//android.widget.EditText");
      WaitUtility.TryClick(textInp);
      SendKeys(textInp, flightNo);


      WaitUtility.TryClick(By.XPath("//android.widget.Button[@content-desc=\"View Flight Status\"]"));
      ReportUtility.LogInfo("Loading flight status");
      IWebElement flightStatus = null;
      string flightStatusText = "";
      try
      {
        flightStatus = WaitUtility.WaitForElementVisible(By.XPath("//android.widget.ScrollView/android.view.View[1]//android.widget.ImageView"));        
      }
      catch (WebDriverTimeoutException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (NoSuchElementException ex)
      {
        Console.WriteLine(ex.Message);
      }

      if (flightStatus == null)
      {
        try
        {
          flightStatus = WaitUtility.WaitForElementVisible(By.XPath("//android.widget.ScrollView/android.view.View[1]"));
        }
        catch (WebDriverTimeoutException ex)
        {
          Console.WriteLine(ex.Message);
        }
        catch (NoSuchElementException ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      if (flightStatus != null)
      {
        flightStatusText = flightStatus.GetAttribute("content-desc") ?? "";
      }

      return flightStatusText;
    }

    public string FormatFlightStatus(string raw)
    {
      if (raw.Contains("no flights"))
        return raw;

      string[] lines = raw.Split(new[] { "\n" }, StringSplitOptions.None);

      string GetValue(string[] arr, int index) =>
          (index < arr.Length && !string.IsNullOrWhiteSpace(arr[index])) ? arr[index].Trim() : "TBA";

      return
$@"Flight: {GetValue(lines, 0)} ({GetValue(lines, 1)})
From: {GetValue(lines, 2).Replace("Depart-", "")}   →   To: {GetValue(lines, 3).Replace("Arrival-", "")}
Depart Time:    {GetValue(lines, 4).Replace("Schedule ", "")}
Arrival Time:   {GetValue(lines, 6).Replace("Schedule ", "")} 
Duration:       {GetValue(lines, 5)}
Terminal:       {GetValue(lines, 7).Replace("Terminal ", "")} → {GetValue(lines, 8).Replace("Terminal ", "")}
Gate:           {GetValue(lines, 9).Replace("Gate ", "")}
Baggage Belt:   {GetValue(lines, 10).Replace("Baggage Belt ", "")}";
    }
  }
}
