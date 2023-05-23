using DomainLayer.Entities.UserEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PresentaionLayer.Extentions
{
    public static class TokenSetUp
    {
        public static IServiceCollection RegisterToken(this IServiceCollection services, WebApplicationBuilder applicationBuilder)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            #region Configure Session
            services.AddDistributedMemoryCache();
            services.AddSession(
               options =>
               {
                   options.Cookie.IsEssential = true;
                   options.Cookie.HttpOnly = true;
                   options.IdleTimeout = TimeSpan.FromHours(10);
               }
           );
            #endregion
           

            services.Configure<TokenManagement>(applicationBuilder.Configuration.GetSection("tokenManagement"));
            var token = applicationBuilder.Configuration.GetSection("tokenManagement").Get<TokenManagement>();

            services.AddAuthentication(jwtBearerDefaults =>
            {
                jwtBearerDefaults.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                jwtBearerDefaults.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                    // ValidIssuer = token.Issuer,
                    // ValidAudience = token.Audience,

                };

            });


            return services;
        }
    }
}
