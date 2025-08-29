using APIatividade2.Data;
using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Repositorios
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly AppDbContext _context;

        public ProdutoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Adicionar(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> Apagar(int id)
        {
            var produtoPorId = await BuscarPorId(id);
            if (produtoPorId == null)
            {
                throw new Exception($"Produto para o ID: {id} não encontrado.");
            }
            _context.Produtos.Remove(produtoPorId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Produto> BuscarPorId(int id)
        {
            // .Include(p => p.Categoria) carrega os dados da Categoria junto com o Produto
            return await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Produto>> BuscarTodosProdutos()
        {
            return await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<Produto> Atualizar(Produto produto, int id)
        {
            var produtoPorId = await BuscarPorId(id);
            if (produtoPorId == null)
            {
                throw new Exception($"Produto para o ID: {id} não encontrado.");
            }

            produtoPorId.Nome = produto.Nome;
            produtoPorId.Preco = produto.Preco;
            produtoPorId.CategoriaId = produto.CategoriaId;
            // Adicione aqui a Descrição se tiver na sua model
            // produtoPorId.Descricao = produto.Descricao;

            _context.Produtos.Update(produtoPorId);
            await _context.SaveChangesAsync();
            return produtoPorId;
        }
    }
}