
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
    public class PostsController : ODataController
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Post> Get()
        {
            return _context.Posts;
        }

        [EnableQuery]
        public SingleResult<Post> Get([FromODataUri] int key)
        {
            var result = _context.Posts.Where(p => p.Id == key);
            return SingleResult.Create(result);
        }
    }
}
