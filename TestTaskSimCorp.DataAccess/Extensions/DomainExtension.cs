using Microsoft.Extensions.DependencyInjection;

namespace TestTaskSimCorp.DataAccess.Extensions;

public static class DataAccessExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services) =>
        services.AddSingleton<IDataSource, FileDataSource>();
}


