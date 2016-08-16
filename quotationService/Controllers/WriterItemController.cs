using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using quotationService.DataObjects;
using quotationService.Models;
using System.Collections.Generic;

namespace quotationService.Controllers
{
    public class WriterItemController : TableController<Writer>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            quotationContext context = new quotationContext();
            DomainManager = new EntityDomainManager<Writer>(context, Request, Services);
        }

        // GET tables/TodoItem
        public IQueryable<Writer> GetAllWriterItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Writer> GetWriterItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Writer> PatchWriterItem(string id, Delta<Writer> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public IHttpActionResult PostWriterItem(IEnumerable<Writer> items)
        {
            foreach (var item in items)
            {
                InsertAsync(item);
            }
            //Writer current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current.PkWriterId }, current);
            return Ok();
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteWriterItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}