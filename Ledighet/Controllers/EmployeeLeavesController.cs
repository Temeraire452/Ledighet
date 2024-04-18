using Ledighet.Data;
using Ledighet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Ledighet.Controllers
{
    public class EmployeeLeavesController : Controller
    {
        private readonly LedighetDbContext _context;

        public EmployeeLeavesController(LedighetDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string employeeName)
        {
            var leaveApplications = await _context.LeaveApplications.ToListAsync();

            var query = from ll in _context.LeaveLists
                        join e in _context.Employees on ll.EmployeeId equals e.EmployeeId
                        join la in _context.LeaveApplications on ll.LeaveApplicationId equals la.LeaveApplicationId
                        select new { ll, e, la };

            if (!string.IsNullOrEmpty(employeeName))
            {
                query = query.Where(x => x.e.EmployeeName.Contains(employeeName));
            }

            var employees = await query.Select(x => new EmployeeWithLeaveApplication
            {
                EmployeeName = x.e.EmployeeName,
                LeaveApplicationNote = x.la.LeaveApplicationNote
            }).ToListAsync();

            var sortedEmployees = employees.OrderBy(e => e.EmployeeName);

            var viewModel = new EmployeeLeaveApplicationViewModel
            {
                LeaveApplications = leaveApplications,
                Employees = sortedEmployees
            };

            return View(viewModel);
        }

        public async Task<IActionResult> GetLeaveApplicationsByEmployeeName(string employeeName)
        {
            var leaveApplications = await _context.LeaveApplications
                .Where(l => l.Employee.EmployeeName == employeeName)
                .ToListAsync();

            var query = from ll in _context.LeaveLists
                        join e in _context.Employees on ll.EmployeeId equals e.EmployeeId
                        select new { ll, e };

            var employees = query.ToList() // Hämta data från databasen
                            .Join(leaveApplications,
                                  ll => ll.ll.LeaveApplicationId,
                                  la => la.LeaveApplicationId,
                                  (ll, la) => new EmployeeWithLeaveApplication
                                  {
                                      EmployeeName = ll.e.EmployeeName,
                                      LeaveApplicationNote = la.LeaveApplicationNote
                                  });

            var sortedEmployees = employees.OrderBy(e => e.EmployeeName);

            var viewModel = new EmployeeLeaveApplicationViewModel
            {
                LeaveApplications = leaveApplications,
                Employees = sortedEmployees
            };

            return View("Index", viewModel);
        }
        public async Task<IActionResult> LeaveApplicationsByMonth(int month)
        {
            var year = DateTime.Now.Year; // Använd aktuellt år, kan anpassas om det behövs
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var leaveApplications = await _context.LeaveApplications
                .Include(l => l.Employee) // inkludera Employee-objektet
                .Where(l => l.ApplicationDate >= startDate && l.ApplicationDate <= endDate)
                .ToListAsync();

            var employeeLeaveDays = leaveApplications
                .GroupBy(l => l.EmployeeId)
                .Select(g => new EmployeeLeaveDays
                {
                    EmployeeId = g.FirstOrDefault()?.Employee?.EmployeeName ?? "Unknown",
                    TotalLeaveDays = g.Sum(l => l.NumberOfDays),
                    ApplicationDates = string.Join(", ", g.Select(l => l.ApplicationDate.ToString("yyyy-MM-dd")))
                })
                .ToList();

            var viewModel = new EmployeeLeaveApplicationViewModel
            {
                Month = startDate.ToString("MMMM"),
                Year = year,
                EmployeeLeaveDays = employeeLeaveDays
            };

            return View(viewModel);
        }
    }
}