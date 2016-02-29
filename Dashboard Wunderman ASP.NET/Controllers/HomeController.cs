using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dashboard_Wunderman_ASP.NET.Models;
using Umbraco.Web.Mvc;

namespace Dashboard_Wunderman_ASP.NET.Controllers
{
    public class HomeController : SurfaceController
    {
        public async Task<ActionResult> Home()
        {
            var userDisplayModel = new UserDisplayModel();

            var authenticateResult = await HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie");
            if (authenticateResult != null)
            {
                var tokenClaim = authenticateResult.Identity.Claims.FirstOrDefault(claim => claim.Type == "urn:token:google");
                if (tokenClaim != null)
                {
                    var accessToken = tokenClaim.Value;
                    userDisplayModel.AccessToken = accessToken;
                }
            }

            return PartialView(userDisplayModel);
        }

        public ActionResult AuthorizeGoogle()
        {
            return new ChallengeResult("Google", Url.Action("Home"));
        }
    }
}