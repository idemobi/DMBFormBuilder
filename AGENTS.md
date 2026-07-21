# AI Rules - DMBFormBuilder

## Scope

- Applies to `DMBFormBuilder` folder and descendants.
- This project is autonomous: required rules are defined in local documentation files.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Primary consumers: Razor views in PageBuilder ecosystem applications such as `labs_idemobi_com`.
- Main dependencies: `DMBPageBuilder`, `DMBBootstrapBuilder`, `DMBComponentBuilder`, and `DMBServerWebHelper`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Module intent

- Provide reusable, fluent form and field builders for the PageBuilder ecosystem.
- Keep Razor helper discovery, model binding names, generated HTML, validation attributes, accessibility attributes, Bootstrap classes, and field behavior stable for consumers.
- Avoid mixing low-level HTML primitives, Bootstrap layout primitives, or ASP.NET middleware responsibilities into this package.
- Labs, examples, and package views must demonstrate the DMB builder stack first: `DMBPageBuilder`,
  `DMBBootstrapBuilder`, `DMBComponentBuilder`, and `DMBFormBuilder`. Raw HTML is allowed only for behavior that no
  builder currently supports; if the pattern is reusable, add or extend a builder.

## Form Actions

- Use `FormActionItemFactory` for standard form command buttons instead of hand-building JavaScript action items:
  - `FormActionItemFactory.Cancel(controller, action, title, area?, icon?)` for leaving the form through a controller/action route without posting.
  - `FormActionItemFactory.Reset(title?, icon?)` for a native `type="reset"` button that restores the form's rendered values.
  - `FormActionItemFactory.Sent(title?, icon?, lockUntilChanged?)` for a native `type="submit"` button.
- When a form uses `FormBuilder.EnableSubmitWhenChanged()`, use `FormActionItemFactory.Sent(..., lockUntilChanged: true)`
  for the submit action so the button stays inactive until a rendered field changes.
- Reset actions marked by `FormActionItemFactory.Reset()` are controlled by the shared `FormBuilder.js` change tracking:
  they are inactive while the form is unchanged, active after a change, and inactive again after reset.
- Keep these actions inside the `<form>` rendered by `FormBuilder`. Do not place form action buttons in `FooterBuilder`
  or another panel/card footer that renders outside the form.

## Key constraints

- Keep public APIs backward compatible unless a change request explicitly allows breakage.
- Prefer additive changes over structural rewrites.
- Keep generated markup deterministic and predictable.
- Treat password fields, token values, captcha input, generated JavaScript, user-provided attributes, and raw HTML output as security-sensitive areas.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.

## Documentation objective

- Documentation must be authored so it can be extracted and rendered by DocumentationBuilder.
- Publication target is `labs_idemobi_com`.
- Documentation output must serve both developers and AI assistants.
- AI prepares documentation content and structure; the developer runs DocumentationBuilder.
- XML documentation comments must be written in English.
- Public classes, public methods, public constructors, public properties, public fields, public constants, public enums, public enum values, public records, and extension methods must have useful XML documentation.

## Local rule sources

- Use [AI_CONTEXT.md](AI_CONTEXT.md) for the project summary, safe-change strategy, and builder-first UI policy.
- Use [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md) for XML HeaderDoc, README/reference documentation, and DocumentationBuilder-ready documentation.
- Use [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md) only when creating or updating example, demo, information, instruction, concept, or tutorial pages.
- Use [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md) when adding editable Draw.io diagrams to information, instruction, concept, architecture, form lifecycle, validation, example, or tutorial pages.
- Use `CodeBlockBuilder` or the local `Html.CodeBlock(...)` helper for code examples in information, instruction, concept, example, and tutorial pages.
- Use `ActionItem` with `ButtonRender` for page action links when the target publication project exposes those helpers.
- Store editable Draw.io diagrams as enriched `.drawio.svg` files under `labs_idemobi_com/wwwroot/drawio/{Area}/`.

## Localization

- Follow local [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md).
- Do not assume external localization rules unless duplicated here.

## Before delivery

- Update local docs when behavior changes.
- State untested areas explicitly.
- Do not claim build/test or DocumentationBuilder execution when they were not run.
