# SQL Comparison by EF Version

## EF 6

### Flattened SQL

```text
SELECT
    [GroupBy1].[K1] AS [Year],
    [GroupBy1].[K2] AS [Quarter],
    [GroupBy1].[A1] AS [C1],
    [GroupBy1].[A2] AS [C2]
    FROM ( SELECT
        [Extent1].[Year] AS [K1],
        [Extent1].[Quarter] AS [K2],
        AVG([Extent2].[TaxRate]) AS [A1],
        SUM([Extent2].[TaxRate]) AS [A2]
        FROM  [dbo].[Sales] AS [Extent1]
        INNER JOIN [dbo].[Products] AS [Extent2] ON [Extent1].[ProductId] = [Extent2].[Id]
        GROUP BY [Extent1].[Year], [Extent1].[Quarter]
    )  AS [GroupBy1]
```

### Non-Flattened SQL

```text
SELECT
    [Project2].[Year] AS [Year],
    [Project2].[Quarter] AS [Quarter],
    [Project2].[C1] AS [C1],
    (SELECT
        SUM([Extent5].[TaxRate]) AS [A1]
        FROM  [dbo].[Sales] AS [Extent4]
        INNER JOIN [dbo].[Products] AS [Extent5] ON [Extent4].[ProductId] = [Extent5].[Id]
        WHERE ([Project2].[Year] = [Extent4].[Year]) AND (([Project2].[Quarter] = [Extent4].[Quarter]) OR (([Project2].[Quarter] IS NULL) AND ([Extent4].[Quarter] IS NULL)))) AS [C2]
    FROM ( SELECT
        [Distinct1].[Year] AS [Year],
        [Distinct1].[Quarter] AS [Quarter],
        (SELECT
            AVG([Extent3].[TaxRate]) AS [A1]
            FROM  [dbo].[Sales] AS [Extent2]
            INNER JOIN [dbo].[Products] AS [Extent3] ON [Extent2].[ProductId] = [Extent3].[Id]
            WHERE ([Distinct1].[Year] = [Extent2].[Year]) AND (([Distinct1].[Quarter] = [Extent2].[Quarter]) OR (([Distinct1].[Quarter] IS NULL) AND ([Extent2].[Quarter] IS NULL)))) AS [C1]
        FROM ( SELECT DISTINCT
            [Extent1].[Year] AS [Year],
            [Extent1].[Quarter] AS [Quarter]
            FROM [dbo].[Sales] AS [Extent1]
        )  AS [Distinct1]
    )  AS [Project2]
```

## EF Core 3.x

### Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], AVG([p].[TaxRate]) AS [AverageProductTaxRate], SUM([p].[TaxRate]) AS [SumProductTaxRate]
FROM [Sales] AS [s]
LEFT JOIN [Products] AS [p] ON [s].[ProductId] = [p].[Id]
GROUP BY [s].[Year], [s].[Quarter]
```

### Non-Flattened SQL

```text
❌ Exception thrown
```

## EF Core 5.x

### Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], AVG([p].[TaxRate]) AS [AverageProductTaxRate], COALESCE(SUM([p].[TaxRate]), 0.0) AS [SumProductTaxRate]
FROM [Sales] AS [s]
LEFT JOIN [Products] AS [p] ON [s].[ProductId] = [p].[Id]
GROUP BY [s].[Year], [s].[Quarter]
```

### Non-Flattened SQL

```text
❌ Exception thrown
```

## EF Core 6.x

### Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], AVG([p].[TaxRate]) AS [AverageProductTaxRate], COALESCE(SUM([p].[TaxRate]), 0.0) AS [SumProductTaxRate]
FROM [Sales] AS [s]
INNER JOIN [Products] AS [p] ON [s].[ProductId] = [p].[Id]
GROUP BY [s].[Year], [s].[Quarter]
```

### Non-Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], AVG([p].[TaxRate]) AS [AverageProductTaxRate], COALESCE(SUM([p].[TaxRate]), 0.0) AS [SumProductTaxRate]
FROM [Sales] AS [s]
INNER JOIN [Products] AS [p] ON [s].[ProductId] = [p].[Id]
GROUP BY [s].[Year], [s].[Quarter]
```

## EF Core 7.x

### Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], (
    SELECT AVG([p].[TaxRate])
    FROM [Sales] AS [s0]
    INNER JOIN [Products] AS [p] ON [s0].[ProductId] = [p].[Id]
    WHERE [s].[Year] = [s0].[Year] AND [s].[Quarter] = [s0].[Quarter]) AS [AverageProductTaxRate], (
    SELECT COALESCE(SUM([p0].[TaxRate]), 0.0)
    FROM [Sales] AS [s1]
    INNER JOIN [Products] AS [p0] ON [s1].[ProductId] = [p0].[Id]
    WHERE [s].[Year] = [s1].[Year] AND [s].[Quarter] = [s1].[Quarter]) AS [SumProductTaxRate]
FROM [Sales] AS [s]
GROUP BY [s].[Year], [s].[Quarter]
```

### Non-Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], (
    SELECT AVG([p].[TaxRate])
    FROM [Sales] AS [s0]
    INNER JOIN [Products] AS [p] ON [s0].[ProductId] = [p].[Id]
    WHERE [s].[Year] = [s0].[Year] AND [s].[Quarter] = [s0].[Quarter]) AS [AverageProductTaxRate], (
    SELECT COALESCE(SUM([p0].[TaxRate]), 0.0)
    FROM [Sales] AS [s1]
    INNER JOIN [Products] AS [p0] ON [s1].[ProductId] = [p0].[Id]
    WHERE [s].[Year] = [s1].[Year] AND [s].[Quarter] = [s1].[Quarter]) AS [SumProductTaxRate]
FROM [Sales] AS [s]
GROUP BY [s].[Year], [s].[Quarter]
```

## EF Core 8.x

### Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], (
    SELECT AVG([p].[TaxRate])
    FROM [Sales] AS [s0]
    INNER JOIN [Products] AS [p] ON [s0].[ProductId] = [p].[Id]
    WHERE [s].[Year] = [s0].[Year] AND [s].[Quarter] = [s0].[Quarter]) AS [AverageProductTaxRate], (
    SELECT COALESCE(SUM([p0].[TaxRate]), 0.0)
    FROM [Sales] AS [s1]
    INNER JOIN [Products] AS [p0] ON [s1].[ProductId] = [p0].[Id]
    WHERE [s].[Year] = [s1].[Year] AND [s].[Quarter] = [s1].[Quarter]) AS [SumProductTaxRate]
FROM [Sales] AS [s]
GROUP BY [s].[Year], [s].[Quarter]
```

### Non-Flattened SQL

```text
SELECT [s].[Year], [s].[Quarter], (
    SELECT AVG([p].[TaxRate])
    FROM [Sales] AS [s0]
    INNER JOIN [Products] AS [p] ON [s0].[ProductId] = [p].[Id]
    WHERE [s].[Year] = [s0].[Year] AND [s].[Quarter] = [s0].[Quarter]) AS [AverageProductTaxRate], (
    SELECT COALESCE(SUM([p0].[TaxRate]), 0.0)
    FROM [Sales] AS [s1]
    INNER JOIN [Products] AS [p0] ON [s1].[ProductId] = [p0].[Id]
    WHERE [s].[Year] = [s1].[Year] AND [s].[Quarter] = [s1].[Quarter]) AS [SumProductTaxRate]
FROM [Sales] AS [s]
GROUP BY [s].[Year], [s].[Quarter]
```
