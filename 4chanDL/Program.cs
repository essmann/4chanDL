using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

class Program
{
    static void Main()
    {
        ChromeOptions options = new ChromeOptions();

        // Set up the ChromeDriver instance with proxy configuration using AddArgument
        options.AddArgument("--proxy-server=http://23.247.136.252:80");


        // Set up the ChromeDriver instance
        IWebDriver driver = new ChromeDriver(options);

        // Navigate to target website
        driver.Navigate().GoToUrl("http://ident.me");

        // Add a wait for three seconds
        Thread.Sleep(3000);

        // Select the HTML body
        IWebElement pageElement = driver.FindElement(By.TagName("body"));

        // Get and print the text content of the page
        string pageContent = pageElement.Text;
        Console.WriteLine(pageContent);

        // Close the browser
        driver.Quit();
    }
}
