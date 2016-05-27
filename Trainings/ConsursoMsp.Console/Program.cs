using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Threading;

namespace ConsursoMsp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("--incognito", "--ignore-certificate-errors");


                IWebDriver driver = new ChromeDriver(@"D:\Repos\Trainings\exc", options);
                driver.Navigate().GoToUrl("https://channel9.msdn.com/Blogs/MVP-Azure/Despliege-e-integracin-contnua-en-Azure ");
                IWebElement playElement = driver.FindElement(By.ClassName("playButtonImage"));
                playElement.Click();

                Thread.Sleep(1200000);
                //Thread.Sleep(5000);
                driver.Close();
            }
           
             
        }
    }
}
