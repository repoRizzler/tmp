using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace koll_2.Models;
[Table("Seedling_Batch")]
public class Batch
{
    [Key]
    public int BatchId { get; set; }

    public int NurseryId { get; set; }
        
    public int SpeciesId { get; set; }
    
    public int Quantity { get; set; }

    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }


    [ForeignKey("NurseryId")]
    public virtual Nursery Nursery { get; set; }
        
    [ForeignKey("SpeciesId")]
    public virtual Species Species { get; set; }
        
    public virtual ICollection<Responsible> BatchEmployees { get; set; } = new List<Responsible>();
}