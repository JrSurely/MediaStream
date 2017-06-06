using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace MediaLiveDemo.Controllers
{
    [Route("api/{controller}")]
    public class ValuesController : ApiController
    {
        [HttpGet, Route("{ext}/{filename}")]
        public HttpResponseMessage Get(string filename, string ext)
        {
            var video = new VideoStream(filename, ext);
            Action<Stream, HttpContent, TransportContext> send = video.WriteToStream;
            var response = Request.CreateResponse();
            response.Content = new System.Net.Http.PushStreamContent(send, new MediaTypeHeaderValue("video/" + ext));
            //调用异步数据推送接口  
            return response;
        }

        [HttpGet,Route("download")]
        public HttpResponseMessage GetFileFromWebApi()
        {
            try
            {
                //var filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/download/EditPlus64_xp85.com.zip");
                var filePath = @"D:\Youku Files\transcode\人民的名义 01_高清.MP4";
                var stream = new FileStream(filePath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Wep Api Demo File.zip"
                };
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }     
    }
}
