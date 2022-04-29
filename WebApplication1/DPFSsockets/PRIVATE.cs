using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// // The payloads of the op:private messages contain the real time market
/// information. Each message follows this form:
/// {
///   "channel":<CHANNEL_ID>,
///   "op":"private",
///   "private":<MESSAGE_TYPE>,
///   <MESSAGE_TYPE>:<DATA_PAYLOAD>
/// }
/// </summary>
public class PRIVATE {

    #region Aggregations


    #endregion

    #region Compositions


    #endregion

    #region Attributes

    /// <summary>
    /// 
    /// </summary>
    public object channel {
        get {
            return m_channel;
        }
        set {
            m_channel = value;
        }
    }
    private object m_channel = new CHANNEL_ID();


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
    private object m_op = new PRIVATE();


    /// <summary>
    /// 
    /// </summary>
    public object M_PRIVATE {
        get {
            return m_PRIVATE;
        }
        set {
            m_PRIVATE = value;
        }
    }
    private object m_PRIVATE = new MESSAGE_TYPE();


    /// <summary>
    /// 
    /// </summary>
    public object MESSAGE_TYPE {
        get {
            return m_MESSAGE_TYPE;
        }
        set {
            m_MESSAGE_TYPE = value;
        }
    }
    private object m_MESSAGE_TYPE;
    #endregion


    #region Public methods
    #endregion


    #region Protected methods
    #endregion


    #region Private methods
    #endregion
}

