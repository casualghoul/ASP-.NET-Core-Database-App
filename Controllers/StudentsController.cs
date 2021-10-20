using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_460_Assignment_2_Andrew_Horton.Data;
using IS_460_Assignment_2_Andrew_Horton.Models;

namespace IS_460_Assignment_2_Andrew_Horton.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IS_460_Assignment_2_Andrew_HortonContext _context;

        public StudentsController(IS_460_Assignment_2_Andrew_HortonContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Student.ToListAsync());
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

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentID == id);
        }

    }
}
