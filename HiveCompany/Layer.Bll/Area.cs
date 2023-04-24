using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveCompany.Bll
{
    public class Area
    {
        public string nome { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string coordenadas { get; set; }
    }


    public class AreaResumo
    {
        public Int64 Id { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public long Total { get; set; }
    }

}