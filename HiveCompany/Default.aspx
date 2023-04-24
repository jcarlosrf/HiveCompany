<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HiveCompany._Default" %>

<%@ Register Src="~/Controls/ucLoader.ascx" TagPrefix="uc1" TagName="ucLoader" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script async
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCKrcc-cBCWC3ZdpYi37nAksoq2NbUCK8o&callback=initMap">
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset id="Fieldset2" runat="server" class="fieldset">
        <legend class="legendFieldset well-sm">Carregar áreas de cobertura</legend>


        <asp:UpdatePanel ID="updMapa" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <uc1:ucLoader runat="server" ID="ucLoader" AssociatedUpdatePanelID="updMapa" />
                <div id="mapa" style="height: 700px;" class="col-sm-12 col-md-10">
                </div>
                <div class="col-vs-12 col-sm-2">

                        <div class="input-group">
                            <asp:Label ID="lblArquivos" runat="server" Text="Selecionar arquivos (.kml)" class="form-control" ></asp:Label>
                            <input id="kmlFile" title="Selecione" type='file' accept=".kml" multiple="multiple" class="kmlFile" />
                        </div>

                    
                        <div class="input-group">
                            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn-primary btn-salvar-areas" Enabled="False" class="form-control"
                                data-content="Salvar áreas" />
                        </div>
                    
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </fieldset>

</asp:Content>
