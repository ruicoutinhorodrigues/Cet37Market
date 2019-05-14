using Cet37Market.Web.Data;
using Cet37Market.Web.Data.Entities;
using Cet37Market.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Controllers.API
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        public IProductRepository ProductRepository { get; }
        public IUserHelper userHelper { get; }

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.ProductRepository = productRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.ProductRepository.GetAllWithUsers());
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Common.Models.Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = await this.userHelper.GetUserByEmailAsync(product.User.UserName);
            if (user == null)
            {
                return this.BadRequest("Invalid user");
            }

            //TODO: Upload images
            var entityProduct = new Product
            {
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = user
            };

            var newProduct = await this.ProductRepository.CreateAsync(entityProduct);
            return Ok(newProduct);
        }
    }
}
