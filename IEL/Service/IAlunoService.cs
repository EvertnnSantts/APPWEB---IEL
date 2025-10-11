using IEL.Models;

namespace IEL.Service
{
    public interface IAlunoService
    {
        // Metado para chama a lista de Alunos
        Task<IEnumerable<Aluno>> GetAlunos();
        // Metado para chama um Aluno pelo Id
        Task<Aluno> GetAluno(int id);
        // Metado para Adicionar um Aluno
        Task<IEnumerable<Aluno>> GetAlunosByName(string name);

        //Metado para criar um novo Aluno
        Task<Aluno> CreateAluno(Aluno aluno);
        // Metado para Atualizar um Aluno
        Task<Aluno> UpdateAluno(Aluno aluno);
        // Metado para Deletar um Aluno
        Task DeleteAluno(Aluno aluno);
    }
}
