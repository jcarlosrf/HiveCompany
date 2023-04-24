using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiveCompany.Bll
{
    public class Contidos
    {
        public Int64 Id { get; set; }
        public string Cep { get; set; }

        public string Uf { get; set; }

        public string Cidade { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}