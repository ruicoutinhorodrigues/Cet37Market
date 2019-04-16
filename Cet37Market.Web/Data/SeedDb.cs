namespace Cet37Market.Web.Data
{ 
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;
    using Cet37Market.Web.Helpers;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        public readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            var user = await this.userHelper.GetUserByEmailAsync("rui.coutinho.rodrigues@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Rui",
                    LastName = "Rodrigues",
                    Email = "rui.coutinho.rodrigues@gmail.com",
                    UserName = "rui.coutinho.rodrigues@gmail.com",
                    PhoneNumber = "962778137"
                };

                var result = await this.userHelper.AddUserAsync(user, "lagarto75");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user uin seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone X", user);
                this.AddProduct("Rato Mickey", user);
                this.AddProduct("Iwatch I", user);

                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
