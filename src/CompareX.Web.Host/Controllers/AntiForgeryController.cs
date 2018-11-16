using Microsoft.AspNetCore.Antiforgery;
using CompareX.Controllers;

namespace CompareX.Web.Host.Controllers
{
    public class AntiForgeryController : CompareXControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
