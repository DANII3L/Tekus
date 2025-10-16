using System.Text;

/// <summary>
/// Service to build dynamic SQL filters
/// </summary>
public class SqlFilterBuilder
{
    /// <summary>
    /// Builds a SQL LIKE filter for multiple columns
    /// </summary>
    /// <param name="columns">Array of column names to search</param>
    /// <param name="filterValue">Value to search for</param>
    /// <param name="table">Table alias (optional)</param>
    /// <returns>SQL filter string with OR conditions</returns>
    public static string BuildLikeFilter(string[] columns, string filterValue, string? table = null)
    {
        if (string.IsNullOrWhiteSpace(filterValue) || columns == null || columns.Length == 0)
            return string.Empty;

        var prefix = string.IsNullOrWhiteSpace(table) ? "" : $"{table}.";
        var escapedValue = EscapeSqlLike(filterValue);

        var conditions = columns
            .Where(column => !string.IsNullOrWhiteSpace(column))
            .Select(column => $"{prefix}{column} LIKE N'%{escapedValue}%'");

        return $"AND ({string.Join(" OR ", conditions)})";
    }

    /// <summary>
    /// Escapes SQL LIKE special characters
    /// </summary>
    private static string EscapeSqlLike(string value)
    {
        return value
            .Replace("'", "''")
            .Replace("[", "[[]")
            .Replace("%", "[%]")
            .Replace("_", "[_]");
    }
}

