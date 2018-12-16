using Abp.Domain.Entities.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.PhoneBook.Cache
{
    public interface IPersonCache : IEntityCache<PersonCacheItem, Guid>
    {

    }
}
