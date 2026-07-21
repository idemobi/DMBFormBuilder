# DMBFormBuilder AI Context

## Purpose

This file gives AI assistants the minimum project context required to work safely in `DMBFormBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Main dependencies: `DMBPageBuilder`, `DMBBootstrapBuilder`, `DMBComponentBuilder`, and `DMBServerWebHelper`.
- Publication host: `labs_idemobi_com`
- Primary documentation audience: developers building forms in Razor views.

## What this project is

`DMBFormBuilder` is a form and field builder package.

It provides:

- fluent form composition,
- model-bound Razor helper extension methods,
- Bootstrap-oriented field rendering,
- validation attribute rendering,
- Boolean validation attributes for FormBuilder checkbox and switch helpers,
- label presentation control,
- field variants and accessibility markup,
- captcha field rendering,
- form-specific localization resources.

## What this project is not

This project is not:

- a low-level HTML builder package,
- a general Bootstrap layout package,
- an ASP.NET middleware package,
- a server configuration package,
- a documentation website.

## Main concepts

- `FormBuilder` renders the `<form>` element and controls validation, method, action, multipart behavior, and required legend output.
- Field builders render Bootstrap-compatible form controls and expose fluent methods for labels, values, validation constraints, disabled state, variants, and custom attributes.
- Razor helper extension methods create builders directly from `IHtmlHelper` or model expressions.
- `FormBuilderConfiguration` provides default label presentation and validation behavior.
- Captcha rendering depends on `DMBServerWebHelper`.
- `FormActionItemFactory` provides the standard form command actions:
  - `Cancel(controller, action, title, area?, icon?)` creates a route action for leaving a form without submitting it.
  - `Reset(title?, icon?)` creates a native reset button for restoring rendered field values.
  - `Sent(title?, icon?, lockUntilChanged?)` creates a native submit button.
- `FormBuilder.EnableSubmitWhenChanged()` tracks rendered form values. Use `FormActionItemFactory.Sent(..., lockUntilChanged: true)` when the submit button must remain inactive until the user changes the form.
- `FormActionItemFactory.Reset()` participates in the same change tracking: reset is inactive while unchanged, active after a change, and inactive again after reset.
- Form action buttons must remain inside the `<form>` output. Avoid `FooterBuilder` or panel/card footer builders inside a form when they render the footer outside the form element.

## Builder-first UI policy

- FormBuilder Labs pages and examples must use the DMB builder stack for layout, titles, actions, tables, forms, fields,
  alerts, separators, and code samples whenever a builder exists.
- Do not use raw `<input>`, `<button>`, Bootstrap utility markup, or local CSS in examples just because it is faster.
- Raw markup is acceptable only when the example is explicitly about unsupported browser/model-binding behavior. Keep it
  minimal, and prefer adding or extending a builder when the behavior is reusable.

## Change strategy

- Keep changes localized to the relevant builder or extension family.
- Preserve public API names and behavior unless the request explicitly asks for a breaking change.
- Document public API behavior in XML comments when the code is touched.
- Update README and local rule files when project behavior or documentation strategy changes.

## Documentation strategy

- Use `DOCUMENTATION_RULES.md` for XML docs, README/reference docs, and DocumentationBuilder-ready documentation.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only for pages, examples, tutorials, and walkthroughs.
- Use `DRAWIO_DIAGRAM_RULES.md` when diagrams clarify form lifecycle, field rendering, validation flow, model binding, captcha flow, or accessibility behavior.
- Keep all generated documentation in English unless the user explicitly requests another language for user-facing website content.
