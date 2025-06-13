using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koll_2.Models;


[Table("Employee")]
public class Employee
{
    [Key]
    public int EmployeeId { get; set; }
        
    [MaxLength(50)]
    public string FirstName { get; set; }
        
    [MaxLength(50)]
    public string LastName { get; set; }
        
    public virtual ICollection<Responsible> BatchEmployees { get; set; } = new List<Responsible>();
}