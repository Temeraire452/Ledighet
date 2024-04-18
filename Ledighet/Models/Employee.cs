using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ledighet.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        [DisplayName("Name")]
        public string EmployeeName { get; set; }
        [DisplayName("Email")]
        public string EmployeeMail { get; set; }
        [DisplayName("Phonenumber")]
        public int EmployeePhoneNr { get; set; }

        public virtual ICollection<LeaveApplication>? LeaveApplications { get; set; }
        public IList<LeaveList>? LeaveList { get; set; }
    }
}
