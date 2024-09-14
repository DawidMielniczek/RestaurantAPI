using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreatedMultipleRestaurants : IAuthorizationRequirement
    {
        public CreatedMultipleRestaurants(int _MinimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = _MinimumRestaurantsCreated;
        }
        public int MinimumRestaurantsCreated { get; }
    }
}
