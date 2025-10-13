# Gerenciamento de ALunos - IEL
O "gerenciamento de alunos - IEL" apresenta características básicas de controle de dados, como, por exemplo, a possibilidade de criar novos alunos, 
editar cadastros de alunos já existentes na base de dados e excluir alunos da base. A aplicação também possui um filtro de busca por CPF.

## Características

- 👤 Cadastro de usuários/alunos: criação, edição e exclusão de registros.
- ⚛️ React no Frontend: interface.
- 💻 ASP.NET MVC/API no Backend: arquitetura estruturada, separando lógica de apresentação, negócio.
- 🗄 Entity Framework Core: manipulação de banco de dados SQL Server.
- 🎨 Estilo consistente: uso de bibliotecas CSS, Bootstrap.

## Instalação & configuração de ambiente

### ⚙️ Configuração de Ambiente

Clone [reporsitorio](https://github.com/EvertnnSantts/APPWEB---IEL) em seu ambiente<br>
```bash
git clone https://github.com/EvertnnSantts/APPWEB---IEL
```
[Node.Js](https://nodejs.org/en/download) - necessário para executar o frontend React.<br>
[NET.SDK](https://dotnet.microsoft.com/pt-br/) - necessário para executar o backend ASP.NET.

Bibliotecas para funcionamento correto do frontend:

```bash
# Axios para requisições HTTP
npm install axios

# Bootstrap para estilização
npm install bootstrap

# React Number Format para formatação de números e CPF
npm install react-number-format

# Reactstrap, React e React DOM para componentes React
npm install reactstrap react react-dom

```
Bibliotecas para funcionamento correto do bacckend:

```bash
# Pacote principal do Entity Framework Core, contém o núcleo do ORM
dotnet add package Microsoft.EntityFrameworkCore

# Provedor de banco de dados que permite ao EF Core conectar-se e comunicar-se com o SQL Server
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

### ⚙️ Configuração avançadas
Já dentro dos arquivos da aplicação, configure a conexão do banco de dados SQL Server com a sua base de dados local.</br>
caminho para acessar o arquivo a seguir: </br>
caminho: APPWEB---IEL\IEL\IELappsettings.json
```bash
{
  "ConnectionStrings": {
    # configure a conexão aqui:
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=appielDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
### 🚀 Iniciação da aplicação

Navegue até a pasta do frontend da aplicação e inicie o backend </br>
caminho: APPWEB---IEL/IEL/
```bash
dotnet run
```

Navegue até a pasta do frontend da aplicação e inicie o frontend </br>
caminho: APPWEB---IEL/IEL/Frontend/appielfront/ 
```bash
npm start
```
