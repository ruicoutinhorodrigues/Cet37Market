namespace Cet37Market.Web.Data
{ 
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Cet37Market.Web.Data.Entities;

    public class SeedDb
    {
        public readonly DataContext context;

        private readonly Random random;

        public SeedDb(DataContext contex)
        {
            this.context = contex;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Products.Any())
            {
                this.AddProduct("Iphone X");
                this.AddProduct("Rato Mickey");
                this.AddProduct("Iwatch I");

                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailable = true,
                Stock = this.random.Next(100)
            });
        }
    }
}
