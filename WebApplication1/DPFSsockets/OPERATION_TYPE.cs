using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// // The OPERATION_TYPE field may take these values:
/// {
///   "op":<OPERATION_TYPE>
/// }
/// </summary>
public class OPERATION_TYPE {

    #region Aggregations


    #endregion

    #region Compositions


    #endregion

    #region Attributes

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
    private object m_op = new OPERATION_TYPE();


    /// <summary>
    /// 
    /// </summary>
    public string strPrivate {
        get {
            return m_strPrivate;
        }
        set {
            m_strPrivate = value;
        }
    }
    private string m_strPrivate = "private";


    /// <summary>
    /// 
    /// </summary>
    public object REMARK {
        get {
            return m_REMARK;
        }
        set {
            m_REMARK = value;
        }
    }
    private object m_REMARK = "remark";


    /// <summary>
    /// 
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


    /// <summary>
    /// 
    /// </summary>
    public string strSubscribe {
        get {
            return m_strSubscribe;
        }
        set {
            m_strSubscribe = value;
        }
    }
    private string m_strSubscribe = "subscribe";


    /// <summary>
    /// 
    /// </summary>
    public string strUnsubscribe {
        get {
            return m_strUnsubscribe;
        }
        set {
            m_strUnsubscribe = value;
        }
    }
    private string m_strUnsubscribe = "unsubscribe";



    #endregion


    #region Public methods

    #endregion


    #region Protected methods

    #endregion


    #region Private methods

    #endregion


}

