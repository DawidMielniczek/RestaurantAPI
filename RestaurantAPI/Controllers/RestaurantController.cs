﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/Restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [Authorize(Policy ="HasNationality")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _restaurantService.GetAll();

            return Ok(restaurants);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public ActionResult <RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles ="Admin, Manager")]
        public ActionResult CreateRestuarant([FromBody] CreateRestaurantDto dto)
        {         
            var id = _restaurantService.Create(dto);
            return Created($"/api/Restaurant/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
           
            return NoContent();

        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(dto, id);

            return Ok();
        }
    }
}