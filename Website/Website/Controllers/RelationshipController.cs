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
    public class RelationshipController : Controller
    {
        private AutoDataContractMapper mapper = new AutoDataContractMapper();
        private string serviceBaseUri = ConfigurationManager.AppSettings["serviceBaseUri"];
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["userName"] == null)
                FormsAuthentication.RedirectToLoginPage();
            PopulateAssociationTypes();
            return PartialView();
        }

        private void PopulateAssociationTypes()
        {
            var typesData = HttpHelper.Get<AssociationTypesData>(serviceBaseUri+"/AssociationTypes");

            var associationTypes = new AssociationTypes();
            mapper.MapList(typesData, associationTypes, typeof (AssociationType));
            ViewData["associationTypes"] = associationTypes;
        }

        [GridAction]
        public ActionResult AllAssociations()
        {
            return PartialView(new GridModel(GetAssociations((int)Session["constituentId"])));
        }

        [HttpPost]
        public ActionResult GetConstituent(string text)
        {
            Thread.Sleep(1000);
            var uriString = string.Format(serviceBaseUri + @"/Search?firstName={0}&lastName={0}&email={1}&phone={1}&occupationName={1}&occupationDescription={1}&instituteName={1}&instituteLocation={1}&qualification={1}&yearOfGradutation={1}&address={1}&state={1}&city={1}&country={1}&postcode={1}&preferedName={0}&houseName={1}&branch={1}", text, null);
            var constituentsData = HttpHelper.Get<ConstituentsData>(uriString);
            mapper = new AutoDataContractMapper();
            var constituents = new Constituents();
            mapper.MapList(constituentsData, constituents, typeof(Constituent));
            IEnumerable<Constituent> enumerable = null;
            if (text.HasValue())
            {
                enumerable = constituents.Where((p) => p.Name.FirstName.StartsWith(text, true, null) || p.Name.LastName.StartsWith(text, true, null));
            }

            IEnumerable<SelectListItem> selectList =
                                                    from c in enumerable
                                                    select new SelectListItem
                                                    {
                                                        Text = c.Name.NameWithoutSalutation,
                                                        Value = c.Id.ToString()
                                                    };

            return new JsonResult { Data = selectList };
        }

        private Associations GetAssociations(int constituentId)
        {
            var associationsData = HttpHelper.Get<AssociationsData>(string.Format(serviceBaseUri+"/Associations?ConstituentId={0}", constituentId));

            mapper = new AutoDataContractMapper();
            var associations = new Associations();
            mapper.MapList(associationsData, associations, typeof (Association));
            return associations;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Create(int associationType, int constiuent)
        {
            var association = new Association();
            TryUpdateModel(association);
            var constituentId = (int)Session["constituentId"];
            if (association.AssociatedConstituentId <= 0)
                association.AssociatedConstituent = null;
            association.AssociatedConstituent = new Constituent() { Id = constiuent }; 
            association.Constituent = new Constituent(){Id = constituentId};
            association.Type = new AssociationType { Id = associationType };

            mapper = new AutoDataContractMapper();
            var associationData = new AssociationData();
            mapper.Map(association, associationData);

            HttpHelper.Post(serviceBaseUri+"/Associations?ConstituentId="+constituentId, associationData);

            return PartialView(new GridModel(GetAssociations((int)Session["constituentId"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int associationType, int constiuent)
        {
            var association = new Association();

            var constituentId = (int)Session["constituentId"];
            TryUpdateModel(association);
            association.Type = new AssociationType {Id = associationType};
            association.Constituent = new Constituent {Id =constituentId };
            association.AssociatedConstituent = new Constituent() { Id = constiuent };
            
            mapper = new AutoDataContractMapper();
            var associationData = new AssociationData();
            mapper.Map(association, associationData);

            HttpHelper.Put(string.Format(serviceBaseUri+"/Associations/{0}", id), associationData);
            return PartialView(new GridModel(GetAssociations((int)Session["constituentId"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(serviceBaseUri+"/Associations/{0}", id));
            return PartialView(new GridModel(GetAssociations((int)Session["constituentId"])));
        }
    }
}