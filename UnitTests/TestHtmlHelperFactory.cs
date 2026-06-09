#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Dynamic;
using System.Reflection;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

#endregion

namespace DMBFormBuilderUnitTest;

public static class TestHtmlHelperFactory
{
    #region Static methods

    public static IHtmlHelper Create(TextWriter? writer = null)
    {
        IHtmlHelper proxy = DispatchProxy.Create<IHtmlHelper, TestHtmlHelperProxy>();
        ((TestHtmlHelperProxy)(object)proxy).Configure(writer ?? new StringWriter());
        return proxy;
    }

    public static IHtmlHelper<TModel> Create<TModel>(TModel model, TextWriter? writer = null)
    {
        IHtmlHelper<TModel> proxy = DispatchProxy.Create<IHtmlHelper<TModel>, TestHtmlHelperProxy>();
        ((TestHtmlHelperProxy)(object)proxy).Configure(writer ?? new StringWriter(), model);
        return proxy;
    }

    #endregion

    #region Nested type: TestHtmlHelperProxy

    public class TestHtmlHelperProxy : DispatchProxy
    {
        #region Instance fields and properties

        private DefaultHttpContext _httpContext = new();
        private readonly EmptyModelMetadataProvider _metadataProvider = new();
        private ITempDataDictionary _tempData = null!;
        private readonly ExpandoObject _viewBag = new();
        private ViewContext _viewContext = null!;
        private ViewDataDictionary _viewData = null!;

        #endregion

        #region Instance methods

        public void Configure(TextWriter writer)
        {
            _httpContext = new DefaultHttpContext();
            _viewData = new ViewDataDictionary(_metadataProvider, new ModelStateDictionary());
            _tempData = new TempDataDictionary(_httpContext, new TestTempDataProvider());
            ActionContext actionContext = new(_httpContext, new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            _viewContext = new ViewContext(actionContext, new TestView(), _viewData, _tempData, writer, new HtmlHelperOptions());
        }

        public void Configure<TModel>(TextWriter writer, TModel model)
        {
            _httpContext = new DefaultHttpContext();
            _viewData = new ViewDataDictionary<TModel>(_metadataProvider, new ModelStateDictionary())
            {
                Model = model
            };
            _tempData = new TempDataDictionary(_httpContext, new TestTempDataProvider());
            ActionContext actionContext = new(_httpContext, new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            _viewContext = new ViewContext(actionContext, new TestView(), _viewData, _tempData, writer, new HtmlHelperOptions());
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null)
            {
                return null;
            }

            return targetMethod.Name switch
            {
                "get_Html5DateRenderingMode" => Html5DateRenderingMode.Rfc3339,
                "get_IdAttributeDotReplacement" => "_",
                "get_MetadataProvider" => _metadataProvider,
                "get_ViewBag" => _viewBag,
                "get_ViewContext" => _viewContext,
                "get_ViewData" => _viewData,
                "get_TempData" => _tempData,
                "get_UrlEncoder" => UrlEncoder.Default,
                "Encode" => HtmlEncoder.Default.Encode(Convert.ToString(args?[0]) ?? string.Empty),
                "FormatValue" => Convert.ToString(args?[0]) ?? string.Empty,
                "GenerateIdFromName" => Convert.ToString(args?[0])?.Replace(".", "_", StringComparison.Ordinal) ?? string.Empty,
                "Id" => Convert.ToString(args?[0])?.Replace(".", "_", StringComparison.Ordinal) ?? string.Empty,
                "Name" => Convert.ToString(args?[0]) ?? string.Empty,
                "Raw" => new HtmlString(Convert.ToString(args?[0]) ?? string.Empty),
                "Value" => Convert.ToString(args?[0]) ?? string.Empty,
                _ => throw new NotSupportedException($"{targetMethod.Name} is not supported by the test HTML helper.")
            };
        }

        #endregion
    }

    #endregion

    #region Nested type: TestTempDataProvider

    private sealed class TestTempDataProvider : ITempDataProvider
    {
        #region Instance methods

        #region From interface ITempDataProvider

        public IDictionary<string, object> LoadTempData(HttpContext context)
        {
            return new Dictionary<string, object>();
        }

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
        }

        #endregion

        #endregion
    }

    #endregion

    #region Nested type: TestView

    private sealed class TestView : IView
    {
        #region Instance fields and properties

        #region From interface IView

        public string Path => "/UnitTest.cshtml";

        #endregion

        #endregion

        #region Instance methods

        #region From interface IView

        public Task RenderAsync(ViewContext context)
        {
            return Task.CompletedTask;
        }

        #endregion

        #endregion
    }

    #endregion
}