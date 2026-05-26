# DMBFormBuilder Project Map

## Purpose

Map the structure of `DMBFormBuilder` so AI assistants can find the right files quickly.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Publication host: `labs_idemobi_com`

## Root files

- `DMBFormBuilder.csproj`: project file and package metadata.
- `README.md`: package overview and documentation entry point.
- `AGENTS.md`: local AI instructions.
- `AI_CONTEXT.md`: project context for AI assistants.
- `DOCUMENTATION_RULES.md`: XML and reference documentation rules.
- `EXAMPLES_AND_TUTORIALS_RULES.md`: website page, example, and tutorial rules.
- `DRAWIO_DIAGRAM_RULES.md`: editable Draw.io diagram rules.
- `DELIVERY_CHECKLIST.md`: pre-delivery checklist.
- `ARCHITECTURE_DECISIONS.md`: durable architecture decisions.
- `LOCALIZATION_NOMENCLATURE.md`: localization key rules.
- `LOCAL_DEVELOPMENT_RUNBOOK.md`: local workflow guide.
- `TROUBLESHOOTING.md`: common issue guide.
- `GLOSSARY.md`: common term definitions.

## Components/Captcha

- `CaptchaBuilder.cs`: captcha field builder.
- `CaptchaBuilderExtensions.cs`: Razor helper entry points for captcha fields.

## Components/Fields

- `TextFieldBuilder.cs`: text-like input builder.
- `PasswordFieldBuilder.cs`: password input builder with toggle and strength support.
- `TextAreaFieldBuilder.cs`: textarea builder.
- `SelectFieldBuilder.cs`: select builder.
- `BooleanFieldBuilder.cs`: checkbox/switch builder.
- `EnumRadioFieldBuilder.cs`: enum radio option builder.
- `FlagFieldBuilder.cs`: enum flag field builder.
- `TokenFieldBuilder.cs`: token input builder with visibility/copy behavior.
- `TextFieldInputKind.cs`: text input kind enum.
- `*Extensions.cs`: Razor helper entry points for field builders.

## Components/Form

- `FormBuilder.cs`: form container builder.
- `FormBuilderExtensions.cs`: Razor helper entry points for forms.
- `FormLabelPresentation.cs`: label presentation enum.
- `FormSubmissionMethod.cs`: form submission method enum.
- `FormValidationMode.cs`: validation mode enum.

## Configuration

- `FormBuilderConfiguration.cs`: default form builder configuration.
- `FormBuilderConfigureOptions.cs`: static file options configuration for embedded form assets.

## Resources

- `DMBFormBuilderDataAnnotationLocalization.Designer.cs`: generated data annotation localization accessors.
- `DMBFormBuilderInternalLocalization.Designer.cs`: generated internal localization accessors.

Do not edit generated designer files manually unless the generation workflow requires it.

## Related projects

- `DMBPageBuilder`: low-level page and HTML builder package.
- `DMBBootstrapBuilder`: Bootstrap-oriented visual builder package.
- `DMBComponentBuilder`: reusable visual component package.
- `DMBServerWebHelper`: ASP.NET web services, middleware, request localization, static assets, and captcha helpers.
- `labs_idemobi_com`: publication host for examples, tutorials, information pages, and diagrams.
