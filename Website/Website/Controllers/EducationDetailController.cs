using System.Web.Mvc;
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
            var educationDetailsData = HttpHelper.Get<EducationDetailsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails?ConstituentId=0");

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

            educationDetail.Constituent = new Constituent { Id = 0 };
            educationDetail.Type = new EducationType() { Id = educationType };

            mapper = new AutoDataContractMapper();
            var educationDetailData = new EducationDetailData();
            mapper.Map(educationDetail, educationDetailData);

            HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/EducationDetails?ConstituentId=0", educationDetailData);

            return PartialView(new GridModel(GetEducations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int educationType)
        {
            var educationDetail = new EducationDetail();

            TryUpdateModel(educationDetail);
            educationDetail.Type = new EducationType() { Id = educationType };
            educationDetail.Constituent = new Constituent { Id = 0 };
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