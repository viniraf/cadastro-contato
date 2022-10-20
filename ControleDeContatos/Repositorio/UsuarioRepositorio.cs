using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio {
    public class UsuarioRepositorio : IUsuarioRepositorio {

        private readonly BancoContext _Context;


        public UsuarioRepositorio(BancoContext bancoContext)  {

            this._Context = bancoContext;
        }


        public UsuarioModel BuscarPorLogin(string login) {
            return _Context.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }


        public UsuarioModel ListarPorId(int id) {
            return _Context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos() {
            return _Context.Usuarios.ToList();
        }


        public UsuarioModel Adicionar(UsuarioModel usuario) {

            usuario.DataCadastro = DateTime.Now;
            _Context.Usuarios.Add(usuario);
            _Context.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario) {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) 
                throw new System.Exception("Houve um erro na atualização do usuário");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _Context.Usuarios.Update(usuarioDB);
            _Context.SaveChanges();
            return usuarioDB;
        }

        public bool Apagar(int id) {
            UsuarioModel usuarioDB = ListarPorId(id);

            if (usuarioDB == null)
                throw new System.Exception("Houve um erro na deleção do contato do usuário");

            _Context.Usuarios.Remove(usuarioDB);
            _Context.SaveChanges();

            return true;

        }

        
    }
}
