using System.Collections.Generic;

namespace APIatividade2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Relacionamento - um usuário pode ter vários pedidos
        public List<Pedido> Pedidos { get; set; }
    }
}
