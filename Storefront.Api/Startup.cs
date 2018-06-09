using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Storefront.Api.AutoMapper;
using Storefront.Api.ExceptionHandling;
using Storefront.Dal.DbContext;
using Storefront.Dal.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Text;

namespace Storefront.Api
{
	public class Startup
	{
		private readonly string _connStr = Environment.GetEnvironmentVariable("ConnStr");
		private readonly string _issuer = Environment.GetEnvironmentVariable("JwtIssuer");
		private readonly string _key = Environment.GetEnvironmentVariable("JwtKey");

		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton(this.Configuration);
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			ConfigureContexts(services);
			ConfigureAuth(services);
			ConfigureCompression(services);
			ConfigureMvc(services);
			ConfigureLogging(services);

			AutoMapperConfig.RegisterAutoMapperProfiles();

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseResponseCompression();
			app.UseCors("CorsPolicy");
			app.UseStaticFiles();
			app.UseMiddleware(typeof(ExceptionMiddleware));
			app.UseHttpsRedirection();
			app.UseMvc();
		}

		private void ConfigureAuth(IServiceCollection services)
		{
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				}).AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = this._issuer,
						ValidAudience = this._issuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._key)),
						ClockSkew = TimeSpan.Zero // remove delay of token when expire
					};
				});

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
#if DEBUG
					  .AllowAnyOrigin()
					  .AllowAnyMethod()
					  .AllowAnyHeader()
					  .AllowCredentials()
#else
                      .WithOrigins("")
#endif
				.Build());
			});
		}

		private static void ConfigureCompression(IServiceCollection services)
		{
			services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
			services.AddResponseCompression();
		}

		private void ConfigureContexts(IServiceCollection services)
		{
			services.AddDbContext<StorefrontDbContext>(options =>
			{
				options.UseSqlServer(this._connStr);
				options.UseLazyLoadingProxies();
#if DEBUG
				options.EnableSensitiveDataLogging();
#endif
			});

			services.AddIdentity<StorefrontUser, StorefrontRole>()
				.AddEntityFrameworkStores<StorefrontDbContext>()
				.AddDefaultTokenProviders();
		}

		private static void ConfigureMvc(IServiceCollection services)
		{
			var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				//.RequireRole("Admin", "SuperUser")
				.Build();

			services
				//.AddMvc()
				.AddMvc(options => options.Filters.Add(new AuthorizeFilter(policy)))
				.AddJsonOptions(options => options.SerializerSettings.DateFormatString = "YYYY-MM-DDTHH:mm:ssZ");
		}

		private void ConfigureLogging(IServiceCollection services)
		{
			NLog.LogManager.LoadConfiguration("nlog.config");
			services.AddSingleton(new LoggerFactory().AddNLog());
			services.AddLogging();
		}
	}
}
