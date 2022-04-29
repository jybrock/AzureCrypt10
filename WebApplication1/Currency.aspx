<%@ Page Title="Currency" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="WebApplication1.Currency" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
        <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Approved Currencies List.</h2>
    </hgroup>
    <aside>
        <h3>Approved Currencies</h3>
        <p>        
            Find instructions or learn more about approved currencies:
        </p>
        <ul>
            <li><a runat="server" href="~/Instructions.aspx">Instructions</a></li>
            <li><a runat="server" href="~/FAQ.aspx">Frequently Asked Questions</a></li>
            <li><a runat="server" href="~/Ticker.aspx">Ticker</a></li>
        </ul>
    </aside>
    <article>
        <h3>
            PayPal
        </h3>
        <p>        
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/paypal-trademark.png" Width="128px" />
        </p>
        <h3>
            Dwolla
        </h3>
        <p>        
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/dwolla-trademark.png" Width="128px" />
        </p>
        <h3>
            Western Union
        </h3>
        <p>        
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/western-union-dark-glossed.png" />
        </p>
        <h3>
            Cash
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/cash-payment.png" />
        </p>
        <h3>
            Check 
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/check-mail.png" />
        </p>
        <h3>
            Prepaid Debit Card
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/debit-card-black-white.png" />
        </p>
        <h3>
            Walmart Money Card
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/moneycard-medium128x80.png" />
        </p>
        <h3>
            Credit Card
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/credit-card-small-128x128.png" Height="109px" Width="109px" />
        </p>
        <h3>
            Cashier's Check
        </h3>
        <p>
            <asp:Image runat="server" ImageUrl="~/Images/Crypto/cash-register.png" />
        </p>
    </article>
</asp:Content>