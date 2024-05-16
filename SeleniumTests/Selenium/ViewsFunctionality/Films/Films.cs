using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestsSakila.Selenium.ViewsFunctionality.Films
{
    //base configuration for testing
    public abstract class BaseTest : IDisposable
    {
        protected IWebDriver driver;

        [SetUp]
        public void BaseSetup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void Teardown()
        {
            Dispose();
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
    public class Tests : BaseTest
    {
        [Test]
        public void Availability()
        {
            driver.Navigate().GoToUrl("https://localhost:7001/Film/IndexFilm");

            //Find an element
            try
            {
                // Esperar hasta 10 segundos para que el título esté visible en la página
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                // Cambia el selector By a lo que corresponda en tu caso: By.TagName("h1"), By.Id("titulo"), etc.
                IWebElement titulo = wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("h1")));

                // Comprobar el texto del título
                if (titulo.Text.Equals("Lista de Peliculas"))
                {
                    Console.WriteLine("El título está presente y es correcto.");
                }
                else
                {
                    Console.WriteLine("El título está presente, pero el texto no es el esperado.");
                }
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("El título no está presente en la página.");
            }
        }

    }
}
