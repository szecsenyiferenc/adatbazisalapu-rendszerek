using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShop.Models.DomainModels;
using WebShop.Providers;

namespace WebShop.Controllers
{
    [ApiController]
    public class LikeController : ControllerBase
    {
        ProductProvider productProvider = new ProductProvider();

        // GET: api/Like/5
        [Route("api/like/{id}")]
        [HttpGet]
        public List<Like> Get(string id)
        {
            return productProvider.GetLikesFromDatabase(id);
        }

        [Route("api/like")]
        // POST: api/Like
        [HttpPost]
        public bool Post([FromBody] Like like)
        {
            return productProvider.AddLikeToDatabase(like);
        }

    }
}
