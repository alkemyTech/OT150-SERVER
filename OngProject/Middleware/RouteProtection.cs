using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Middleware
{
    public class RouteProtection
    {
        private readonly RequestDelegate _next;
        public RouteProtection(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            List<string> methods = new List<string>();
            methods.Add("post");
            methods.Add("put");
            methods.Add("delete");
            var method = context.Request.Method;
            List<string> paths = new List<string>();
            paths.Add("/activities");
            paths.Add("/category");
            paths.Add("/news");
            paths.Add("/organization");
            paths.Add("/testimonials");
            string path = context.Request.Path;
            if (methods.Contains(method.ToLower()) && paths.Contains(path.ToLower()))
            {
                if (!context.User.IsInRole("Admin"))
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }
}
