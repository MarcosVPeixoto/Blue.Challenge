using Blue.Challenge.Business.Extensions;
using Blue.Challenge.Business.Mapper;
using Blue.Challenge.Infra.Context;
using Blue.Challenge.Infra.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Blue.Challenge.App
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyMethod();
                builder.SetIsOriginAllowed(x => true);
                builder.AllowCredentials();
            }));
            services.AddHttpClient();
            var b =  Configuration.GetConnectionString("DefaultConnection");
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ChallengeMapper));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Solar Web API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticação JWT. Digite 'Bearer' [space] e insira o token abaixo. Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(
                    new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference{
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = "Bearer"
                                                    },
                                    Scheme = "oauth2",
                                    Name = "Bearer",
                                    In = ParameterLocation.Header,
                                },
                            new List<string>()
                        }
                    });
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();            
            services.AddMediatr();
            services.AddServices();
            services.AddAuthentication(Configuration);
            services.AddAuthorization();
            services.AddRepositories();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization());
            
            
        }
    }
}

