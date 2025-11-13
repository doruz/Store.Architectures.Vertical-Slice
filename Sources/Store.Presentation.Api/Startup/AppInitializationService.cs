using Store.Core.Shared;

internal sealed class AppInitializationService(IEnumerable<IAppInitializer> initializers) 
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var initializer in initializers)
        {
            await initializer.Execute();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}