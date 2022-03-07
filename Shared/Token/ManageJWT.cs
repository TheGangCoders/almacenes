using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Token
{
    public class ManageJWT
    {
        public static readonly string SECRET_KEY = "CB4533633567E1D3FD8D4261A2DB6";
        public static readonly string AUDIENCE_TOKEN = "DefaultAudience";
        public static readonly string ISSUER_TOKEN = "DefaultIssuer";
        public static readonly int EXPIRE_TIME_SECONDS = 30*60*1000;

        public static readonly string CLAIM_TYPE_USER = "userData";

        public static string GenerateToken(ValueJWT valueJWT)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(SECRET_KEY));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: AUDIENCE_TOKEN,
                issuer: ISSUER_TOKEN,
                subject: new ClaimsIdentity(new[] {
                    new Claim(CLAIM_TYPE_USER, JsonConvert.SerializeObject(valueJWT))
                }),
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(EXPIRE_TIME_SECONDS),
                signingCredentials: signingCredentials
            );

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }

        public static ValueJWT GetJWTFromCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            ValueJWT jwt = null;
            if (claimsPrincipal != null)
            {
                var enumerator = claimsPrincipal.Claims.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var value = enumerator.Current.Value;
                    if (enumerator.Current.Type.Equals(CLAIM_TYPE_USER))
                    {
                        jwt = JsonConvert.DeserializeObject<ValueJWT>(value);
                    }
                }
            }
            return jwt;
        }

    }
}
