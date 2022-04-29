<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Page.Title %>.</h1>
        <h2>About Crypto Capital.</h2>
    </hgroup>

    <article>
        <p>        
            As members of the Bitcoin community use Bitcoins peer-to-peer and for payments of goods and services businesses offer, the Bitcoin network will continue to expand. 
            We value the ownership and direct connection of that growth as we are evolving the way business is done throughout the world. 
            The goal of Crypto Capital is to become a major global provider of the Bitcoin. 
            The demand for a change in the right direction for all economies throughout the world is extremely high. 
            The Bitcoin network, if utilized properly, can be an integral factor in the way we motivate and drive change.
        </p>

        <p>        
            As stated in our Ethics Statement, we do not condone the use of Bitcoins for activities known to be illegal or deemed to be immoral. 
            If any possibility or discussion of these activities is made known, we will discontinue any communication and business will seize. 
            The core values Crypto Capital is established upon are honor, trust, and respect of privacy. 
        </p>
        <ul>
            <li>Honor: Each order is backed with our personal honor to provide an accurate and timely processing time. 
                We guarantee to honor each and every transaction to its completion. </li>
            <li>Trust: We understand the Bitcoin relies heavily on the trust of individuals, as it’s currently used primarily as a peer-to-peer system. 
                With our confirmation transaction numbers, ease of ordering, and preferred communication through encrypted emails, you do not have to be concerned about making us your trusted provider. </li>
            <li>Respect of Privacy: Simply put, we offer our clients the privacy everyone deserves when conducting business. 
                One way we do this is through our primary means of communication, which is PGP encrypted emails. 
                We use encryption to maximize our client’s personal information that needs to neither be stored, nor provided to third parties. 
                The Bitcoin is a semi-anonymous process and we strive to accommodate and respect that policy. 
                The only information we require are to follow current laws and regulations. 
                As stated in our Privacy Policy, that information will not be distributed or sold to any third party and will be used only for our records. </li>
        </ul>
        <p>        
            As stated above, feel free to communicate via encrypted emails. 
            The emails, along with any information included will be used solely for our company records and for communicating purposes. 
            We will not report, sell, or distribute emails to any outside or third party. 
            Our primary means of communication is PGP encryption, but that is our client’s preference. 
            Also, feel free to call if you would like speak to us.
        </p>
    </article>

    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/About.aspx">About</a></li>
            <li><a runat="server" href="~/Contact.aspx">Contact</a></li>
        </ul>
    </aside>
</asp:Content>