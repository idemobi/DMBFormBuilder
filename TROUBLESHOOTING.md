# DMBFormBuilder Troubleshooting

## Purpose

Collect common issues and investigation paths for `DMBFormBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Publication host: `labs_idemobi_com`

## Field value does not bind to the model

Check:

- the helper method uses the correct model expression,
- the input name and ID were not overwritten incorrectly,
- repeated calls to `SetInput(...)` use the expected value,
- custom attributes do not conflict with ASP.NET Core model binding.

## Validation does not run

Check:

- the form validation mode,
- generated `required`, `maxlength`, `minlength`, `pattern`, comparison, Boolean, or custom validation attributes,
- `BoolMustBeTrueAttribute` and `BoolMustBeFalseAttribute` are applied through `CheckboxFieldBuilderFor(...)` or `SwitchFieldBuilderFor(...)`,
- whether browser validation was disabled,
- whether server-side model validation is configured in the consuming application.

## Label or accessibility behavior is wrong

Check:

- label presentation settings,
- hidden-label settings,
- label-left settings for boolean fields,
- `aria-label`, `for`, and `id` relationships,
- custom label icons or custom input IDs.

## Captcha field does not refresh or validate

Check:

- the refresh URL points to a server endpoint that returns a captcha image,
- `DMBServerWebHelper` session and captcha generation are configured,
- the captcha input name matches server-side validation expectations,
- required validation is enabled when the captcha must be submitted.

## Password or token field exposes sensitive values

Check:

- visibility toggle behavior,
- copy button behavior,
- rendered `value` attributes,
- browser autocomplete expectations,
- whether examples accidentally include real secrets.

## Documentation page issues

When pages in `labs_idemobi_com` are wrong or inconsistent:

- read `EXAMPLES_AND_TUTORIALS_RULES.md`,
- use `CodeBlockBuilder` or `Html.CodeBlock(...)` for code examples,
- use `ActionItem` with `ButtonRender` for action links,
- use `DRAWIO_DIAGRAM_RULES.md` for editable diagrams,
- keep DocumentationViewer links targeting `DMBFormBuilder`.
