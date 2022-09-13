using MailContainerTest.Abstractions;
using MailContainerTest.Data;
using MailContainerTest.Services;
using MailContainerTest.Types;
using MailContainerTest.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailContainerTest
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMailContainerTest(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(DataStoreTypeOptions.DataStoreTypeConfig);
            services.Configure<DataStoreTypeOptions>(options => section.Bind(options));

            services.AddScoped<IMailContainerDataStoreFactory, MailContainterDataStoreFactory>();
            services.AddScoped<IMailContainerValidator, MailContainerValidator>();
            services.AddScoped<IMailTransferService, MailTransferService>();

            return services;
        }
    }
}