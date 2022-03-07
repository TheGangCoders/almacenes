using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;

using Shared.Util;

namespace Shared.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CorsOrigin : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        public CorsOrigin()
        {
            // Create a CORS policy. 
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            // Add allowed origins. 
            var origins = ConfigurationManager.AppSettings[Global.APP_SETTINGS_CORS_ORIGIN].Split(',');

            foreach (var origin in origins)
            {
                _policy.Origins.Add(origin);
            }
        }
        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}
