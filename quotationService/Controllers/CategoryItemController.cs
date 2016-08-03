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
    public class CategoryItemController : TableController<Category>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            quotationContext context = new quotationContext();
            DomainManager = new EntityDomainManager<Category>(context, Request, Services);
        }

        // GET tables/TodoItem
        public IQueryable<Category> GetAllCategoryItems()
        {
            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Category> GetCategoryItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Category> PatchCategoryItem(string id, Delta<Category> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostCategoryItem(IEnumerable<Category> items)
        {
            foreach(var item in items)
            {
                 await InsertAsync(item);
            }
            //CategoryItem current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current. }, current);
            return Ok();
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCategoryItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}