using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ledighet.Models
{
    public class LeaveList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        [Required]
        [ForeignKey("LeaveApplication")]
        public int LeaveApplicationId { get; set; }
        public LeaveApplication? LeaveApplication { get; set; }
    }
}
