using Core.Security.Entities;
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
        public DbSet<AdditionalService> AdditionalServices { get; set; }
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
        public DbSet<User> Users { get; set; }
        public DbSet<CarDamage> CarDamages { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CreditCardInfo> CreditCardInfos { get; set; }
        public DbSet<FindexScore> FindexScores { get; set; }
        public DbSet<AdditionalServiceForRentals> AdditionalServiceForRentals { get; set; }


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
                b.ToTable("Brands").HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.Name).HasColumnName("Name");
                b.HasMany(p => p.Models);
            });
            modelBuilder.Entity<Payment>(b =>
            {
                b.ToTable("Payments").HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.PaymentDate).HasColumnName("PaymentDate");
                b.Property(p => p.TotalSum).HasColumnName("TotalSum");
                b.Property(p => p.RentalId).HasColumnName("RentalId");
                b.HasOne(p => p.Rental);
            });
            modelBuilder.Entity<CreditCardInfo>(b =>
            {
                b.ToTable("CreditCardInfos").HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.CardHolder).HasColumnName("CardHolder");
                b.Property(p => p.CardNo).HasColumnName("CardNo");
                b.Property(p => p.Date).HasColumnName("Date");
                b.Property(p => p.Cvv).HasColumnName("Cvv");
                b.Property(p => p.CustomerId).HasColumnName("CustomerId");
                b.HasOne(p => p.Customer);
            });
            modelBuilder.Entity<Model>(m =>
            {
                m.ToTable("Models").HasKey(p => p.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.Name).HasColumnName("Name");
                m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
                m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                m.Property(p => p.BrandId).HasColumnName("BrandId");
                m.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
                m.Property(p => p.FuelId).HasColumnName("FuelId");
                m.HasOne(p => p.Transmission);
                m.HasOne(p => p.Fuel);
                m.HasOne(p => p.Brand);
                m.HasMany(p => p.Cars);
            });

            modelBuilder.Entity<User>(c =>
            {
                c.ToTable("Users").HasKey(p => p.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Email).HasColumnName("Email");
                c.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
                c.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
                c.Property(p => p.Status).HasColumnName("Status");
                

            });
            modelBuilder.Entity<FindexScore>(b =>
            {
                b.ToTable("FindexScore").HasKey(p => p.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.CustomerId).HasColumnName("CustomerId");
                b.Property(p => p.Score).HasColumnName("Score");
                b.HasOne(p => p.Customer);
            });
            modelBuilder.Entity<OperationClaim>(c =>
            {
                c.ToTable("OperationClaims").HasKey(p => p.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                


            });
            modelBuilder.Entity<Color>(c =>
            {
                c.ToTable("Colors").HasKey(p=> p.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                c.HasMany(p => p.Cars);

            });
            modelBuilder.Entity<City>(c =>
            {
                c.ToTable("Cities").HasKey(p => p.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Name).HasColumnName("Name");
                
            });

            modelBuilder.Entity<CarDamage>(c =>
            {
                c.ToTable("CarDamages").HasKey(p => p.Id);
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.CarId).HasColumnName("CarId");
                c.Property(p => p.DamageInfo).HasColumnName("DamageInfo");
                c.HasOne(p => p.Car);


            });

            modelBuilder.Entity<Customer>(c =>
            {
                c.ToTable("Customers");
                c.Property(p => p.Id).HasColumnName("Id");
                c.Property(p => p.Email).HasColumnName("Email");
                c.HasMany(c => c.Rentals);


            });
            modelBuilder.Entity<IndividualCustomer>(c =>
            {
                c.ToTable("IndividualCustomers");
                c.Property(i => i.Id).HasColumnName("Id");
                c.Property(i => i.FirstName).HasColumnName("FirstName");
                c.Property(i => i.LastName).HasColumnName("LastName");
                c.Property(i => i.NationalId).HasColumnName("NationalId");
                c.Property(i => i.Email).HasColumnName("Email");




            });
            modelBuilder.Entity<CorporateCustomer>(c =>
            {
                c.ToTable("CorporateCustomers");
                c.Property(c => c.Id).HasColumnName("Id");
                c.Property(c => c.CompanyName).HasColumnName("CompanyName");
                c.Property(c => c.Email).HasColumnName("Email");
                c.Property(c => c.TaxNumber).HasColumnName("TaxNumber");


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
                c.HasMany(p => p.CarDamages);

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
                r.HasMany(r => r.AdditionalServiceForRentals);
            });

            modelBuilder.Entity<AdditionalService>(a =>
            {
                a.ToTable("AdditionalServices").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.Property(p => p.Price).HasColumnName("Price");
            });
            modelBuilder.Entity<AdditionalServiceForRentals>(r =>
            {
                r.ToTable("AdditionalServiceForRentals").HasKey(r => r.Id);
                r.Property(r => r.Id).HasColumnName("Id");
                r.Property(r => r.RentalId).HasColumnName("RentalId");
                r.Property(r => r.AdditionalServiceId).HasColumnName("AdditionalServiceId");
                r.HasOne(r => r.Rental);
                r.HasOne(r => r.AdditionalService);
            });

            var brand1 = new Brand(1, "BMW");
            var brand2 = new Brand(2, "Mercedes");
            modelBuilder.Entity<Brand>().HasData(brand1, brand2);

            var color1 = new Color(1, "Kırmızı");
            var color2 = new Color(2, "Mavi");
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

            modelBuilder.Entity<Car>().HasData(new Car(1, 1, 1, 2, "06ABC06", 2018, CarState.Available, 600));
            modelBuilder.Entity<Car>().HasData(new Car(2, 2, 2, 1, "34ABC34", 2018, CarState.Available, 600));

            modelBuilder.Entity<OperationClaim>().HasData(new OperationClaim(1, "Admin"));
            


        }
    }
}
