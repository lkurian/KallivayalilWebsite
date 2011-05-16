using System.Web.Mvc;
using Kallivayalil.Client;
using Website.Helpers;
using Website.Models;

namespace Website.Controllers
{
    public class EmailController : Controller
    {
        private AutoDataContractMapper mapper;

        public ActionResult Index()
        {
            var emailsData = HttpHelper.Get<EmailsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails?ConstituentId=1");
            mapper = new AutoDataContractMapper();
            var emails = new Emails();
            mapper.MapList(emailsData, emails, typeof(Email));

            return View(emails);
        }



        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var email = new Email();
            TryUpdateModel(email, formCollection);

            email.Constituent = new Constituent { Id = 1 };

            mapper = new AutoDataContractMapper();
            var emailData = new EmailData();
            mapper.Map(email, emailData);

            HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails?ConstituentId=1", emailData);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var emailData = HttpHelper.Get<EmailData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails/" + id);
            mapper = new AutoDataContractMapper();
            var email = new Email();
            mapper.Map(emailData, email);
            return View(email);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {
            var email = new Email();
            TryUpdateModel(email, formCollection);
            email.Constituent = new Constituent { Id = 1 };
            mapper = new AutoDataContractMapper();
            var emailData = new EmailData();
            mapper.Map(email, emailData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails?constituentId=1"), emailData);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails/{0}", id));
            return RedirectToAction("Index");
        }

    }
}
