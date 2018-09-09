using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloApiTemplateDemo.Controllers
{
    public class ValuesController : ApiController
    {
        private static List<string> data = initList();

        private static List<string> initList()
        {
            var ret = new List<string> { "value1", "value2" };
            return ret;
        }

        public IEnumerable<string> Get()
        {
            return data;
        }

        public HttpResponseMessage Get(int id)
        {
            if (data.Count > id)
                return Request.CreateResponse(HttpStatusCode.OK, data[id]);

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found");
        }

        public HttpResponseMessage Post([FromBody]string value)
        {
            data.Add(value);
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            // Location header should always be set when your status code is created because the location should point to the newly created resource. These are just the rules of HTTP.
            msg.Headers.Location = new Uri(Request.RequestUri + "/" + (data.Count - 1));
            return msg;
        }

        public void Put(int id, [FromBody]string value)
        {
            data[id] = value;
        }

        public void Delete(int id)
        {
            data.RemoveAt(id);
        }
    }
}
