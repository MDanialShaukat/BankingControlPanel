using BankingControlPanel.Data;
using BankingControlPanel.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using BankingControlPanel.Extensions;
using FluentValidation;
using AutoMapper;
using System.Reflection;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add DbContext and Identity
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        // Add controllers and configure FluentValidation
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<ClientValidator>();

        //Resolve AutoMapper
        services.AddAutoMapper(typeof(Startup));

        // Register custom services and identity
        // Configure JWT Authentication
        // Configure Swagger
        services
            .AddDataLayer(Configuration)
            .AddJwtBearerConfiguration(Configuration)
            .AddSwaggerDocs();
    }
}
