using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompareX.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.PhoneBook.Dto
{
    public class GetPersonForEditInput
    {
        public Guid Id { get; set; }
    }

    [AutoMapFrom(typeof(Person))]
    public class GetPersonForEditOutput : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string EmailAddress { get; set; }
    }

    public class EditPersonInput
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }
    }

    
}
