
using System;
using GraphQL;
using GraphQL.Server;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using productsWebapi.GraphQl;
using productsWebapi.GraphQl.Messaging;
using productsWebapi.Products;
using productsWebapi.Repositories;
using static productsWebapi.DemoData;

namespace productsWebapi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<IProduct> _products;
        private readonly IMutableRepository<Review> _reviews;
        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            Seed(out _products, out _reviews);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            ILogger log = new LoggerConfiguration()
                                .MinimumLevel.Warning()
                                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                .MinimumLevel.Override("GraphQL", LogEventLevel.Information)
                                .WriteTo.Console()
                                .CreateLogger();
            services.AddLogging(l => l.AddSerilog(log));
            services.AddScoped<IRepository<IProduct>>(_ => _products);
            services.AddScoped<IRepository<Review>>(_ => _reviews);
            services.AddScoped<IMutableRepository<Review>>(_ => _reviews);
            services.AddScoped<IServiceProvider>(s => new FuncServiceProvider(s.GetRequiredService));
            services.AddSingleton<ReviewMessageService>();
            services.AddScoped<ProductSchema>();
            
            services.AddGraphQL((options, provider) =>
                    {
                        options.EnableMetrics = _env.IsDevelopment();
                        options.UnhandledExceptionDelegate = ctx => log.Error(ctx.OriginalException.Message, "{Error} occured");
                    }
                )
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = _env.IsDevelopment())
            .AddWebSockets()
            .AddGraphTypes(typeof(ProductSchema));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            // Check these settings!
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ProductSchema>();
            app.UseGraphQL<ProductSchema>();
            app.UseGraphiQLServer();
            app.UseGraphQLPlayground();
            app.UseCookiePolicy();
        }
    }
}
