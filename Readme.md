## Fase 1: Uso do Sistemas

### 1. Pré-requisitos

- Instalar o DotNet Core SDK 8
  - [.NET SDK Downdload](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Pacotes de Dependências instalados nos Projetos.
  ```bash
  dotnet add package AutoMapper
  dotnet add package FluentValidation.AspNetCore
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Abstractions
  dotnet add package Microsoft.EntityFrameworkCore.Tools
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  dotnet add Newtonsoft.Json
  dotnet add Serilog.AspNetCore
  dotnet add Bogus
  dotnet add Dapper
  dotnet add Shouldly
  dotnet add Moq
  dotnet add coverlet.collector
  dotnet add Microsoft.EntityFrameworkCore.InMemory
  dotnet add Microsoft.AspNetCore.Mvc.Testing
  ```
- Instalar o Docker Desktop ou docker cli (Linux ou MacOs)
  - [Docker Downdload](https://www.docker.com/products/docker-desktop/)

- Criar a rede local do Docker e criar o container do SQL Server

  ```bash
  docker network create -d bridge localdev

  docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=senha@123" --network localdev -p 1433:1433 --name sqlserver-2022 --hostname sqlserver-2022 -d mcr.microsoft.com/mssql/server:2022-latest
  ```

* Abra a pasta root do projeto no terminal e rode o comando bash abaixo para buildar o projeto e criar a imagem docker
  ```bash
  docker build -f API\DockerFile . --force-rm -t taskflow
  ```
* Com a pasta root do projeto aberto, execute o comando abaixo para criar o container
  ```bash
  docker run -d --network localdev  -p 5000:8080 -e "ASPNETCORE_ENVIRONMENT=Development" --name taskflow taskflow
  ```
### 2. Unit Testing
1. Ambiente Linux ou MacOS
    + Existe um software se precisar instalar parar gerar a transcrição dos arquivo coverage.cobertura.xml, que é gerado pelo dotnet cli `dotnet test --collect:"XPlat Code Coverage"`, chamado ReportGenerator [ReportGenerator](https://github.com/danielpalme/ReportGenerator). Na pasta Report você encontrará uma imagem de como é o gráfico.
2. Ambiente Linux ou MacOS
   + Caso utilize Windows na própria IDE do Visual Studio 2022 tem a ferramenta de teste e teste coverage.
## OBSERVAÇÃO

O projeto está com as connections strings apontando para o Docker, caso deseja executar localmente, é necessário apontar para o servidor correto.

## Fase 2: Refinamento

- O projeto poderia incluir o vínculo de épicos associados nas tarefas e no futuro termos o mapeamento das funcionalidades desenvolvidas.
- Seria interessante criarmos campos que possamos rastrear as PRs que foram feitos MERGED em PROD, assim garantirmos o rastreio do bug.
- Outro detalhe não menos importante, a inclussão de funcionalidade de upload de Arquivos, assim temos os históricos de scripts que foram executados em PROD, quando a tarefa utilizar tecnologia de Banco de Dados.

## Fase 3: Final

- Acredito que sistema de autenticação para validar as permissões de autorização do usuários.
- Inclusão de Claims por Roles, para garantir que o usuário logado tem Autorização e Permissão para efetuar aquele tipo de Operação (Leitura, escrita, atualização ou remoção).
- Utilizaria o MongoDB para melhorar a performance da consultas dos logs, principalmente pensando no cenários de muitas conexões de acesso ao sistema.
- Poderia utilizar MediaR para realizar chamadas nos serviços.
- Implementar o SinalR para notificações assíncronas.
- Implementaria o Quartz ou Hangfire ou Azure Service Bus (Function Trigger) para executar de tempo em tempo Task que estão criadas por muito tempo sem status, serem removidas automaticamente.

```

```
