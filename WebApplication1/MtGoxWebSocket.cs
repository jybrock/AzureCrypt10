using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Net;
using System.Runtime.Remoting;



namespace WebApplication1 {
    public class MtGoxWebSocket {
      //  private static HttpServer _httpsv;
      //        _httpsv = new HttpServer(4649);
      ////_httpsv.Sweeped = false;
      //_httpsv.AddWebSocketService<Echo>("/Echo");
      //_httpsv.AddWebSocketService<Chat>("/Chat");

      //_httpsv.OnGet += (sender, e) => {
      //  onGet(e);
      //};

      //_httpsv.OnError += (sender, e) => {
      //  // Console.WriteLine(e.Message);
      //};

      //_httpsv.Start();
      //// Console.WriteLine("HTTP Server listening on port: {0} service path:", _httpsv.Port);
      //foreach (var path in _httpsv.ServicePaths)
      //  // Console.WriteLine("  {0}", path);
      //  // Console.WriteLine();

      //  // Console.WriteLine("Press any key to stop server...");
      //  // Console.ReadLine();

      //_httpsv.Stop();       
        


      //  private static HttpRequest _httpRequest = null;
      //  /*
      //  * Http Request Object to use later
      //  */ 
      //  public HttpRequest httpRequest { get; set; }

        

      //  public HttpRequestWrapper httpRequestWrapper  = new  HttpRequestWrapper(_httpRequest);
      //  /*
      //  * Start getting Client Browser Session Information 
      //  */ 
      //  public void getAcceptedTypes() {
      //      string[] arrAcceptTypes;
      //          arrAcceptTypes = httpRequestWrapper.AcceptTypes;
      //          if (!String.IsNullOrEmpty(arrAcceptTypes[0].ToString())) {
      //              // Do something with Browser's Available MIMI Types
      //          };
      //  }
    




      //  private static void onGet( HttpRequestEventArgs eventArgs ) {
      //      var request = eventArgs.Request;                                    // Event Args #Request
      //      var response = eventArgs.Response;                                  // Event Args #Response
      //      var content = getContent(request.RawUrl);                           // Event Args #Content
      //      if (content != null) {
      //          response.WriteContent(content);
      //          return;
      //      }

      //      response.StatusCode = (int)HttpStatusCode.NotFound;                 // Result
      //  }

      //  private static byte[] getContent( string path ) {
      //      if (path == "/")
      //          path += "index.html";

      //      return _httpsv.GetFile(path);
      //  }


    }
}