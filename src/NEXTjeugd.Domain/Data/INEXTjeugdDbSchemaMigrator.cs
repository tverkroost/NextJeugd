using System.Threading.Tasks;

namespace NEXTjeugd.Data
{
    public interface INEXTjeugdDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}