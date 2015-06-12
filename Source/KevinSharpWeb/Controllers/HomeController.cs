using KevinSharp.DataModel;
using Mandrill;
using Mandrill.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Course course = dbContext.Courses.Include("TimeSlotGroups").Include("TimeSlotGroups.TimeSlots").FirstOrDefault(c => c.Code == courseCode);

            List<string[]> efficient = new List<string[]>();
            List<string[]> intense = new List<string[]>();

            foreach (TimeSlotGroup tsg in course.TimeSlotGroups)
            {
                if (tsg.TimeSlots.Min(ts => ts.StartTimeUtc.Ticks) < DateTime.UtcNow.AddDays(-1).Ticks) continue;
                if (tsg.TimeSlots.Min(ts => ts.StartTimeUtc.Ticks) > DateTime.UtcNow.AddDays(30).Ticks) continue;

                if (tsg.TimeSlots.Max(ts => ts.Duration) < 240)
                {
                    // effective
                    efficient.Add(new string[2] { tsg.Code, tsg.ToString() });
                }
                else
                {
                    // intense
                    intense.Add(new string[2] { tsg.Code, tsg.ToString() });
                }
            }

            return course == null ? null : Json(new object[2] { efficient.ToArray(), intense.ToArray() });
        }

        [HttpPost]
        public async Task<JsonResult> OrderCompleted(string courseCode, string timeSlotGroupCode)
        {
            string name = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.First(c => c.Type == "name").Value;
            string emailAddress = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.First(c => c.Type == "emailaddress").Value;

            Course course = dbContext.Courses.Include("TimeSlotGroups").Include("TimeSlotGroups.TimeSlots").FirstOrDefault(c => c.Code == courseCode);
            TimeSlotGroup tsgroup = course.TimeSlotGroups.FirstOrDefault(ts => ts.Code == timeSlotGroupCode);

            // ADD USER AS APPLICANT
            dbContext.Applicants.Add(new Applicant() { Course = course, TimeSlotGroupCode = tsgroup.Code, Email = emailAddress });
            dbContext.SaveChangesAsync();


            // SEND EMAIL
            MandrillApi mandrillApi = new Mandrill.MandrillApi("MAuhKuH81By7l81ySamQIw");

            EmailAddress recipient = new EmailAddress(emailAddress, name);

            EmailMessage emailMessage = new EmailMessage();
            List<EmailAddress>  recipients = new List<EmailAddress>();
            recipients.Add(recipient);
            
            emailMessage.To = recipients;
            emailMessage.FromEmail = "info@kevinsharp.net";
            emailMessage.FromName = "Kevin Sharp Training";
            emailMessage.BccAddress = "info@kevinsharp.net";
            emailMessage.TrackOpens = true;
            emailMessage.TrackClicks = true;

            //add recipient variables to EmailMessage object
            emailMessage.AddRecipientVariable(recipient.Email, "SUBJECT", "Welcome to Kevin Sharp Training");
            emailMessage.AddRecipientVariable(recipient.Email, "COURSE_NAME", course.Name);
            emailMessage.AddRecipientVariable(recipient.Email, "COURSE_DATES", tsgroup.ToString());
            emailMessage.AddRecipientVariable(recipient.Email, "COURSE_DURATION", course.Length);
            emailMessage.AddRecipientVariable(recipient.Email, "COURSE_URL", "http://www.kevinsharp.net/Course/" + course.Code + tsgroup.Code);
            emailMessage.AddRecipientVariable(recipient.Email, "CURRENT_YEAR", DateTime.UtcNow.Year.ToString());
            emailMessage.AddRecipientVariable(recipient.Email, "COMPANY", "Kevin Sharp Training");
            
            List<TemplateContent> templateContents = new List<TemplateContent>();
            templateContents.Add(new TemplateContent() { Name = "COURSE_NAME", Content = "C# Fundamentals" });

            Mandrill.Requests.Messages.SendMessageTemplateRequest sendRequest = new Mandrill.Requests.Messages.SendMessageTemplateRequest(
                emailMessage,
                "welcome-to-kevin-sharp",
                templateContents);

            List<EmailResult> results = await mandrillApi.SendMessageTemplate(sendRequest);

            return null;
        }    

        public void AddSessionEvent(string category, string action, string label = "", int value = 0)
        {
            MvcApplication.AddSessionEvent(Session, category, action, label, value);
        }
    }
}