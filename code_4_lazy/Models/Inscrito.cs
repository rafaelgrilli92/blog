using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class Inscrito
    {
        public virtual string Email { get; set; }
    }

    class InscritoMapping : ClassMap<Inscrito>
    {
        public InscritoMapping()
        {
            Table("Inscritos");
            Id(inscrito => inscrito.Email).Unique();
        }
    }
}