﻿using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PolyglotAPI.Configs;
using PolyglotAPI.Data;
using PolyglotAPI.Data.Repos;
using PolyglotAPI.Health;
using System.Configuration;
using System.Text.Json.Serialization;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        services.AddHealthChecks()
            .AddCheck<CustomHealthCheck>("Custom Check");

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

            // Add the following lines to configure Bearer authentication
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] {}
                }
            });
        });

        services.AddMemoryCache();

        var jwtBearerOptions = new JwtBearerOptionsConfig();
        Configuration.Bind("JwtBearerOptions", jwtBearerOptions);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            //var region = "us-east-1";
            //var userPoolId = "us-east-1_sSfL2aipf";
            options.Authority = $"https://cognito-idp.{jwtBearerOptions.Region}.amazonaws.com/{jwtBearerOptions.UserPoolId}";
            options.Audience = jwtBearerOptions.Audience;
            var cache = services.BuildServiceProvider().GetRequiredService<IMemoryCache>();

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
                {
                    var issuer = $"https://cognito-idp.{jwtBearerOptions.Region}.amazonaws.com/{jwtBearerOptions.UserPoolId}";
                    var cacheKey = $"{issuer}_{kid}";

                    if (!cache.TryGetValue(cacheKey, out SecurityKey issuerSigningKey))
                    {
                        var cognitoIssuer = $"https://cognito-idp.{jwtBearerOptions.Region}.amazonaws.com/{jwtBearerOptions.UserPoolId}";
                        var jwtKeySetUrl = $"{cognitoIssuer}/.well-known/jwks.json";
                        var cognitoKeySet = new JsonWebKeySet(new HttpClient().GetStringAsync(jwtKeySetUrl).Result);

                        issuerSigningKey = cognitoKeySet.Keys.FirstOrDefault(x => x.KeyId == kid);

                        if (issuerSigningKey != null)
                        {
                            cache.Set(cacheKey, issuerSigningKey, TimeSpan.FromHours(1));
                        }
                    }

                    return new[] { issuerSigningKey };
                },
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false
            };
        });

        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine("Connection String: " + connectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
                    //.UseLazyLoadingProxies()
                    );

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Use(async (context, next) =>
        {
            // Log request details
            var request = context.Request;
            request.EnableBuffering();
            var requestBodyContent = await new StreamReader(request.Body).ReadToEndAsync();

            Console.WriteLine($"Request Method: {request.Method}");
            Console.WriteLine($"Request Path: {request.Path}");
            Console.WriteLine($"Request Headers: {string.Join(", ", request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");
            Console.WriteLine($"Request Body: {requestBodyContent}");

            // Reset the request body stream position for the next middleware to read
            context.Request.Body.Position = 0;

            await next.Invoke();
        });

        app.UseSwagger();
        app.UseSwaggerUI();

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = (check) => true
            });
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
    }
}