using Abp.AutoMapper;
using CompareX.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.PhoneBook.Cache
{
    [AutoMapFrom(typeof(Person))]
    public class PersonCacheItem
    {
        public string Name { get; set; }
    }
}
