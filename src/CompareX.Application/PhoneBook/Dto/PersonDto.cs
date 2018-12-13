using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompareX.People;
using System;
using System.Collections.ObjectModel;

namespace CompareX.PhoneBook.Dto
{
    [AutoMapFrom(typeof(Person))]
    public class PersonDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public Collection<PhoneInPersonDto> Phones { get; set; }
    }
}
