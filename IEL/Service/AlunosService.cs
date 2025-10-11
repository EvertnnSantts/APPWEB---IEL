using IEL.Context;
using IEL.Models;
using Microsoft.EntityFrameworkCore;

namespace IEL.Service
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        // Construtor
        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        // Implementação dos métodos da interface IAlunoService
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        // Método para obter um aluno pelo ID
        public async Task<Aluno> GetAluno(int id)
        {
            return await _context.Alunos.FindAsync(id);
        }

        // Método para buscar alunos pelo nome
        public async Task<IEnumerable<Aluno>> GetAlunosByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return await _context.Alunos
                    .Where(a => a.Name!.Contains(name))
                    .ToListAsync();
            }
            return await GetAlunos();
        }

        // Método para buscar alunos pelo cpf
        public async Task<Aluno> GetAlunoByCpf(string cpf)
        {
            return await _context.Alunos
            .FirstOrDefaultAsync(a => a.CPF == cpf);
        }

        // Método para buscar alunos pela data de conclusão
        public async Task<IEnumerable<Aluno>> GetAlunosByDataDeConclusao(DateTime dataDeConclusao)
        {
            return await _context.Alunos
                .Where(a => a.DateConclusao.Date == dataDeConclusao.Date)
                .ToListAsync();
        }


        // Método para criar um novo aluno
        public async Task<Aluno> CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }
        // Método para atualizar um aluno existente
        public async Task<Aluno> UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return aluno;
        }
        // Método para deletar um aluno
        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }

    }
}
