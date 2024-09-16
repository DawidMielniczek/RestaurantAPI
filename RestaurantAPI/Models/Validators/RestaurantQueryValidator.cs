using FluentValidation;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new[]
        {
            5,
            10,
            15
        };

        private string[] allowedSortByColumnNames = new []{ nameof(Restaurant.Name), nameof(Restaurant.Category), nameof(Restaurant.Description) };
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optiona or must be in [{string.Join(",", allowedSortByColumnNames)}]");

            //RuleFor(r => r.SortBy).Custom((value, context) =>
            //{
            //    if (string.IsNullOrEmpty(value) || !allowedSortByColumnNames.Contains(value))
            //    {
            //        context.AddFailure("SortBy", $"Sort by is optiona or mus be in [{string.Join(",", allowedSortByColumnNames)}]");
            //    }
            //});
        }
    }
}
