using IEL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace IEL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definindo tabela Alunos:
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Populando a tabela Alunos com dados iniciais (seed)
            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Name = "Alice Johnson",
                    Email = "AliceJohnson@gmail.com",
                    DateConclusao = new DateTime(2024, 06, 30),
                    Address = "Rua das Flores, 123 - São Paulo/SP",
                    Cpf = "123.456.789-00"
                },
                new Aluno
                {
                    Id = 2,
                    Name = "Bob Smith",
                    Email = "BobSmith@gmail.com",
                    DateConclusao = new DateTime(2024, 06, 30),
                    Address = "Av. Central, 456 - Rio de Janeiro/RJ",
                    Cpf = "987.654.321-00"
                }
            );
        }
    }
}
