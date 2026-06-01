# DMBFormBuilder Architecture Decisions

## Purpose

Record durable architecture decisions that AI assistants and maintainers must preserve unless a change request explicitly supersedes them.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Main dependencies: `DMBPageBuilder`, `DMBBootstrapBuilder`, `DMBComponentBuilder`, and `DMBServerWebHelper`.
- Publication host: `labs_idemobi_com`

## Decisions

### Keep field builders fluent and deterministic

Builder methods should return the same builder instance for chaining and should update rendering state predictably.

### Keep model binding names explicit

Model-bound helper methods must preserve ASP.NET Core expression-derived input names, IDs, labels, values, prompts, and validation metadata.

### Keep validation behavior visible

Required, length, pattern, comparison, numeric, Boolean, ASCII, Unix text, and captcha constraints must be documented as generated HTML attributes or validation behavior.

### Keep Boolean validation local to FormBuilder

`BoolMustBeTrueAttribute` and `BoolMustBeFalseAttribute` belong to FormBuilder and are translated only by Boolean field helpers. Their JavaScript support should be loaded on demand by the field that emits Boolean validation metadata.

### Keep visual styling Bootstrap-oriented

Form fields should render Bootstrap-compatible markup and classes through existing BootstrapBuilder and PageBuilder conventions.

### Treat sensitive fields carefully

Password, token, captcha, generated JavaScript, and custom attribute APIs must avoid undocumented exposure of sensitive values and must document rendering side effects.

### Keep examples outside the package

Example pages, tutorials, diagrams, and explanatory pages are published through `labs_idemobi_com` when requested.

The package should not embed documentation website pages directly.
