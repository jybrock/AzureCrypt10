using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// // The subscribe and unsubscribe message data are very simple, containing the
/// channel and the operation.
/// {
///   "channel":<CHANNEL_ID>,
///   "op":"subscribe" OR "unsubscribe"
/// }
/// </summary>
public class CHANNEL_ID {

    #region Aggregations
    #endregion

    #region Compositions
    #endregion

    #region Attributes
    private object m_op = new CHANNEL_ID();
    /// <summary>
    /// 
    /// </summary>
    public object op {
        get {
            return m_op;
        }
        set {
            m_op = value;
        }
    }

    private string m_Ticker = "d5f06780-30a8-4a48-a2f8-7ed181b4a13f";
    /// <summary>
    /// 
    /// </summary>
    public string Ticker {
        get {
            return m_Ticker;
        }
        set {
            m_Ticker = value;
        }
    }
    

    private string m_Depth = "24e67e0d-1cad-4cc0-9e7a-f8523ef460fe";
    /// <summary>
    /// 
    /// </summary>
    public string Depth {
        get {
            return m_Depth;
        }
        set {
            m_Depth = value;
        }
    }
    


    private string m_Trades = "dbf1dee9-4f2e-4a08-8cb7-748919a71b21";
    /// <summary>
    /// 
    /// </summary>
    public string Trades {
        get {
            return m_Trades;
        }
        set {
            m_Trades = value;
        }
    }
    #endregion


    #region Public methods
    #endregion


    #region Protected methods
    #endregion


    #region Private methods
    #endregion
}