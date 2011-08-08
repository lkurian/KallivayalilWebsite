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
            var userName = Session["userName"];

            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            var userName = user.UserName;
            var password = user.Password;

            var authenticated = HttpHelper.Get<bool>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Authenticate?username={0}&password={1}",userName,password));

            if (true)
            {
                Session["userName"] = userName;
                Session["password"] = password;
                FormsAuthentication.SetAuthCookie(userName,false);
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["userName"] = string.Empty;
            Session["password"] = string.Empty;

            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();

            return View("Index");
        }
    }
}