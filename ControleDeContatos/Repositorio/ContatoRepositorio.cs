using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio {
    public class ContatoRepositorio : IContatoRepositorio {

        private readonly BancoContext _Context;


        public ContatoRepositorio(BancoContext bancoContext)  {

            this._Context = bancoContext;

        }


        public ContatoModel ListarPorId(int id) {
            return _Context.Contatos.FirstOrDefault(x => x.Id == id);
        }

        public List<ContatoModel> BuscarTodos() {
            return _Context.Contatos.ToList();
        }


        public ContatoModel Adicionar(ContatoModel contato) {
            _Context.Contatos.Add(contato);
            _Context.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato) {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) 
                throw new System.Exception("Houve um erro na atualização do contato");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _Context.Contatos.Update(contatoDB);
            _Context.SaveChanges();
            return contatoDB;
        }

        public bool Apagar(int id) {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null)
                throw new System.Exception("Houve um erro na deleção do contato do contato");

            _Context.Contatos.Remove(contatoDB);
            _Context.SaveChanges();

            return true;

        }
    }
}
