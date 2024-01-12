using Microsoft.AspNetCore.Mvc;

namespace AspDotNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public string GetShirts()
        {
            return "reading all the shirts";
        }

        [HttpGet("{id}")]
        public string GetShirtById(int id)
        {
            return "reading shirt " + id;
        }

        [HttpPost]
        public string CreateShirt()
        {
            return "creating a shirt";
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return "updating shirt " + id;
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return "deleting shirt " + id;
        }
    }
}