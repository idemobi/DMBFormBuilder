# DMBFormBuilder Examples and Tutorials Rules

## Objective

Define how information pages, instruction pages, concept pages, example pages, demo pages, component pages, and tutorials are created for `DMBFormBuilder`.

These rules apply only when the task explicitly creates or updates example, demo, information, instruction, concept, or tutorial pages.

Do not use this file as the rule source for XML API documentation or reference documentation. Use `DOCUMENTATION_RULES.md` for that work.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Default documentation area: `FormBuilder`
- Publication target: `../labs_idemobi_com`
- Shared UI stack: `DMBBootstrapBuilder`, `DMBComponentBuilder`, `DMBFormBuilder`, and `DMBPageBuilder`
- Default DocumentationViewer package id for this project: `DMBFormBuilder`
- Default DocumentationViewer namespace for this project: `DMBFormBuilder`
- Expected component page subjects: `FormBuilder`, `TextFieldBuilder`, `PasswordFieldBuilder`, `TextAreaFieldBuilder`, `SelectFieldBuilder`, `BooleanFieldBuilder`, `EnumRadioFieldBuilder`, `FlagFieldBuilder`, `TokenFieldBuilder`, and `CaptchaBuilder`.

## Publication target

Examples and tutorials must be written in `../labs_idemobi_com`.

Use the existing MVC conventions in that project:

- controller actions in `labs_idemobi_com/Controllers`,
- full pages in `labs_idemobi_com/Views/{FeatureOrComponent}/`,
- reusable example partials in `labs_idemobi_com/Views/Shared/Examples/`,
- generated raw-code mirrors in `labs_idemobi_com/Views/Shared/Examples_Raw/`.

AI may create or update source example partials under `Views/Shared/Examples/`. The developer or prebuild step is responsible for regenerating `Views/Shared/Examples_Raw/` when required.

## Shared UI stack

Example, information, concept, and tutorial pages must use the existing PageBuilder ecosystem components instead of ad-hoc layout markup when a suitable component exists.

Prefer:

- `DMBBootstrapBuilder` for layout, titles, cards, rows, columns, alerts, badges, buttons, tables, tabs, and Bootstrap-oriented UI.
- `DMBComponentBuilder` for reusable non-form visual components available in the project.
- `DMBFormBuilder` for forms, fields, validation examples, and form-state demonstrations.
- `DMBPageBuilder` for page metadata, raw HTML builders, render lifecycle concepts, and low-level HTML composition.

Do not introduce a new frontend framework or independent demo system for examples.

## Page categories

There are two distinct page formats:

- general information pages,
- component pages.

Do not merge the two formats unless the user explicitly asks for a hybrid page.

## General information page format

Use this format for package introductions, architecture pages, form lifecycle pages, validation guides, getting started pages, conceptual guides, and tutorials that are not focused on one component class.

Required structure:

1. Title.
2. Short context paragraph explaining the topic and audience.
3. Explanation sections with deterministic headings.
4. Practical integration or usage section when relevant.
5. Notes, constraints, or next steps.
6. Links to related documentation pages or API reference when useful.

General information pages:

- may include short code snippets rendered with `CodeBlockBuilder`,
- may include diagrams or structured lists when they clarify architecture, instructions, form lifecycle, validation behavior, or concepts,
- should not include component galleries unless the page is explicitly an overview gallery,
- should avoid long inline API listings that belong in DocumentationViewer.

## Draw.io diagrams on information and tutorial pages

Information pages, instruction pages, concept pages, architecture pages, form lifecycle pages, validation pages, and tutorials may include Draw.io diagrams when a visual model clarifies the explanation.

Draw.io diagrams must follow `DRAWIO_DIAGRAM_RULES.md`.

Do not use Draw.io diagrams as decoration. Add a diagram only when it explains a real form rendering flow, field builder lifecycle, validation flow, model binding flow, captcha flow, or accessibility concern.

## Code examples on information pages

Code examples on general information pages must use `CodeBlockBuilder` or the existing `Html.CodeBlock(...)` helper when available.

Do not write raw `<pre><code>` blocks by hand when `CodeBlockBuilder` can render the example.

## Links and actions on information pages

Links that behave like page actions must be generated through `ActionItem` implementations and rendered with `ButtonRender` when possible.

Use plain inline anchors only for natural text links inside paragraphs where a button/action would be visually inappropriate.

## Component page format

Use this format for a page dedicated to one form builder class or one tightly scoped field family.

Every component page must follow the same high-level order:

1. Title.
2. Explanation of the component.
3. DocumentationViewer button linking to the relevant API documentation.
4. Gallery of examples rendered with `@await Html.RenderExamplePartialAsync(...)`.
5. Showcase list.

### DocumentationViewer button

Every component page must include a visible button linking to the generated API documentation.

Use the existing DocumentationViewer route through the `Documentation` controller:

```csharp
@Url.Action("Show", "Documentation", new
{
    groupName = "NuGet",
    packageId = "DMBFormBuilder",
    version = "0.9",
    namespaceName = "DMBFormBuilder",
    objectName = "TextFieldBuilder"
})
```

Adjust `packageId`, `namespaceName`, and `objectName` to the component being documented.

Prefer an `ActionItem` implementation rendered through `ButtonRender` instead of a handwritten `<a class="btn ...">` element.

### Example gallery

The gallery must render example partials through:

```csharp
@await Html.RenderExamplePartialAsync("Examples/{ComponentClassName}/{ExamplePartialName}", "Readable example title", model: Model)
```

The source partials must live under:

```text
labs_idemobi_com/Views/Shared/Examples/{ComponentClassName}/{ExamplePartialName}.cshtml
```

Use the component class name as the folder name for new component pages.

## Minimum component example coverage

Every component page must include at least:

- normal state,
- empty state,
- error or invalid state,
- one realistic data example.

Add more examples when the component has important validation modes, accessibility modes, layout modes, form behavior, JavaScript behavior, or sensitive-value behavior.

## Delivery checklist for examples

Before finishing an example or tutorial task, verify:

- the page is under `labs_idemobi_com`,
- the page uses existing BootstrapBuilder, ComponentBuilder, FormBuilder, or PageBuilder components where appropriate,
- code examples use `CodeBlockBuilder` or `Html.CodeBlock(...)`,
- action links use `ActionItem` with `ButtonRender` when appropriate,
- Draw.io diagrams follow `DRAWIO_DIAGRAM_RULES.md`,
- DocumentationViewer links target `DMBFormBuilder`,
- no build/test command was run unless the user explicitly requested it.
