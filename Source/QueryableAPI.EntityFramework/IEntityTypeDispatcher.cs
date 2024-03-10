namespace QueryableAPI.EntityFramework;

public interface IEntityTypeDispatcher
{
    Type Dispatch(string entityName);
}