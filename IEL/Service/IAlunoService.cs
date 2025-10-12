using IEL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Método para buscar aluno pelo endereço
        Task<Aluno> GetAlunoByAddress(string address);

        // Método para buscar alunos pela data de conclusão
        Task<IEnumerable<Aluno>> GetAlunosByDataDeConclusao(DateTime dataDeConclusao);

        // Método para buscar aluno pelo CPF
        Task<Aluno> GetAlunoByCpf(string cpf);

        // Método para criar um novo aluno
        Task<Aluno> CreateAluno(Aluno aluno);

        // Método para atualizar um aluno existente
        Task<Aluno> UpdateAluno(Aluno aluno);

        // Método para deletar um aluno
        Task DeleteAluno(Aluno aluno);
    }
}
