using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetCore.EntityFrameworkCore.Tests;
using DotNetCore.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.EntityFrameworkCore.Tests
{
    [TestClass]
    public class EntityFrameworkCoreTests
    {
        public EntityFrameworkCoreTests()
        {
            var services = new ServiceCollection();
            services.AddDbContextPool<FakeContext>(options => options.UseInMemoryDatabase(nameof(FakeContext)));
            Context = services.BuildServiceProvider().GetService<FakeContext>();
            Context.Database.EnsureCreated();
            Repository = new FakeRepository(Context);
            SeedDatabase();
        }

        private FakeContext Context { get; }

        private IFakeRepository Repository { get; }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAdd()
        {
            var entity = CreateEntity();
            Repository.Add(entity);
            Context.SaveChanges();
            Assert.IsNotNull(Repository.Select(entity.FakeEntityId));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAddAsynchronous()
        {
            var entity = CreateEntity();
            Repository.AddAsync(entity);
            Context.SaveChanges();
            Assert.IsNotNull(Repository.Select(entity.FakeEntityId));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAddRange()
        {
            var count = Repository.Count();
            Repository.AddRange(new List<FakeEntity> { CreateEntity() });
            Context.SaveChanges();
            Assert.IsTrue(Repository.Count() > count);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAddRangeAsynchronous()
        {
            var count = Repository.Count();
            Repository.AddRangeAsync(new List<FakeEntity> { CreateEntity() });
            Context.SaveChanges();
            Assert.IsTrue(Repository.Count() > count);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAny()
        {
            Assert.IsTrue(Repository.Any());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAnyAsynchronous()
        {
            Assert.IsTrue(Repository.AnyAsync().Result);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAnyWhere()
        {
            Assert.IsTrue(Repository.Any(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryAnyWhereAsynchronous()
        {
            Assert.IsTrue(Repository.AnyAsync(w => w.FakeEntityId == 1L).Result);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryCount()
        {
            Assert.IsTrue(Repository.Count() > 0);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryCountAsynchronous()
        {
            Assert.IsTrue(Repository.CountAsync().Result > 0);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryCountWhere()
        {
            Assert.IsTrue(Repository.Count(w => w.FakeEntityId == 1) == 1L);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryCountWhereAsynchronous()
        {
            Assert.IsTrue(Repository.CountAsync(w => w.FakeEntityId == 1L).Result == 1L);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryDelete()
        {
            Repository.Delete(70L);
            Context.SaveChanges();
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryDeleteAsynchronous()
        {
            Repository.DeleteAsync(80L);
            Context.SaveChanges();
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryDeleteWhere()
        {
            Repository.Delete(w => w.FakeEntityId == 90L);
            Context.SaveChanges();
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryDeleteWhereAsynchronous()
        {
            Repository.DeleteAsync(w => w.FakeEntityId == 100L);
            Context.SaveChanges();
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefault()
        {
            Assert.IsNotNull(Repository.FirstOrDefault());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultAsynchronous()
        {
            Assert.IsNotNull(Repository.FirstOrDefaultAsync().Result);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultInclude()
        {
            Assert.IsNotNull(Repository.FirstOrDefault(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultIncludeAsynchronous()
        {
            Assert.IsNotNull(Repository.FirstOrDefaultAsync(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultResult()
        {
            Assert.IsNotNull(Repository.FirstOrDefault<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultResultAsynchronous()
        {
            Assert.IsNotNull(Repository.FirstOrDefaultAsync<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultWhere()
        {
            Assert.IsNotNull(Repository.FirstOrDefault(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultWhereAsynchronous()
        {
            Assert.IsNotNull(Repository.FirstOrDefaultAsync(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultWhereInclude()
        {
            Assert.IsNotNull(Repository.FirstOrDefault(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryFirstOrDefaultWhereIncludeAsynchronous()
        {
            Assert.IsNotNull(Repository.FirstOrDefaultAsync(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryList()
        {
            Assert.IsNotNull(Repository.List());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListInclude()
        {
            Assert.IsNotNull(Repository.List(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListIncludeAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListIncludeResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListIncludeResultAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync<FakeEntityModel>(i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPaged()
        {
            Assert.IsNotNull(Repository.List(new PagedListParameters()));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedInclude()
        {
            Assert.IsNotNull(Repository.List(new PagedListParameters(), i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedIncludeResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(new PagedListParameters(), i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(new PagedListParameters()));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedWhere()
        {
            Assert.IsNotNull(Repository.List(new PagedListParameters(), w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedWhereInclude()
        {
            Assert.IsNotNull(Repository.List(new PagedListParameters(), w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedWhereIncludeResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(new PagedListParameters(), w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListPagedWhereResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(new PagedListParameters(), w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListResultAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync<FakeEntityModel>());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhere()
        {
            Assert.IsNotNull(Repository.List(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereInclude()
        {
            Assert.IsNotNull(Repository.List(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereIncludeAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereIncludeResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereIncludeResultAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync<FakeEntityModel>(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereResult()
        {
            Assert.IsNotNull(Repository.List<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryListWhereResultAsynchronous()
        {
            Assert.IsNotNull(Repository.ListAsync<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryQueryable()
        {
            Assert.IsNotNull(Repository.Queryable.OrderByDescending(o => o.FakeEntityId).FirstOrDefault());
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySelect()
        {
            Assert.IsNotNull(Repository.Select(1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySelectAsynchronous()
        {
            Assert.IsNotNull(Repository.SelectAsync(1L).Result);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySelectResult()
        {
            Assert.IsNotNull(Repository.Select<FakeEntityModel>(1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySelectResultAsynchronous()
        {
            Assert.IsNotNull(Repository.SelectAsync<FakeEntityModel>(1L).Result);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultResult()
        {
            Assert.IsNotNull(Repository.SingleOrDefault<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultResultAsynchronous()
        {
            Assert.IsNotNull(Repository.SingleOrDefaultAsync<FakeEntityModel>(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultWhere()
        {
            Assert.IsNotNull(Repository.SingleOrDefault(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultWhereAsynchronous()
        {
            Assert.IsNotNull(Repository.SingleOrDefaultAsync(w => w.FakeEntityId == 1L));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultWhereInclude()
        {
            Assert.IsNotNull(Repository.SingleOrDefault(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositorySingleOrDefaultWhereIncludeAsynchronous()
        {
            Assert.IsNotNull(Repository.SingleOrDefaultAsync(w => w.FakeEntityId == 1L, i => i.FakeEntityChild));
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryUpdate()
        {
            var entity = Repository.Select(1L);
            entity.Name = Guid.NewGuid().ToString();
            Repository.Update(entity, 1L);
            Context.SaveChanges();
            Assert.AreEqual(entity.Name, Repository.Select(1L).Name);
        }

        [TestMethod]
        public void EntityFrameworkCoreTestsRepositoryUpdateAsynchronous()
        {
            var entity = Repository.Select(1L);
            entity.Name = Guid.NewGuid().ToString();
            Repository.UpdateAsync(entity, 1L);
            Context.SaveChanges();
            Assert.AreEqual(entity.Name, Repository.Select(1L).Name);
        }

        private static FakeEntity CreateEntity()
        {
            return new FakeEntity { Name = $"Name {Guid.NewGuid().ToString()}" };
        }

        private void SeedDatabase()
        {
            for (var i = 1L; i <= 100; i++)
            {
                Repository.Add(CreateEntity());
            }

            Context.SaveChanges();
        }
    }
}
