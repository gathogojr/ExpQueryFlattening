
# 📝 Entity Framework SQL Generation Comparison: Flattened vs Non-Flattened Queries

In **Entity Framework 6**, "flattening queries" refers to transforming LINQ expressions - especially those involving navigation properties and projections - to avoid generating inefficient or overly complex SQL queries. The was often necessary because:
- EF6 had limited support for projecting into complex anonymous or DTO types.
- Nested projections and navigation property accessors could result in large or suboptimal SQL.
- Flattening (e.g., turnning `.Select(d => new { d.A, d.B.C })` into `new { d.A, dC = d.B.C }`) helped EF generate simpler, more performant SQL.
- Deep projections or queries with complex object graphs could fail or result in inefficient queries.

Flattening in EF Core isn't necessary as much due to signicant improvements made in handling:
- Deep projections and includes.
- Navigation property loading.
- Projection into custom types and anonymous types.
- Expression translation to SQL.

However, flattening may still be useful in some cases, such as:
- When fine-turning performance.
- Avoiding redundant joins or includes.
- Projecting only the columns needed to avoid overfetching.

> **When EF Core can't translate part of a query to SQL, it will either throw (default in recent versions), or fall back to client evaluation (in older versions).**

The summary below captures the differences in SQL generation behavior across various Entity Framework versions when comparing flattened and non-flattened LINQ queries involving `GroupBy` and navigation property access.

The experiment used the following shared data model (Category → Product → Sale with Customer), applied consistently across **EF6**, **EF Core 3.x through EF Core 8.x**.

> 🔍 Goal: Understand whether **manual flattening logic** is still required for proper query translation — particularly relevant for `$apply` (aggregation) feature in the [Microsoft.AspNetCore.OData](https://github.com/OData/AspNetCoreOData) library.

---

## ✅ Summary Table

| EF Version       | Flattened Query | Non-Flattened Query | Behavior Comparison                                                                 |
|------------------|------------------|----------------------|--------------------------------------------------------------------------------------|
| **EF6**          | ✅ Valid SQL      | ✅ Valid SQL          | Non-flattened produces nested subqueries, but both execute correctly                |
| **EF Core 3.x**  | ✅ Valid SQL      | ❌ Throws Exception   | Non-flattened query fails; flattening required                                     |
| **EF Core 5.x**  | ✅ Valid SQL      | ❌ Throws Exception   | Same behavior as EF Core 3.x                                                       |
| **EF Core 6.x**  | ✅ Valid SQL      | ✅ Valid SQL          | Same SQL for both — no flattening needed                                           |
| **EF Core 7.x**  | ✅ Valid SQL      | ✅ Valid SQL          | Minor variation in subqueries; both functionally correct                           |
| **EF Core 8.x**  | ✅ Valid SQL      | ✅ Valid SQL          | Same behavior as EF Core 7.x — functionally equivalent                             |

---

## 🔍 Flattened vs Non-Flattened: What’s the Difference?

- **Flattened Query**: Navigates to `Product.TaxRate` before `GroupBy`, avoiding navigation in aggregation.
- **Non-Flattened Query**: Performs `GroupBy` first, then navigates within aggregation (e.g., `e.Product.TaxRate`).

Flattening was a common workaround in older EF Core versions to avoid translation issues during grouping and projection.

---

## 💡 Key Insight

Starting from **EF Core 6.x**, both flattened and non-flattened queries generate functionally equivalent and valid SQL. This means:

> **The flattening workaround in OData's `$apply` aggregation feature can safely be removed Microsoft.AspNetCore.OData 8.x+**.

This allows for **cleaner, more maintainable code** and leverages improved translation support in newer EF Core versions.

---

Find complete comparisons of the [generated SQL here](ef_sql_comparison.md)