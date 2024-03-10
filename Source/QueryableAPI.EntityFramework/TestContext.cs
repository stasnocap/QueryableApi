using Microsoft.EntityFrameworkCore;

namespace QueryableAPI.EntityFramework;

public class TestContext : DbContext
{
    public DbSet<TestEntity> Entities { get; set; }
    
}

public class TestEntity
{
    
}