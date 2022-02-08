using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext: DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration):base(dbContextOptions)
        {
            Configuration = configuration;
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<FindexScore> FindexScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("RentACarConnectionString")));
            //}
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Brand>(b =>
            {
                b.ToTable("Brands").HasKey(k => k.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.Name).HasColumnName("Name");
                b.HasMany(p => p.Models);
        });
            modelBuilder.Entity<Model>(m =>
            {
                m.ToTable("Models").HasKey(k => k.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.Name).HasColumnName("Name");
                m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
                m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                m.Property(p => p.BrandId).HasColumnName("BrandId");
                m.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
                m.Property(p => p.FuelId).HasColumnName("FuelId");
                m.HasOne(x => x.Transmission);
                m.HasOne(x => x.Fuel);
                m.HasOne(x => x.Brand);
                m.HasMany(x => x.Cars);
            });

            modelBuilder.Entity<Color>(c =>
            {
                c.ToTable("Colors").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                c.HasMany(p => p.Cars);

            });
            modelBuilder.Entity<City>(c =>
            {
                c.ToTable("Cities").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                
            });

            modelBuilder.Entity<Customer>(c =>
            {
                c.ToTable("Customers").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Email).HasColumnName("Email");
                c.HasOne(c => c.CorporateCustomer);
                c.HasOne(c => c.FindexScore);
                c.HasOne(c => c.IndividualCustomer);
                c.HasMany(c => c.Rentals);


            });
            modelBuilder.Entity<IndividualCustomer>(c =>
            {
                c.ToTable("IndividualCustomers");
                c.Property(i => i.Id).HasColumnName("Id");
                c.Property(i => i.CustomerId).HasColumnName("CustomerId");
                c.Property(i => i.FirstName).HasColumnName("FirstName");
                c.Property(i => i.LastName).HasColumnName("LastName");
                c.Property(i => i.NationalId).HasColumnName("NationalId");
                c.Property(i => i.Email).HasColumnName("Email");
                c.HasOne(i => i.Customer);




            });
            modelBuilder.Entity<CorporateCustomer>(c =>
            {
                c.ToTable("CorporateCustomers");
                c.Property(c => c.Id).HasColumnName("Id");
                c.Property(c => c.CustomerId).HasColumnName("CustomerId");
                c.Property(c => c.CompanyName).HasColumnName("CompanyName");
                c.Property(c => c.Email).HasColumnName("Email");
                c.Property(c => c.TaxNumber).HasColumnName("TaxNumber");
                c.HasOne(c => c.Customer);


            });
            modelBuilder.Entity<Fuel>(f =>
            {
                f.ToTable("Fuels").HasKey(k => k.Id);
                f.Property(p => p.Id).HasColumnName("Id");
                f.Property(p => p.Name).HasColumnName("Name");
                f.HasMany(p => p.Models);

            });
            modelBuilder.Entity<Transmission>(f =>
            {
                f.ToTable("Transmissions").HasKey(k => k.Id);
                f.Property(p => p.Id).HasColumnName("Id");
                f.Property(p => p.Name).HasColumnName("Name");
                f.HasMany(p => p.Models);

            });
            modelBuilder.Entity<Car>(c =>
            {
                c.ToTable("Cars").HasKey(k => k.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.ModelYear).HasColumnName("ModelYear");
                c.Property(p => p.Plate).HasColumnName("Plate");
                c.Property(p => p.ColorId).HasColumnName("ColorId");
                c.Property(p => p.ModelId).HasColumnName("ModelId");
                c.Property(p => p.CarState).HasColumnName("State");
                c.Property(p => p.FindexScore).HasColumnName("FindexScore");
                c.Property(p => p.CityId).HasColumnName("CityId");
                c.HasOne(p => p.Color);
                c.HasOne(p => p.Model);
                c.HasOne(c => c.City);

            });
            modelBuilder.Entity<FindexScore>(f =>
            {
                f.ToTable("FindexScores").HasKey(f => f.Id);
                f.Property(f => f.Id).HasColumnName("Id");
                f.Property(f => f.CustomerId).HasColumnName("CustomerId");
                f.Property(f => f.Score).HasColumnName("Score");
                f.HasOne(f => f.Customer);
            });
            modelBuilder.Entity<Maintenance>(m =>
            {
                m.ToTable("Maintenances").HasKey(k => k.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.CarId).HasColumnName("CarId");
                m.Property(p => p.Description).HasColumnName("Description");
                m.Property(p => p.MaintenanceDate).HasColumnName("MaintenanceDate");
                m.Property(p => p.ReturnDate).HasColumnName("ReturnDate");
                m.HasOne(c => c.Car);
            });

            modelBuilder.Entity<Rental>(r =>
            {
                r.ToTable("Rentals").HasKey(r => r.Id);
                r.Property(p => p.Id).HasColumnName("Id");
                r.Property(p => p.CustomerId).HasColumnName("CustomerId");
                r.Property(p => p.CarId).HasColumnName("CarId");
                r.Property(p => p.RentCityId).HasColumnName("RentCityId");
                r.Property(p => p.ReturnCityId).HasColumnName("ReturnCityId");
                r.Property(p => p.ReturnedCityId).HasColumnName("ReturnedCityId");
                r.Property(p => p.RentDate).HasColumnName("RentDate");
                r.Property(p => p.ReturnDate).HasColumnName("ReturnDate");
                r.Property(p => p.ReturnedDate).HasColumnName("ReturnedDate");
                r.Property(p => p.RentedKilometer).HasColumnName("RentedKilometer");
                r.Property(p => p.ReturnedKilometer).HasColumnName("ReturnedKilometer");
                r.HasOne(p => p.Car);
                r.HasOne(p => p.Customer);
            });
            var brand1 = new Brand(1, "BMW");
            var brand2 = new Brand(2, "Mercedes");
            modelBuilder.Entity<Brand>().HasData(brand1, brand2);

            var color1 = new Color(1, "Red");
            var color2 = new Color(2, "Blue");
            modelBuilder.Entity<Color>().HasData(color1, color2);

            var transmission1 = new Transmission(1, "Manuel");
            var transmission2 = new Transmission(2, "Automatic");
            modelBuilder.Entity<Transmission>().HasData(transmission1, transmission2);

            var fuel1 = new Fuel(1, "Diesel");
            var fuel2 = new Fuel(2, "Electric");
            modelBuilder.Entity<Fuel>().HasData(fuel1, fuel2);

            var model1 = new Model(1, "418i", 1000, 2, 1, 1, "");
            var model2 = new Model(2, "CLA 180D", 600, 2, 1, 2, "");
            modelBuilder.Entity<Model>().HasData(model1, model2);

            var city1 = new City(1, "Ankara");
            var city2 = new City(2, "İstanbul");
            modelBuilder.Entity<City>().HasData(city1, city2);

            modelBuilder.Entity<Car>().HasData(new Car(1, 1, 1,2, "06ABC06", 2018, CarState.Available,600));
            modelBuilder.Entity<Car>().HasData(new Car(2, 2, 2 ,1, "34ABC34", 2018, CarState.Available,600));

        }
    }
}
