<%@ Page Title="Security" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Security.aspx.cs" Inherits="WebApplication1.Security" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Page.Title %>.</h1>
        <h2>Your security is our #1 priority.</h2>
    </hgroup>
    <aside>
        <h3>Find out more about:</h3>
        <ul>
            <li><a runat="server" href="http://www.mcafee.com/us/services.aspx">McAfee.com</a> <a runat="server" href="http://en.wikipedia.org/wiki/Mcafee">Wikipedia</a></li>
            <li><a runat="server" href="http://research.microsoft.com/apps/catalog/default.aspx">Microsoft Research</a> <a runat="server" href="http://en.wikipedia.org/wiki/Microsoft_Research">Wikipedia</a></li>
            <li><a runat="server" href="http://owasp.com/index.php/Main_Page">OWASP</a> <a runat="server" href="http://en.wikipedia.org/wiki/OWASP">Wikipedia</a></li>
        </ul>
    </aside>
    <article id="secTitle">
        <h2>
            <a runat="server" href="http://cryptocap.azurewebsites.net">Cryptocap.com</a> tests all products and services.  
        </h2>
        <h3>
            We are tested against 
            <mark>
                <q>
                    hacker-safe
                </q> 
                logic and state-of-the-art web security audits
            </mark> 
            to ensure transactional integrity. 
        </h3>
        <p>
            <a runat="server" href="~/" title="Cryptocap.com">Cryptocap.com</a> receives each visitor anonymously and passes standards set by
            <mark>
                web security technologies
            </mark> 
            such as:
        </p>
    </article>
    <br />
    <article id="secmcafee">
        <h2>    McAfee Web security.</h2>
        <br />
        <asp:Image ID="imageMcafeeMain" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Crypto/mcafeeElite-532x173.png" Width="532px" />
        <br />
     </article>
    <br /><br />
     <article id="secmicroresearch">
        <h2>    Microsft Research.</h2>
         <br />
        <asp:Image ID="imageMicrosftMain" runat="server" ImageUrl="~/Images/Crypto/microsoft-research-black.png" />
         <br />
    </article>
    <br /><br />
     <article id="secowasp">
        <h2>    OWASP (Open Web Application Security Project).</h2><br />
        <asp:Image ID="imageOWASPMain" runat="server" ImageAlign="Middle" ImageUrl="~/Images/Crypto/owaspSecurityLogo-800x.png" />
         <br />
    </article>
</asp:Content>