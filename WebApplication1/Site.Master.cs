using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1 {
    public partial class SiteMaster : MasterPage {


        #region crypto Properties and Fields
        public int unixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

        #endregion


        #region WebSocket
        public WebApplication1.WebSocket websocket { get; set; }
        public void _websocket() {
            websocket.Url = new Uri("https://data.mtgox.com/api/2/BTCUSD/money/ticker", UriKind.Absolute);
            if (websocket.IsSecure) { 
                //HttpWebRequest.Create("https://data.mtgox.com/api/2/BTCUSD/money/ticker"); 
                websocket.Connect();
                string strArrayExtensions = websocket.Extensions;
            };
            
            
        }


        #endregion


        #region Newtonsoft
        public static string json = @"{
              'href': 'https://data.mtgox.com/api/2/BTCUSD/money/ticker',
              'target': '_blank'
                }";

        public static JsonSerializer jsonSerializer = new JsonSerializer();
        
        public static Dictionary<string,string> htmlAttributes {get; set;}
        public static IDictionary<string,string> _htmlAttributes() {
            Dictionary<string, string> HtmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            htmlAttributes = HtmlAttributes;
            
            return HtmlAttributes;
        }
        #endregion


        //#region Mt.Gox Ticker
        //public WebClient _webclient = new WebClient();

        //public WebClient getMtGox() {

        //    return _webclient;
        //}

        //public string usd() {
        //    //_websocket();
        //    WebClient webclient = getMtGox();
        //    webclient.BaseAddress = new Uri("http://data.mtgox.com/api/2/BTCUSD/money/ticker", UriKind.Absolute).ToString();
        //    int intKeys = 0;
        //    if (!webclient.IsNull()) {
        //        string[] arrAllKeys;
        //        string[] arrAllValues;
        //        if (!Object.Equals(null, webclient)) {
        //            foreach (var key in webclient.ResponseHeaders.Keys) {
        //                arrAllKeys = webclient.ResponseHeaders.AllKeys;
        //                intKeys = intKeys + 1;
        //            }
        //        } else {
                    
        //        }

                
        //        if (!webclient.IsNull()) {
        //            int i = 0;
        //            do {
        //                i = i + 1;
        //            } while (i < intKeys);
        //        }
                
                


        //        getMtGox();
        //        return webclient.QueryString.GetValues("ResponseBody").ToString();
        //    }

        //    return "no workie";
        //}
        

        //#endregion


        #region Date and Time 
        public static DateTime _dtDateTime {
            get {
                int intPosixTime;
                System.DateTime __DateTime__ = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if (_unixTime == 0) {
                    intPosixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                    __DateTime__ = __DateTime__.AddSeconds(intPosixTime).ToLocalTime();
                }

                return __DateTime__;
            }
            set {
                _dtDateTime = value;
            }
        }
        /*
        * Conversion of POSIX (Unix time) from Mt.Gox to C#
        */
        public DateTime UnixTimeStampToDateTime( int unixTimeStamp ) {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return _dtDateTime;
        }
        public static int _unixTime { get { return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds; } }
        /*
        * Conversion of C# Localtime to POSIX (Unix time)
        */
        public int UnixTime { get { return _unixTime; } private set { unixTime = value; } }

        #endregion


        #region Multiview and Views
        private static int _intActiveViewIndex = 0;
        public int intActiveViewIndex { get; set; }
        /*
        * 
        */
        private static string _htmlActiveViewIndex = "";
        public string htmlActiveViewIndex {
            get {
                string strDefault = "";
                if (String.IsNullOrEmpty(_htmlActiveViewIndex)) {
                    strDefault = "<asp:MultiView ActiveViewIndex=" + 0 + " />";
                }
                return "ddfa"; // _htmlActiveViewIndex ?? strDefault;
            }
            set {
                _htmlActiveViewIndex = value;
            }
        }
        /*
        * Multiview Tickers and Grids  
        */
        protected void NextButton_Command( object sender, EventArgs e ) {
            /* 
            * Determine which button was clicked
            * and set the ActiveViewIndex property to
            * the view selected by the user.
            */
            _intActiveViewIndex = _intActiveViewIndex + 1;
            if (cryptoContent.ActiveViewIndex > -1 & cryptoContent.ActiveViewIndex < 3) {
                /* 
                * Increment the ActiveViewIndex property 
                * by one to advance to the next view.
                */
                cryptoContent.ActiveViewIndex += 1;
            } else if (cryptoContent.ActiveViewIndex == 3) {
                /* 
                * This is the final view.
                * The user wants go back to Mt.Gox
                * // buttonCampBX.Enabled = false;
                * // buttonCampBX.Enabled = false;
                */
            } else {
                throw new Exception("An error occurred.");
            }
        }
        
        #endregion


        #region JSON Validation
        public void jsonParse() {
            string strJSONObject = "";
            string strJSONArray = "";
            JObject.Parse(strJSONObject);
            JArray.Parse(strJSONArray);
            //var my_JSON_object = !(/[^,:{}\[\]0-9.\+Eaeflnr-u\n\r\t]/.test(strJSONObject.replace(/"(\\.[^"\\])*"g,"))) && eval('(' + strJSONObject +')');

        }


        #endregion

        #region Site Master Constructor Code //////////////  Site Master Constructor Code ////////////// Site Master Constructor Code //////////////
        /*
        * Main Page Constructor
        */
        protected void Page_Load( object sender, EventArgs e ) {


            /*
            * Load Nothing Do Nothing
            */
            _htmlAttributes();
            if (object.Equals(null, htmlAttributes)) {
                if (object.Equals(null, _htmlAttributes())) {
                    Dictionary<string, string> HtmlAttributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
                JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }

            //string strLastBTCPrice = htmlAttributes["high"];
            string strLastBTCPrice = "";
            strLastBTCPrice = strLastBTCPrice + "Mt.Gox | Live BTC Price | Data Length: " + json.Length.ToString();
            

            if (!String.IsNullOrEmpty(strLastBTCPrice)) {
                labelTestTickerOutput.Text = strLastBTCPrice;
            } else {
                labelTestTickerOutput.Text = "MtGox JSON NULL";
            }
            

            UnixTime = UnixTime;
            labelUnixTime.Text = "Satoshi Time: " + UnixTime.ToString();
            if (UnixTime > 0) {
                UnixTimeStampToDateTime(UnixTime);
            }

            //string testUSDticker = usd();
            //if (!String.IsNullOrEmpty(testUSDticker)) {
            //    labelTestTickerOutput.Text = testUSDticker;
            //}

        }

        #endregion

    }
}