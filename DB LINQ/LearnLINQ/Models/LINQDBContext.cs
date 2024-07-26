using Microsoft.EntityFrameworkCore;

namespace LearnLINQ.Models
{
    public class LINQDBContext : DbContext
    {
        public LINQDBContext(DbContextOptions<LINQDBContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            

            modelBuilder.Entity<Customer>().HasData(new[]
            {
                new Customer { CustomerId = 1, CompanyName = "Alfreds Futterkiste", ContactName = "Maria Anders", Address = "Obere Str. 57", City = "Berlin", Country = "Germany" },
                new Customer { CustomerId = 2, CompanyName = "Ana Trujillo Emparedados y helados", ContactName = "Ana Trujillo", Address = "Avda. de la Constitución 2222", City = "México D.F.", Country = "Mexico" },
                new Customer { CustomerId = 3, CompanyName = "Antonio Moreno Taquería", ContactName = "Antonio Moreno", Address = "Mataderos  2312", City = "México D.F.", Country = "Mexico" },
                new Customer { CustomerId = 4, CompanyName = "Around the Horn", ContactName = "Thomas Hardy", Address = "120 Hanover Sq.", City = "London", Country = "UK" },
                new Customer { CustomerId = 5, CompanyName = "Berglunds snabbköp", ContactName = "Christina Berglund", Address = "Berguvsvägen  8", City = "Luleå", Country = "Sweden" },
            });
            modelBuilder.Entity<Product>().HasData(new[]
            {
                new Product { ProductId=1, ProductName = "Chai", Category = "Beverages", UnitPrice = 19.00m },
                new Product { ProductId=2, ProductName = "Chang", Category = "Beverages", UnitPrice = 19.00m },
                new Product { ProductId=3, ProductName = "Aniseed Syrup", Category = "Condiments", UnitPrice = 10.00m },
                new Product { ProductId=4, ProductName = "Chef Anton's Cajun Seasoning", Category = "Condiments", UnitPrice = 22.00m },
                new Product { ProductId=5, ProductName = "Chef Anton's Gumbo Mix", Category = "Condiments", UnitPrice = 21.35m },
            });

            modelBuilder.Entity<Order>().HasData(new[]
            {
                new Order { OrderId=1, CustomerId = 1, OrderDate = DateTime.Parse("2015-09-25"), Total = 814.50m },
                new Order { OrderId=2, CustomerId = 2, OrderDate = DateTime.Parse("2015-10-03"), Total = 228.00m },
                new Order { OrderId=3, CustomerId = 3, OrderDate = DateTime.Parse("2015-10-13"), Total = 100.00m },
                new Order { OrderId=4, CustomerId = 4, OrderDate = DateTime.Parse("2015-10-20"), Total = 500.00m },
                new Order { OrderId=5, CustomerId = 5, OrderDate = DateTime.Parse("2015-11-01"), Total = 200.00m },
                new Order { OrderId = 6, CustomerId = 3, OrderDate = DateTime.Parse("2015-11-10"), Total = 456.00m },
                new Order { OrderId = 7, CustomerId = 1, OrderDate = DateTime.Parse("2015-11-20"), Total = 190.00m },
                new Order { OrderId = 8, CustomerId = 4, OrderDate = DateTime.Parse("2015-12-01"), Total = 1350.00m },
                new Order { OrderId = 9, CustomerId = 2, OrderDate = DateTime.Parse("2015-12-15"), Total = 567.00m },
                new Order { OrderId = 10, CustomerId = 3, OrderDate = DateTime.Parse("2016-01-01"), Total = 945.00m },
                new Order { OrderId = 11, CustomerId = 5, OrderDate = DateTime.Parse("2016-01-20"), Total = 678.00m },
                new Order { OrderId = 12, CustomerId = 4, OrderDate = DateTime.Parse("2016-02-01"), Total = 1200.00m },
                new Order { OrderId = 13, CustomerId = 1, OrderDate = DateTime.Parse("2016-02-15"), Total = 399.00m },
                new Order { OrderId = 14, CustomerId = 2, OrderDate = DateTime.Parse("2016-03-01"), Total = 756.00m },
                new Order { OrderId = 15, CustomerId = 3, OrderDate = DateTime.Parse("2016-03-20"), Total = 945.00m },

            });

            modelBuilder.Entity<OrderDetail>().HasData(new[]
            {
                new OrderDetail { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 19.00m },
                new OrderDetail { OrderId = 1, ProductId = 2, Quantity = 20, UnitPrice = 19.00m },
                new OrderDetail { OrderId = 2, ProductId = 3, Quantity = 5, UnitPrice = 10.00m },
                new OrderDetail { OrderId = 3, ProductId = 1, Quantity = 2, UnitPrice = 19.00m },
                new OrderDetail { OrderId = 4, ProductId = 4, Quantity = 10, UnitPrice = 22.00m },
                new OrderDetail { OrderId = 5, ProductId = 5, Quantity = 5, UnitPrice = 21.35m },
                new OrderDetail { OrderId = 6, ProductId = 1, Quantity = 2, UnitPrice = 150.00m },
                new OrderDetail { OrderId = 6, ProductId = 2, Quantity = 3, UnitPrice = 50.00m },
                new OrderDetail { OrderId = 7, ProductId = 3, Quantity = 1, UnitPrice = 190.00m },
                new OrderDetail { OrderId = 8, ProductId = 1, Quantity = 5, UnitPrice = 270.00m },
                new OrderDetail { OrderId = 8, ProductId = 4, Quantity = 2, UnitPrice = 200.00m },
                new OrderDetail { OrderId = 9, ProductId = 2, Quantity = 4, UnitPrice = 60.00m },
                new OrderDetail { OrderId = 10, ProductId = 3, Quantity = 3, UnitPrice = 210.00m },
                new OrderDetail { OrderId = 10, ProductId = 5, Quantity = 1, UnitPrice = 150.00m },
                new OrderDetail { OrderId = 11, ProductId = 2, Quantity = 2, UnitPrice = 120.00m },
                new OrderDetail { OrderId = 11, ProductId = 5, Quantity = 1, UnitPrice = 150.00m },
                new OrderDetail { OrderId = 12, ProductId = 1, Quantity = 4, UnitPrice = 300.00m },
                new OrderDetail { OrderId = 12, ProductId = 3, Quantity = 2, UnitPrice = 200.00m },
                new OrderDetail { OrderId = 13, ProductId = 4, Quantity = 1, UnitPrice = 399.00m },
                new OrderDetail { OrderId = 14, ProductId = 2, Quantity = 3, UnitPrice = 90.00m },
                new OrderDetail { OrderId = 14, ProductId = 5, Quantity = 2, UnitPrice = 180.00m },
                new OrderDetail { OrderId = 15, ProductId = 1, Quantity = 3, UnitPrice = 210.00m },
                new OrderDetail { OrderId = 15, ProductId = 3, Quantity = 2, UnitPrice = 240.00m },
            });
        }
    }
}
