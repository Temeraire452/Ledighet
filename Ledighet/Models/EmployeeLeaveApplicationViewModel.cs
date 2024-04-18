namespace Ledighet.Models
{
    public class EmployeeLeaveApplicationViewModel
    {
        public IEnumerable<EmployeeWithLeaveApplication> Employees { get; set; }
        public IEnumerable<LeaveApplication> LeaveApplications { get; set; }
        public IEnumerable<EmployeeLeaveDays> EmployeeLeaveDays { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
    }
}
