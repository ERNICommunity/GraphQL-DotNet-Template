using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using productsWebapi.GraphQl;
using productsWebapi.Products;
using productsWebapi.Repositories;
using static productsWebapi.DemoData;

namespace productsWebapi
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;
        private readonly IRepository<IProduct> _products;
        private readonly IMutableRepository<Review> _reviews;
        public Startup(IConfiguration config, IHostingEnvironment env)
        {
            _env = env;
            _config = config;
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
            services.AddScoped<IRepository<IProduct>>(_ => _products);
            services.AddScoped<IRepository<Review>>(_ => _reviews);
            services.AddScoped<IMutableRepository<Review>>(_ => _reviews);
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<ProductSchema>();
            
            services.AddGraphQL(o => { o.ExposeExceptions = _env.IsDevelopment(); })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(context => context.User);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseHttpsRedirection();
            app.UseGraphQL<ProductSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            app.UseCookiePolicy();
        }
    }
}
