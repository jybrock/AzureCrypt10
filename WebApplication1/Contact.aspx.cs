using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1 {
    public partial class Contact : Page {

        public static string strPhoneNumber { get; set; }
        public static string strPhoneNumberAfterHours { get; set; }
        public static string strEmailInfo { get; set; }
        public static string strEmailMarketing { get; set; }
        public static string strEmailGeneral { get; set; }

        protected void Page_Load( object sender, EventArgs e ) {

            string strphoneNumber = "888-856-9856";
            strPhoneNumber = strphoneNumber;

            string strphoneNumberAfterHours = "888-856-9857";
            strPhoneNumberAfterHours = strphoneNumberAfterHours;

            string stremailInfo = "Info@CryptoCap.com";
            strEmailInfo = stremailInfo;

            string stremailMarketing = "Marketing@CryptoCap.com";
            strEmailMarketing = stremailMarketing;

            string stremailGeneral = "General@CryptoCap.com";
            strEmailGeneral = stremailGeneral;
        }
    }
}