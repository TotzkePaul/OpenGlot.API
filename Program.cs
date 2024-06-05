using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
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

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(options =>
 {
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
 })
    .AddJwtBearer(options =>
    {
        var region = "us-east-1";
        var userPoolId = "us-east-1_sSfL2aipf";
        options.Authority = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
        options.Audience = "2ilnvfris0u021205020gruug1";
        var cache = builder.Services.BuildServiceProvider().GetRequiredService<IMemoryCache>();

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
            {
                var issuer = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
                var cacheKey = $"{issuer}_{kid}";

                if (!cache.TryGetValue(cacheKey, out SecurityKey issuerSigningKey))
                {
                    var cognitoIssuer = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
                    var jwtKeySetUrl = $"{cognitoIssuer}/.well-known/jwks.json";
                    var cognitoKeySet = new JsonWebKeySet(new HttpClient().GetStringAsync(jwtKeySetUrl).Result);

                    issuerSigningKey = cognitoKeySet.Keys.FirstOrDefault(x => x.KeyId == kid);

                    if (issuerSigningKey != null)
                    {
                        cache.Set(cacheKey, issuerSigningKey, TimeSpan.FromHours(1));
                    }
                }

                return [issuerSigningKey];
            },
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = false
        };
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
    .WithOrigins("https://openglot.com") // Replace with your frontend URL
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
