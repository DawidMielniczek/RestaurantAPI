using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumAgeRequirment : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumAgeRequirment(int minimumAge)
        {
            MinimumAge = minimumAge;    
        }
    }
}
