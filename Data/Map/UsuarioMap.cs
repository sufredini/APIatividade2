using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIatividade2.Models
{
    [Table("Usuarios")] // Equivalente a builder.ToTable("Usuarios");
    public class UsuarioMap
    {
        [Key] // Equivalente a builder.HasKey(x => x.Id);
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        // Relacionamento 1:N (um para muitos)
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}