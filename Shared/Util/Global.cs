using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Util
{
    public class Global
    {
        public static readonly string APP_SETTINGS_CORS_ORIGIN = "CORSOrigin";
        public static readonly string APP_SETTINGS_BASIC_AUTHORIZATION_USERNAME = "BasicAuthorizationUsername";
        public static readonly string APP_SETTINGS_BASIC_AUTHORIZATION_PASSWORD = "BasicAuthorizationPassword";
        public static readonly string APP_SETTINGS_RFC_USER = "RFCUser";
        public static readonly string APP_SETTINGS_RFC_PASSWORD = "RFCPassword";

        public static readonly string HEADER_AUTHORIZATION = "Authorization";
        public static readonly string HEADER_AUTHORIZATION_VALUE_PREFIX = "Bearer ";
        public static readonly string HEADER_BASIC_AUTHORIZATION = "BasicAuthorization";
        public static readonly string HEADER_BASIC_AUTHORIZATION_VALUE_PREFIX = "Basic ";

        public static readonly string MESSAGE_ERROR_BASIC_AUTHORIZATION_NOT_SEND = "Not send Header Basic Authorization.";
        public static readonly string MESSAGE_ERROR_BASIC_AUTHORIZATION_WRONG = "The Header Basic Authorization is wrong.";
        public static readonly string MESSAGE_ERROR_BASIC_AUTHORIZATION_EXCEPTION = "Exception in Validation Header Basic Authorization.";

    }
}
