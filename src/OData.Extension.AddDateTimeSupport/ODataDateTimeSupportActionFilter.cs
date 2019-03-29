using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OData.ActionFilter.AddDateTimeSupport
{
    public class ODataDateTimeSupportActionFilter : ActionFilterAttribute
    {
        private readonly ODataDateTimeSupportService _customFilter;
        public ODataDateTimeSupportActionFilter()
        {
            _customFilter = new ODataDateTimeSupportService(); ;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var uri = actionContext.Request.RequestUri;
            actionContext.Request.RequestUri = _customFilter.UpdateRequestUri(uri);
        }
    }
}
