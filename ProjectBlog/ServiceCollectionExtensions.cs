﻿using ProjetoBlog.Store;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lib.Net.Http.WebPush;
using Microsoft.EntityFrameworkCore;

public static class ServiceCollectionExtensions
{
    private const string SQLITE_CONNECTION_STRING_NAME = "PushSubscriptionSqliteDatabase";

    public static IServiceCollection AddPushSubscriptionStore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PushSubscriptionContext>(options =>
            options.UseSqlite(configuration.GetConnectionString(SQLITE_CONNECTION_STRING_NAME))
        );

        services.AddTransient<IPushSubscriptionStore, SqlitePushSubscriptionStore>();
        return services;
    }

    public static IServiceCollection AddPushNotificationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.AddMemoryCache();
        services.AddMemoryVapidTokenCache();
        services.AddPushServiceClient(options =>
        {
            IConfigurationSection pushNotificationServiceConfigurationSection = configuration.GetSection(nameof(PushServiceClient));

            options.Subject = pushNotificationServiceConfigurationSection.GetValue<string>(nameof(options.Subject));
            options.PublicKey = pushNotificationServiceConfigurationSection.GetValue<string>(nameof(options.PublicKey));
            options.PrivateKey = pushNotificationServiceConfigurationSection.GetValue<string>(nameof(options.PrivateKey));
        });

        return services;
    }
}