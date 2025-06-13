using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace koll_2.Models;
[Table("Responsible")]
[PrimaryKey(nameof(BatchId), nameof(EmployeeId))]
public class Responsible
{
    public int BatchId { get; set; }
    public int EmployeeId { get; set; }
        
    [MaxLength(50)]
    public string Role { get; set; }
        
    [ForeignKey("BatchId")]
    public virtual Batch Batch { get; set; }
        
    [ForeignKey("EmployeeId")]
    public virtual Employee Employee { get; set; }
}