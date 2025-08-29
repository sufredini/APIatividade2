using APIatividade2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Interfaces
{
    public interface IPedidoRepositorio
    {
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<Pedido> BuscarPorId(int id);
        Task<Pedido> Adicionar(Pedido pedido);
        Task<Pedido> Atualizar(Pedido pedido, int id);
        Task<bool> Apagar(int id);
    }
}