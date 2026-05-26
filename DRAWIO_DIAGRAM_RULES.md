# DMBFormBuilder Draw.io Diagram Rules

## Objective

Draw.io diagrams may be created for information pages, instruction pages, concept pages, architecture pages, form lifecycle pages, validation pages, and tutorials when a visual model makes the explanation clearer.

Do not create a diagram as decoration. A diagram must explain a real concept, flow, dependency, lifecycle, state model, validation flow, model binding flow, or form rendering relationship.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Diagram publication root: `labs_idemobi_com/wwwroot/drawio`
- Default area folder for this project: `FormBuilder`
- Shared template path: `labs_idemobi_com/wwwroot/drawio/_templates/concept-flow-template.drawio.svg`
- Preferred page rendering approach: use existing PageBuilder or BootstrapBuilder image helpers when available.

## Required format

All Draw.io diagrams must be saved as enriched SVG files:

```text
*.drawio.svg
```

The SVG must remain editable in Draw.io. It must contain the embedded Draw.io model, either through the Draw.io `content` attribute or Draw.io metadata containing an `<mxfile>` payload.

Do not commit flattened SVG-only exports, PNG-only exports, screenshots, or manually rewritten SVG diagrams for Draw.io-managed documentation diagrams.

## Publication path

Diagrams used by `labs_idemobi_com` pages must be stored under:

```text
labs_idemobi_com/wwwroot/drawio/{Area}/{diagram-name}.drawio.svg
```

Use stable, descriptive diagram file names:

- `form-rendering-flow.drawio.svg`
- `field-builder-lifecycle.drawio.svg`
- `validation-flow.drawio.svg`
- `model-binding-flow.drawio.svg`
- `captcha-field-flow.drawio.svg`

When referenced from Razor, use the web path:

```html
<img src="/drawio/FormBuilder/form-rendering-flow.drawio.svg" alt="DMBFormBuilder form rendering flow diagram" />
```

Prefer rendering diagrams through the existing PageBuilder or BootstrapBuilder image helpers when an appropriate helper exists.

## Draw.io grid and layout rules

Draw.io source geometry must stay grid-aligned:

- enable the Draw.io grid,
- use a `gridSize` of `10`,
- place x, y, width, and height values on multiples of `10`,
- keep connector waypoints on grid intersections,
- use orthogonal connectors for process and architecture flows,
- keep related elements in rows or columns with consistent spacing,
- avoid freehand, skewed, or visually approximate positioning.

Use a left-to-right flow for builder and validation pipelines. Use top-to-bottom flow only when it matches the concept better.

## Light and dark mode compatibility

Diagrams must work on both light and dark page themes.

Required practices:

- keep the root SVG background transparent,
- include `color-scheme: light dark` on the root SVG when exported,
- use Draw.io `adaptiveColors="auto"` in the embedded graph model when available,
- avoid pure black text on transparent backgrounds,
- avoid pure white fills without a visible border,
- use high-contrast stroke and text colors,
- use restrained semantic colors that remain distinguishable in both themes,
- verify labels remain readable when the hosting page switches between light and dark themes.

## Accessibility and page usage

Every diagram rendered in a page must have meaningful alternative text.

The surrounding page must explain the diagram in normal text. Do not rely on the diagram as the only source of critical information.

## Template

Use this editable SVG template as the starting point for new diagrams:

```text
labs_idemobi_com/wwwroot/drawio/_templates/concept-flow-template.drawio.svg
```
