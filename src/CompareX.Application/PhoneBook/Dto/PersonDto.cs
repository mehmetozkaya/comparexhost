using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompareX.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.PhoneBook.Dto
{
    [AutoMapFrom(typeof(Person))]
    public class PersonDto : FullAuditedEntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
    }
}
