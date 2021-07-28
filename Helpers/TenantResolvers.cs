using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Helpers
{
    public class UserTenantResolver
    {
        public UserTenantResolver()
        {

        }
    }

    public class HostTenantResolver
    {

    }

    public class QueryStringTenantResolver
    {
        private readonly IHttpContextAccessor _accessor;

        public QueryStringTenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        internal int GetTenantId()
        {
            var query = _accessor.HttpContext.Request.Query;
            _ = int.TryParse(query["tenant"], out var tenantId);

            Console.WriteLine($"Tenant: {tenantId}");

            return tenantId;
        }
    }
}
