using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var userName = collection["userName"];
            var password = collection["password"];

            var authenticated = HttpHelper.Get<bool>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Authenticate?username={0}&password={1}",userName,password));

            if (authenticated)
            {
                var constituentData = HttpHelper.Get<ConstituentData>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Find?emailId={0}", userName));
                Session["userName"] = constituentData.Name.FirstName;
                Session["password"] = password;
                Session["constituentId"] = constituentData.Id;
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