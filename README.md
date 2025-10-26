# Desafio de Estágio .NET - Cadastro de Fornecedores

Este projeto é uma aplicação Web ASP.NET Core MVC desenvolvida como solução para o desafio de estágio de Analista Desenvolvedor .NET.

## Funcionalidades

* **CRUD** completo de Fornecedores (Criar, Ler, Atualizar, Deletar).
* **Integração com API Externa:** Consumo da API [ViaCEP](https://viacep.com.br/) para preenchimento automático de endereço.
* **Upload de Imagem:** Permite o upload de uma foto (PNG) para o fornecedor, que é salva no servidor e referenciada no banco.
* **Validações** de dados no backend e frontend.

## Tecnologias Utilizadas

* **Backend:** ASP.NET Core MVC (.NET 6+)
* **Banco de Dados:** SQL Server Express
* **ORM:** Entity Framework Core (Code-First)
* **Frontend:** HTML, Bootstrap, CSS e JavaScript (Fetch API)
* **Arquitetura:** Padrão MVC com uso de ViewModels para DTOs.

## Como Executar o Projeto

1.  Clone este repositório.
2.  Abra a solução (`.sln`) no Visual Studio 2022.
3.  **Crie** um arquivo `appsettings.json` na raiz do projeto.
4.  **Copie** o conteúdo de `appsettings.Example.json` para o seu `appsettings.json`.
5.  **Configure** a sua "DefaultConnection" no `appsettings.json` para apontar para o seu SQL Server.
6.  No Visual Studio, vá em **Tools > NuGet Package Manager > Package Manager Console**.
7.  Execute o comando: `Update-Database`
8.  Pressione `Ctrl + F5` para rodar a aplicação.