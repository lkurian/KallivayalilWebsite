using System.Web.Mvc;
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

        [HttpGet]
        public ActionResult Index()
        {
            PopulateAssociationTypes();
            return PartialView();
        }

        private void PopulateAssociationTypes()
        {
            var typesData = HttpHelper.Get<AssociationTypesData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/AssociationTypes");

            var associationTypes = new AssociationTypes();
            mapper.MapList(typesData, associationTypes, typeof (AssociationType));
            ViewData["associationTypes"] = associationTypes;
        }

        [GridAction]
        public ActionResult AllAssociations()
        {
            return PartialView(new GridModel(GetAssociations()));
        }

        private Associations GetAssociations()
        {
            var associationsData = HttpHelper.Get<AssociationsData>(@"http://localhost/kallivayalilService/KallivayalilService.svc/Associations?ConstituentId=1");

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

            if (association.AssociatedConstituentId <= 0)
                association.AssociatedConstituent = null;
            association.Type = new AssociationType { Id = associationType };

            mapper = new AutoDataContractMapper();
            var associationData = new AssociationData();
            mapper.Map(association, associationData);

            HttpHelper.Post(@"http://localhost/kallivayalilService/KallivayalilService.svc/Associations?ConstituentId=1", associationData);

            return PartialView(new GridModel(GetAssociations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Edit(int id, int associationType)
        {
            var association = new Association();

            TryUpdateModel(association);
            association.Type = new AssociationType {Id = associationType};
            association.Constituent = new Constituent {Id = 1};
            
            mapper = new AutoDataContractMapper();
            var associationData = new AssociationData();
            mapper.Map(association, associationData);

            HttpHelper.Put(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Associations/{0}", id), associationData);
            return PartialView(new GridModel(GetAssociations()));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [GridAction]
        public ActionResult Delete(int id)
        {
            HttpHelper.DoHttpDelete(string.Format(@"http://localhost/kallivayalilService/KallivayalilService.svc/Associations/{0}", id));
            return PartialView(new GridModel(GetAssociations()));
        }
    }
}