using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace NEXTjeugd.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class NEXTjeugdBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "NEXTjeugd";
    }
}
