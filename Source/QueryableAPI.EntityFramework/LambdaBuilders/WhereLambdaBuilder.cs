using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using QueryableAPI.EntityFramework.Exceptions;

namespace QueryableAPI.EntityFramework.LambdaBuilders;

public class WhereLambdaBuilder
{
    public Expression Build(Where modelWhere, Type entityType)
    {
        ParameterExpression parameter = Expression.Parameter(entityType);

        PropertyInfo? propertyInfo = entityType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(x => x.Name == modelWhere.PropertyName);

        if (propertyInfo is null)
        {
            throw new EntityPropertyWasNotFoundException();
        }

        MemberExpression property = Expression.Property(parameter, propertyInfo);

        if (modelWhere.Value is JsonElement element)
        {
            ConstantExpression comparisonValue =
                Expression.Constant(
                    element.ValueKind == JsonValueKind.Array
                        ? element.EnumerateArray().Select(x => x.Deserialize(propertyInfo.PropertyType))
                        : Convert.ChangeType(element.Deserialize(propertyInfo.PropertyType), propertyInfo.PropertyType),
                    propertyInfo.PropertyType
                );

            return GetComparisonExpression(modelWhere.Comparison, property, comparisonValue);
        }

        var alice = Expression.Constant("alice");
        var equal = Expression.Equal(property, alice);
        var result = Expression.Lambda(equal, parameter);

        return result;
    }

    private Expression GetComparisonExpression(Comparison comparison,
        MemberExpression property,
        ConstantExpression comparisonValue)
    {
        switch (comparison)
        {
            case Comparison.None:
                throw new Exception("Comparison was not specified");
            case Comparison.Equal:
                return Expression.Equal(property, comparisonValue);
            case Comparison.NotEqual:
                return Expression.NotEqual(property, comparisonValue);
            case Comparison.GreaterThan:
                return Expression.GreaterThan(property, comparisonValue);
            case Comparison.GreaterThanOrEqual:
                return Expression.GreaterThanOrEqual(property, comparisonValue);
            case Comparison.LesserThan:
                return Expression.LessThan(property, comparisonValue);
            case Comparison.LesserThanOrEqual:
                return Expression.LessThanOrEqual(property, comparisonValue);
            case Comparison.Contains:
                
                
                return ;
            default:
                throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
        }
    }
}