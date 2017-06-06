using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
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

        [HttpGet]
        public HttpResponseMessage Download()
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
                    FileName = "Peter自拍.MP4"
                };
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            // Check whether the POST operation is MultiPart?  
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form  
            // data will be loaded.  
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.  
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.  
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // We implement MultipartFormDataStreamProvider to override the filename of File which  
        // will be stored on server, or else the default name will be of the format like Body-  
        // Part_{GUID}. In the following implementation we simply get the FileName from   
        // ContentDisposition Header of the Request Body.  
        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            }
        }
    }
}
