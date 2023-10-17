using System.Threading;
using Microsoft.Extensions.Configuration;
namespace WebAPI;
public class RequestLimiter
{
    private readonly SemaphoreSlim _semaphore;

    public RequestLimiter(IConfiguration configuration)
    {
        int parallelLimit = configuration.GetValue<int>("Settings:ParallelLimit");
        _semaphore = new SemaphoreSlim(parallelLimit, parallelLimit);
    }

    public async Task<bool> TryAcquireAsync()
    {
        return await _semaphore.WaitAsync(0);
    }

    public void Release()
    {
        _semaphore.Release();
    }
}
