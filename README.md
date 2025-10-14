# Gerenciamento de ALunos - IEL
O "gerenciamento de alunos - IEL" apresenta características básicas de controle de dados, como, por exemplo, a possibilidade de criar novos alunos, 
editar cadastros de alunos já existentes na base de dados e excluir alunos da base. A aplicação também possui um filtro de busca por CPF.

## Características

- 👤 Cadastro de alunos: criação, edição, filto e exclusão de registros.
  
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
## Explicando os Métodos
### Operações CRUD (Create, Read, Update, Delete)

## Criar novo aluno:
##### Frontend (Axios):
```bash
// 🔹 Criar novo aluno 
const pedidoPost = async () => {
  const { name, email, dateConclusao, address, cpf } = alunoSelecionado;
  if (!name.trim() || !email.trim() || !dateConclusao.trim() || !address.trim() || !cpf.trim()) {
    setErro('Por favor, preencha todos os campos antes de salvar.');
    return;
  }

  const novoAluno = { ...alunoSelecionado };
  delete novoAluno.id; // remove o ID, pois será gerado pelo backend

  try {
    const response = await axios.post(baseUrl, novoAluno);
    setData([...data, response.data]);
    abrirFecharModalIncluir();
    setAlunoSelecionado({
      id: '', name: '', email: '', dateConclusao: '', address: '', cpf: ''
    });
  } catch (error) {
    console.log(error);
  }
};

```

### 🟢 Explicação:

- Os campos do aluno são validados antes do envio.

- Caso estejam preenchidos, os dados são enviados para o endpoint configurado em baseUrl (que aponta para o backend).

- O backend retorna o aluno criado, que é adicionado na lista de alunos (setData).

- Por fim, o formulário é limpo e o modal de cadastro é fechado.


##### Controller (.NET):
```bash
[HttpPost]
public async Task<ActionResult<Aluno>> CreateAluno([FromBody] Aluno aluno)
{
    try
    {
        if (aluno == null)
            return BadRequest("O objeto aluno não pode ter espaços vazios.");

        await _alunoService.CreateAluno(aluno);

        // Retorna 201 Created com a rota do GET por ID
        return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao criar aluno: {ex.Message}");
    }
}
```

### 🟢 Explicação:

- O método [HttpPost] define que essa ação responderá às requisições POST.

- O corpo da requisição ([FromBody]) contém o objeto Aluno vindo do frontend.

- Caso o objeto esteja vazio, o método retorna 400 (Bad Request).

- Caso contrário, o aluno é enviado ao service para ser salvo no banco.

- Se tudo ocorrer bem, o controller retorna 201 (Created) com a rota para buscar o aluno criado.


#####  Service (Lógica e Persistência) :
```bash
// Método para criar um novo aluno 
public async Task<Aluno> CreateAluno(Aluno aluno)
{
    _context.Alunos.Add(aluno);
    await _context.SaveChangesAsync();
    return aluno;
}
```

### 🟢 Explicação:

- O aluno recebido é adicionado ao contexto do Entity Framework (_context.Alunos.Add).

- O método SaveChangesAsync() salva as alterações no banco de dados.

- O aluno criado é retornado ao controller, que por sua vez envia a resposta ao frontend.


<hr style="border: 7px solid #00FF00;">


## Deleta um aluno:
#####  Frontend (Axios):
```bash
// 🔹 Deletar aluno
const pedidoDelete = async () => {
  await axios.delete(`${baseUrl}/${alunoSelecionado.id}`)
    .then(response => {
      setData(data.filter(aluno => aluno.id !== alunoSelecionado.id));
      abrirFecharModalExcluir();
    })
    .catch(error => console.log(error));
};
```

### 🟢 Explicação:

- A função monta a URL incluindo o ID do aluno selecionado (${baseUrl}/${alunoSelecionado.id}).

- Envia uma requisição DELETE para o backend.

- Caso o backend confirme a exclusão, o frontend remove o aluno da lista local (setData) e fecha o modal de exclusão.

- Se ocorrer algum erro, ele é exibido no console para debug.

##### Controller (.NET):
```bash
// DELETE: api/Aluno/1 (Deleta cadastro)
[HttpDelete("{id:int}")]
public async Task<ActionResult> ExcluirAluno(int id)
{
    try
    {
        var aluno = await _alunoService.GetAluno(id);
        if (aluno != null)
        {
            await _alunoService.DeleteAluno(aluno);
            return Ok("Aluno deletado com sucesso.");
        }
        else
        {
            return NotFound("Aluno não encontrado.");
        }
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao deletar aluno: {ex.Message}");
    }
}
```

### 🟢 Explicação:

- A rota [HttpDelete("{id:int}")] indica que o método recebe um ID numérico na URL.

- O método busca o aluno correspondente no banco via GetAluno(id).

- Se o aluno existir, o controller chama o service para removê-lo e retorna 200 (OK) com a mensagem de sucesso.

- Caso o aluno não seja encontrado, retorna 404 (Not Found).

- Qualquer erro inesperado retorna 500 (Internal Server Error).


##### Service (Lógica e Persistência):

```bash
public async Task DeleteAluno(Aluno aluno)
{
    _context.Alunos.Remove(aluno);
    await _context.SaveChangesAsync();
}
```

### 🟢 Explicação:

- O método Remove() marca o aluno para exclusão no contexto do banco.

- SaveChangesAsync() confirma a operação e aplica a exclusão no banco de dados.

- Não há necessidade de retornar valores, pois a operação é apenas de remoção.


<hr style="border: 7px solid #00FF00;">

## Atualização de aluno:

##### Frontend (Axios):
```bash
// 🔹 Atualizar aluno
const pedidoPut = async () => {
  try {
    const alunoAtualizado = {
      ...alunoSelecionado,
      dateConclusao: new Date(alunoSelecionado.dateConclusao).toISOString()
    };

    const response = await axios.put(`${baseUrl}/${alunoSelecionado.id}`, alunoAtualizado);
    setData(data.map((aluno) =>
      aluno.id === alunoSelecionado.id ? response.data : aluno
    ));
    setModalEditar(false);
  } catch (error) {
    console.log(error);
  }
};
```
### 🟢 Explicação:
- O objeto alunoAtualizado é criado a partir do aluno selecionado, formatando a data de conclusão para o padrão ISO (toISOString()).

- O Axios envia uma requisição PUT para o endpoint do backend (/api/aluno/{id}), passando o objeto atualizado no corpo.

- Ao receber a resposta com sucesso, o frontend substitui o aluno antigo pelo novo na lista (setData).

- Por fim, o modal de edição é fechado (setModalEditar(false)).

##### Controller (.NET):
```bash
[HttpPut("{id:int}")]
public async Task<ActionResult> EditAluno(int id, [FromBody] Aluno aluno)
{
    try
    {
        if (aluno == null)
            return BadRequest("O espaço não pode ser vazio.");

        if (aluno.Id != id)
            return BadRequest("O ID do aluno não confere com o informado.");

        await _alunoService.UpdateAluno(aluno);

        return Ok(new { message = "Cadastro do aluno atualizado com sucesso", aluno });
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            $"Erro ao atualizar aluno: {ex.Message}");
    }
}
```

### 🟢 Explicação:
- A rota [HttpPut("{id:int}")] define que o método responde a requisições PUT com um ID numérico.

- O controller verifica se o objeto aluno é nulo e se o ID enviado na URL corresponde ao ID do aluno no corpo da requisição.

- Em caso de sucesso, o método UpdateAluno do service é chamado.

- Após atualizar, o método retorna um status 200 (OK) com uma mensagem e os dados do aluno atualizado.


##### Service (Lógica e Persistência):

```bash
public async Task<Aluno> UpdateAluno(Aluno aluno)
{
    _context.Entry(aluno).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return aluno;
}
```
### 🟢 Explicação:

- O método Entry(aluno).State = EntityState.Modified informa ao Entity Framework que todas as propriedades do objeto aluno foram alteradas e precisam ser atualizadas no banco.

- SaveChangesAsync() aplica efetivamente as mudanças.

- O método retorna o aluno atualizado, que é enviado de volta ao controller.
