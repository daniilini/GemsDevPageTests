using System;
using Xunit;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace GemsDevPageTests
{
    public class GemsDevPageTests
    {
        IWebDriver driver; 
        private readonly By productsButton = By.XPath("/html/body/header/div/div/div[3]/nav/ul/li[2]/a");
        private readonly By existingHrefTest = By.XPath("//*[@class='col-12 col-md-6 mb-5 mb-md-0 wow fadeInUp']");
        /*��� �������� � XPath id, ������ ��� css-��������� (� ������ �����) ������� ������ "element not interactable", 
        ������� � ������ ������ ��� ������ ������ ���� XPath. ������ �����, ����� ����������� � ����������� ��������� ������ Selenium � C#*/

        List<By> ListXPathOfHeading = new List<By>();
        List<string> ListNamesOfHeading = new List<string>();
        List<IWebElement> ListElementsWithHrefTest;

        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://gemsdev.ru");
            var button = driver.FindElement(productsButton);
            button.Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 400);");
            System.Threading.Thread.Sleep(500);
            js.ExecuteScript("window.scrollTo(0, 800);");
            System.Threading.Thread.Sleep(500);
            js.ExecuteScript("window.scrollTo(0, 1200);");
            System.Threading.Thread.Sleep(500);
            js.ExecuteScript("window.scrollTo(0, 1600);");
            System.Threading.Thread.Sleep(2000);

            ListXPathOfHeading.Add(By.XPath("//*[text()='GeoMeta']"));
            ListXPathOfHeading.Add(By.XPath("//*[text()='��������������� ������� ����������� ����������������� ������������']"));
            ListXPathOfHeading.Add(By.XPath("//*[text()='��������� ���������']"));
            ListXPathOfHeading.Add(By.XPath("//*[@class='wow fadeInUp']")); 
            //�� ����� �������� ������� ������� �������� ������ ������� - ���� ������ ������ �� ������

            foreach (var element in ListXPathOfHeading)
            {
                ListNamesOfHeading.Add(driver.FindElement(element).Text);
            }

            ListElementsWithHrefTest = driver.FindElements(By.XPath("//*[@class='btn btn-orange mt-4']")).ToList();
        }
        [Fact]
        public void ExistListOfHeadTest()
        {
            //arrange
            List<string> expected = new List<string>();
            expected.Add("GeoMeta");
            expected.Add("��������������� ������� ����������� ����������������� ������������");
            expected.Add("��������� ���������");
            expected.Add("������ ���� ��������");
            //act
            Setup();
            //assert
            bool isEqual = Enumerable.SequenceEqual(expected, ListNamesOfHeading);
            Assert.True(isEqual);
            driver.Close();
        }
        [Fact]
        public void ExistHrefTest()
        {
            //arrange
            string expected = "https://xn--c1aaceme9acfqh.xn--p1ai/";
            //act
            Setup();
            bool isExistHref = false;
            foreach (var element in ListElementsWithHrefTest)
            {
                if (element.GetAttribute("href").ToString() == expected)
                    isExistHref = true;
            }
            //assert
            Assert.True(isExistHref);
            driver.Close();
        }
    }
}
