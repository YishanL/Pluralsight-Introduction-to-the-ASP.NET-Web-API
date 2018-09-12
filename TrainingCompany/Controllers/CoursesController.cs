using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrainingCompany.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        public IEnumerable<course> AllCourses()
        {
            return courses;
        }

        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage msg;
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();

            //if null - should return a 404
            if (ret == null)
            {
                msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Course not found!");
            }
            else
            {
                msg = Request.CreateResponse(HttpStatusCode.OK, ret);
            }

            return msg;
        }

        // Need to make sure web API knows where to look for the data I expect to come in on the post 
        // because sometimes you expect data to come in on a query string, 
        // sometimes you expect it to come in from the body, 
        // and because of the way that the model binding and serialization works inside of the web API, 
        // the web API kind of needs to know where this is coming from.
        public HttpResponseMessage Post([FromBody] course c)
        {
            c.id = courses.Count;
            courses.Add(c);

            // should return a 201 with a location header
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + c.id.ToString());
            return msg;
        }

        public void Put(int id, [FromBody]course course)
        {
            var ret = (from c in courses
                       where c.id == id
                       select c).FirstOrDefault();
            ret.title = course.title;

        }

        public void Delete(int id)
        {
            courses.RemoveAt(id);
        }

        private static List<course> courses = InitCourses();

        private static List<course> InitCourses()
        {
            var ret = new List<course>
            {
                new course { id = 0, title = "Web API" },
                new course { id = 1, title = "Mobile Apps with HTML5" }
            };
            return ret;
        }
    }

    // Note: made these types lowercase so that it looks more like natural JSON.
    // could also add attributes to change the name if want the internal name to be more .NET like with uppercase
    public class course
    {
        public int id;
        public string title;
    }
}