<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HiveCompany._Default" %>

<%@ Register Src="~/Controls/ucLoader.ascx" TagPrefix="uc1" TagName="ucLoader" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script async
        src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCKrcc-cBCWC3ZdpYi37nAksoq2NbUCK8o&callback=initMap">
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updMapa" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <uc1:ucLoader runat="server" ID="ucLoader" AssociatedUpdatePanelID="updMapa"/>

            <fieldset id="Fieldset2" runat="server" class="fieldset">
                <legend class="legendFieldset well-sm">Carregar áreas de cobertura</legend>
                <div id="maparota" style="height: 600px;" class="col-vs-12 col-sm-9">
                </div>
                <div class="col-vs-12 col-sm-3">                    
                        
                            <div class="input-group">
                                <asp:Label ID="lblArquivos" runat="server" Text="Selecionar arquivos (.kml)" ></asp:Label>
                                <input id="imageFile" type='file' accept=".kml,.kmz" class="imageFile" multiple="multiple" />
                            </div>
                        
                    </div>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
