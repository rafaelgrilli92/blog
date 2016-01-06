using code_4_lazy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace code_4_lazy
{
    public class MetodoHelper
    {
        public MetodoHelper() { }

        public void ContarVisita(object visitou)
        {
            string IP = visitou.ToString();
            VisitaSite visita = new VisitaSite();
            visita.IP = IP;
            visita.DataHoraVisita = DateTime.Now;
            VisitaSiteDAO dao = new VisitaSiteDAO();
            dao.InserirVisita(visita);
        }
    }
}