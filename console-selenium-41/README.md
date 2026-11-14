# Console Selenium 41

Atividade de Garantia da Qualidade de Software.

O projeto utiliza C#, .NET 10, MSTest e Selenium para testar o formulário de exemplo do Selenium.

## Testes

- Preenchimento dos campos de texto, senha e descrição.
- Seleção de opções no dropdown e preenchimento do datalist.
- Alteração de checkboxes, radio button e seletor de cores.

Os três testes verificam o envio do formulário.

## Como executar

É necessário ter o .NET 10 e o Google Chrome instalados.

No terminal, dentro da pasta do projeto:

```powershell
dotnet restore
dotnet test
```

O ChromeDriver é gerenciado pelo Selenium Manager, que pode precisar de acesso à internet na primeira execução.
