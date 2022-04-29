<%@ Page Title="Instructions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Instructions.aspx.cs" Inherits="WebApplication1.Instructions" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Page.Title %>.</h1>
        <h2>Encryption technology, e-commerce, and anonymous currency exchange are our top priorities. Software and accounting security is our #1 priority.

        </h2>
    </hgroup>
<%--    <aside>
        <h3>More Information</h3>
        <p>        
            Use these links to find additional information.
        </p>
        <ul>
            <li><a id="A1" runat="server" href="~/FAQ.aspx">FAQ</a></li>
            <li><a id="A2" runat="server" href="~/About.aspx">About</a></li>
            <li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>
        </ul>
    </aside>--%>
         <asp:Panel ID="Panel1" runat="server">
    <article>
        <header>
            <h3>
                Typical experience for a site visitor at <a href="~/" title="CryptoCap">Cryptocap.com</a>:
            </h3>
        </header>
        <br />
        <%--<img src="Images/Crypto/CryptoCapDiagramSiteVisitor.png" />--%>
        <figure runat="server">
            <asp:Image ID="imageInstructions" runat="server" ImageUrl="~/Images/Crypto/CryptoCapDiagramSiteVisitor.png"  BorderColor="#66FFFF" BorderStyle="Solid" BorderWidth="1px" />
        </figure>
            
    </article>
             </asp:Panel>

</asp:Content>