using System;
using Microsoft.AspNetCore.Mvc;
using Fisher.Bookstore.Models;

namespace Fisher.Bookstore.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Featured()
        {
            var featuredAuthor = new Author()
            {
                Id = 1,
                Name = "J.K. Rowling" 
            };
            return View(featuredAuthor);
        }
    }
}