using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class EmailController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            PopulateEmailTypes();
            return PartialView();
        }

        private void PopulateEmailTypes()
        {
            var emailTypesData = HttpHelper.Get<EmailTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/EmailTypes");

            var emailTypes = new EmailTypes();
            mapper.MapList(emailTypesData, emailTypes, typeof(EmailType));
            ViewData["emailTypes"] = emailTypes;
        }

        [GridAction]
        public ActionResult AllEmails()
        {
            return PartialView(new GridModel(GetEmails()));
        }

        private Emails GetEmails()
        {
            var emailsData = HttpHelper.Get<EmailsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails?ConstituentId=123");

            mapper = new AutoDataContractMapper();
            var emails = new Emails();
            mapper.MapList(emailsData, emails, typeof(Email));
            return emails;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int emailType)
        {
            var email = new Email();
            TryUpdateModel(email);

            email.Constituent = new Constituent { Id = 123 };
            email.Type = new EmailType() { Id = emailType };

            mapper = new AutoDataContractMapper();
            var emailData = new EmailData();
            mapper.Map(email, emailData);

            HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails?ConstituentId=123", emailData);

            return PartialView(new GridModel(GetEmails()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int emailType)
        {
            var email = new Email();

            TryUpdateModel(email);
            email.Type = new EmailType() { Id = emailType };
            email.Constituent = new Constituent { Id = 123 };
            mapper = new AutoDataContractMapper();
            var emailData = new EmailData();
            mapper.Map(email, emailData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails/{0}",id), emailData);
            return PartialView(new GridModel(GetEmails()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Emails/{0}", id));
            return PartialView(new GridModel(GetEmails()));
        }

    }
}
