using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TainaTech.Application.Contracts;
using TainaTech.Domain.Common;
using TainaTech.Domain.Entities;
using TainaTech.Domain.Enums;

namespace TainaTech.Persistance
{
    public class PeopleDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {

        }       

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleDbContext).Assembly);

            modelBuilder.Entity<Person>().HasData(new Person
            {
                PersonId = 1,
                Firstname = "Test Firstname",
                Surname = "Test Surname",
                Gender = Gender.Male,
                EmailAddress = "Test Email",
                PhoneNumber = "Test Phonenumber",
                DateOfBirth = DateTime.Now.AddYears(-30)
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;                    
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;                       
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
