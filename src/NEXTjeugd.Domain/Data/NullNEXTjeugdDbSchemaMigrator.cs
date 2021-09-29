using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace NEXTjeugd.Data
{
    /* This is used if database provider does't define
     * INEXTjeugdDbSchemaMigrator implementation.
     */
    public class NullNEXTjeugdDbSchemaMigrator : INEXTjeugdDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}