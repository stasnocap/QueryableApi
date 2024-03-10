namespace QueryableAPI.EntityFramework;

public record QueryableOptions(
    IReadOnlyDictionary<string, Type?> EntitiesConfiguration
    );
