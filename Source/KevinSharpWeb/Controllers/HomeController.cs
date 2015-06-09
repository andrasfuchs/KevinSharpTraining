using KevinSharp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KevinSharp.Web.Controllers
{
    public class HomeController : Controller
    {
        private KevinSharpDbContext dbContext = new KevinSharpDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return Redirect("http://blog.kevinsharp.net");
        }

        public ActionResult Courses()
        {
            return View();
        }

        public ActionResult OneOnOne()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult GetCourseTimeSlots(string courseCode)
        {
            Course course = dbContext.Courses.Include("TimeSlots").Include("TimeSlots.TimeSlots").FirstOrDefault(c => c.Code == courseCode);

            return course == null ? null : Json(course.TimeSlots.ToArray());
        }
    }
}