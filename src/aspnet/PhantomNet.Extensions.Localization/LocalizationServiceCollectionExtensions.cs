using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizationServiceCollectionExtensions
    {
        public static IServiceCollection AddLocalization(this IServiceCollection services, string defaultCulture, params string[] supportedCultures)
        {
            if (string.IsNullOrWhiteSpace(defaultCulture))
            {
                throw new ArgumentNullException(nameof(defaultCulture));
            }

            services.AddLocalization(options => options.ResourcesPath = "Resources")
                    .AddSingleton<IStringLocalizerFactory, AssemblyAwareResourceManagerStringLocalizerFactory>();

            services.Configure<RequestLocalizationOptions>(options => {
                options.DefaultRequestCulture = new RequestCulture(culture: defaultCulture, uiCulture: defaultCulture);
                var cultures = supportedCultures.Select(x => new CultureInfo(x)).ToArray();
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            return services;
        }
    }
}
