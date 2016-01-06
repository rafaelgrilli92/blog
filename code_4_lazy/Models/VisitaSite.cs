using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy.Models
{
    public class VisitaSite
    {
        public virtual int Id { get; set; }
        public virtual string IP { get; set; }
        public virtual DateTime DataHoraVisita { get; set; }
    }

    class VisitaSiteMapping : ClassMap<VisitaSite>
    {
        public VisitaSiteMapping()
        {
            Table("VisitasSite");
            Id(visita => visita.Id).GeneratedBy.Identity();
            Map(visita => visita.IP);
            Map(visita => visita.DataHoraVisita).CustomSqlType("TIMESTAMP").Default("CURRENT_TIMESTAMP");
        }
    }
}