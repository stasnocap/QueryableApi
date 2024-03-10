using Microsoft.Extensions.Options;
using QueryableAPI.EntityFramework.Exceptions;

namespace QueryableAPI.EntityFramework;

public class DefaultEntityTypeDispatcher(IOptions<QueryableOptions> options) : IEntityTypeDispatcher
{
    private readonly QueryableOptions _options = options.Value;

    public Type Dispatch(string entityName)
    {
        if (!_options.EntitiesConfiguration.TryGetValue(entityName, out Type? entityType) || entityType == null)
        {
            throw new EntityTypeWasNotFoundException();
        }

        return entityType;
    }
}