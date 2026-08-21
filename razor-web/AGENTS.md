# AGENTS

Operating contract for AI agents and automation helpers working in this project.

## Mission

- Keep this a server-rendered Razor Pages app: pages and page models, no client-side framework and no API layer unless the app actually needs one.

## Core Rules

- Nullable and implicit usings stay enabled; fix warnings rather than suppressing them.
- A page's logic lives in its `PageModel`; `.cshtml` files stay presentation only.
- Never edit `Pages/Shared/_Layout.cshtml` to add navigation — add an `INavContributor`.
- Forms post to a page handler, keep the antiforgery token, and validate with data annotations. Never disable antiforgery to make a test pass.
- Do not add external CDN assets: everything the app serves comes from `wwwroot`.
- Every page and handler gets a `WebApplicationFactory` test in the test project.
- Package versions stay aligned with the target framework (net9.0).
- Update docs in the same change when behavior or process changes.

## Required Checks Before Finishing

- `dotnet build` compiles clean.
- `dotnet test` passes.

```sh templetry:checks
dotnet test
```

## Safe Change Workflow

1. Read the affected files fully before editing.
2. Make the smallest change that solves the task.
3. Build and test, then review the diff with git before committing.

## This project came from a template

Four facts you cannot infer from the code in front of you:

- **Never hand-edit `.templetry-answers.yml`.** It records what generated this project. Editing it makes the next update merge against a state that never existed.
- **Before writing a capability by hand, run `templetry pieces`.** Auth, RBAC, audit trails, API keys and whole CRUD resources may already exist as pieces for this template. Adopting one is `templetry add <name>`, and it brings its own tests.
- **`templetry update` pulls improvements from the template** through a three-way merge that keeps your edits. Use it instead of copying files from the template by hand.
- **Directives like `tpl:if` belong to the template, not here.** If you find one in this project, it is a rendering bug worth reporting — do not try to interpret it.
