using System.Web.Mvc;

namespace LoadCheck.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var errorPartial = new PartialViewResult();
            filterContext.Result = new PartialViewResult();
        }
    }
}