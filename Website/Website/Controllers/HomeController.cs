using System;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Website.Helpers;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly AutoDataContractMapper mapper = new AutoDataContractMapper();

        public ActionResult Index()
        {
            PopulateEvents();
            var userName = Session["userName"];

            return View();
        }


        private void PopulateEvents()
        {
            var eventsData = HttpHelper.Get<EventsData>(string.Format("{0}?isApproved={1}&startDate={2}&endDate={3}&includeBirthdays={4}"
                                    ,"http://localhost/kallivayalilService/KallivayalilService.svc/Events",true,DateTime.Today,DateTime.Today,true));

            var events = new Events();
            mapper.MapList(eventsData, events, typeof(Event));
            ViewData["events"] = events;
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var userName = collection["userName"];
            var password = collection["password"];

            var authenticated = HttpHelper.Get<bool>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Authenticate?username={0}&password={1}",userName,password));

            if (true)
            {
                Session["userName"] = userName;
                Session["password"] = password;
                FormsAuthentication.RedirectFromLoginPage(userName,false);
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["userName"] = null;
            Session["password"] = null;

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();

            return View("Index");
        }
    }
}