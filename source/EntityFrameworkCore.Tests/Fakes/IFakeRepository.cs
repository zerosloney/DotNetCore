using DotNetCore.Repositories;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    public interface IFakeRepository : IRelationalRepository<FakeEntity>
    {
    }
}
