﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Book Book { get; set; }
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid && Book.Id != null)
            {
                var bookRetrievedFromDb = await _db.Book.FindAsync(Book.Id);
                if (bookRetrievedFromDb != null)
                {
                    bookRetrievedFromDb.Name = Book.Name;
                    bookRetrievedFromDb.Author = Book.Author;
                    bookRetrievedFromDb.ISBN = Book.ISBN;

                    await _db.SaveChangesAsync();

                    return RedirectToPage("index");
                }

            }
            return Page();
        }
    }
}