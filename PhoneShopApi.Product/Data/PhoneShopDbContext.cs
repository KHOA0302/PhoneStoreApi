using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Microsoft.AspNetCore.Http;
using Azure.Core;
using PhoneShopApi.Product.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PhoneShopApi.Product.Data
{
    public class PhoneShopDbContext : IdentityDbContext<User>
    {
        public PhoneShopDbContext(
            DbContextOptions<PhoneShopDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator == null) return;

                if (!databaseCreator.CanConnect()) databaseCreator.Create();
                if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PhoneDetail> PhoneDetails { get; set; } = null!;
        public DbSet<PhoneOption> PhoneOptions { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<PhoneColor> PhoneColors { get; set; } = null!;
        public DbSet<BuiltInStorage> BuiltInStorages { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        private void AddDataToRoleTable(ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole>
            {
                new()
                {
                    Name ="Admin",
                    NormalizedName = "ADMIN"
                },
                new()
                {
                    Name ="Staff",
                    NormalizedName = "STAFF"
                },
                new()
                {
                    Name ="User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }


        private void AddDataToBrandTable(ModelBuilder modelBuilder)
        {
            var brands = new List<Brand>
            {
                new()
                {
                    Id = 1,
                    Name = "Apple"
                },
                new()
                {
                    Id = 2,
                    Name = "Samsung"
                },
                new()
                {
                    Id = 3,
                    Name = "Google"
                },
                new()
                {
                    Id = 4,
                    Name = "XIAOMI"
                },
            };
            modelBuilder.Entity<Brand>().HasData(brands);
        }

        private void AddDataToBuiltInStorageTable(ModelBuilder modelBuilder)
        {
            var builtInStorages = new List<BuiltInStorage>
            {
                new()
                {
                    Id = 1,
                    Capacity = 128,
                    Unit = "GB"
                },
                new()
                {
                    Id = 2,
                    Capacity = 256,
                    Unit = "GB"
                },
                new()
                {
                    Id = 3,
                    Capacity = 512,
                    Unit = "GB"
                },
                new()
                {
                    Id = 4,
                    Capacity = 1,
                    Unit = "T"
                },
            };
            modelBuilder.Entity<BuiltInStorage>().HasData(builtInStorages);
        }

        private void AddDataToPhoneTable(ModelBuilder modelBuilder)
        {
            var phones = new List<Phone>
            {
                new()
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {
                    Id = 2,
                    Name = "iPhone 15 Pro",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {
                    Id = 3,
                    Name = "iPhone 15 Plus",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {
                    Id = 4,
                    Name = "iPhone 14 Pro Max",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {
                    Id = 5,
                    Name = "iPhone 14 Pro",
                    IsSelling = true,
                    BrandId = 1,
                },
                new()
                {
                    Id = 6,
                    Name = "Galaxy S24",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 7,
                    Name = "Galaxy S24 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 8,
                    Name = "Galaxy S24 Plus",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 9,
                    Name = "Galaxy S22 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 10,
                    Name = "Galaxy S21 Ultra",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 11,
                    Name = "Galaxy Note 10 Plus",
                    IsSelling = true,
                    BrandId = 2,
                },
                new()
                {
                    Id = 12,
                    Name = "Pixel 8 Pro 5G",
                    BrandId = 3,
                },
                new()
                {
                    Id = 13,
                    Name = "Pixel 8",
                    BrandId = 3,
                },
                new()
                {
                    Id = 14,
                    Name = "Pixel Fold",
                    BrandId = 3,
                },
                new()
                {
                    Id = 15,
                    Name = "Pixel 7",
                    BrandId = 3,
                },
                new()
                {
                    Id = 16,
                    Name = "Pixel 7a",
                    BrandId = 3,
                },
                new()
                {
                    Id = 17,
                    Name = "Pixel 7 Pro",
                    BrandId = 3,
                },
            };
            modelBuilder.Entity<Phone>().HasData(phones);
        }

        private void AddDataToPhoneDetailTable(ModelBuilder modelBuilder)
        {
            var iPhonesDetail = new List<PhoneDetail>
            {
                new()
                {
                    Id = 1,
                    PhoneId = 1
                },
                new()
                {
                    Id = 2,
                    PhoneId = 2
                },
                new()
                {
                    Id = 3,
                    PhoneId = 3
                },
                new()
                {
                    Id = 4,
                    PhoneId = 4
                },
                new()
                {
                    Id = 5,
                    PhoneId = 5
                },
                new()
                {
                    Id = 6,
                    PhoneId = 6
                },
                new()
                {
                    Id = 7,
                    PhoneId = 7
                },
                new()
                {
                    Id = 8,
                    PhoneId = 8
                },
                new()
                {
                    Id = 9,
                    PhoneId = 9
                },
                new()
                {
                    Id = 10,
                    PhoneId = 10
                },
                new()
                {
                    Id = 11,
                    PhoneId = 11
                },
                new()
                {
                    Id = 12,
                    PhoneId = 12
                },
                new()
                {
                    Id = 13,
                    PhoneId = 13
                },
                new()
                {
                    Id = 14,
                    PhoneId = 14
                },
                new()
                {
                    Id = 15,
                    PhoneId = 15
                },
                new()
                {
                    Id = 16,
                    PhoneId = 16
                },
                new()
                {
                    Id = 17,
                    PhoneId = 17
                },
            };
            modelBuilder.Entity<PhoneDetail>().HasData(iPhonesDetail);
        }

        private void AddDataToPhoneColorTable(ModelBuilder modelBuilder)
        {
            var hostUrl = "http://14.225.207.131:19001";
            var phoneColors = new List<PhoneColor>
            {
                //iphone 15 promax 
                new()
                {
                    Id = 1,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_den.webp",
                },
                new()
                {
                    Id = 2,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_xanh.webp",
                },
                new()
                {
                    Id = 3,
                    Name = "Trang",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_trang.webp",
                },
                new()
                {
                    Id = 4,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_prm_titan.png",
                },
                //iphone 15 pro
                new()
                {
                    Id = 5,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pro_den.webp",
                },
                new()
                {
                    Id = 6,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pro_trang.webp",
                },
                new()
                {
                    Id = 7,
                    Name = "Xanh",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pro_xanh.webp",
                },
                new()
                {
                    Id = 8,
                    Name = "Titan",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_pro_titan.webp",
                },
                //iphone 15 plus
                new()
                {
                    Id = 9,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_plus_den.webp",
                },
                new()
                {
                    Id = 10,
                    Name = "Xanh Sky",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_plus_skyblue.webp",
                },
                new()
                {
                    Id = 11,
                    Name = "Xanh Mint",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_plus_mint.webp",
                },
                new()
                {
                    Id = 12,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_plus_vang.webp",
                },
                new()
                {
                    Id = 13,
                    Name = "Hồng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/ip15_plus_hong.webp",
                },
                //iphone 14 promax
                new()
                {
                    Id = 14,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-spaceblack.webp",
                },
                new()
                {
                    Id = 15,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-silver.webp",
                },
                new()
                {
                    Id = 16,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-gold.webp",
                },
                new()
                {
                    Id = 17,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-max-deeppurple.webp",
                },
                //iphone 14 pro
                new()
                {
                    Id = 18,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-spaceblack.webp",
                },
                new()
                {
                    Id = 19,
                    Name = "Trắng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-silver.webp",
                },
                new()
                {
                    Id = 20,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-gold.webp",
                },
                new()
                {
                    Id = 21,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/iphone-14-pro-deeppurple.webp",
                },
                //Samsung s24
                new()
                {
                    Id = 22,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_den.webp",
                },
                new()
                {
                    Id = 23,
                    Name = "Bạc",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_bac.webp",
                },
                new()
                {
                    Id = 24,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_vang.webp",
                },
                new()
                {
                    Id = 25,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_tim.webp",
                },
                //samsung s24 ultra
                new()
                {
                    Id = 26,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_den.webp",
                },
                new()
                {
                    Id = 27,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_tim.webp",
                },
                new()
                {
                    Id = 28,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_vang.webp",
                },
                new()
                {
                    Id = 29,
                    Name = "Xám",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_xam.webp",
                },
                //
                new()
                {
                    Id = 30,
                    Name = "Bạc",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_bac.webp",
                },
                new()
                {//2
                    Id = 31,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_den.webp",
                },
                new()
                {//3
                    Id = 32,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_tim.webp",
                },
                new()
                {//4
                    Id = 33,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24_vang.webp",
                },
                new()
                {
                    Id = 34,
                    Name = "Xám",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_xam.webp",
                },
                new()
                {//2
                    Id = 35,
                    Name = "Đen",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_den.webp",
                },
                new()
                {//3
                    Id = 36,
                    Name = "Tím",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_tim.webp",
                },
                new()
                {//4
                    Id = 37,
                    Name = "Vàng",
                    ImageUrl = $"{hostUrl}/Uploads/PhoneImages/s24u_vang.webp",
                },
            };
            modelBuilder.Entity<PhoneColor>().HasData(phoneColors);
        }



        private void AddDataToPhoneOptionTable(ModelBuilder modelBuilder)
        {
            var iPhone15ProMax256Option = new List<PhoneOption>
            {
                //15 promax 256 
                new()
                {
                    Id = 1,
                    PhoneId = 1,
                    PhoneColorId = 1,
                    BuiltInStorageId = 2,
                    Price = 26490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 2,
                    PhoneId = 1,
                    PhoneColorId = 2,
                    BuiltInStorageId = 2,
                    Price = 26490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 3,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 3,
                    Price = 26490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 4,
                    PhoneId = 1,
                    BuiltInStorageId = 2,
                    PhoneColorId = 4,
                    Price = 26490000,
                    Quantity = 100
                },
                //15 promax 512
                new()
                {
                    Id = 5,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 1,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 6,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 2,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 7,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 3,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 8,
                    PhoneId = 1,
                    BuiltInStorageId = 3,
                    PhoneColorId = 4,
                    Price = 30990000,
                    Quantity = 100
                },
                // 15 pro 128
                new()
                {
                    Id = 9,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 5,
                    Price = 21990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 10,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 6,
                    Price = 21990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 11,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 7,
                    Price = 21990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 12,
                    PhoneId = 2,
                    BuiltInStorageId = 1,
                    PhoneColorId = 8,
                    Price = 21990000,
                    Quantity = 100
                },
                //15 pro 256
                new()
                {
                    Id = 13,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 5,
                    Price = 23490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 14,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 6,
                    Price = 23490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 15,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 7,
                    Price = 23490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 16,
                    PhoneId = 2,
                    BuiltInStorageId = 2,
                    PhoneColorId = 8,
                    Price = 23490000,
                    Quantity = 100
                },
                //15 pro 512
                new()
                {
                    Id = 17,
                    PhoneId = 2,
                    BuiltInStorageId = 3,
                    PhoneColorId = 5,
                    Price = 26990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 18,
                    PhoneId = 2,
                    BuiltInStorageId = 3,
                    PhoneColorId = 6,
                    Price = 26990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 19,
                    PhoneId = 2,
                    BuiltInStorageId = 3,
                    PhoneColorId = 7,
                    Price = 26990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 20,
                    PhoneId = 2,
                    BuiltInStorageId = 3,
                    PhoneColorId = 8,
                    Price = 26990000,
                    Quantity = 100
                },
                //iPhone 15 plus 128
                new()
                {
                    Id = 21,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 9,
                    Price = 21490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 22,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 10,
                    Price = 22490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 23,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 11,
                    Price = 22490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 24,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 12,
                    Price = 22490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 25,
                    PhoneId = 3,
                    BuiltInStorageId = 1,
                    PhoneColorId = 13,
                    Price = 22990000,
                    Quantity = 100
                },
                //iphone 15 plus 256
                new()
                {
                    Id = 26,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 9,
                    Price = 23990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 27,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 10,
                    Price = 23690000,
                    Quantity = 100
                },
                new()
                {
                    Id = 28,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 11,
                    Price = 23990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 29,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 12,
                    Price = 24290000,
                    Quantity = 100
                },
                new()
                {
                    Id = 30,
                    PhoneId = 3,
                    BuiltInStorageId = 2,
                    PhoneColorId = 13,
                    Price = 25490000,
                    Quantity = 100
                },
                //iphone 15 plus 512
                new()
                {
                    Id = 31,
                    PhoneId = 3,
                    BuiltInStorageId = 3,
                    PhoneColorId = 9,
                    Price = 24990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 32,
                    PhoneId = 4,
                    BuiltInStorageId = 1,
                    PhoneColorId = 10,
                    Price = 24990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 33,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 11,
                    Price = 24990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 34,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 12,
                    Price = 24990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 35,
                    PhoneId = 3,
                    BuiltInStorageId = 3,
                    PhoneColorId = 13,
                    Price = 25490000,
                    Quantity = 100
                },
                // iphone 14 pro max 256
                new()
                {
                    Id = 36,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 14,
                    Price = 25990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 37,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 15,
                    Price = 26490000,
                    Quantity = 100
                },
                new()
                {
                    Id = 38,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 16,
                    Price = 25990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 39,
                    PhoneId = 4,
                    BuiltInStorageId = 2,
                    PhoneColorId = 17,
                    Price = 25990000,
                    Quantity = 100
                },
                //iphone 14 pro 256
                new()
                {
                    Id = 40,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 18,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 41,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 19,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 42,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 20,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 43,
                    PhoneId = 5,
                    BuiltInStorageId = 2,
                    PhoneColorId = 21,
                    Price = 30990000,
                    Quantity = 100
                },
                //
                new()
                {
                    Id = 44,
                    PhoneId = 6,
                    BuiltInStorageId = 1,
                    PhoneColorId = 36,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 45,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 33,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 46,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 34,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 47,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 35,
                    Price = 30990000,
                    Quantity = 100
                },
                new()
                {
                    Id = 48,
                    PhoneId = 7,
                    BuiltInStorageId = 1,
                    PhoneColorId = 36,
                    Price = 30990000,
                    Quantity = 100
                },
            };
            modelBuilder.Entity<PhoneOption>().HasData(iPhone15ProMax256Option);
        }

        private void AddDataToPaymentTable(ModelBuilder modelBuilder)
        {
            var payments = new List<Payment>
            {
                new()
                {
                    Id = 1,
                    Name = "Thanh toán khi nhận hàng"
                },
                new()
                {
                    Id = 2,
                    Name = "MOMO"
                }
            };
            modelBuilder.Entity<Payment>().HasData(payments);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Phone>()
                .HasIndex(p => new { p.BrandId, p.Name })
                .IsUnique();

            modelBuilder.Entity<PhoneOption>()
                .HasKey(po => new { po.Id });

            modelBuilder.Entity<PhoneOption>()
                .HasIndex(po => new { po.PhoneId, po.BuiltInStorageId, po.PhoneColorId })
                .IsUnique();

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.Phone)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.PhoneId);

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.BuiltInStorage)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.BuiltInStorageId);

            modelBuilder.Entity<PhoneOption>()
                .HasOne(po => po.PhoneColor)
                .WithMany(po => po.PhoneOptions)
                .HasForeignKey(po => po.PhoneColorId);

            modelBuilder.Entity<CartItem>()
                .HasKey(ci => new { ci.PhoneOptionId, ci.CartId });

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(ci => new { ci.PhoneOptionId, ci.OrderId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(ci => ci.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(ci => ci.OrderId);

            AddDataToRoleTable(modelBuilder);
            AddDataToBrandTable(modelBuilder);
            AddDataToPhoneTable(modelBuilder);
            AddDataToPhoneDetailTable(modelBuilder);
            AddDataToBuiltInStorageTable(modelBuilder);
            AddDataToPhoneColorTable(modelBuilder);
            AddDataToPhoneOptionTable(modelBuilder);
            AddDataToPaymentTable(modelBuilder);
        }
    }
}
