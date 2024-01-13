using AspDotNetApp.Filters.ActionFilters;
using AspDotNetApp.Filters.ExceptionFilters;
using AspDotNetApp.Models;
using AspDotNetApp.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetShirts()
        {
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [Shirt_ValidateShirtIdFilter]  // data validation kind of like a middleware (before executing anything inside)
        public IActionResult GetShirtById(int id)
        {
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {
            if (shirt == null)
            {
                return BadRequest();
            }

            var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Color, shirt.Gender, shirt.Size);
            if (existingShirt != null)
            {
                return BadRequest();
            }

            ShirtRepository.AddShirt(shirt);

            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt); // check arguments of CreatedAtAction to understand
        }

        [HttpPut("{id}")]
        // [Shirt_ValidateShirtIdFilter]
        [Shirt_ValidateUpdateShirtIdFilter] //? Don't know why it doesn't work... I used a try catch instead.
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            try
            {
                ShirtRepository.UpdateShirt(shirt);
            }
            catch
            {
                if (!ShirtRepository.ShirtExists(id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return "deleting shirt " + id;
        }
    }
}