using Newtonsoft.Json;
using PriceCalculatorAPI.Helper;
using System.Dynamic;
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
                context.Response.ContentType = "application/json";
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add(e.Field, e.Message);
                await context.Response.WriteAsync(message.ToJsonCamelCase());
            }
            catch(PriceCalculatorNotImplementedException e) 
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                context.Response.ContentType = "application/json";
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add(e.Field, e.Message);
                await context.Response.WriteAsync(message.ToJsonCamelCase());
            }   
            catch (PriceCalculatorException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.Response.ContentType = "application/json";
                Dictionary<string, object> message = new Dictionary<string, object>();
                message.Add(e.Field, e.Message);
                await context.Response.WriteAsync(message.ToJsonCamelCase());
            }
            

            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(e.Message);

            }

        }
    }
}
