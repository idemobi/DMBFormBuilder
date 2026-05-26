# DMBFormBuilder Documentation Rules

## Language

- Documentation must be written in English.
- XML documentation comments must be written in English.

## Target audience

- Primary: developers maintaining or integrating `DMBFormBuilder`.
- Secondary: developers building Razor forms with the PageBuilder ecosystem.
- Tertiary: AI assistants consuming structured project rules and technical context.

Documentation must be useful without private chat context. A reader should understand what each builder renders, which Bootstrap structures and validation attributes it produces, how Razor helper methods derive model-bound field metadata, and what constraints apply before reading the implementation.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Primary API families: form builders, field builders, captcha builders, Razor helper extensions, form enums, form configuration, localization resources, and embedded static assets.
- Important types to reference when relevant: `FormBuilder`, `TextFieldBuilder`, `PasswordFieldBuilder`, `TextAreaFieldBuilder`, `SelectFieldBuilder`, `BooleanFieldBuilder`, `EnumRadioFieldBuilder`, `FlagFieldBuilder`, `TokenFieldBuilder`, `CaptchaBuilder`, `FormBuilderConfiguration`, `FormLabelPresentation`, `FormValidationMode`, `FormSubmissionMethod`, and `TextFieldInputKind`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Strict C# XML documentation policy

- Always write XML HeaderDoc for:
  - public classes,
  - public interfaces,
  - public structs,
  - public records,
  - public methods,
  - public constructors,
  - public properties,
  - public fields,
  - public constants,
  - public events,
  - public delegates,
  - public enums,
  - public enum values,
  - public extension methods.
- Also write XML HeaderDoc for protected members when they are part of the builder inheritance contract or are expected to be overridden by derived builders.
- Internal and private members do not require XML HeaderDoc unless they explain complex rendering, validation, localization, or security behavior that would otherwise be difficult to maintain.
- XML documentation must use valid C# XML syntax.
- Prefer these tags:
  - `<summary>`
  - `<param>`
  - `<typeparam>`
  - `<returns>`
  - `<value>`
  - `<remarks>`
  - `<exception>`
  - `<see cref="..."/>`
  - `<seealso cref="..."/>`
- Use `<inheritdoc/>` only when the inherited documentation is accurate for the current member.

## XML documentation quality standard

XML documentation must explain the public contract, not repeat the member name.

For classes and interfaces, document:

- the builder's role in form composition,
- the rendered HTML element or Bootstrap form artifact,
- the relationship with related builders and Razor helper extensions,
- lifecycle expectations, including whether the type is used directly, through Razor helpers, or by model-bound extension methods.

For methods and constructors, document:

- what the member changes in generated form output, validation attributes, field state, or accessibility markup,
- every parameter and expected format when relevant,
- the returned fluent builder instance when the method supports chaining,
- side effects such as adding attributes, changing validation behavior, changing input type, registering captcha refresh behavior, or toggling label visibility,
- validation rules and exceptions,
- whether `null`, empty strings, duplicate attributes, repeated calls, or invalid numeric values have special behavior.

For properties, fields, and constants, document:

- the meaning of the value,
- the default value when meaningful,
- whether consumers may set it directly,
- how it affects rendering, validation, model binding, localization, accessibility, or static assets.

For enums and enum values, document:

- where the enum is used,
- how each value maps to rendered HTML, HTTP submission method, validation strategy, label presentation, input type, or fallback behavior.

For extension methods, document:

- the receiver type,
- the builder returned,
- the intended Razor usage pattern,
- how model expressions are used to derive input names, IDs, values, labels, prompts, and validation metadata.

## Project API documentation requirements

- Form builders must identify the rendered form element, method/action behavior, multipart behavior, browser validation behavior, and required legend behavior.
- Field builders must identify the rendered input, textarea, select, checkbox, radio list, token, or captcha structure.
- Fluent builder methods must state that they return the same builder type for chaining.
- Validation APIs must document generated validation attributes and how custom messages are used.
- Attribute helpers must describe whether they set, replace, remove, encode, or serialize an attribute.
- Label APIs must document label text, icon behavior, hidden-label behavior, label placement, and accessibility implications.
- Model-bound helper methods must document how expression metadata is applied.
- Captcha APIs must document the dependency on server-side captcha generation and refresh URL behavior.
- Security-sensitive APIs must mention password visibility toggles, token copying, raw values, generated JavaScript, HTML attributes, and user-provided values when relevant.

## Examples in XML documentation

Use `<example>` when it materially improves understanding of:

- Razor helper entry points,
- non-obvious fluent chains,
- model-bound field creation,
- validation constraints,
- captcha rendering,
- password strength/toggle behavior,
- token copy/toggle behavior.

Examples must be short, realistic, and compile-oriented. Prefer Razor examples for Razor helpers and C# examples for lower-level builder APIs.

## Markdown documentation policy

- Follow PageBuilder markdown conventions in:
  - `../MARKDOWN_GUIDELINES.md`
- Keep this structure where applicable:
  1. Context
  2. Explanation
  3. Example
  4. Notes / constraints

## Draw.io diagrams for conceptual documentation

Information pages, instruction pages, concept pages, architecture pages, and form lifecycle pages may use Draw.io diagrams when they clarify a real model or flow.

Draw.io diagrams must follow:

- `DRAWIO_DIAGRAM_RULES.md`

Do not use Draw.io diagrams in XML documentation comments. XML documentation may reference concepts that are diagrammed on pages, but the diagram artifact belongs to the website documentation layer.

## DocumentationBuilder-first rule

Documentation in this module must be authored with a DocumentationBuilder-first objective.

- Write docs so they can be extracted and rendered without manual rewrite.
- Keep headings deterministic and stable.
- Keep examples self-contained and realistically useful.
- Avoid implicit references to chat history or hidden context.
- Prefer stable type and member names that DocumentationBuilder can cross-reference.
- Use `<see cref="..."/>` and `<seealso cref="..."/>` for related PageBuilder types whenever it improves navigation.

## Separation from examples and tutorials

`EXAMPLES_AND_TUTORIALS_RULES.md` is not a general documentation rule source.

- Use this file for API documentation, XML HeaderDoc, README updates, reference pages, and DocumentationBuilder-ready documentation.
- Use `../MARKDOWN_GUIDELINES.md` for general Markdown formatting rules.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only when the task explicitly creates or updates example pages, demo pages, information pages, instruction pages, concept pages, tutorials, or tutorial-like walkthroughs.
- Do not import example-page requirements into XML documentation or reference documentation unless the task also changes examples or tutorials.

## Minimum update policy

If public form rendering behavior, validation behavior, Razor helper behavior, label behavior, captcha behavior, or static asset behavior changes, update in the same change set:

- local `README.md`,
- relevant XML docs,
- impacted guidance/examples when the task includes pages.

## Review checklist for documentation changes

- The documentation names the real FormBuilder concept, not a copied source project concept.
- All public and protected-contract API members touched by the change have valid XML documentation.
- Summaries are specific enough to help IntelliSense users choose the right API.
- Parameters, return values, generic parameters, exceptions, and side effects are documented where applicable.
- Examples reflect current code behavior and realistic Razor form usage.
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`.
- DocumentationBuilder can extract the content without needing hidden context or manual rewrite.
