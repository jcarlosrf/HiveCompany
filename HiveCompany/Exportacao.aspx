<%@ Page Title="Exportação" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exportacao.aspx.cs" Inherits="HiveCompany.Exportacao" %>

<%@ Register Src="~/Controls/ucLoader.ascx" TagPrefix="uc1" TagName="ucLoader" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
    $(function () {
        // Esconde o loader ao carregar a página
        $("#<%= ucLoader.ClientID %>").hide();

        // Mostra o loader quando o botão de download for clicado
        $("#<%= dgvDados.ClientID %>").click(function () {
            $("#<%= ucLoader.ClientID %>").show();
        });
    });
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset id="Fieldset2" runat="server" class="fieldset">
        <legend class="legendFieldset well-sm">Carregar áreas de cobertura</legend>


        <asp:UpdatePanel ID="updMapa" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning" EnableViewState="False" Visible="false"></asp:Label>
                <uc1:ucLoader runat="server" ID="ucLoader" AssociatedUpdatePanelID="updMapa" />

                <div id="divBuscaDados" title="Buscar Registros" class="container-fluid">
                    <div class="col-xs-6 col-sm-4">
                        <asp:Label ID="lblCidades" runat="server" Text="Detalhar por cidade" AssociatedControlID="chkCidades"></asp:Label>
                        <asp:CheckBox ID="chkCidades" runat="server" AutoPostBack="false" />
                    </div>

                    <div class="col-xs-6 col-sm-4">
                            <div class="input-group">
                                <asp:Label ID="lblUf" runat="server" Text="Filtrar por UF" AssociatedControlID="txtUf"
                                    SkinID="labelItem"></asp:Label>
                                <asp:TextBox ID="txtUf" runat="server" placeholder="Uf" aria-describedby="basic-addon1"
                                    SkinID="text100" ></asp:TextBox>
                            </div>
                    </div>

                    <div class="col-xs-12 col-sm-4">
                        <div class="right">
                            <asp:Button ID="btnSearch" Text="Pesquisar" runat="server" CssClass="btn btn-pesquisar btn-primary form-control" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="col-xs-12" style="margin-top: 10px">
                        <div class="table-responsive">
                            <asp:GridView ID="dgvDados" runat="server" AutoGenerateColumns="False" EmptyDataText="A pesquisa não encontrou resultados."
                                OnRowCommand="dgvDados_RowCommand" OnRowDataBound="dgvDados_RowDataBound" SkinID="gridViagem"
                                DataKeyNames="id">
                                <Columns>
                                    <asp:BoundField DataField="id" HeaderText="Id" ReadOnly="True" SortExpression="id" />
                                    <asp:BoundField DataField="uf" HeaderText="UF" ReadOnly="True" SortExpression="uf" />
                                    <asp:BoundField DataField="cidade" HeaderText="Cidade" SortExpression="cidade" />
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnExcel" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CommandName="cmdExcel" ImageUrl="~/Content/images/excel.png" Text="Planilha" CausesValidation=true />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="dgvDados" />
            </Triggers>
        </asp:UpdatePanel>


    </fieldset>
</asp:Content>
