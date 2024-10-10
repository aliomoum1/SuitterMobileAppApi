using Microsoft.Extensions.DependencyInjection;

namespace SuitterAppApi.Infrastructure.Persistence.Initialization;
internal class CustomSeederRunner
{
    private readonly ICustomSeeder[] _seeders;
    private readonly ICreator[] _creators;

    public CustomSeederRunner(IServiceProvider serviceProvider)
    {
        _seeders = serviceProvider.GetServices<ICustomSeeder>().ToArray();
        _creators = serviceProvider.GetServices<ICreator>().ToArray();

    }


    public async Task RunSeedersAsync(CancellationToken cancellationToken)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.InitializeAsync(cancellationToken);
        }
        //Console.WriteLine("Are You Sure u want to run creator (yes,no)");
        //if (Console.ReadLine().ToLower() == "yes")
        //{
        //    foreach (var creator in _creators)
        //    {
        //        await creator.InitializeAsync(cancellationToken);

        //    }

        //}
    }
}