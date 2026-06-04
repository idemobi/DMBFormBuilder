#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilder;
using DMBEffectBuilder;
using DMBFormBuilder;
using DMBFormBuilderLabs.Controllers;
using DMBFormBuilderWebsite;
using DMBPageBuilder;
using DMBServerHelper;
using DMBServerWebHelper;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ServerHelperConfiguration.LoadCommonConfig(builder);
ServerHelperConfiguration.Config.CookiePrefix = "DFB";
ServerWebHelperConfiguration.LoadCommonConfig(builder);
PageBuilderConfiguration.LoadCommonConfig(builder);
BootstrapBuilderConfiguration.LoadCommonConfig(builder);
ComponentBuilderConfiguration.LoadCommonConfig(builder);
EffectBuilderConfiguration.LoadCommonConfig(builder);
FormBuilderConfiguration.LoadCommonConfig(builder);

var mvcBuilder = builder.Services.AddControllersWithViews();
mvcBuilder.AddApplicationPart(typeof(FormBuilderController).Assembly);
mvcBuilder.AddMvcOptions(options => options.Filters.Add(new DMBFormBuilderWebsiteSidebarActionFilter()));

builder.Services.AddTransient<IMenuBarSectionProvider, DMBFormBuilderWebsiteMenuBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, ThemeBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, DebugBarSectionProvider>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

ServerWebHelperConfiguration.UseApp(app);

app.MapGet("/", context =>
{
    context.Response.Redirect("/FormBuilder/Introduction");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FormBuilder}/{action=Introduction}/{id?}");

app.Run();
