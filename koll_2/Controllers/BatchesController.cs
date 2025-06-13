using koll_2.DTOs;
using koll_2.Service;
using Microsoft.AspNetCore.Mvc;

namespace koll_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BatchesController : ControllerBase
{
    private readonly IDbService _batchService;

    public BatchesController(IDbService batchService)
    {
        _batchService = batchService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBatch([FromBody] CreateBatchDto createBatchDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _batchService.CreateBatchAsync(createBatchDto);

        if (!result.Success)
        {
            return BadRequest(result.ErrorMessage);
        }

        return CreatedAtAction(
            nameof(CreateBatch),
            new { id = result.Result.BatchId },
            result.Result
        );
    }
}