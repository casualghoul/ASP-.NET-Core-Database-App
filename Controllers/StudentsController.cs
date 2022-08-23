 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_460_Assignment_3_Andrew_Horton.Data;
using IS_460_Assignment_3_Andrew_Horton.Models;

namespace IS_460_Assignment_3_Andrew_Horton.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IS_460_Assignment_3_Andrew_HortonContext _context;

        public StudentsController(IS_460_Assignment_3_Andrew_HortonContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, int? pageNumber, string currentFilter, string searchString)
        {
            //this if/else statement allows the ability to toggle between ascending, descending, or default sorting
            if(sortOrder == "name_asc" || sortOrder == "gpa_asc")
            {
                ViewData["NameSortParam"] = "";
                ViewData["GPASortParam"] = "";
            }
            else
            {
                ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
                ViewData["GPASortParam"] = String.IsNullOrEmpty(sortOrder) ? "gpa_desc" : "gpa_asc";
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;



            //var students = await _context.Student.ToListAsync();

            var students = from s in _context.Student
                           select s;

            //getting the averages of the GPA and Credits Earned and passing them to the view
            var creditsAverage = Math.Round((double)students.Average(m => m.CreditsEarned),2);          
            ViewData["creditsAverage"] = creditsAverage;
            var gpaAverage = Math.Round((double)students.Average(m => m.GPA),2);            
            ViewData["gpaAverage"] = gpaAverage;
            //getting the min and max of credits and GPA and passing them to the view
            var creditsMin = Math.Round((double)students.Min(m => m.CreditsEarned), 2);
            ViewData["creditsMin"] = creditsMin;
            var creditsMax = Math.Round((double)students.Max(m => m.CreditsEarned), 2);
            ViewData["creditsMax"] = creditsMax;
            var gpaMin = Math.Round((double)students.Min(m => m.GPA), 2);
            ViewData["gpaMin"] = gpaMin;
            var gpaMax = Math.Round((double)students.Max(m => m.GPA), 2);
            ViewData["gpaMax"] = gpaMax;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "name_asc":
                    students = students.OrderBy(s => s.LastName);
                    break;
                case "gpa_desc":
                    students = students.OrderByDescending(s => s.GPA);
                    break;
                case "gpa_asc":
                    students = students.OrderBy(s => s.GPA);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
            
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {

            Models.Student s = new Models.Student();

            return View("Create", s);
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,LastName,FirstName,CreditsEarned,GPA,Level,Graduated")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,LastName,FirstName,CreditsEarned,GPA,Level,Graduated")] Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET: /Students/FindDetail
        [HttpGet]
        public IActionResult FindDetail()
        {
            return View();
        }

        //POST: /Students/FindDetail
        //Takes the StudentID as an argument, finds the record, and returns that student's "Detail" view
        [HttpPost]
        public async Task<IActionResult> FindDetail(string? StudentID)
        {
            //If the user enters no StudentID, return the "Not Found" page
            if (StudentID == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                     .FirstOrDefaultAsync(m => m.StudentID == StudentID);

            //If the user enters in a StudentID that does not exist, return the "Not Found" page
            if(student == null)
            {
                return NotFound();
            }

            return View("Details", student);
        }

        //Generates the sample data in the database
        public IActionResult CreateSampleData()
        {
            GenerateSampleData.Initialize(_context);

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }

    }
}
