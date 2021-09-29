using NEXTjeugd.Localization;
using Volo.Abp.Application.Services;

namespace NEXTjeugd
{
    /* Inherit your application services from this class.
     */
    public abstract class NEXTjeugdAppService : ApplicationService
    {
        protected NEXTjeugdAppService()
        {
            LocalizationResource = typeof(NEXTjeugdResource);
        }
    }
}
