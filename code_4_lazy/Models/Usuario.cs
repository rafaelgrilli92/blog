using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class Usuario
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual string Login { get; set; }

        [Required]
        public virtual string Senha { get; set; }

        [Required]
        public virtual string Nome { get; set; }

        [Required]
        public virtual string Email { get; set; }
    }

    class UsuarioMapping : ClassMap<Usuario>
    {
        public UsuarioMapping()
        {
            Table("Usuarios");
            Id(usuario => usuario.Id).GeneratedBy.Identity();
            Map(usuario => usuario.Login);
            Map(usuario => usuario.Senha);
            Map(usuario => usuario.Nome);
            Map(usuario => usuario.Email);
        }
    }

}