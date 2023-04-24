<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLoader.ascx.cs" Inherits="HiveCompany.Controls.ucLoader" %>
<asp:UpdateProgress ID="upgProgress" runat="server" DisplayAfter="10" class="upgProgress">    
    <ProgressTemplate>                        
        <div class="loader">                  
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>