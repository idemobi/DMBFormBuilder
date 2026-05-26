#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormBuilderConfiguration.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBServerWebHelper;
using DMBFormBuilder.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Provides default rendering options for form builders.
    /// </summary>
    public sealed class FormBuilderConfiguration : WebGenericConfiguration<FormBuilderConfiguration>, IServerWebConfig
    {
        /// <summary>
        /// Gets the process-wide default configuration used by builders when no host override is provided.
        /// </summary>
        public static FormBuilderConfiguration Default { get; } = new();

        /// <summary>
        /// Gets or sets the default label presentation used by field builders.
        /// </summary>
        /// <value>
        /// The default value is <see cref="FormLabelPresentation.Normal"/>.
        /// </value>
        public FormLabelPresentation LabelPresentation { get; set; } = FormLabelPresentation.Normal;

        /// <summary>
        /// Gets or sets the default validation strategy used by <see cref="FormBuilder"/> and field builders.
        /// </summary>
        /// <value>
        /// The default value is <see cref="FormValidationMode.ClientAndServer"/>.
        /// </value>
        public FormValidationMode ValidationMode { get; set; } = FormValidationMode.ClientAndServer;

        /// <summary>
        /// Registers embedded static assets and localization resources after host configuration is available.
        /// </summary>
        /// <param name="appBuilder">The application builder receiving FormBuilder services.</param>
        /// <param name="configBuilder">The configuration builder used by the host.</param>
        /// <param name="configRoot">The resolved configuration root.</param>
        public override void AfterConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
            appBuilder.Services.ConfigureOptions<FormBuilderConfigureOptions>();
            AddAnnotationLocalization(appBuilder,
                typeof(DMBFormBuilderDataAnnotationLocalization),
                typeof(DMBFormBuilderInternalLocalization)
            );
        }

        /// <summary>
        /// Indicates whether this package exposes API description endpoints.
        /// </summary>
        /// <returns>
        /// Always <see langword="false"/> because FormBuilder contributes Razor rendering helpers rather than HTTP APIs.
        /// </returns>
        public override bool ApiDescription()
        {
            return false;
        }

        /// <summary>
        /// Runs before configuration binding; FormBuilder currently has no pre-binding work.
        /// </summary>
        /// <param name="appBuilder">The application builder receiving configuration.</param>
        /// <param name="configBuilder">The configuration builder used by the host.</param>
        /// <param name="configRoot">The resolved configuration root.</param>
        public override void BeforeConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
        }

        /// <summary>
        /// Indicates whether the package requires an external configuration file or appsettings section.
        /// </summary>
        /// <returns>
        /// Always <see langword="false"/> because built-in defaults are sufficient.
        /// </returns>
        public override bool NeedsConfigFileOrAppSettings()
        {
            return false;
        }

        /// <summary>
        /// Populates fake configuration values for diagnostics; FormBuilder currently has no fake data to generate.
        /// </summary>
        public override void RandomFake()
        {
        }
    }
}
