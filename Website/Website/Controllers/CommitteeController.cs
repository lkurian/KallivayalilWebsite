using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Kallivayalil.Client;
using Telerik.Web.Mvc;
using Telerik.Web.Mvc.Extensions;
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


        [HttpPost]
        public ActionResult GetConstituent(string text)
        {
            Thread.Sleep(1000);
            var uriString = string.Format(serviceBaseUri + @"/Search?firstName={0}&lastName={0}&email={1}&phone={1}&occupationName={1}&occupationDescription={1}&instituteName={1}&instituteLocation={1}&qualification={1}&yearOfGradutation={1}&address={1}&state={1}&city={1}&country={1}&postcode={1}&preferedName={0}&houseName={1}&branch={1}", text,null);
            var constituentsData = HttpHelper.Get<ConstituentsData>(uriString);
            mapper = new AutoDataContractMapper();
            var constituents = new Constituents();
            mapper.MapList(constituentsData, constituents, typeof(Constituent));
            IEnumerable<Constituent> enumerable = null;
            if (text.HasValue())
            {
                enumerable = constituents.Where((p) => p.Name.FirstName.StartsWith(text,true,null) || p.Name.LastName.StartsWith(text,true,null));
            }
            return new JsonResult { Data = new SelectList(enumerable.ToList(), "Id", "Name.NameWithoutSalutation") };
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