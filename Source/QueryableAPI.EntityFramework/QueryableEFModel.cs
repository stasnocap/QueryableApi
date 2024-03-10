using System.Text;

namespace QueryableAPI.EntityFramework;

public record QueryableEFModel(
    string PropertyName,
    Include? Include,
    bool AsSplitQuery,
    Where? Where
);

public record Where(
    string PropertyName,
    Comparison Comparison,
    object? Value
)
{
}

public enum Comparison
{
    None,
    Equal,
    NotEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LesserThan,
    LesserThanOrEqual,
    Contains
}

public record Include(
    string PropertyName,
    Include? ThenInclude
)
{
    public string ToNavigationPropertyPath(StringBuilder sb)
    {
        sb.Append(PropertyName);

        if (ThenInclude == null)
        {
            return sb.ToString();
        }
        
        sb.Append(SBConstants.Dot);

        return ToNavigationPropertyPath(sb);
    }
}