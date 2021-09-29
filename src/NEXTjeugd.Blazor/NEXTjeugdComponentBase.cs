using NEXTjeugd.Localization;
using Volo.Abp.AspNetCore.Components;

namespace NEXTjeugd.Blazor
{
    public abstract class NEXTjeugdComponentBase : AbpComponentBase
    {
        protected NEXTjeugdComponentBase()
        {
            LocalizationResource = typeof(NEXTjeugdResource);
        }
    }
}
