using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCore.MongoDB.Tests
{
    public sealed class FakeRepository : MongoRepository<FakeDocument>, IFakeRepository
    {
        public FakeRepository(FakeContext context) : base(context)
        {
        }
    }
}
