using System.Web.Http;

namespace HelloWebApiDemo
{
    public class SecondController : ApiController
    {
        public string Get()
        {
            return "This is the Second Controller";
        }
    }
}