using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests;

[TestClass]
public class WebFormularioDesafioTest
{
    private IWebDriver driver = null!;

    [TestInitialize]
    public void Iniciar()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(
            "https://www.selenium.dev/selenium/web/web-form.html");
    }

    [TestCleanup]
    public void Finalizar()
    {
        driver?.Quit();
    }

    [TestMethod]
    public void CadastroCompleto()
    {
        driver.FindElement(By.Name("my-text"))
            .SendKeys("Yasmin Aparecida Souza de Paula");

        driver.FindElement(By.Name("my-password"))
            .SendKeys("Teste@2026Seguro");

        driver.FindElement(By.Name("my-textarea"))
            .SendKeys("Estou aprendendo a usar testes automatizados com Selenium.");

        var campo = driver.FindElement(By.Name("my-readonly"));
        Assert.AreEqual("Readonly input", campo.GetAttribute("value"));

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        Console.WriteLine("URL atual: " + driver.Url); Console.WriteLine("Titulo: " + driver.Title); Console.WriteLine("Conteudo: " + driver.FindElement(By.TagName("body")).Text); Assert.AreEqual("Received!", AguardarConfirmacao());
    }

    [TestMethod]
    public void SelecionarDropdownEDatalist()
    {
        var dropdown = new SelectElement(
            driver.FindElement(By.Name("my-select")));

        dropdown.SelectByText("Two");

        Assert.AreEqual("2",
            dropdown.SelectedOption.GetAttribute("value"));

        driver.FindElement(By.Name("my-datalist"))
            .SendKeys("Seattle");

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        Console.WriteLine("URL atual: " + driver.Url); Console.WriteLine("Titulo: " + driver.Title); Console.WriteLine("Conteudo: " + driver.FindElement(By.TagName("body")).Text); Assert.AreEqual("Received!", AguardarConfirmacao());
    }

    [TestMethod]
    public void AlterarCheckboxRadioECor()
    {
        var checkbox1 = driver.FindElement(By.Id("my-check-1"));
        Assert.IsTrue(checkbox1.Selected);

        var checkbox2 = driver.FindElement(By.Id("my-check-2"));
        checkbox2.Click();
        Assert.IsTrue(checkbox2.Selected);

        var radio = driver.FindElement(By.Id("my-radio-2"));
        radio.Click();
        Assert.IsTrue(radio.Selected);

        var cor = driver.FindElement(By.Name("my-colors"));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].value = '#ff0000';" +
            "arguments[0].dispatchEvent(new Event('input', {bubbles:true}));" +
            "arguments[0].dispatchEvent(new Event('change', {bubbles:true}));",
            cor);

        Assert.AreEqual("#ff0000", cor.GetAttribute("value"));

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        Console.WriteLine("URL atual: " + driver.Url); Console.WriteLine("Titulo: " + driver.Title); Console.WriteLine("Conteudo: " + driver.FindElement(By.TagName("body")).Text); Assert.AreEqual("Received!", AguardarConfirmacao());
    }

    private string AguardarConfirmacao()
    {
        var espera = new WebDriverWait(
            driver, TimeSpan.FromSeconds(20));

        espera.IgnoreExceptionTypes(
            typeof(StaleElementReferenceException),
            typeof(NoSuchElementException));

        return espera.Until(d =>
        {
            if (!d.Url.Contains("submitted-form.html"))
                return null;

            return d.FindElement(By.TagName("body"))
                .Text.Contains("Received!")
                ? "Received!"
                : null;
        })!;
    }}







