using System.Web.Mvc;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class OccupationController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();

        [HttpGet]
        public ActionResult Index()
        {
            PopulateOccupationTypes();
            return PartialView();
        }

        private void PopulateOccupationTypes()
        {
            var occupationTypesData = HttpHelper.Get<OccupationTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/OccupationTypes");

            var occupationTypes = new OccupationTypes();
            mapper.MapList(occupationTypesData, occupationTypes, typeof (OccupationType));
            ViewData["occupationTypes"] = occupationTypes;
        }

        [GridAction]
        public ActionResult AllOccupations()
        {
            return PartialView(new GridModel(GetOccupations()));
        }

        private Occupations GetOccupations()
        {
            var OccupationsData = HttpHelper.Get<OccupationsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Occupations?ConstituentId=1");

            mapper = new AutoDataContractMapper();
            var Occupations = new Occupations();
            mapper.MapList(OccupationsData, Occupations, typeof (Occupation));
            return Occupations;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int OccupationType)
        {
            var Occupation = new Occupation();
            TryUpdateModel(Occupation);

            Occupation.Constituent = new Constituent {Id = 1};
            Occupation.Type = new OccupationType { Id = OccupationType };

            mapper = new AutoDataContractMapper();
            var OccupationData = new OccupationData();
            mapper.Map(Occupation, OccupationData);

            var newOccupation = HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Occupations?ConstituentId=1", OccupationData);

            return PartialView(new GridModel(GetOccupations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int OccupationType)
        {
            var Occupation = new Occupation();

            TryUpdateModel(Occupation);
            Occupation.Type = new OccupationType {Id = OccupationType};
            Occupation.Address = new Address() {Id = 1};
            Occupation.Constituent = new Constituent {Id = 1};
            mapper = new AutoDataContractMapper();
            var OccupationData = new OccupationData();
            mapper.Map(Occupation, OccupationData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Occupations/{0}",id), OccupationData);
            return PartialView(new GridModel(GetOccupations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Occupations/{0}", id));
            return PartialView(new GridModel(GetOccupations()));
        }
    }
}