using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with ID: {id} DELETE action invoked");

            var restaurants = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id == id);

            if (restaurants is null) { throw new NotFoundException("Restaurant not found"); }

            _dbContext.Remove(restaurants);
            _dbContext.SaveChanges();

        }

        public void Update(UpdateRestaurantDto dto, int id)
        {
            var restaurants = _dbContext
              .Restaurants
              .FirstOrDefault(r => r.Id ==id);
            
            if(restaurants is null) { throw new NotFoundException("Restaurant not Found"); }

            restaurants.Name = dto.Name;
            restaurants.Description = dto.Description;
            restaurants.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
        }
    }
}
