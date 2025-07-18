
using EFManyToMany.Data;
using EFManyToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Formatter;

namespace EFManyToMany.Controllers
{
    [Route("odata/[controller]")]
    public class TagsController : ODataController
    {
        private readonly DataContext _context;

        public TagsController(DataContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Tag> Get()
        {
            return _context.Tags;
        }

        [EnableQuery]
        public SingleResult<Tag> Get([FromODataUri] int key)
        {
            var result = _context.Tags.Where(t => t.Id == key);
            return SingleResult.Create(result);
        }
    }
}
