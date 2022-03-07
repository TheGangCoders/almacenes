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
using Shared.Util;

namespace Shared.Handlers
{
    public class BasicAuthorizationValidationHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                string tokenBasicAuthorization;

                if (request.Method != HttpMethod.Options)
                {
                    if (!MetodoComun.TryRetrieveHeader(request, Global.HEADER_BASIC_AUTHORIZATION, Global.HEADER_BASIC_AUTHORIZATION_VALUE_PREFIX, out tokenBasicAuthorization))
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Unauthorized, Global.MESSAGE_ERROR_BASIC_AUTHORIZATION_NOT_SEND);
                    }

                    if (!ValidBasicAuthorization(tokenBasicAuthorization))
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Unauthorized, Global.MESSAGE_ERROR_BASIC_AUTHORIZATION_WRONG);
                    }

                }
                return await base.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, Global.MESSAGE_ERROR_BASIC_AUTHORIZATION_EXCEPTION);
            }
        }
       
        private static bool ValidBasicAuthorization(string credentials)
        {
            var usernameConfig = ConfigurationManager.AppSettings[Global.APP_SETTINGS_BASIC_AUTHORIZATION_USERNAME];
            var passwordConfig = ConfigurationManager.AppSettings[Global.APP_SETTINGS_BASIC_AUTHORIZATION_PASSWORD];

            var encoding = Encoding.GetEncoding("iso-8859-1");
            credentials = encoding.GetString(Convert.FromBase64String(credentials));
            var credentialsArray = credentials.Split(':');
            var username = credentialsArray[0];
            var password = credentialsArray[1];
            /* Aquí se validan las credenciales o el token enviado en el encabezado de la solicitud */
            return username == usernameConfig && password == passwordConfig;
        }
    }
}
