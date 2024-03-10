using System.Reflection;
using QueryableAPI.EntityFramework.Exceptions;

namespace QueryableAPI.EntityFramework;

public class QueryableCache
{
    private MethodInfo? _whereMethod;
    
    public MethodInfo WhereMethod
    {
        get
        {
            if (_whereMethod == null)
            {
                _whereMethod = typeof(Queryable)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(x => x.Name == "Where");
                
                if (_whereMethod == null)
                {
                    throw new WhereQueryableMethodWasNotFound();
                }
            }

            return _whereMethod;
        }
    }
}