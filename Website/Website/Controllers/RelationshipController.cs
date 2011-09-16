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
        public ActionResult Create(int associationType)
        {
            var association = new Association();
            TryUpdateModel(association);
            var constituentId = (int)Session["constituentId"];
            if (association.AssociatedConstituentId <= 0)
                association.AssociatedConstituent = null;
            association.Type = new AssociationType { Id = associationType };

            mapper = new AutoDataContractMapper();
            var associationData = new AssociationData();
            mapper.Map(association, associationData);

            HttpHelper.Post(serviceBaseUri+"/Associations?ConstituentId="+constituentId, associationData);

            return PartialView(new GridModel(GetAssociations((int)Session["constituentId"])));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int associationType)
        {
            var association = new Association();

            var constituentId = (int)Session["constituentId"];
            TryUpdateModel(association);
            association.Type = new AssociationType {Id = associationType};
            association.Constituent = new Constituent {Id =constituentId };
            
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