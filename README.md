# Gerenciador de Tarefas

API REST para gerenciamento de tarefas, desenvolvida em C# com .NET 8.

## Funcionalidades

- **CRUD de Tarefas**: Crie, leia, atualize e delete tarefas.
- **Busca Avançada**: Filtre tarefas por título, data ou status.
- **Documentação**: A API é autodocumentada com Swagger.

## Tecnologias Utilizadas

- **.NET 8**: Plataforma de desenvolvimento.
- **Entity Framework Core**: ORM para interação com o banco de dados.
- **SQL Server**: Banco de dados para armazenamento das tarefas.
- **Swagger**: Ferramenta para documentação e teste da API.
- **xUnit**: Framework para os testes unitários.

## Começando

Siga as instruções abaixo para configurar e executar o projeto em seu ambiente local.

### Pré-requisitos

- **.NET 8 SDK**: [Download e instalação](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server**: [Express Edition](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou outra versão.

### Configuração

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/seu-usuario/gerenciador-de-tarefas.git
   cd gerenciador-de-tarefas
   ```

2. **Configure a Connection String:**
   - Abra o arquivo `GerenciadorTarefas/appsettings.Development.json`.
   - Modifique a `ConexaoPadrao` para apontar para sua instância do SQL Server. A string de conexão padrão é:
     ```json
     "Server=localhost\\sqlexpress,1433;Database=Gerenciador_Tarefas;User Id=sa;Password=##s3c8r7t1##;TrustServerCertificate=True;"
     ```

3. **Aplique as Migrations:**
   - Navegue até a pasta do projeto principal:
     ```bash
     cd GerenciadorTarefas
     ```
   - Execute o comando para criar o banco de dados e a tabela de tarefas:
     ```bash
     dotnet ef database update
     ```

### Executando a Aplicação

1. **Inicie a API:**
   ```bash
   dotnet run
   ```
2. **Acesse a documentação do Swagger:**
   - Abra seu navegador e acesse `http://localhost:<porta>/swagger` (a porta pode variar, verifique o console).

## Endpoints da API

A seguir estão os endpoints disponíveis na API:

| Método | URL                       | Descrição                                 |
|--------|---------------------------|-------------------------------------------|
| `GET`  | `/Tarefa/{id}`            | Busca uma tarefa pelo ID.                 |
| `POST` | `/Tarefa`                 | Cria uma nova tarefa.                     |
| `PUT`  | `/Tarefa/{id}`            | Atualiza uma tarefa existente.            |
| `DELETE`| `/Tarefa/{id}`            | Deleta uma tarefa.                        |
| `GET`  | `/Tarefa/ObterTodos`      | Lista todas as tarefas.                   |
| `GET`  | `/Tarefa/ObterPorTitulo`  | Filtra tarefas pelo título.               |
| `GET`  | `/Tarefa/ObterPorData`    | Filtra tarefas pela data.                 |
| `GET`  | `/Tarefa/ObterPorStatus`  | Filtra tarefas pelo status (Pendente/Finalizado). |

## Rodando os Testes

O projeto possui uma suíte de testes unitários para garantir a qualidade do código.

1. **Navegue até a pasta de testes:**
   ```bash
   cd ../GerenciadorTarefasTestes
   ```
2. **Execute os testes:**
   ```bash
   dotnet test
   ```
