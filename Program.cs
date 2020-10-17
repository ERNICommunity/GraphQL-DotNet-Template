﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace productsWebapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
            builder.Build().Run();
        }
    }
}
