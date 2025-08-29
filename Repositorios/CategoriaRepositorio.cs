using APIatividade2.Data;
using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Repositorios
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        private readonly AppDbContext _context;

        public CategoriaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> Apagar(int id)
        {
            var categoriaPorId = await BuscarPorId(id);
            if (categoriaPorId == null)
            {
                throw new Exception($"Categoria para o ID: {id} não encontrada.");
            }
            _context.Categorias.Remove(categoriaPorId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Categoria> BuscarPorId(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Categoria>> BuscarTodasCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> Atualizar(Categoria categoria, int id)
        {
            var categoriaPorId = await BuscarPorId(id);
            if (categoriaPorId == null)
            {
                throw new Exception($"Categoria para o ID: {id} não encontrada.");
            }

            categoriaPorId.Nome = categoria.Nome;
            // Assumindo que sua model Categoria tem um Status
            // categoriaPorId.Status = categoria.Status;

            _context.Categorias.Update(categoriaPorId);
            await _context.SaveChangesAsync();
            return categoriaPorId;
        }
    }
}