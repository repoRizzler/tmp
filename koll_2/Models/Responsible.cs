using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koll_2.Models;
[Table("Responsible")]
public class Responsible
{
        
    [Key]
    public int BatchId { get; set; }

    public int EmployeeId { get; set; }
        
    [MaxLength(50)]
    public string Role { get; set; }
        
    [ForeignKey("BatchId")]
    public virtual Batch Batch { get; set; }
        
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }
}