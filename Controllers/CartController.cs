﻿using FoodApp_project.ContextDBConfig;
using FoodApp_project.Models;
using FoodApp_project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp_project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IData data;
        private readonly FoodApplicationDBContext context;
        public CartController(IData data, FoodApplicationDBContext context)
        {
            this.data = data;
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.Carts.Where(c=> c.UserId == user.Id).ToList();
            return View(cartList);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCart(Cart cart)
        {
            var user = await data.GetUser(HttpContext.User);
            cart.UserId = user?.Id;
            if (ModelState.IsValid)
            {
                context.Carts.Add(cart);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAddedCarts()
        {
            var user = await data.GetUser(HttpContext.User);
            var carts = context.Carts.Where(c => c.UserId == user.Id).Select(c => c.RecipeId).ToList();
            return Ok(carts);
        }

        [HttpPost]
        public IActionResult RemoveCartFromList(string Id)
        {
            var cart = context.Carts.Where(c=> c.RecipeId == Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                context.Carts.Remove(cart);
                context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetCartList()
        {
            var user = await data.GetUser(HttpContext.User);
            var cartList = context.Carts.Where(c=> c.UserId == user.Id).Take(3).ToList();
            return PartialView("_CartList", cartList);
        }
    }
}
