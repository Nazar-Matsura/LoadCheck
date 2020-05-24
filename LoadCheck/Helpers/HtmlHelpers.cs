using System.Web;
using System.Web.Mvc;

namespace LoadCheck.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString Json(this HtmlHelper helper, object data)
        {
            return helper.Raw(System.Web.Helpers.Json.Encode(data));
        }
    }
}