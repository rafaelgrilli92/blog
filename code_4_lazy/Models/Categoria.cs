using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class Categoria
    {
        public virtual int Id { get; set; }
        public virtual string NomeCategoria { get; set; }
    }

    class CategoriaMapping : ClassMap<Categoria>
    {
        public CategoriaMapping()
        {
            Table("Categorias");
            Id(post => post.Id).GeneratedBy.Identity();
            Map(post => post.NomeCategoria);
        }
    }
}