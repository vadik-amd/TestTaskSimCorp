using Microsoft.Extensions.DependencyInjection;
using TestTaskSimCorp.DataAccess.Extensions;

namespace TestTaskSimCorp.Extensions;

public static class DomainExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services
            .AddDataAccess()
            .AddSingleton<IUniqueWordsReader, UniqueWordsReader>();
}


