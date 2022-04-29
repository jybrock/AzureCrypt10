using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebApplication1.MtGox {
    public class AllTickers {

        int intAllTickerTimeStamp { get; set; }

        string strCurrency { get; set; }
            //"sell": "112.50999", 
            //"buy": "112.50000", 
            //"last": "112.50999", 
            //"vol": "39445.35586445", 
            //"vwap": "112.62406", 
            //"high": "115.84000", 
            //"low": "108.80000" 
        
        /* Sell
         */
        string sell { get; set; }
        double doubleSell { get; set; }
        decimal decimalSell { get; set; }
        int intSell { get; set; }
        
        /* Buy
         */ 
        string buy { get; set; }
        double doubleBuy { get; set; }
        decimal decimalBuy { get; set; }
        int intBuy { get; set; }
        
        /* Last
         */ 
        string last { get; set; }
        double doubleLast { get; set; }
        decimal decimalLast { get; set; }
        int intLast { get; set; }
        
        /* Vol
         */ 
        string vol { get; set; }
        double doubleVol { get; set; }
        decimal decimalVol { get; set; }
        int intVol { get; set; }
        
        /* VWAP
         */ 
        string vwap { get; set; }
        double doubleVWAP { get; set; }
        decimal decimalVWAP { get; set; }
        int intVWAP { get; set; }
        
        /* High
         */ 
        string high { get; set; }
        double doubleHigh { get; set; }
        decimal decimalHigh { get; set; }
        int intHigh { get; set; }
        
        /* Low
         */ 
        string low { get; set; }
        double doubleLow { get; set; }
        decimal decimalLow { get; set; }
        int intLow { get; set; }
    
    }
}