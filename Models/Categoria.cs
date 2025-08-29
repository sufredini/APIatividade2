using System.Collections.Generic;

namespace APIatividade2.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        // Relacionamento - uma categoria pode ter vários produtos
        public List<Produto> Produtos { get; set; }
    }
}
