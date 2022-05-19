using Newtonsoft.Json;
using System.Net;

namespace PriceCalculatorAPI.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (PriceCalculatorValidationException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);
            }
            catch(PriceCalculatorNotImplementedException e) 
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);
            }   
            catch (PriceCalculatorException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);
            }
            

            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(e.Message);

            }

        }
    }
}
