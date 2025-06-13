using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koll_2.Models;

[Table("Species")]
public class Species
{
    [Key]
    public int SpeciesId { get; set; }
        
    [MaxLength(100)]
    public string LatinName { get; set; }
        
    public int GrowthTimeInYears { get; set; }
        
    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
