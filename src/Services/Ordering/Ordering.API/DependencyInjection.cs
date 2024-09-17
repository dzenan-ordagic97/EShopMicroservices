﻿namespace Ordering.API;
public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //add.carter()

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication webApplication)
    {
        //app.MapCarter();
        return webApplication;
    }
}