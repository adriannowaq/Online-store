using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Extensions
{
    public static class ContextExtension
    {
        public static Dictionary<string, string> GetQueryParameters(this HttpContext context)
        {
            return context.Request.Query.ToDictionary(d => d.Key, d => d.Value.ToString());
        }
    }
}
