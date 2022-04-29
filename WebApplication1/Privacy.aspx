<%@ Page Title="Privacy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Privacy.aspx.cs" Inherits="WebApplication1.Privacy" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>CryptoCap Privacy Policy.</h2>
    </hgroup>

<%--    <article>
        <p>        
            [Crypto Capital Privacy Policy].
        </p>

    </article>--%>
     <article>
        <header>
            <h1>
                Ethics Statement
            </h1>
        </header>
            <blockquote>
                <q>
                    <mark>
                        Crypto Capital values the privacy and freedom of choice of our clients. 
                    </mark>
                </q>
                <i>
                    That said, we do abide by laws and regulations set in place.
                </i>
            </blockquote>
            <blockquote>
                <q>
                    We do not believe one’s private information should be compromised or released to any party outside our company. 
                </q>
            </blockquote>
            <blockquote>
                <q>
                    We do not condone or contribute to any parties that use the Bitcoin for activities that may deem illegal or immoral.
                </q>
            </blockquote>
            <blockquote>
                <q>
                    We will not conduct business with any person(s) having the intent to use Bitcoins for anything thought to be unlawful or ethically immoral.
                </q>
            </blockquote>
    </article>
<%--    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a id="A1" runat="server" href="~/">Home</a></li>
            <li><a id="A2" runat="server" href="~/About">About</a></li>
            <li><a id="A3" runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>--%>
</asp:Content>