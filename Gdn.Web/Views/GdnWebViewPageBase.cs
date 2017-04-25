using Abp.Web.Mvc.Views;

namespace Gdn.Web.Views
{
    public abstract class GdnWebViewPageBase : GdnWebViewPageBase<dynamic>
    {

    }

    public abstract class GdnWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected GdnWebViewPageBase()
        {
            LocalizationSourceName = GdnConsts.LocalizationSourceName;
        }
    }
}