<%@ Page Title="Crypto Capital" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>
                    <%: Page.Title %>.
                </h1>
                <h2>
                    With Crypto Capital, you can expect nothing less than exceptional service and low fees.
                </h2>
            </hgroup>
            <h2>
                <a href="http://cryptocap.azurewebsites.net" title="Crypto Capital">Crypto Capital</a> 
                is dedicated to providing a <mark>safe, easy, and professional</mark> delivery of Bitcoins. 
            </h2>
            <p>
                We pride ourselves in giving the same respect for the Bitcoin as the people who use the Bitcoin. 
                We understand and value the importance of <mark>quick delivery, a low-risk process, and anonymous ordering.</mark> <br />

                As the Bitcoin continues to thrive and gain recognition throughout the world, we strive to be your trusted provider.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h5>Getting Started</h5>
          Here is a section for easy instruction on the basic transaction cycle and process.
            A link is provided to show a picture description or step-by-step cycle for what to expect and how the currency exchange is completed.
            <a href="Instructions.aspx">Link to Learn more…</a>
        </li>
        <li class="two">
         <h5>Payment Methods</h5>
            Approved currency types and payment methods should be briefly explained here by mentioning basic restrictions and security.
            <a href="Currency.aspx">Link to Complete List of Approved Sources…</a>
        </li>
        <li class="three">
            <h5>Safe and Anonymous Exchange</h5>
            Anonymous commercial transactions can protect the privacy of consumers. Our promise is to maximize anonymity for trusted members.
            Some consumers prefer to use cash when buying everyday goods (like groceries or tools), to prevent sellers from aggregating information or soliciting them in the future.
            Our goal is to bring the same security to the online marketplace.
            <a href="Security.aspx">Link to Security Policy…</a>
        </li>
    </ol>
</asp:Content>
