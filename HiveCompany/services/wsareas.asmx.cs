using HiveCompany.Bll;
using HiveCompany.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace HiveCompany.services
{
    /// <summary>
    /// Summary description for wsareas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]    
    public class wsareas : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string SaveSessionPolygon(List<Area> areas)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            try
            {
                if (Session["Areas"] == null)
                    Session["Areas"] = new List<Area>();

                List<Area> SessionArea = (List<Area>)Session["Areas"];

                string cidade = areas.FirstOrDefault(a => String.IsNullOrEmpty(a.cidade) == false).cidade;
                string uf = areas.FirstOrDefault(a => String.IsNullOrEmpty(a.cidade) == false).uf;


                foreach (var area in areas)
                {
                    string coordenada = area.coordenadas.Replace(",0 ", "? ").Replace(",0", "").Replace(",", " ").Replace("? ", ", ");
                    var coords = coordenada.Split(',');

                    if (coords[0] != coords[coords.Length - 1])
                        coordenada += ", " + coords[0];

                    area.coordenadas = coordenada;
                    area.cidade = cidade;
                    area.uf = uf;

                    SessionArea.Add(area);
                }

                Session["Areas"] = SessionArea;

                return js.Serialize("");
            }
            catch
            {
                return js.Serialize("Atualize a tela (F5) e tente novamente, não foi possível carregar área.");
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public string SaveBdPolygon()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            try
            {
                if (Session["Areas"] == null)
                    return js.Serialize("Nenhuma área salva");

                List<Area> SessionArea = (List<Area>)Session["Areas"];

                var areaRepository = new AreasRepository();

                if (areaRepository.InserirAreas(SessionArea))
                {
                    Session["Areas"] = new List<Area>();
                    return js.Serialize("");
                }
                else
                    return js.Serialize("Erro ao salvar áreas, tente novamente.");
            }
            catch
            {
                return js.Serialize("Erro ao salvar áreas, tente novamente.");
            }
        }
    }

}
