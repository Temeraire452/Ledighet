using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ledighet.Data;
using Ledighet.Models;

namespace Ledighet.Controllers
{
    public class LeaveListsController : Controller
    {
        private readonly LedighetDbContext _context;

        public LeaveListsController(LedighetDbContext context)
        {
            _context = context;
        }

        // GET: LeaveLists
        public async Task<IActionResult> Index()
        {
            var ledighetDbContext = _context.LeaveLists.Include(l => l.Employee).Include(l => l.LeaveApplication);
            return View(await ledighetDbContext.ToListAsync());
        }

        // GET: LeaveLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(l => l.Employee)
                .Include(l => l.LeaveApplication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // GET: LeaveLists/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationId", "LeaveApplicationId");
            return View();
        }

        // POST: LeaveLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,LeaveApplicationId")] LeaveList leaveList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveList.EmployeeId);
            ViewData["LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationId", "LeaveApplicationId", leaveList.LeaveApplicationId);
            return View(leaveList);
        }

        // GET: LeaveLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveList.EmployeeId);
            ViewData["LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationId", "LeaveApplicationId", leaveList.LeaveApplicationId);
            return View(leaveList);
        }

        // POST: LeaveLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,LeaveApplicationId")] LeaveList leaveList)
        {
            if (id != leaveList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveListExists(leaveList.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", leaveList.EmployeeId);
            ViewData["LeaveApplicationId"] = new SelectList(_context.LeaveApplications, "LeaveApplicationId", "LeaveApplicationId", leaveList.LeaveApplicationId);
            return View(leaveList);
        }

        // GET: LeaveLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(l => l.Employee)
                .Include(l => l.LeaveApplication)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // POST: LeaveLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList != null)
            {
                _context.LeaveLists.Remove(leaveList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveListExists(int id)
        {
            return _context.LeaveLists.Any(e => e.Id == id);
        }
    }
}
