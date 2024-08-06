﻿using EcommerceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();
            return View(products);
        }
    }
}
