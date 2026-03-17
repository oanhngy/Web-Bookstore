# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Purpose
Purpose of this project is learning
- The goal is understanding and skill improvement, not just finishing the implement quickly
- Explanations, reasoning and learning value are more important than fast code generation

## Workflow
- Guide me like a senior developer teaching a fresher developer
- Explain things step by step
- Use very simple English and ELI5 style when posible

For each task, first explain:
    - what are we doing
    - why are we doing this
    - what options exist
    - trade-offs of each option
After each step, ask for my opinion or confirmation before moving on, show how to commit to github and what should in the comment

Code changes:
- Do not write, edit or refactor code until I explicitly approve
- Do not make implementation changes silently
- If code changes are needed, first propose the plan in simple bullet point
- Wait for my approval before applying any code change

Decision making:
- When multiple valid approaches exist, recommend one approach
- Also explain ehy it is recommended

General behavior:
- Prefer teaching and reasoning over jumping straight to the answer
- Help me understand how to think about the problem, not only how to solve it

## Coding rules
- Follow clean code practices whenever possible, every function need comment
- Keep code simple, readable and easy to understand
- Prefer clear naming over short or clever code
- Avoid unnecessary complexity

## Commands

```bash
dotnet run                              # Run the app (https://localhost:7001)
dotnet build                            # Build
dotnet ef migrations add <Name>         # Add EF migration
dotnet ef database update               # Apply migrations to DB
```

No test project exists in this repository.

## Database Setup

1. Import `dulieuBookstoreDb.sql` into SQL Server to initialize the database with seed data.
2. Update the connection string in `appsettings.json` if needed (default: `Server=localhost;Database=BookstoreDb;Trusted_Connection=True;TrustServerCertificate=True;`).
3. On first run, `SeedData.Initialize` creates the `Admin` role only — the admin user must be manually created in the database and assigned to that role.

> Note: The `InitialCreate` migration has an empty `Up()` — the schema is bootstrapped via `dulieuBookstoreDb.sql`, not migrations.

## Architecture

- **ASP.NET Core 6 MVC** with SQL Server via Entity Framework Core. No repository pattern — controllers inject `BookstoreContext` directly.
- Need to change database from SQL Server to MySQL
- Lack of folder Views/, Models/, unit testing...

### Data Model
- `Product` → belongs to `Category`, has many `ProductImage`
- `Order` → belongs to `IdentityUser` (via `UserID`), has many `OrderDetail`
- `OrderDetail` → belongs to `Order` and `Product`
- `BookstoreContext` extends `IdentityDbContext<IdentityUser>` (ASP.NET Identity tables co-located)
- A separate `Users` DbSet exists alongside `AspNetUsers` — do not confuse the two

### Order / Cart Lifecycle
The shopping cart is **not a separate entity** — it is an `Order` with `Status = "New"`. Status transitions:
```
"New" (cart) → "Checked Out" → "Confirmed" → "Completed"
                                            → "Cancelled"
```
- `CartController` manages the "New" order (add, remove, update quantity, checkout)
- `CartController.Checkout` transitions to "Checked Out" and renders the checkout form
- `OrderController.ConfirmOrder` captures delivery info (FullName, Email, Phone, Address, Note, PaymentMethod) onto the Order row and transitions to "Confirmed"
- `OrderController.ViewOrderStatus` shows orders with `Status != "New"` to the customer

### Controllers
| Controller | Purpose |
|---|---|
| `ProductController` | Public book listing (search, category filter, sort, pagination) and detail view |
| `CartController` | Cart management — requires `[Authorize]` |
| `OrderController` | Customer order status and cancellation |
| `AccountController` | Register, Login, Logout via ASP.NET Identity |
| `AdminController` | Admin CRUD for products/orders and revenue API — requires `[Authorize(Roles = "Admin")]` |

### Roles
- **Admin** — `AdminController` is fully protected. After login, admins redirect to `https://localhost:7001/Admin/Dashboard`.
- **Customer** — assigned automatically on registration.

### Default Route
`{controller=Product}/{action=Index}/{id?}` — home page is `ProductController.Index` (12 items/page via `X.PagedList`).

### Revenue API
`GET /Admin/GetRevenueData?timeFrame={day|week|month|year}` returns JSON used by a Chart.js chart in the `ViewRevenue` view. Only counts `Status = "Completed"` orders.

### Image Upload
Product images are saved to `wwwroot/images/` with only the filename stored in `ProductImage.ImagePath`. Both `IsPrimary` and `ImageType` fields exist; the listing page filters on `IsPrimary == true && ImageType == "main"`.