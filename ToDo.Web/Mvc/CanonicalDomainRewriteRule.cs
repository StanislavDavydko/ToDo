using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Logging;
using System;

namespace ToDo.Web.Mvc
{
    public class CanonicalDomainRewriteRule : IRule
    {
        public void ApplyRule(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var hostString = request.Host.Value;

            if (hostString.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
            {
                var newHost = new HostString(hostString.Replace("www.", string.Empty));
                var response = context.HttpContext.Response;
                response.StatusCode = StatusCodes.Status301MovedPermanently;

                response.Headers["Location"] = UriHelper.BuildAbsolute(request.Scheme, newHost, request.PathBase, request.Path, request.QueryString);
                context.Result = RuleResult.EndResponse;
                context.Logger.LogInformation(13, $"Request redirected from {hostString} to canonical domain: {newHost.Value}");
            }
            else
            {
                context.Result = RuleResult.ContinueRules;
            }
        }
    }
}
