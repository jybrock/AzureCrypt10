using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication1.Net.WebSockets;
using WebApplication1.Net.Security;

namespace WebApplication1 {
    public class Chat : MtGoxWebSocket {
        //private static int _num = 0;
        //private string _name;
        //private string getName() {
            
        //    return QueryString.Contains("name")
        //           ? QueryString["name"]
        //           : "anon#" + getNum();
        //}

        //private int getNum() {
        //    return Interlocked.Increment(ref _num);
        //}

        //protected override void OnOpen() {
        //    _name = getName();
        //}

        //protected override void OnMessage( MessageEventArgs e ) {
        //    var msg = String.Format("{0}: {1}", _name, e.Data);
        //    Broadcast(msg);
        //}

        //protected override void OnClose( CloseEventArgs e ) {
        //    var msg = String.Format("{0} got logged off...", _name);
        //    Broadcast(msg);
        //}
    }
}