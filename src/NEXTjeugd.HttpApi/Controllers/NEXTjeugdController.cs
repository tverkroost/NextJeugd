using NEXTjeugd.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace NEXTjeugd.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class NEXTjeugdController : AbpController
    {
        protected NEXTjeugdController()
        {
            LocalizationResource = typeof(NEXTjeugdResource);
        }
    }
}