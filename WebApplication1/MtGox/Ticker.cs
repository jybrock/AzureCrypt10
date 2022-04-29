using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.MtGox {
    public class Ticker {
        
        /* Result
         */ 
        string Result { get; set; }

        /* Data
         */ 
        object Data { get; set; }

        /* Item
         */ 
        string Item { get; set; }

        /* Now
         */ 
        int Now { get; set; }
    }

    public class High : Ticker {
        
        /* Value
         */
        decimal value { get; set; }
        
        /* Value
         */ 
        int value_int { get; set; }

        /* Display
         */ 
        string display { get; set; }

        /* Display
         */ 
        short display_short { get; set; }
        
        /* Currency
         */ 
        string currency { get; set; }

    }


    public class Low : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }

    }


    public class avg : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class vwap : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }

    public class vol : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class last_local : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }

    public class last_orig : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class last_all : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class last : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class buy : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }


    public class sell : Ticker {

        /* Value
         */
        decimal value { get; set; }

        /* Value
         */
        int value_int { get; set; }

        /* Display
         */
        string display { get; set; }

        /* Display
         */
        short display_short { get; set; }

        /* Currency
         */
        string currency { get; set; }
    }

}