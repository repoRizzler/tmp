using koll_2.DTO_s;

namespace koll_2.Service;
public interface IDbService
{
    Task<NurseryWithBatchesDto?> GetNurseryWithBatchesAsync(int nurseryId);
}
public class DbService : IDbService
{
    public Task<NurseryWithBatchesDto?> GetNurseryWithBatchesAsync(int nurseryId)
    {
        throw new NotImplementedException();
    }
}