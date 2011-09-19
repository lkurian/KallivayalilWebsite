using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Website.Helpers;
using Website.Models;
using Website.Models.ReferenceData;

namespace Website.Controllers
{
    public class CommitteeController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulatePositionTypes();
            return View();
        }

        private void PopulatePositionTypes()
        {
            var positionTypesData = HttpHelper.Get<PositionTypesData>(serviceBaseUri+"/PositionTypes");

            var positionTypes = new PositionTypes();
            mapper.MapList(positionTypesData, positionTypes, typeof(PositionType));
            ViewData["positionTypes"] = positionTypes;
        }

        [GridAction]
        public ActionResult AllCommitteeMembers()
        {
            return PartialView(new GridModel(GetCommitteeMembers()));
        }

        private Committees GetCommitteeMembers()
        {
            var committeesData = HttpHelper.Get<CommitteesData>(serviceBaseUri+"/Committees");

            mapper = new AutoDataContractMapper();
            var committees = new Committees();
            mapper.MapList(committeesData, committees, typeof(Committee));
            return committees;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int positionType)
        {
            var committee = new Committee();
            TryUpdateModel(committee);

            var constituentId = (int)Session["constituentId"];

            committee.Constituent = new Constituent { Id = constituentId };
            committee.Type = new PositionType() { Id = positionType };

            mapper = new AutoDataContractMapper();
            var committeeData = new CommitteeData();
            mapper.Map(committee, committeeData);

            HttpHelper.Post(string.Format(serviceBaseUri+"/Committees"), committeeData);

            return PartialView(new GridModel(GetCommitteeMembers()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int positionType)
        {
            var committee = new Committee();
            var constituentId = (int)Session["constituentId"];
            TryUpdateModel(committee);
            committee.Type = new PositionType() { Id = positionType };
            committee.Constituent = new Constituent { Id = constituentId };
            mapper = new AutoDataContractMapper();
            var committeeData = new CommitteeData();
            mapper.Map(committee, committeeData);

            HttpHelper.Put(string.Format(serviceBaseUri + "/Committees/{0}", id), committeeData);
            return PartialView(new GridModel(GetCommitteeMembers()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(serviceBaseUri + "/Committees/{0}", id));
            return PartialView(new GridModel(GetCommitteeMembers()));
        }

    }
}