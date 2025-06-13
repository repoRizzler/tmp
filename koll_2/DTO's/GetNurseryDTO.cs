namespace koll_2.DTO_s;

public class NurseryWithBatchesDto
{
    public int NurseryId { get; set; }
    public string Name { get; set; }
    public string EstablishedDate { get; set; }
    public ICollection<BatchDto> Batches { get; set; } = new List<BatchDto>();
}

public class BatchDto
{
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public string SownDate { get; set; }
    public string ReadyDate { get; set; }
    public SpeciesDto Species { get; set; }
    public ICollection<ResponsibleDto> Responsible { get; set; } = new List<ResponsibleDto>();
}

public class SpeciesDto
{
    public string LatinName { get; set; }
    public int GrowthTimeInYears { get; set; }
}

public class ResponsibleDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}