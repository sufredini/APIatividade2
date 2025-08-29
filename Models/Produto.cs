using System.Collections.Generic;

namespace APIatividade2.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        // FK para Categoria
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        // Relação N:N com Pedido
        public List<PedidoProduto> PedidoProdutos { get; set; }
    }
}
