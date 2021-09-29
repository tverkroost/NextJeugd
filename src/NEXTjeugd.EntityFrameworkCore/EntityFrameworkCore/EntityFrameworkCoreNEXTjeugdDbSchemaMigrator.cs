using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NEXTjeugd.Data;
using Volo.Abp.DependencyInjection;

namespace NEXTjeugd.EntityFrameworkCore
{
    public class EntityFrameworkCoreNEXTjeugdDbSchemaMigrator
        : INEXTjeugdDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreNEXTjeugdDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the NEXTjeugdDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<NEXTjeugdDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
