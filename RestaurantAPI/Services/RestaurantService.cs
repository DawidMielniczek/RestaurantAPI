﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger, IAuthorizationService authorizationService) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(d => d.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restauranat not found");
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return result;
        }

        public IEnumerable<RestaurantDto> GetAll() 
        {
            var restaurants = _dbContext
              .Restaurants
              .Include(r => r.Address)
              .Include(d => d.Dishes)
              .ToList();
            var restaurantDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantDtos;
        }

        public int Create(CreateRestaurantDto dto, int userId)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            _logger.LogError($"Restaurant with ID: {id} DELETE action invoked");

            var restaurants = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id == id);

            if (restaurants is null) { throw new NotFoundException("Restaurant not found"); }

            var result = _authorizationService.AuthorizeAsync(user, restaurants,
               new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!result.Succeeded)
            {
                throw new ForbidException("Authorization failed");
            }

            _dbContext.Remove(restaurants);
            _dbContext.SaveChanges();

        }

        public void Update(UpdateRestaurantDto dto, int id, ClaimsPrincipal user)
        {
            
            var restaurant = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id ==id);
            
            if(restaurant is null) { throw new NotFoundException("Restaurant not Found"); }

            var result = _authorizationService.AuthorizeAsync(user, restaurant,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!result.Succeeded)
            {
                throw new ForbidException("Authorization failed");
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
