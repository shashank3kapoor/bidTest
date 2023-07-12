using Microsoft.OpenApi.Models;

namespace BackEndApi
{
	public class Startup
	{
		public IConfiguration ConfigRoot { get; }
		private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public Startup(IConfiguration configuration)
		{
			ConfigRoot = configuration;
		}

		public void ConfigureServices (IServiceCollection services)
		{
			services.AddControllers ();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer ();
			services.AddSwaggerGen (c =>
			{
				c.SwaggerDoc ("v1", new OpenApiInfo { Title = "BackEndApi", Version = "v1" });
			});
			services.RegisterDependencies ();
			services.RegisterInfrastructureServices (ConfigRoot);
			services.AddCors (opts =>
			{
				opts.AddPolicy (MyAllowSpecificOrigins, builder =>
				    builder.WithOrigins ("http://localhost:4200")
				    .AllowAnyMethod ()
				    .AllowAnyHeader ()
				    .AllowCredentials ()
				);
			});
		}

		public void Configure (WebApplication app, IWebHostEnvironment env)
		{
			// Configure the HTTP request pipeline.
			if (env.IsDevelopment ()) {
				app.UseDeveloperExceptionPage ();
				app.UseSwagger ();
				app.UseSwaggerUI (c => c.SwaggerEndpoint ("/swagger/v1/swagger.json", "BackEndApi v1"));
			}

			app.UseCors (MyAllowSpecificOrigins);
			app.UseHttpsRedirection ();
			app.UseAuthorization ();
			app.MapControllers ();
			app.Run ();
		}
	}
}

