using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ledighet.Models;

namespace Ledighet.Models
{
    public class LeaveApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveApplicationId { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Application Date")]
        public DateTime ApplicationDate { get; set; }
        [DisplayName("Number of Days")]
        public int NumberOfDays
        {
            get
            {
                return (EndDate - StartDate).Days + 1;
            }
        }
        [DisplayName ("Note")]
        public string LeaveApplicationNote { get; set; }

        [ForeignKey("Employee")]
        [DisplayName("Name")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [ForeignKey("LeaveType")]
        [DisplayName("Type of Leave")]
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public IList<LeaveList>? LeaveList { get; set; }

        public LeaveApplication()
        {
            ApplicationDate = DateTime.Today;
        }
    }
}
