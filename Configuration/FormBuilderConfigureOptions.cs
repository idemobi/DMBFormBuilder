#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBFormBuilder.csproj FormBuilderConfigureOptions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

#endregion

namespace DMBFormBuilder
{
    /// <summary>
    /// Adds the embedded FormBuilder static web assets to ASP.NET Core static file options.
    /// </summary>
    /// <remarks>
    /// The post-configuration composes the host web root provider with the embedded <c>wwwroot</c> provider so
    /// FormBuilder CSS and JavaScript can be served by consuming MVC/Razor applications.
    /// </remarks>
    public sealed class FormBuilderConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        private const string BasePath = "wwwroot";

        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormBuilderConfigureOptions"/> class.
        /// </summary>
        /// <param name="environment">The web host environment that provides the application web root file provider.</param>
        public FormBuilderConfigureOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        /// <summary>
        /// Composes the current static file provider with the embedded FormBuilder asset provider.
        /// </summary>
        /// <param name="name">The options name supplied by the ASP.NET Core options pipeline.</param>
        /// <param name="options">The static file options to update.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="name"/> or <paramref name="options"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown when neither the options nor the host environment provide a file provider.
        /// </exception>
        public void PostConfigure(string? name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();

            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider ??= Environment.WebRootFileProvider;
            ManifestEmbeddedFileProvider filesProvider = new(GetType().Assembly, BasePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
