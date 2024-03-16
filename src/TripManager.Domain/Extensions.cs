using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripManager.Domain.Emails;

namespace TripManager.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetRequiredSection(EmailSettings.SectionName));

        return services;
    }
}