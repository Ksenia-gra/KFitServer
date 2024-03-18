using KFitServer.BusinessLogic.DBContext;
using KFitServer.BusinessLogic.Helpers;
using KFitServer.BusinessLogic.Repository;
using KFitServer.BusinessLogic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
	private static void Main(string[] args)
	{		
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FFF1DA25-F6B0-4754-877F-7B6CC77B20D2"));

		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddDbContext<KfitContext>();
		builder.Services.AddSingleton<IHashCreator, BCryptCreator>();
		builder.Services.AddScoped<IDbRepository, DbRepository>();
		builder.Services.AddScoped<IUserRepository, UserDbRepository>();
		builder.Services.AddScoped<INutritionRepository, NutritionDbRepository>();
		builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
		builder.Services.AddTransient<AuthentificationService>();
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = key,
					ValidateAudience = false,
					ValidateIssuer = false
				};
			});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCookiePolicy(new CookiePolicyOptions
		{
			MinimumSameSitePolicy = SameSiteMode.Strict,
			HttpOnly = HttpOnlyPolicy.Always,
			Secure = CookieSecurePolicy.Always
		});

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}