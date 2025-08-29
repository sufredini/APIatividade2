using APIatividade2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Interfaces
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> BuscarTodasCategorias();
        Task<Categoria> BuscarPorId(int id);
        Task<Categoria> Adicionar(Categoria categoria);
        Task<Categoria> Atualizar(Categoria categoria, int id);
        Task<bool> Apagar(int id);
    }
}