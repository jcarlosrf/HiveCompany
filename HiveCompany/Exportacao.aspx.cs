using HiveCompany.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HiveCompany.Dao;
using HiveCompany.Layer.Bll;

namespace HiveCompany
{
    public partial class Exportacao : Page
    {
        private List<AreaResumo> AreasResumos
        {
            get
            {
                if (Session["AreasResumos"] == null)
                    Session["AreasResumos"] = new List<AreaResumo>();

                return (List<AreaResumo>)Session["AreasResumos"];
            }
            set
            {
                Session["AreasResumos"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                chkCidades.Checked = false;
            }
        }


        protected void dgvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdExcel")
            {
                try
                {
                   
                    int index = Convert.ToInt32(e.CommandArgument); // obter índice da linha selecionada
                    int idArea = Convert.ToInt32(dgvDados.DataKeys[index].Value);

                    var areaSelecionada = AreasResumos.FirstOrDefault(a => a.Id == idArea);

                    if (areaSelecionada == null)
                    {
                        lblMessage.Text = "Não foi possível gerar planilha";
                        lblMessage.Visible = true;
                        return;
                    }

                    var repository = new AreasRepository();
                    var retorno = repository.GetPontosContidos(areaSelecionada.UF, chkCidades.Checked ? areaSelecionada.Cidade : string.Empty);                                        
                    string FileName = string.Format("{0}{1}_{2}.csv", areaSelecionada.UF, chkCidades.Checked ? areaSelecionada.Cidade : string.Empty
                        , DateTime.Now.ToString("yyyyMMddHHmmss"));

                    if (ArquivoCsv.ExportarParaCSV(retorno, FileName))
                    {                        
                        ArquivoCsv.DownloadCSV(FileName);
                    }
                    else
                    {
                        lblMessage.Text = "Não foi possível gerar planilha";
                        lblMessage.Visible = true;
                        return;
                    }
                                        
                }
                catch (Exception ex)
                {

                    lblMessage.Text = "Não foi possível gerar planilha - ERRO: " + ex.Message;
                    lblMessage.Visible = true;
                }
                
            }
        }

        protected void dgvDados_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    AreasRepository rep = new AreasRepository();

                    AreasResumos = rep.GetAreasResumo(chkCidades.Checked);



                    dgvDados.AutoGenerateColumns = false;
                    dgvDados.DataSource = AreasResumos;
                    dgvDados.DataBind();

                    if (AreasResumos.Count == 0)
                    {
                        lblMessage.Text = "Nenhuma viagem enctrada.";
                        lblMessage.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Pesquisa de viagens indisponível. Tente mais tarde";
                lblMessage.Visible = true;
            }
        }
    }
}