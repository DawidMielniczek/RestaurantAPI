using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurantsHandler : AuthorizationHandler<CreatedMultipleRestaurants>
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public CreatedMultipleRestaurantsHandler(RestaurantDbContext restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurants requirement)
        {
            var userId = int.TryParse(context.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int parsedUserId)
                ? parsedUserId
                : (int?)null;
            var restaurants = _restaurantDbContext.Restaurants.Count(x => x.CreatedById == userId);

            if(restaurants >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
