using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using Shared.Token;
using Shared.Util;

namespace Shared.Handlers
{
    public class JWTValidationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                string tokenAuthorization;

                if (request.Method != HttpMethod.Options)
                {
                    if (!MetodoComun.TryRetrieveHeader(request, Global.HEADER_AUTHORIZATION, Global.HEADER_AUTHORIZATION_VALUE_PREFIX, out tokenAuthorization))
                    {
                        return base.SendAsync(request, cancellationToken);
                    }

                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(ManageJWT.SECRET_KEY));

                    SecurityToken securityToken;
                    var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = ManageJWT.AUDIENCE_TOKEN,
                        ValidIssuer = ManageJWT.ISSUER_TOKEN,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey
                    };

                    // Extract and assign Current Principal and user
                    Thread.CurrentPrincipal = tokenHandler.ValidateToken(tokenAuthorization, validationParameters, out securityToken);
                    HttpContext.Current.User = tokenHandler.ValidateToken(tokenAuthorization, validationParameters, out securityToken);
                }

                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                return base.SendAsync(request, cancellationToken);
            }
        }

        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }
    }
}
