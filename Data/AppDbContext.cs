using APIatividade2.Models;
using Microsoft.EntityFrameworkCore;

namespace APIatividade2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets = tabelas do banco (isso continua igual)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProduto> PedidoProdutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. REMOVA OU COMENTE ESTA LINHA:
            // Ela carregava automaticamente todos os seus arquivos Map antigos.
            // modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // 2. ADICIONE ISTO:
            // Configura a chave primária composta para a tabela de junção PedidoProduto.
            // Isso é necessário porque Data Annotations sozinhas não lidam bem com chaves compostas.
            modelBuilder.Entity<PedidoProduto>()
                .HasKey(pp => new { pp.PedidoId, pp.ProdutoId });

            base.OnModelCreating(modelBuilder); // É uma boa prática chamar o método base.
        }
    }
}