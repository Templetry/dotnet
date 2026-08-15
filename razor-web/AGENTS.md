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

## Safe Change Workflow

1. Read the affected files fully before editing.
2. Make the smallest change that solves the task.
3. Build and test, then review the diff with git before committing.
