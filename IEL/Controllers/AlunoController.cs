using IEL.Models;
using IEL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace IEL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        // GET: api/Aluno
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter os alunos: {ex.Message}");
            }
        }

        // GET: api/Aluno/buscar-por-id?id=1
        [HttpGet("{id}", Name = "GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno([FromQuery] int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                    return NotFound($"Aluno com ID {id} não encontrado.");

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter o aluno: {ex.Message}");
            }
        }

        // GET: api/Aluno/buscar-por-nome?name=Fulano
        [HttpGet("buscar-por-nome")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunosByNome([FromQuery] string name)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByName(name);
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter os alunos: {ex.Message}");
            }
        }

        // GET: api/Aluno/buscar-por-endereco?endereco=Rua%20das%20Flores
        [HttpGet("buscar-por-endereco")]
        public async Task<ActionResult<Aluno>> GetAlunoByAddress([FromQuery] string endereco)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoByAddress(endereco);
                if (aluno == null)
                    return NotFound($"Aluno com endereço {endereco} não encontrado.");

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter o aluno: {ex.Message}");
            }
        }

        // GET: api/Aluno/buscar-por-cpf?cpf=123.456.789-00
        [HttpGet("buscar-por-cpf")]
        public async Task<ActionResult<Aluno>> GetAlunoByCpf([FromQuery] string cpf)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoByCpf(cpf);
                if (aluno == null)
                    return NotFound($"Aluno com CPF {cpf} não encontrado.");

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter o aluno pelo CPF: {ex.Message}");
            }
        }

        // GET: api/Aluno/buscar-por-dataConclusao?dataConclusao=2025-12-01
        [HttpGet("buscar-por-dataConclusao")]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunosByDataConclusao([FromQuery] string dataConclusao)
        {
            try
            {
                if (!DateTime.TryParse(dataConclusao, out var parsedDate))
                {
                    return BadRequest("Formato de data inválido. Use o formato 'yyyy-MM-dd'.");
                }

                var alunos = await _alunoService.GetAlunosByDataDeConclusao(parsedDate);
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar obter os alunos: {ex.Message}");
            }
        }

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

        // PUT: api/Aluno/1 (Atualização de cadastro)
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
    }
}
