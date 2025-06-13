using ForestNursery.Data;
using koll_2.DTO_s;
using koll_2.DTOs;
using koll_2.Models;
using Microsoft.EntityFrameworkCore;

namespace koll_2.Service;
public interface IDbService
{
    Task<NurseryWithBatchesDto?> GetNurseryWithBatchesAsync(int nurseryId);
    Task<(bool Success, string ErrorMessage, BatchCreatedDto? Result)> CreateBatchAsync(CreateBatchDto createBatchDto);
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
    public async Task<(bool Success, string ErrorMessage, BatchCreatedDto? Result)> CreateBatchAsync(
        CreateBatchDto createBatchDto)
    {
        var species = await _context.Species
            .FirstOrDefaultAsync(s => s.LatinName == createBatchDto.Species);

        if (species == null)
        {
            return (false, "Species not found", null);
        }

        var nursery = await _context.Nurseries
            .FirstOrDefaultAsync(n => n.Name == createBatchDto.Nursery);

        if (nursery == null)
        {
            return (false, "Nursery not found", null);
        }

        var employeeIds = createBatchDto.Responsible.Select(r => r.EmployeeId).ToList();
        var employees = await _context.Employees
            .Where(e => employeeIds.Contains(e.EmployeeId))
            .ToListAsync();

        if (employees.Count != employeeIds.Count)
        {
            var missingIds = employeeIds.Except(employees.Select(e => e.EmployeeId)).ToList();
            return (false, $"Employee(s) not found with ID(s): {string.Join(", ", missingIds)}", null);
        }

        var sownDate = DateTime.Now;
        var readyDate = sownDate.AddYears(species.GrowthTimeInYears);

        var batch = new Batch
        {
            Quantity = createBatchDto.Quantity,
            SownDate = sownDate,
            ReadyDate = readyDate,
            NurseryId = nursery.NurseryId,
            SpeciesId = species.SpeciesId
        };

        _context.Batches.Add(batch);
        await _context.SaveChangesAsync();

        var responsibleList = new List<Responsible>();
        foreach (var responsibleDto in createBatchDto.Responsible)
        {
            var responsible = new Responsible
            {
                BatchId = batch.BatchId,
                EmployeeId = responsibleDto.EmployeeId,
                Role = responsibleDto.Role
            };
            responsibleList.Add(responsible);
            _context.Responsibles.Add(responsible);
        }

        await _context.SaveChangesAsync();

        var result = new BatchCreatedDto
        {
            BatchId = batch.BatchId,
            Quantity = batch.Quantity,
            SownDate = batch.SownDate.ToString("yyyy-MM-dd"),
            ReadyDate = batch.ReadyDate?.ToString("yyyy-MM-dd"),
            Species = species.LatinName,
            Nursery = nursery.Name,
            Responsible = createBatchDto.Responsible
        };

        return (true, string.Empty, result);
    }
}
