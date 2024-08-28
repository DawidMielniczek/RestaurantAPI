using RestaurantAPI.Entities;

namespace RestaurantAPI.Seeder
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext) 
        {
            _dbContext = dbContext;
            
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = getRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }
            }
            if (!_dbContext.Roles.Any())
            {
                var roles = getRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Restaurant> getRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Kfc to kfc",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "889960748",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "chiceeen",
                            Price = 10.30M
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Description = "chiceeen",
                            Price = 5.30M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                 new Restaurant()
                {
                    Name = "MCDonald",
                    Category = "Fast Food",
                    Description = "Donald to kfc",
                    ContactEmail = "contact@donald.com",
                    ContactNumber = "889960748",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Hot Chicken",
                            Description = "chiceeen",
                            Price = 9.30M
                        },
                        new Dish()
                        {
                            Name = "Chicken ",
                            Description = "chiceeen",
                            Price = 8.30M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Krótka 2",
                        PostalCode = "30-501"
                    }
                }
            };

            return restaurants;
        }

        private IEnumerable<Role> getRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
    }
}
