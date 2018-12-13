using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompareX.PhoneNumber;

namespace CompareX.PhoneBook.Dto
{
    [AutoMapFrom(typeof(Phone))]
    public class PhoneInPersonDto : CreationAuditedEntityDto<long>
    {
        public PhoneType Type { get; set; }
        public string Number { get; set; }
    }
}
