using System.Text.Json;

namespace LoggingManagerAPI.Configuration
{
    public class UnauthorizeMiddleware
    {
        private readonly RequestDelegate next;

        public UnauthorizeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await this.next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Message = "You are not authorize to access the endpoint",
                    ResquestStatus = "Unauthorize",
                    StatusCode = StatusCodes.Status401Unauthorized,
                }));
                return;
            }

        }
    }
}
