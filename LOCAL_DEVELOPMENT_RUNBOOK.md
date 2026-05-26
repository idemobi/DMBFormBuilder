# DMBFormBuilder Local Development Runbook

## Purpose

Provide a lightweight workflow for local work in `DMBFormBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBFormBuilder`
- Project folder: `DMBFormBuilder`
- Project role: Bootstrap-oriented form builder package for MVC/Razor applications.
- Publication host: `labs_idemobi_com`

## Orientation

Start by reading:

- [README.md](README.md)
- [PROJECT_MAP.md](PROJECT_MAP.md)
- [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md)
- [DELIVERY_CHECKLIST.md](DELIVERY_CHECKLIST.md)

For example or tutorial page work, also read:

- [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md)
- [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md)

## Work loop

1. Identify the affected feature family:
   - form composition,
   - text fields,
   - password fields,
   - textarea fields,
   - select fields,
   - boolean fields,
   - enum radio fields,
   - flag fields,
   - token fields,
   - captcha fields,
   - configuration,
   - localization,
   - documentation pages.
2. Read the relevant code and local rules before editing.
3. Keep edits local to the smallest useful area.
4. Update XML documentation for touched public APIs.
5. Update README or guidance files when behavior changes.
6. Run only checks that the user explicitly permits.

## Build and test policy

Do not run these commands unless explicitly requested:

```text
dotnet build
dotnet test
dotnet restore
dotnet format
```

## Safe inspection commands

Useful read-only commands:

```text
rg "TextFieldBuilder" DMBFormBuilder
rg "FormBuilder" DMBFormBuilder
find DMBFormBuilder -maxdepth 3 -type f | sort
git diff -- DMBFormBuilder
```

Prefer `rg` for searches.
