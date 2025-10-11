using IEL.Context;
using IEL.Models;
using Microsoft.EntityFrameworkCore;

namespace IEL.Service
{
    // Implementação da interface IAlunoService
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _Context;

        // Implementação dos métodos da interface IAlunoService (Criação de um novo Aluno)
        public async Task<Aluno> CreateAluno(Aluno aluno)
        {
            _Context.Alunos.Add(aluno);
            await _Context.SaveChangesAsync();
            return aluno;
        }

        // Implementação dos métodos da interface IAlunoService (Atualizar um Aluno)
        public async Task<Aluno> UpdateAluno(Aluno aluno)
        {
            _Context.Entry(aluno).State = EntityState.Modified;
            await _Context.SaveChangesAsync();
            return aluno;
        }

        // Implementação dos métodos da interface IAlunoService (Delete de um novo Aluno)
        public async Task DeleteAluno(Aluno aluno)
        {
            _Context.Alunos.Remove(aluno);
            await _Context.SaveChangesAsync();
        }

        // Implementação dos métodos da interface IAlunoService (Pegar a lista de Alunos)
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            // Retorna a lista de Alunos do banco de dados
            try
            {
                return await _Context.Alunos.ToListAsync();
            }
            // Em caso de erro, lança a exceção para ser tratada em outro lugar
            catch
            {
                throw;
            }
        }


        // Implementação dos métodos da interface IAlunoService (Pegar um Aluno pelo Id)
        public async Task<Aluno> GetAluno(int id)
        {
            // Retorna o Aluno correspondente ao Id fornecido
            var aluno = await _Context.Alunos.FindAsync(id);
            // Se o Aluno não for encontrado, lança uma exceção
            return aluno!;
        }

        // Implementação dos métodos da interface IAlunoService (Pegar Alunos pelo Nome)
        public async Task<IEnumerable<Aluno>> GetAlunosByName(string name)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrWhiteSpace(name))
            {
                // Consulta os alunos cujo nome contém a string fornecida (case-insensitive)
                alunos = await _Context.Alunos
                    .Where(n => n.Name!.Contains(name))
                    .ToListAsync();
            }
            else
            {
                // Se o nome for nulo ou vazio, retorna todos os alunos
                alunos = await GetAlunos();
            }
            // Retorna a lista de alunos encontrados
            return alunos;
        }
    }
}
