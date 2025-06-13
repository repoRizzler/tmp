using ForestNursery.Data;
using koll_2.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace koll_2.Service;
public interface IDbService
{
    Task<NurseryWithBatchesDto?> GetNurseryWithBatchesAsync(int nurseryId);
}
public class DbService : IDbService
{
    private readonly AppDbContext _context;
    
    public DbService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<NurseryWithBatchesDto?> GetNurseryWithBatchesAsync(int nurseryId)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.Batches)
            .ThenInclude(b => b.Species)
            .Include(n => n.Batches)
            .ThenInclude(b => b.BatchEmployees)
            .ThenInclude(be => be.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == nurseryId);
        if (nursery == null)
            return null;

        return new NurseryWithBatchesDto
        {
            NurseryId = nursery.NurseryId,
            Name = nursery.Name,
            EstablishedDate = nursery.EstablishedDate.ToString("yyyy-MM-dd"),
            Batches = nursery.Batches.Select(b => new BatchDto
            {
                BatchId = b.BatchId,
                Quantity = b.Quantity,
                SownDate = b.SownDate.ToString("yyyy-MM-dd"),
                ReadyDate = b.ReadyDate?.ToString("yyyy-MM-dd"),
                Species = new SpeciesDto
                {
                    LatinName = b.Species.LatinName,
                    GrowthTimeInYears = b.Species.GrowthTimeInYears
                },
                Responsible = b.BatchEmployees.Select(be => new ResponsibleDto
                {
                    FirstName = be.Employee.FirstName,
                    LastName = be.Employee.LastName,
                    Role = be.Role
                }).ToList()
            }).ToList()
        };
    }
}