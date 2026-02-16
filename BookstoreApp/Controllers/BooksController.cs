using BookstoreApp.Data;          // To access BookstoreContext
using BookstoreApp.Models;        // To access Book model
using BookstoreApp.Services;
using Microsoft.AspNetCore.Mvc;   // Base MVC features
using Microsoft.EntityFrameworkCore; // EF Core async methods
using BookstoreApp.Services;


namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        // Private field to hold database context
        private readonly BookstoreContext _context;

        //Private field to hold AI summary service 
        private readonly AiSummaryService _aiSummaryService;

        // Constructor – dependency injection of DbContext
        public BooksController(BookstoreContext context, AiSummaryService aiSummaryService)
        {
            _context = context;
            _aiSummaryService = aiSummaryService;
        }


        // GET: Books
        // Shows list of books with:
        // 1. search
        // 2. pagination
        public async Task<IActionResult> Index(string? searchString, int pageNumber = 1)
        {
            // Number of records per page
            int pageSize = 5;

            // We keep the search value so that it can be reused in the view
            ViewData["CurrentFilter"] = searchString;

            // Start building the query (not executed yet)
            var booksQuery = _context.Books.AsQueryable();

            // If user typed something in search box
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                // Convert search text to lower-case for case-insensitive search
                var lowerSearch = searchString.ToLower();

                // Apply filter on Title and Author
                booksQuery = booksQuery.Where(b =>
                    b.Title.ToLower().Contains(lowerSearch) ||
                    b.Author.ToLower().Contains(lowerSearch));
            }

            // Total number of records AFTER search filter
            int totalRecords = await booksQuery.CountAsync();

            // Calculate how many records to skip
            // Example:
            // pageNumber = 2, pageSize = 5
            // skip = (2 - 1) * 5 = 5
            int skip = (pageNumber - 1) * pageSize;

            // Fetch only one page of data
            var books = await booksQuery
                .OrderBy(b => b.Title) // Always keep a fixed order when using pagination(by tittle)
                .Skip(skip)           // Skip previous records
                .Take(pageSize)       // Take only pageSize records
                .ToListAsync();

            // Total pages
            int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Send paging info to View
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;

            return View(books);
        }



        // GET: /Books/Details/5
        // Displays details of a single book
        public async Task<IActionResult> Details(int? id)
        {
            // If id is not supplied in URL
            if (id == null)
            {
                return NotFound();
            }

            // Find the book with given id
            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);

            // If no book found in database
            if (book == null)
            {
                return NotFound();
            }

            // Generate AI summary
            var summary = await _aiSummaryService.GenerateSummaryAsync(book);

            ViewBag.AiSummary = summary;

            // Send book to view
            return View(book);
        }

        // GET: /Books/Create
        // Shows create form
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Books/Create
        // Saves new book to database
        [HttpPost]
        [ValidateAntiForgeryToken] // Security
        public async Task<IActionResult> Create(Book book)
        {
            // Check validation based on data annotations
            if (ModelState.IsValid)
            {
                // prepare insert, send insert to database
                _context.Add(book);

                // Save changes to database
                await _context.SaveChangesAsync();

                // Redirect to list page after success
                TempData["ToastMessage"] = "Book created successfully.";
                TempData["ToastType"] = "success";

                return RedirectToAction(nameof(Index));

            }

            // If validation fails, return form with same data
            return View(book);
        }

        // GET: /Books/Edit/5
        // Shows edit form
        public async Task<IActionResult> Edit(int? id)
        {
            // If id is missing
            if (id == null)
            {
                return NotFound();
            }

            // Find book by primary key
            var book = await _context.Books.FindAsync(id);

            // If book does not exist
            if (book == null)
            {
                return NotFound();
            }

            // Send existing data to edit form.
            return View(book);
        }

        // POST: /Books/Edit/5
        // Updates existing book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            // If route id and model id do not match
            if (id != book.Id)
            {
                return NotFound();
            }

            // Validate form inputs
            if (ModelState.IsValid)
            {
                //Safety check: Ensure the book still exists in database before updating(Someone deleted the same book in another browser)
                try
                {
                    // generate update SQL to update the row
                    _context.Update(book);

                    // Save changes
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // If the record no longer exists in DB
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Re-throw exception if something else happened
                        throw;
                    }
                }

                // After successful update go back to list
                TempData["ToastMessage"] = "Book updated successfully.";
                TempData["ToastType"] = "success";

                return RedirectToAction(nameof(Index));

            }

            // If validation fails, show form again
            return View(book);
        }

        // GET: /Books/Delete/5
        // Shows delete confirmation page
        public async Task<IActionResult> Delete(int? id)
        {
            // If id is missing
            if (id == null)
            {
                return NotFound();
            }

            // Fetch book for confirmation screen
            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);

            // If not found
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: /Books/Delete/5
        // Deletes the book from database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the book
            var book = await _context.Books.FindAsync(id);

            // If found, remove it
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            // Save changes
            await _context.SaveChangesAsync();

            // Redirect to list page
            TempData["ToastMessage"] = "Book deleted successfully.";
            TempData["ToastType"] = "danger";

            return RedirectToAction(nameof(Index));

        }

        // new backend endpoint for AI summary
        [HttpGet]
        public async Task<IActionResult> GetAiSummary(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return NotFound();

            var summary = await _aiSummaryService.GenerateSummaryAsync(book);

            return Json(new { summary });
        }


        // Helper method to check existence of a book
        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
