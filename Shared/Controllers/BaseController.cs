using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

using Shared.Token;

namespace Shared.Controllers
{
    public abstract class BaseController: ApiController
    {
        protected ValueJWT JWT
        {
            get
            {
                var currentUser = (ClaimsPrincipal)HttpContext.Current.User;
                if (currentUser != null)
                {
                    return ManageJWT.GetJWTFromCurrentUser(currentUser);
                }
                return null;
            }
        }
    }
}
