<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    Instrução MoIP: <a href="https://desenvolvedor.moip.com.br/sandbox/Instrucao.do?token=<%=ViewData["Token"] %>" title="Pagar">Pague no MoIP!</a>
    <h2><%= Html.Encode(ViewData["Message"]) %></h2>
    <p>
        Para aprender mais sobre o MoIP Labs, visite <a href="http://labs.moip.com.br/" title="ASP.NET MVC Website">http://labs.moip.com.br/</a>.
    </p>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
</asp:Content>
