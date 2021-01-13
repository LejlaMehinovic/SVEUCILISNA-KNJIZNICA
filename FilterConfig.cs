using System.Web;
using System.Web.Mvc;

namespace SVEUCILISNA_KNJIZNICA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
