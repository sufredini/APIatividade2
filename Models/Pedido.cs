using System;
using System.Collections.Generic;

namespace APIatividade2.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string Status { get; set; } // Ex: "Pendente", "Pago", "Cancelado"
        public string FormaPagamento { get; set; }

        // FK para Usuário
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relação N:N com Produto
        public List<PedidoProduto> PedidoProdutos { get; set; }
    }
}
