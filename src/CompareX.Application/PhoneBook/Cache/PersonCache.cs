using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using CompareX.People;
using System;

namespace CompareX.PhoneBook.Cache
{
    public class PersonCache : EntityCache<Person, PersonCacheItem, Guid>, IPersonCache, ITransientDependency
    {
        public PersonCache(ICacheManager cacheManager, IRepository<Person, Guid> repository)
                : base(cacheManager, repository)
        {

        }
    }
}
