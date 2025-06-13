using koll_2.Service;
using Microsoft.AspNetCore.Mvc;

namespace koll_2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class NurseriesController : ControllerBase
{
    private readonly IDbService _nurseryService;

    public NurseriesController(IDbService nurseryService)
    {
        _nurseryService = nurseryService;
    }

    [HttpGet("{id}/batches")]
    public async Task<IActionResult> GetNurseryWithBatches(int id)
    {
        var nursery = await _nurseryService.GetNurseryWithBatchesAsync(id);
            
        if (nursery == null)
            return NotFound();

        return Ok(nursery);
    }
}