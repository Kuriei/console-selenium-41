using Microsoft.VisualStudio.TestTools.UnitTesting;
@'
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
            .SendKeys("Seu Nome Completo");

        driver.FindElement(By.Name("my-password"))
            .SendKeys("Teste@2026Seguro");

        driver.FindElement(By.Name("my-textarea"))
            .SendKeys("Estou aprendendo a criar testes automatizados com C# e Selenium.");

        var campoLeitura = driver.FindElement(
            By.Name("my-readonly"));

        Assert.AreEqual(
            "Readonly input",
            campoLeitura.GetAttribute("value"));

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        Assert.AreEqual(
            "Received!",
            driver.FindElement(By.Id("message")).Text);
    }

    [TestMethod]
    public void SelecionarDropdownEDatalist()
    {
        var dropdown = new SelectElement(
            driver.FindElement(By.Name("my-select")));

        dropdown.SelectByText("Two");

        Assert.AreEqual(
            "2",
            dropdown.SelectedOption.GetAttribute("value"));

        driver.FindElement(By.Name("my-datalist"))
            .SendKeys("Seattle");

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        Assert.AreEqual(
            "Received!",
            driver.FindElement(By.Id("message")).Text);
    }

    [TestMethod]
    public void AlterarCheckboxRadioECor()
    {
        var primeiroCheckbox = driver.FindElement(
            By.Id("my-check-1"));

        Assert.IsTrue(primeiroCheckbox.Selected);

        var segundoCheckbox = driver.FindElement(
            By.Id("my-check-2"));

        segundoCheckbox.Click();
        Assert.IsTrue(segundoCheckbox.Selected);

        var radio = driver.FindElement(
            By.Id("my-radio-2"));

        radio.Click();
        Assert.IsTrue(radio.Selected);

        var cor = driver.FindElement(
            By.Name("my-colors"));

        ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].value = '#ff0000';" +
            "arguments[0].dispatchEvent(new Event('input', {bubbles:true}));" +
            "arguments[0].dispatchEvent(new Event('change', {bubbles:true}));",
            cor);

        Assert.AreEqual(
            "#ff0000",
            cor.GetAttribute("value"));

        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        Assert.AreEqual(
            "Received!",
            driver.FindElement(By.Id("message")).Text);
    }
}
'@ | Set-Content -Path "ConsoleSelenium.Tests\WebFormularioDesafioTest.cs" -Encoding UTF8
