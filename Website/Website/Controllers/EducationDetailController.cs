using System.Diagnostics.Eventing.Reader;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class EducationDetailController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulateEducationTypes();
            return PartialView();
        }

        private void PopulateEducationTypes()
        {
            var educationTypesData = HttpHelper.Get<EducationTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationTypes");

            var educationTypes = new EducationTypes();
            mapper.MapList(educationTypesData, educationTypes, typeof(EducationType));
            ViewData["educationTypes"] = educationTypes;
        }

        [GridAction]
        public ActionResult AllEducationDetails()
        {
            return PartialView(new GridModel(GetEducations()));
        }

        private EducationDetails GetEducations()
        {
            var constitinetId = (int) Session["constituentId"];
            var educationDetailsData = HttpHelper.Get<EducationDetailsData>(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails?ConstituentId={0}", constitinetId));

            mapper = new AutoDataContractMapper();
            var educations = new EducationDetails();
            mapper.MapList(educationDetailsData, educations, typeof(EducationDetail));
            return educations;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int educationType)
        {
            var educationDetail = new EducationDetail();
            TryUpdateModel(educationDetail);

            var constituentId = (int)Session["constituentId"];
            educationDetail.Constituent = new Constituent { Id = constituentId};
            educationDetail.Type = new EducationType() { Id = educationType };

            mapper = new AutoDataContractMapper();
            var educationDetailData = new EducationDetailData();
            mapper.Map(educationDetail, educationDetailData);

            HttpHelper.Post(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails?ConstituentId={0}", constituentId), educationDetailData);

            return PartialView(new GridModel(GetEducations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int educationType)
        {
            var educationDetail = new EducationDetail();
            var constituentId = (int)Session["constituentId"];

            TryUpdateModel(educationDetail);
            educationDetail.Type = new EducationType() { Id = educationType };
            educationDetail.Constituent = new Constituent { Id = constituentId };
            mapper = new AutoDataContractMapper();
            var educationDetailData = new EducationDetailData();
            mapper.Map(educationDetail, educationDetailData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails/{0}",id), educationDetailData);
            return PartialView(new GridModel(GetEducations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails/{0}", id));
            return PartialView(new GridModel(GetEducations()));
        }

    }
}