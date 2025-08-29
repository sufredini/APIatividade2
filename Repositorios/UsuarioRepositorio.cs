using APIatividade2.Data;
using APIatividade2.Interfaces;
using APIatividade2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIatividade2.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Conexão com o banco de dados
        private readonly AppDbContext _context;

        public UsuarioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Apagar(int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                // Lança uma exceção se o usuário não for encontrado
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _context.Usuarios.Remove(usuarioPorId);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> BuscarPorId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Usuario>> BuscarTodosUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> Atualizar(Usuario usuario, int id)
        {
            Usuario usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            // Atualiza os campos do usuário encontrado com os dados do usuário recebido
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            // Adicione aqui outros campos que possam ser atualizados
            // ex: usuarioPorId.DataNascimento = usuario.DataNascimento;

            _context.Usuarios.Update(usuarioPorId);
            await _context.SaveChangesAsync();

            return usuarioPorId;
        }
    }
}