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
        [HttpGet,Route("{ext}/{filename}")]
        public HttpResponseMessage Get(string filename, string ext)
        {
            var video = new VideoStream(filename, ext);
            Action<Stream, HttpContent, TransportContext> send = video.WriteToStream;
            var response = Request.CreateResponse();
            response.Content = new System.Net.Http.PushStreamContent(send, new MediaTypeHeaderValue("video/" + ext));
            //调用异步数据推送接口  
            return response;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
