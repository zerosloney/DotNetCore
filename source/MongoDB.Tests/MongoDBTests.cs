using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.MongoDB.Tests
{
    [TestClass]
    public class MongoDBTests
    {
        public MongoDBTests()
        {
            const string connectionString = "mongodb://localhost/Database";
            var context = new FakeContext(connectionString);
            Repository = new FakeRepository(context);
        }

        public IFakeRepository Repository { get; }

        [TestMethod]
        public void MongoDBTestsRepositoryAdd()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAddAsynchronous()
        {
            var document = CreateDocument();
            Repository.AddAsync(document);
            Assert.IsNotNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAddRange()
        {
            var count = Repository.Count();
            Repository.AddRange(new List<FakeDocument> { CreateDocument() });
            Assert.IsTrue(Repository.Count() > count);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAddRangeAsynchronous()
        {
            var count = Repository.Count();
            Repository.AddRangeAsync(new List<FakeDocument> { CreateDocument() });
            Assert.IsTrue(Repository.Count() > count);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAny()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.Any());
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAnyAsynchronous()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.AnyAsync().Result);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAnyWhere()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.Any(x => x.Id == document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryAnyWhereAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.AnyAsync(x => x.Id == document.Id).Result);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryCount()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.Count() > 0);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryCountAsynchronous()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.CountAsync().Result > 0);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryCountWhere()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.Count(x => x.Id == document.Id) == 1);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryCountWhereAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.CountAsync(x => x.Id == document.Id).Result == 1);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryDelete()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
            Repository.Delete(document.Id);
            Assert.IsNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryDeleteAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
            Repository.DeleteAsync(document.Id);
            Assert.IsNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryDeleteWhere()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
            Repository.Delete(x => x.Id == document.Id);
            Assert.IsNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryDeleteWhereAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
            Repository.DeleteAsync(x => x.Id == document.Id);
            Assert.IsNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryFirstOrDefault()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.FirstOrDefault(x => x.Id == document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositoryFirstOrDefaultAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.FirstOrDefaultAsync(x => x.Id == document.Id).Result);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryList()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.List().Any());
        }

        [TestMethod]
        public void MongoDBTestsRepositoryListAsynchronous()
        {
            Repository.Add(CreateDocument());
            Assert.IsTrue(Repository.ListAsync().Result.Any());
        }

        [TestMethod]
        public void MongoDBTestsRepositoryListWhere()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.List(x => x.Id == document.Id).Any());
        }

        [TestMethod]
        public void MongoDBTestsRepositoryListWhereAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsTrue(Repository.ListAsync(x => x.Id == document.Id).Result.Any());
        }

        [TestMethod]
        public void MongoDBTestsRepositorySelect()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.Select(document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositorySelectAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.SelectAsync(document.Id).Result);
        }

        [TestMethod]
        public void MongoDBTestsRepositorySingleOrDefault()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.SingleOrDefault(x => x.Id == document.Id));
        }

        [TestMethod]
        public void MongoDBTestsRepositorySingleOrDefaultAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            Assert.IsNotNull(Repository.SingleOrDefaultAsync(x => x.Id == document.Id).Result);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryUpdate()
        {
            var document = CreateDocument();
            Repository.Add(document);
            var oldName = document.Name;
            document.Name = Guid.NewGuid().ToString();
            Repository.Update(document, document.Id);
            document = Repository.Select(document.Id);
            Assert.AreNotEqual(oldName, document.Name);
        }

        [TestMethod]
        public void MongoDBTestsRepositoryUpdateAsynchronous()
        {
            var document = CreateDocument();
            Repository.Add(document);
            var oldName = document.Name;
            document.Name = Guid.NewGuid().ToString();
            Repository.UpdateAsync(document, document.Id);
            document = Repository.Select(document.Id);
            Assert.AreNotEqual(oldName, document.Name);
        }

        private static FakeDocument CreateDocument()
        {
            return new FakeDocument { Id = ObjectId.GenerateNewId(), Name = Guid.NewGuid().ToString() };
        }
    }
}
