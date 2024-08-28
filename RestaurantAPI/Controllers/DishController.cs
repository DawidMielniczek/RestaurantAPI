using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/Restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : Controller
    {
        private readonly IDishService dishService;
        public DishController(IDishService _dishService )
        {
            dishService = _dishService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute]int restaurantId,[FromBody] CreateDishDto dto)
        {
            var newDishId = dishService.Create(restaurantId, dto);

            return Created($"api/Restaruant/{restaurantId}/dish/{newDishId}", null);
        }
        
        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {

            DishDto dish = dishService.GetById(restaurantId, dishId);

            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get ([FromRoute] int restaurantId)
        {
            var result = dishService.GetAll(restaurantId);

            return Ok(result);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            dishService.RemoveAll(restaurantId);

            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            dishService.RemoveById(restaurantId, dishId);

            return NoContent();
        }
    }
}
