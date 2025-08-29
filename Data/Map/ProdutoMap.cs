using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIatividade2.Models
{
    [Table("Produtos")]
    public class ProdutoMap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Equivalente a .HasColumnType("decimal(18,2)")
        public decimal Preco { get; set; }

        // Chave Estrangeira para Categoria
        public int CategoriaId { get; set; }

        // Propriedade de Navegação para o relacionamento N:1 (muitos para um)
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        // Relacionamento com a tabela de junção PedidoProduto
        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }
}