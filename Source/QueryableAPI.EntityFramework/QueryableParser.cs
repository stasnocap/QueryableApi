using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QueryableAPI.EntityFramework.LambdaBuilders;

namespace QueryableAPI.EntityFramework;

public class QueryableParser(
    IEntityTypeDispatcher _entityTypeDispatcher,
    TestContext _testContext,
    WhereLambdaBuilder _whereLambdaBuilder,
    QueryableCache
        _queryableCache)
{
    public IQueryable Parse(QueryableEFModel model)
    {
        Type entityType = _entityTypeDispatcher.Dispatch(model.PropertyName);

        IQueryable<TestEntity> query = _testContext
            .Entities
            .AsNoTracking();

        if (model.Include is not null)
        {
            query = query.Include(model.Include.ToNavigationPropertyPath(new StringBuilder()));
        }

        if (model.AsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        if (model.Where is not null)
        {
            _queryableCache
                .WhereMethod
                .Invoke(null, [query, _whereLambdaBuilder.Build(model.Where, entityType)]);
        }

        return
    }
}