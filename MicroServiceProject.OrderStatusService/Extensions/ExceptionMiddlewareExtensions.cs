﻿namespace MicroServiceProject.OrderStatusService.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware.ExceptionMiddleware>();
        }
    }
}
