using IEL.Models;

namespace IEL.Service
{
    public interface IAlunoService
    {
        // Método para obter a lista de todos os alunos
        Task<IEnumerable<Aluno>> GetAlunos();

        // Método para obter um aluno pelo ID
        Task<Aluno> GetAluno(int id);

        // Método para buscar alunos pelo nome
        Task<IEnumerable<Aluno>> GetAlunosByName(string name);
        // Metado para buscar alunos pelo endereco
        Task<Aluno> GetAlunoByAddress(string address);
        // Método para buscar alunos pelo datadecoclusao
        Task<IEnumerable<Aluno>> GetAlunosByDataDeConclusao(DateTime dataDeConclusao);
        // Método para criar um novo aluno
        Task<Aluno> CreateAluno(Aluno aluno);

        // Método para atualizar um aluno existente
        Task<Aluno> UpdateAluno(Aluno aluno);

        // Método para deletar um aluno
        Task DeleteAluno(Aluno aluno);
    }
}
