<%@ Page Title="Graph" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Graph.aspx.cs" Inherits="WebApplication1.Graph" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Page.Title %>.</h1>
        <h2>Click on one of the following links for a real-time update.</h2>
    </hgroup>
<section>
    <article>
        <p>        
            Bitcoin Network Hash Rate.
        </p>

        <p>        
            <asp:Image ID="imageBTCHashrate" runat="server" src="http://bitcoin.sipa.be/speed-lin-ever.png" ImageUrl="http://bitcoin.sipa.be/speed-lin-ever.png" Width="100%" BorderColor="#66FFFF" BorderStyle="Solid" BorderWidth="1px" />
        </p>
    </article>
    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a id="A1" runat="server" href="~/">Home</a></li>
            <li><a id="A2" runat="server" href="~/About.aspx">About</a></li>
            <li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>
        </ul>
    </aside>
    <article>
        <p>        
            Bitcoin Network Computational Speed Growth Rate.
        </p>

        <p>        
            <asp:Image ID="imageBTCNetGrowthrate" runat="server" src="http://bitcoin.sipa.be/growth.png" BorderColor="#66FFFF" BorderStyle="Solid" BorderWidth="1px" ImageUrl="http://bitcoin.sipa.be/growth.png" Width="100%" />
        </p>
    </article>
</section>

</asp:Content>