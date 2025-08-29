using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIatividade2.Models
{
    [Table("PedidoProdutos")]
    public class PedidoProdutoMap
    {
        // Parte 1 da Chave Composta e Chave Estrangeira para Pedido
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; }

        // Parte 2 da Chave Composta e Chave Estrangeira para Produto
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }

        [Required]
        public int Quantidade { get; set; }

        // IMPORTANTE: Para a chave composta funcionar com Data Annotations,
        // você precisa ir no seu arquivo DbContext e adicionar o seguinte
        // dentro do método OnModelCreating:
        //
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<PedidoProduto>()
        //         .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });
        // }
        //
        // Isso é necessário porque Data Annotations não têm um jeito limpo
        // de definir chaves compostas diretamente no modelo.
    }
}