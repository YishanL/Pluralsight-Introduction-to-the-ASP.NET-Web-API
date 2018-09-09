using System;
using System.Web.Http;

namespace HelloWebApiDemo
{
    public class HelloApiController:ApiController
    {
        public string Get()
        {
            return "Hello from API at " + DateTime.Now;
        }
    }
}