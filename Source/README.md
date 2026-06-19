# DMBFormBuilder

## Purpose

`DMBFormBuilder` provides Bootstrap-oriented form and field builders for the PageBuilder ecosystem.

It centralizes reusable Razor helpers and fluent builders for forms, text inputs, password inputs, text areas, selects, boolean fields, enum radio lists, flags, tokens, country selectors, special HTML input types, and captcha fields.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Primary consumers: Razor views in PageBuilder ecosystem applications such as `labs_idemobi_com`.
- Main dependencies: `DMBPageBuilder`, `DMBBootstrapBuilder`, `DMBComponentBuilder`, and `DMBServerWebHelper`.
- Publication host: `labs_idemobi_com`

## Scope

This package includes:

- form composition through `FormBuilder`,
- typed field builders for common HTML inputs,
- Razor helper extension methods for model-bound fields,
- Bootstrap-oriented validation and accessibility markup,
- label presentation and validation mode configuration,
- captcha field rendering backed by `DMBServerWebHelper.CaptchaFactory`,
- embedded form-related static assets,
- localization resources for field labels, prompts, descriptions, validation text, and captcha messages.

This package does not define low-level HTML primitives, Bootstrap layout primitives, or server middleware. Those responsibilities belong to related packages.

## Main entry points

- `FormBuilder`
- `TextFieldBuilder`
- `PasswordFieldBuilder`
- `TextAreaFieldBuilder`
- `SelectFieldBuilder`
- `BooleanFieldBuilder`
- `BoolMustBeTrueAttribute`
- `BoolMustBeFalseAttribute`
- `EnumRadioFieldBuilder`
- `FlagFieldBuilder`
- `TokenFieldBuilder`
- `CaptchaBuilder`
- `FormBuilderConfiguration`
- Razor helper extension classes in `Components/Fields`, `Components/Form`, and `Components/Captcha`

## Documentation strategy

Documentation must be written so it can be consumed by developers and AI assistants without private chat context.

Use the local rule files:

- [AGENTS.md](AGENTS.md)
- [AI_CONTEXT.md](AI_CONTEXT.md)
- [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md)
- [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md)
- [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md)
- [PROJECT_MAP.md](PROJECT_MAP.md)
- [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md)
- [DELIVERY_CHECKLIST.md](DELIVERY_CHECKLIST.md)

Documentation pages, examples, tutorials, and diagrams are published through `labs_idemobi_com` when applicable.

## Development constraints

- Keep public APIs backward compatible unless explicitly requested.
- Keep rendered form markup, validation attributes, accessibility attributes, and Razor helper behavior deterministic.
- Document security-sensitive behavior such as password fields, token values, generated JavaScript, captcha input, and user-provided attributes.
- `TokenFieldBuilder` must register `FormBuilder.css` and `FormBuilder.js` when rendered because its copy and visibility buttons are wired by the shared FormBuilder client script.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
