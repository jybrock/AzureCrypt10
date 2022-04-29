<%@ Page Title="Ticker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Ticker.cs" Inherits="WebApplication1.Ticker" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1>
            <br />
            <%: Title %>.
        </h1>
        <h2>Mt.Gox Bitcoin Market Index Data & Exchange Rates.</h2>
    </hgroup>

    <article>
          <style scoped>
                @import url("./Content/Site.css");
            </style>
        <p>        
            Provided by CryptoCap.com.<br /><br />
        </p>
        <!-- AddThis Button BEGIN -->
        <%--<section class="addthis_toolbox addthis_default_style ">
        <a class="addthis_button_preferred_1"></a>
        <a class="addthis_button_preferred_2"></a>
        <a class="addthis_button_preferred_3"></a>
        <a class="addthis_button_preferred_4"></a>
        <a class="addthis_button_compact"></a>
        <a class="addthis_counter addthis_bubble_style"></a>
        </section>--%>
    	<!-- AddThis Button END -->
        
        <!-- #ticker -->
        <h3>
            All Currency Tickers<br /><br />
        </h3>
        <div>
        <section id="ticker" class="all"></section>
           <style scoped>
                @import url("./Content/style_201304011150.css");
            </style>
        </div>
        <!-- #ticker -->
        <p>        
          <%--  Use this area to provide basic ticker.--%>
        </p>
        <br />
        
        <h3>
            Implied Exchange Rates<br /><br />
        </h3>
        <!-- #fxGrid -->
        <div>
        <section class=all id="fxGrid" style="font-size: small; word-spacing: 5px; letter-spacing: 1px; "></section>
             <style id="Style1" scoped="scoped" runat="server">
                @import url("./Content/style_201304011150.css");
            </style>
        </div>
        <!-- #fxGrid -->
        <p>        
            <%-- Use this area to detailed exchange information based on selected controls.--%>
        </p>
        <link rel="stylesheet" href="./Content/style_201304011150.css" media="all" aria-hidden="true" aria-disabled="true"/>
        <link rel="stylesheet" href="./Content/Site.css" media="all"  />
    </article>
    <aside>
          <style scoped>
                @import url("./Content/Site.css");
            </style>
        <br />
        <h3 style=all:default>Aside Title</h3><br /><br />
        <p>        
            Use this area to select different views.<br /><br />
        </p>
        <ul>

            <li><br /><style scoped>
                @import url("./Content/Site.css");
            </style><a id="A1" runat="server" href="~/Ticker.aspx">Main</a><br /></li>
            <li><br /><style scoped>
                @import url("./Content/Site.css");
            </style><a id="A2" runat="server" href="~/Graph.aspx">Graphs</a><br /></li>
            <li><br /><style scoped>
                @import url("./Content/Site.css");
            </style><a id="A3" runat="server" href="~/Ticker.aspx">Spreadsheet</a><br /></li>
        </ul>
    </aside>
    
</asp:Content>

<%-- <div class="header ticker">
                <ul id="nav">
                    <li>Services
                        <ul>
                            <li><a href="../">Mt. Gox Data</a></li>
                            <!--li><a href="../bitfloor/">Bitfloor Data</a></li-->
                        </ul>
                    </li>
                </ul>
                <h1>Mt.Gox Ticker &amp; Exchange Rates</h1>
                <p>brought to you by <a href="http://cryptocap.azurewebsites.net/">CryptoCap Design</a></p>
                    <!-- AddThis Button BEGIN -->
                    <div class="addthis_toolbox addthis_default_style ">
                  <%--      <a class="addthis_button_preferred_1"></a>
                        <a class="addthis_button_preferred_2"></a>
                        <a class="addthis_button_preferred_3"></a>
                        <a class="addthis_button_preferred_4"></a>
                        <a class="addthis_button_compact"></a>
                        <a class="addthis_counter addthis_bubble_style"></a>
                    </div>
    				<!-- AddThis Button END -->
            </div>
        <h3>All Currency Tickers</h3>
        <div id="ticker" class="all"></div>
        <h3>Implied Exchange Rates</h3>
        <div id="fxGrid"></div>
        <script type="text/javascript">var addthis_config = { "data_track_clickback": true };</script>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=ra-4de7f44e58634010"></script>--%>