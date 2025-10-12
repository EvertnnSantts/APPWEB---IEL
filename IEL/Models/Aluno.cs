using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IEL.Models
{
    // Define a tabela "Alunos" no banco de dados
    [Table("Alunos")]
    public class Aluno
    {
        [Key]
        // Define a chave primária
        public int Id { get; set; }

        // Define a propriedade Name com validação
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100)]
        public string Name { get; set; }

        // Define a propriedade Email com validação
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [StringLength(100)]
        public string Email { get; set; }

        // Define a propriedade DateConclusao com validação e formatação
        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [Column(TypeName = "date")]
        public DateTime DateConclusao { get; set; }

        // Define a propriedade Endereco com validação
        [Required(ErrorMessage = "O endereço é obrigatório.")]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
    }
}
