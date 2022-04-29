<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApplication1.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Page.Title %>.</h1>
        <h2>Our Account Executives are available to discuss your processing needs or questions from US Eastern Time:</h2>
        <h3>To reach an Account Executive or to learn more about us, please call [888-895-8965] or email Info@CryptoCap.com.</h3>
    </hgroup>
    <aside>
    <section class="contact">
        <header>
            <h3>Phone:</h3>
        </header>
        <ul>
            <li>Monday – Friday: 8 a.m. to 6 p.m. </li>
            <li>Saturday and Sunday: 9 a.m. to 3 p.m.</li>
        </ul>
        <p>
            <span class="label">Main:</span>
            <span>888-895-8965</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>888-895-8967</span>
        </p>
    </section>
    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Info@CryptoCap.com">Info@CryptoCap.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@CrytpoCap.com">Marketing@CryptoCap.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@CryptoCap.com">General@CryptoCap.com</a></span>
        </p>
    </section>
    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <address>
            One Crypto Capital Way
            <br />
            Bikini, MI 96960-4176
        </address>
    </section>
    </aside>
    <article>
        <h3>
            Feel free to call or email at any time of the day and we will do our best to acknowledge and answer your inquiry. 
            The Bitcoin market operates globally and does not stop, and we will do our best to accommodate any inquiries. 
        </h3>
    </article>
    <br /><br />
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
</asp:Content>