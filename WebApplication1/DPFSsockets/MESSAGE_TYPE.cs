using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The MESSAGE_TYPE field may take the values:
/// </summary>
public class MESSAGE_TYPE {

    #region Aggregations


    #endregion

    #region Compositions


    #endregion

    #region Attributes

    /// <summary>
    /// Ticker messages
    /// </summary>
    public string strTicker {
        get {
            return m_strTicker;
        }
        set {
            m_strTicker = value;
        }
    }
    private string m_strTicker = "ticker";


    /// <summary>
    /// Trades, as they occur
    /// </summary>
    public string strTrades {
        get {
            return m_strTrades;
        }
        set {
            m_strTrades = value;
        }
    }
    private string m_strTrades = "trade";


    /// <summary>
    /// Orders placed or removed
    /// </summary>
    public string strDepth {
        get {
            return m_strDepth;
        }
        set {
            m_strDepth = value;
        }
    }
    private string m_strDepth = "depth";


    /// <summary>
    /// The result of a websocket-encapsulated version 1 HTTP API request
    /// </summary>
    public string strResult {
        get {
            return m_strResult;
        }
        set {
            m_strResult = value;
        }
    }
    private string m_strResult = "result";



    #endregion


    #region Public methods

    #endregion


    #region Protected methods

    #endregion


    #region Private methods

    #endregion


}

