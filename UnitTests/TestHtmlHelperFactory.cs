#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Dynamic;
using System.Linq.Expressions;
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
        return new TestHtmlHelper<object>(new object(), writer ?? new StringWriter());
    }

    public static IHtmlHelper<TModel> Create<TModel>(TModel model, TextWriter? writer = null)
    {
        return new TestHtmlHelper<TModel>(model, writer ?? new StringWriter());
    }

    #endregion

    #region Nested type: TestHtmlHelper

    private sealed class TestHtmlHelper<TModel> : IHtmlHelper<TModel>
    {
        #region Static fields and properties

        private static readonly IHtmlContent EmptyContent = new HtmlString(string.Empty);

        #endregion

        #region Instance fields and properties

        private readonly EmptyModelMetadataProvider _metadataProvider = new();
        private readonly ExpandoObject _viewBag = new();

        #region From interface IHtmlHelper

        public Html5DateRenderingMode Html5DateRenderingMode { get; set; } = Html5DateRenderingMode.Rfc3339;

        public string IdAttributeDotReplacement => "_";

        public IModelMetadataProvider MetadataProvider => _metadataProvider;

        public dynamic ViewBag => _viewBag;

        public ViewContext ViewContext { get; }

        ViewDataDictionary IHtmlHelper.ViewData => ViewData;

        public ITempDataDictionary TempData { get; }

        public UrlEncoder UrlEncoder => UrlEncoder.Default;

        #endregion

        #region From interface IHtmlHelper<TModel>

        public ViewDataDictionary<TModel> ViewData { get; }

        #endregion

        #endregion

        #region Instance constructors and destructors

        public TestHtmlHelper(TModel model, TextWriter writer)
        {
            DefaultHttpContext httpContext = new();
            ViewData = new ViewDataDictionary<TModel>(_metadataProvider, new ModelStateDictionary())
            {
                Model = model
            };
            TempData = new TempDataDictionary(httpContext, new TestTempDataProvider());
            ActionContext actionContext = new(httpContext, new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
            ViewContext = new ViewContext(actionContext, new TestView(), ViewData, TempData, writer, new HtmlHelperOptions());
        }

        #endregion

        #region Instance methods

        #region From interface IHtmlHelper

        public void Contextualize(ViewContext viewContext)
        {
        }

        public IHtmlContent ActionLink(
            string linkText,
            string actionName,
            string controllerName,
            string protocol,
            string hostname,
            string fragment,
            object routeValues,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent AntiForgeryToken()
        {
            return EmptyContent;
        }

        public MvcForm BeginForm(
            string actionName,
            string controllerName,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes
        )
        {
            throw new NotSupportedException("Forms are not supported by the test HTML helper.");
        }

        public MvcForm BeginRouteForm(
            string routeName,
            object routeValues,
            FormMethod method,
            bool? antiforgery,
            object htmlAttributes
        )
        {
            throw new NotSupportedException("Forms are not supported by the test HTML helper.");
        }

        public IHtmlContent CheckBox(string expression, bool? isChecked, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent Display(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            return EmptyContent;
        }

        public string DisplayName(string expression)
        {
            return expression;
        }

        public string DisplayText(string expression)
        {
            return Value(expression, null);
        }

        public IHtmlContent DropDownList(string expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent Editor(string expression, string templateName, string htmlFieldName, object additionalViewData)
        {
            return EmptyContent;
        }

        public string Encode(object value)
        {
            return HtmlEncoder.Default.Encode(Convert.ToString(value) ?? string.Empty);
        }

        public string Encode(string value)
        {
            return HtmlEncoder.Default.Encode(value ?? string.Empty);
        }

        public void EndForm()
        {
        }

        public string FormatValue(object value, string format)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(format)
                ? Convert.ToString(value) ?? string.Empty
                : string.Format(format, value);
        }

        public string GenerateIdFromName(string fullName)
        {
            return (fullName ?? string.Empty).Replace(".", IdAttributeDotReplacement, StringComparison.Ordinal);
        }

        public IEnumerable<SelectListItem> GetEnumSelectList<TEnum>() where TEnum : struct
        {
            return Enumerable.Empty<SelectListItem>();
        }

        public IEnumerable<SelectListItem> GetEnumSelectList(Type enumType)
        {
            return Enumerable.Empty<SelectListItem>();
        }

        public IHtmlContent Hidden(string expression, object value, object htmlAttributes)
        {
            return EmptyContent;
        }

        public string Id(string expression)
        {
            return GenerateIdFromName(expression);
        }

        public IHtmlContent Label(string expression, string labelText, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent ListBox(string expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return EmptyContent;
        }

        public string Name(string expression)
        {
            return expression ?? string.Empty;
        }

        public Task<IHtmlContent> PartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            return Task.FromResult(EmptyContent);
        }

        public IHtmlContent Password(string expression, object value, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent RadioButton(string expression, object value, bool? isChecked, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent Raw(string value)
        {
            return new HtmlString(value ?? string.Empty);
        }

        public IHtmlContent Raw(object value)
        {
            return new HtmlString(Convert.ToString(value) ?? string.Empty);
        }

        public Task RenderPartialAsync(string partialViewName, object model, ViewDataDictionary viewData)
        {
            return Task.CompletedTask;
        }

        public IHtmlContent RouteLink(
            string linkText,
            string routeName,
            string protocol,
            string hostName,
            string fragment,
            object routeValues,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent TextArea(string expression, string value, int rows, int columns, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent TextBox(string expression, object value, string format, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent ValidationMessage(string expression, string message, object htmlAttributes, string tag)
        {
            return EmptyContent;
        }

        public IHtmlContent ValidationSummary(bool excludePropertyErrors, string message, object htmlAttributes, string tag)
        {
            return EmptyContent;
        }

        public string Value(string expression, string format)
        {
            object? value = ResolveModelValue(expression);
            return FormatValue(value ?? string.Empty, format);
        }

        #endregion

        #region From interface IHtmlHelper<TModel>

        public IHtmlContent CheckBoxFor(Expression<Func<TModel, bool>> expression, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent DisplayFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            string templateName,
            string htmlFieldName,
            object additionalViewData
        )
        {
            return EmptyContent;
        }

        public string DisplayNameFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            return GetExpressionName(expression);
        }

        public string DisplayNameForInnerType<TModelItem, TResult>(Expression<Func<TModelItem, TResult>> expression)
        {
            return GetExpressionName(expression);
        }

        public string DisplayTextFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            return ValueFor(expression, null);
        }

        public IHtmlContent DropDownListFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent EditorFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            string templateName,
            string htmlFieldName,
            object additionalViewData
        )
        {
            return EmptyContent;
        }

        public IHtmlContent HiddenFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes)
        {
            return EmptyContent;
        }

        public string IdFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            return GenerateIdFromName(NameFor(expression));
        }

        public IHtmlContent LabelFor<TResult>(Expression<Func<TModel, TResult>> expression, string labelText, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent ListBoxFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            IEnumerable<SelectListItem> selectList,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public string NameFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            return GetExpressionName(expression);
        }

        public IHtmlContent PasswordFor<TResult>(Expression<Func<TModel, TResult>> expression, object htmlAttributes)
        {
            return EmptyContent;
        }

        public IHtmlContent RadioButtonFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            object value,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent TextAreaFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            int rows,
            int columns,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent TextBoxFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            string format,
            object htmlAttributes
        )
        {
            return EmptyContent;
        }

        public IHtmlContent ValidationMessageFor<TResult>(
            Expression<Func<TModel, TResult>> expression,
            string message,
            object htmlAttributes,
            string tag
        )
        {
            return EmptyContent;
        }

        public string ValueFor<TResult>(Expression<Func<TModel, TResult>> expression, string format)
        {
            TResult value = expression.Compile().Invoke(ViewData.Model);
            return FormatValue((object)value ?? string.Empty, format);
        }

        #endregion

        private static string GetExpressionName(LambdaExpression expression)
        {
            Expression body = expression.Body is UnaryExpression unaryExpression
                ? unaryExpression.Operand
                : expression.Body;

            return body is MemberExpression memberExpression
                ? memberExpression.Member.Name
                : string.Empty;
        }

        private object? ResolveModelValue(string expression)
        {
            if (ViewData.Model == null || string.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            return typeof(TModel).GetProperty(expression)?.GetValue(ViewData.Model);
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
