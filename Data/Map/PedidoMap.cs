using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIatividade2.Models
{
    [Table("Pedidos")]
    public class PedidoMap
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataPedido { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        [MaxLength(50)]
        public string FormaPagamento { get; set; }

        // Chave Estrangeira para Usuario
        public int UsuarioId { get; set; }

        // Propriedade de Navegação para o relacionamento N:1
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        // Relacionamento com a tabela de junção PedidoProduto
        public virtual ICollection<PedidoProduto> PedidoProdutos { get; set; }
    }
}