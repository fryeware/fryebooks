using System.Web;
using System.Web.Mvc;

namespace Fryebooks
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // for local development, comment authorize all attrib
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        }
    }
}
