using APIatividade2.Data;
using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Repositorios
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly AppDbContext _context;

        public PedidoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> Adicionar(Pedido pedido)
        {
            // Quando adicionamos um pedido que já contém a lista de PedidoProdutos,
            // o Entity Framework é inteligente o suficiente para salvar tudo de uma vez.
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task<bool> Apagar(int id)
        {
            var pedidoPorId = await BuscarPorId(id);
            if (pedidoPorId == null)
            {
                throw new Exception($"Pedido para o ID: {id} não encontrado.");
            }
            _context.Pedidos.Remove(pedidoPorId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Pedido> BuscarPorId(int id)
        {
            // Aqui usamos .Include para carregar os dados relacionados ao Pedido
            return await _context.Pedidos
                .Include(p => p.Usuario) // Carrega os dados do Usuário
                .Include(p => p.PedidoProdutos) // Carrega a lista de itens do pedido
                    .ThenInclude(pp => pp.Produto) // Para cada item, carrega os dados do Produto
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Pedido>> BuscarTodosPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Usuario) // Carrega o usuário de cada pedido
                .ToListAsync();
        }

        public async Task<Pedido> Atualizar(Pedido pedido, int id)
        {
            var pedidoPorId = await BuscarPorId(id);
            if (pedidoPorId == null)
            {
                throw new Exception($"Pedido para o ID: {id} não encontrado.");
            }

            // Atualiza apenas os dados principais do pedido
            pedidoPorId.Status = pedido.Status;
            // pedidoPorId.EnderecoEntrega = pedido.EnderecoEntrega;
            // pedidoPorId.MetodoPagamento = pedido.MetodoPagamento;

            _context.Pedidos.Update(pedidoPorId);
            await _context.SaveChangesAsync();
            return pedidoPorId;
        }
    }
}