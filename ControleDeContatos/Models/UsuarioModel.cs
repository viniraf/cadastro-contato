using ControleDeContatos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos.Models {
    public class UsuarioModel {

        public int Id { get; set; }

        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string Email { get; set; }

        public PerfilEnum Perfil { get; set; }

        public DateTime DataCadastro { get; set; }

        // "?" Significa que esse campo pode ser nulo
        public DateTime? DataAtualizacao { get; set; }
    }
}
