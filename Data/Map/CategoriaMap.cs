using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIatividade2.Models
{
    [Table("Categorias")] // Equivalente a builder.ToTable("Categorias");
    public class CategoriaMap
    {
        [Key] // Equivalente a builder.HasKey(x => x.Id);
        public int Id { get; set; }

        [Required] // Equivalente a .IsRequired()
        [MaxLength(100)] // Equivalente a .HasMaxLength(100)
        public string Nome { get; set; }

        // Relacionamento 1:N (um para muitos)
        // O EF Core entende esse relacionamento por convenção,
        // não precisa de um atributo aqui.
        public virtual ICollection<Produto> Produtos { get; set; }
    }
}