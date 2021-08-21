﻿using Microsoft.Extensions.Configuration;
using Offlogs.Client.TestApp.AspNetCore3;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit
{
    public class AspNetCore3TestStartup: Startup
    {
        public AspNetCore3TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
