using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using Weather.Domain.Entities;
using Weather.Domain.Validation;

namespace Weather.Domain.PersistentStorage
{
    internal class WeatherDbContext : IdentityDbContext<User>
    {
        public DbSet<Search> Serches { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Forecast> Forecasts { get; set; }
        public DbSet<FavouritePlace> FavouritePlaces { get; set; }

        public WeatherDbContext()
            : base("WeatherConnectionString")
        {
            // Empty!!!
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("appSchema");

            base.OnModelCreating(modelBuilder);
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            var result = base.ValidateEntity(entityEntry, items);
            var dataAnnotationsValidator = new DataAnnotationValidator();
            ICollection<ValidationResult> validationResults;
            if (!dataAnnotationsValidator.TryValidate(entityEntry.Entity, out validationResults))
            {
                foreach (var validationResult in validationResults)
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        result.ValidationErrors.Add(new DbValidationError(memberName, validationResult.ErrorMessage));
                    }
                }
            }
            return result;
        }
    }
}