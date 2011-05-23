using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Website.Helpers;
using Website.Models;
using Website.Models.ViewModels;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private AutoDataContractMapper mapper;

        public ActionResult List()
        {
            var constituentsData =
                HttpHelper.Get<ConstituentsData>(
                    @"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents");
            var mapper = new AutoDataContractMapper();
            var constituent = new Constituents();
            mapper.MapList(constituentsData, constituent, typeof (Constituent));


            return View(constituent);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var constituent = new Constituent();
            TryUpdateModel(constituent, formCollection);


            mapper = new AutoDataContractMapper();
            var constituentData = new ConstituentData();
            mapper.Map(constituent, constituentData);

            ConstituentData newConstituent =
                HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents",
                                constituentData);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var constituentData =
                HttpHelper.Get<ConstituentData>(
                    @"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/" + id);
            mapper = new AutoDataContractMapper();
            var constituent = new Constituent();
            mapper.Map(constituentData, constituent);
            return View(constituent);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var constituentData =
                HttpHelper.Get<ConstituentData>(
                    @"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/" + id);
            mapper = new AutoDataContractMapper();
            var constituent = new Constituent();
            mapper.Map(constituentData, constituent);
            return View(constituent);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            var constituent = new Constituent();
            TryUpdateModel(constituent, formCollection);
            mapper = new AutoDataContractMapper();
            var constituentData = new ConstituentData();
            mapper.Map(constituent, constituentData);

            HttpHelper.Put(
                string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/{0}", id),
                constituentData);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(
                string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Constituents/{0}", id));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string userName = user.UserName;
            string password = user.Password;

//            bool authenticated = string.Equals(userName, password);

            var authenticated = HttpHelper.Get<bool>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Authenticate?username={0}&password={1}",userName,password));

            if (authenticated)
            {
                Session["userName"] = userName;
                Session["password"] = password;

                FormsAuthentication.RedirectFromLoginPage(userName, false);
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["userName"] = string.Empty;
            Session["password"] = string.Empty;

            FormsAuthentication.RedirectToLoginPage();

            return View("Index");
        }
    }
}