#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBFormBuilderLabs.Models;
using DMBPageBuilder;
using DMBServerHelper;
using DMBServerWebHelper;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DMBFormBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides documentation and demonstration pages for FormBuilder.
    /// </summary>
    public class FormBuilderController : RawBootstrapController
    {
        #region Instance methods

        public IActionResult Architecture()
        {
            SetTitle("FormBuilder - Architecture");
            SetDescription("FormBuilder architecture");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Architecture", "Forms", "ASP.NET Core");
            return View();
        }

        [HttpGet]
        public IActionResult BooleanFields()
        {
            SetTitle("FormBuilder - Boolean fields");
            SetDescription("FormBuilder checkbox and switch components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "CheckboxField", "SwitchField", "Forms");
            return View(FormBuilderBooleanExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BooleanFields(FormBuilderBooleanExampleModel model)
        {
            SetTitle("FormBuilder - Boolean fields");
            SetDescription("FormBuilder checkbox and switch components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "CheckboxField", "SwitchField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Boolean model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Captcha()
        {
            SetTitle("FormBuilder - Captcha");
            SetDescription("FormBuilder captcha component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Captcha", "Forms");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Captcha(string captchaValue)
        {
            SetTitle("FormBuilder - Captcha");
            SetDescription("FormBuilder captcha component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Captcha", "Forms");

            if (CaptchaFactory.TestCaptcha(HttpContext, captchaValue))
            {
                Page.AddSuccess(
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Valid_Title"),
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Valid_Message"));
            }
            else
            {
                Page.AddWarning(
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Invalid_Title"),
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Invalid_Message"));
            }

            return View();
        }

        [HttpGet]
        public IActionResult ColorInputFields()
        {
            SetTitle("FormBuilder - Color input fields");
            SetDescription("FormBuilder color input component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "ColorField", "Forms");
            return View(FormBuilderColorInputExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ColorInputFields(FormBuilderColorInputExampleModel model)
        {
            SetTitle("FormBuilder - Color input fields");
            SetDescription("FormBuilder color input component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "ColorField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Color input model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        private IActionResult CompleteExamplePost(FormBuilderCompleteExampleKind kind, FormBuilderCompleteExampleModel form)
        {
            FormBuilderCompleteExamplePageModel pageModel = PrepareCompleteExamplePage(kind, form);
            bool captchaValid = CaptchaFactory.TestCaptcha(HttpContext, form.CaptchaValue);

            if (!form.AcceptTerms)
            {
                ModelState.AddModelError(nameof(FormBuilderCompleteExampleModel.AcceptTerms), WebLocalizer.GetDataAnnotation("FormBuilder_Field_BoolMustBeTrue"));
            }

            if (!captchaValid)
            {
                ModelState.AddModelError(nameof(FormBuilderCompleteExampleModel.CaptchaValue), WebLocalizer.GetDataAnnotation("FormBuilder_Captcha_Invalid"));
                Page.AddWarning(
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Invalid_Title"),
                    WebLocalizer.GetInternal("FormBuilder_Captcha_Invalid_Message"));
            }

            if (ModelState.IsValid && captchaValid)
            {
                pageModel.ShowSummary = true;
                Page.AddSuccess("Example model valid", "The complete example form was submitted successfully.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View("Examples", pageModel);
        }

        [HttpGet]
        public IActionResult CountryFields()
        {
            SetTitle("FormBuilder - Country fields");
            SetDescription("FormBuilder country selector component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "CountryField", "Forms");
            return View(FormBuilderCountryExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CountryFields(FormBuilderCountryExampleModel model)
        {
            SetTitle("FormBuilder - Country fields");
            SetDescription("FormBuilder country selector component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "CountryField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Country model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        public IActionResult CSHtml()
        {
            SetTitle("FormBuilder - CSHtml");
            SetDescription("FormBuilder Razor usage");
            SetKeywords("FormBuilder", "DMBFormBuilder", "CSHtml", "Razor");
            return View();
        }

        [HttpGet]
        public IActionResult DateInputFields()
        {
            SetTitle("FormBuilder - Date input fields");
            SetDescription("FormBuilder date input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "DateField", "Forms");
            return View(FormBuilderDateInputExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DateInputFields(FormBuilderDateInputExampleModel model)
        {
            SetTitle("FormBuilder - Date input fields");
            SetDescription("FormBuilder date input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "DateField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Date input model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult DateTimeInputFields()
        {
            SetTitle("FormBuilder - DateTime input fields");
            SetDescription("FormBuilder datetime-local input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "DateTimeField", "Forms");
            return View(FormBuilderDateTimeInputExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DateTimeInputFields(FormBuilderDateTimeInputExampleModel model)
        {
            SetTitle("FormBuilder - DateTime input fields");
            SetDescription("FormBuilder datetime-local input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "DateTimeField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("DateTime input model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EmailFields()
        {
            SetTitle("FormBuilder - Email fields");
            SetDescription("FormBuilder email field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "EmailField", "Forms");
            return View(FormBuilderEmailFieldExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmailFields(FormBuilderEmailFieldExampleModel model)
        {
            SetTitle("FormBuilder - Email fields");
            SetDescription("FormBuilder email field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "EmailField", "Forms");

            if (ModelState.IsValid)
            {
                Page.AddSuccess("Email field model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EnumRadioFields()
        {
            SetTitle("FormBuilder - Enum radio fields");
            SetDescription("FormBuilder enum radio component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "EnumRadioField", "Forms");
            return View(FormBuilderEnumRadioExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnumRadioFields(FormBuilderEnumRadioExampleModel model)
        {
            SetTitle("FormBuilder - Enum radio fields");
            SetDescription("FormBuilder enum radio component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "EnumRadioField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Enum radio model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Examples()
        {
            return RedirectToAction(nameof(ExamplesNormal));
        }

        [HttpGet]
        public IActionResult ExamplesFloating()
        {
            return View("Examples", PrepareCompleteExamplePage(FormBuilderCompleteExampleKind.Floating));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamplesFloating(FormBuilderCompleteExampleModel form)
        {
            return CompleteExamplePost(FormBuilderCompleteExampleKind.Floating, form);
        }

        [HttpGet]
        public IActionResult ExamplesGrouped()
        {
            return View("Examples", PrepareCompleteExamplePage(FormBuilderCompleteExampleKind.Grouped));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamplesGrouped(FormBuilderCompleteExampleModel form)
        {
            return CompleteExamplePost(FormBuilderCompleteExampleKind.Grouped, form);
        }

        [HttpGet]
        public IActionResult ExamplesInline()
        {
            return View("Examples", PrepareCompleteExamplePage(FormBuilderCompleteExampleKind.Inline));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamplesInline(FormBuilderCompleteExampleModel form)
        {
            return CompleteExamplePost(FormBuilderCompleteExampleKind.Inline, form);
        }

        [HttpGet]
        public IActionResult ExamplesLocked()
        {
            return View("Examples", PrepareCompleteExamplePage(FormBuilderCompleteExampleKind.Locked));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamplesLocked(FormBuilderCompleteExampleModel form)
        {
            return CompleteExamplePost(FormBuilderCompleteExampleKind.Locked, form);
        }

        [HttpGet]
        public IActionResult ExamplesNormal()
        {
            return View("Examples", PrepareCompleteExamplePage(FormBuilderCompleteExampleKind.Normal));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ExamplesNormal(FormBuilderCompleteExampleModel form)
        {
            return CompleteExamplePost(FormBuilderCompleteExampleKind.Normal, form);
        }

        [HttpGet]
        public IActionResult FlagFields()
        {
            SetTitle("FormBuilder - Flag fields");
            SetDescription("FormBuilder flag enum components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "FlagField", "Forms");
            return View(FormBuilderFlagExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FlagFields(FormBuilderFlagExampleModel model)
        {
            SetTitle("FormBuilder - Flag fields");
            SetDescription("FormBuilder flag enum components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "FlagField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Flag model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        public IActionResult GettingStarted()
        {
            SetTitle("FormBuilder - Getting Started");
            SetDescription("FormBuilder getting started guide");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Getting Started", "Forms", "ASP.NET Core");
            return View();
        }

        public IActionResult Introduction()
        {
            SetTitle("FormBuilder - Introduction");
            SetDescription("FormBuilder");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Forms", "ASP.NET Core");
            return View();
        }

        [HttpGet]
        public IActionResult PasswordFields()
        {
            SetTitle("FormBuilder - Password fields");
            SetDescription("FormBuilder password field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "PasswordField", "Forms");
            return View(FormBuilderPasswordFieldExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasswordFields(FormBuilderPasswordFieldExampleModel model)
        {
            SetTitle("FormBuilder - Password fields");
            SetDescription("FormBuilder password field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "PasswordField", "Forms");

            if (ModelState.IsValid)
            {
                Page.AddSuccess("Password field model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        private FormBuilderCompleteExamplePageModel PrepareCompleteExamplePage(FormBuilderCompleteExampleKind kind, FormBuilderCompleteExampleModel? form = null)
        {
            SetTitle("FormBuilder - Examples");
            SetDescription("Complete FormBuilder examples");
            SetKeywords("FormBuilder", "DMBFormBuilder", "Examples", "Forms");
            return new FormBuilderCompleteExamplePageModel
            {
                Kind = kind,
                Form = form ?? FormBuilderCompleteExampleModel.CreateDemo()
            };
        }

        [HttpGet]
        public IActionResult SelectFields()
        {
            SetTitle("FormBuilder - Select fields");
            SetDescription("FormBuilder select and enum components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "SelectField", "Enum", "Forms");
            return View(FormBuilderSelectExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectFields(FormBuilderSelectExampleModel model)
        {
            SetTitle("FormBuilder - Select fields");
            SetDescription("FormBuilder select and enum components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "SelectField", "Enum", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Select model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SliderInputFields()
        {
            SetTitle("FormBuilder - Slider input fields");
            SetDescription("FormBuilder slider input component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "SliderField", "Forms");
            return View(FormBuilderSliderInputExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SliderInputFields(FormBuilderSliderInputExampleModel model)
        {
            SetTitle("FormBuilder - Slider input fields");
            SetDescription("FormBuilder slider input component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "SliderField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Slider input model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SpecialInputFields()
        {
            return RedirectToAction(nameof(DateInputFields));
        }

        [HttpGet]
        public IActionResult TextAreaFields()
        {
            SetTitle("FormBuilder - Textarea fields");
            SetDescription("FormBuilder textarea field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TextAreaField", "Forms");
            return View(FormBuilderTextAreaExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TextAreaFields(FormBuilderTextAreaExampleModel model)
        {
            SetTitle("FormBuilder - Textarea fields");
            SetDescription("FormBuilder textarea field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TextAreaField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Textarea model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult TextFields()
        {
            SetTitle("FormBuilder - Text fields");
            SetDescription("FormBuilder text field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TextField", "Forms");
            return View(FormBuilderTextFieldExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TextFields(FormBuilderTextFieldExampleModel model)
        {
            SetTitle("FormBuilder - Text fields");
            SetDescription("FormBuilder text field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TextField", "Forms");

            if (ModelState.IsValid)
            {
                Page.AddSuccess("Text field model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult TimeInputFields()
        {
            SetTitle("FormBuilder - Time input fields");
            SetDescription("FormBuilder time and month input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TimeField", "MonthField", "Forms");
            return View(FormBuilderTimeInputExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TimeInputFields(FormBuilderTimeInputExampleModel model)
        {
            SetTitle("FormBuilder - Time input fields");
            SetDescription("FormBuilder time and month input components");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TimeField", "MonthField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Time input model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult TokenFields()
        {
            SetTitle("FormBuilder - Token fields");
            SetDescription("FormBuilder token field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TokenField", "Forms");
            return View(FormBuilderTokenExampleModel.CreateDemo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TokenFields(FormBuilderTokenExampleModel model)
        {
            SetTitle("FormBuilder - Token fields");
            SetDescription("FormBuilder token field component");
            SetKeywords("FormBuilder", "DMBFormBuilder", "TokenField", "Forms");
            if (ModelState.IsValid)
            {
                Page.AddSuccess("Token model valid", "The sample form passed model validation.");
            }
            else
            {
                Page.AddInvalidModelAlert(ModelState);
            }

            return View(model);
        }

        #endregion
    }
}