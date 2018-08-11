
using System;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    using Serilog;

    public static class HelpersConfigurationExtensions
    {
        public static IConfigurationBuilder AddIf(this IConfigurationBuilder app, bool include, Func<IConfigurationBuilder, IConfigurationBuilder> action)
        {
            return include ? action(app) : app;
        }
    }
}
