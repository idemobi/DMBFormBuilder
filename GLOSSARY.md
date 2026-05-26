# DMBFormBuilder Glossary

## Purpose

Define common terms used in `DMBFormBuilder` documentation and AI instructions.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Publication host: `labs_idemobi_com`

## Terms

### Form builder

The `FormBuilder` component that renders a Bootstrap-compatible form and controls action, method, validation mode, multipart behavior, and required legend output.

### Field builder

A fluent builder that renders one form field, such as a text input, textarea, select, boolean checkbox, enum radio list, token field, password field, or captcha field.

### Model-bound helper

A Razor extension method that accepts a model expression and derives field name, ID, label, prompt, value, and validation metadata from ASP.NET Core model metadata.

### Label presentation

The visual and accessibility strategy for rendering labels, including normal labels, hidden labels, and label placement.

### Validation mode

The form-level strategy that determines how client-side and server-side validation expectations are represented.

### Constraint badge

A small visual hint that summarizes a validation constraint such as required, min length, max length, or pattern.

### Captcha field

A form field that renders a captcha image, input, and refresh behavior backed by `DMBServerWebHelper`.

### DocumentationViewer

The documentation browsing feature in `labs_idemobi_com` that displays generated API documentation for NuGet packages.

### DocumentationBuilder

The documentation generation process that extracts and renders API documentation. AI prepares content; the developer executes the generator.
