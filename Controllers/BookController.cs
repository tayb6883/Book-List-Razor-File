using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookListRazor.Controllers
{
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Route("api/Book/GetAll")]
        [HttpGet]
        public IActionResult Index()
        {
            return Json(
                new { data = _db.Book.ToList() });
        }

        [Route("api/Book/DeleteBook")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            Book book = await _db.Book.FindAsync(bookId);
            if (book != null)
            {
                //remove the book and save changes.
                _db.Book.Remove(book);
                //save changes 
                await _db.SaveChangesAsync();
                return Json(new { succes = true, message = "Delete Successful" });
            }
            else
            {
                return Json(new { succes = false, message = "Error While Deleting" });
            }
        }
    }
}   