using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CompareX.PhoneBook.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.PhoneBook
{
    public interface IPersonAppService : IApplicationService
    {
        ListResultDto<PersonDto> GetPeople(GetPeopleInput input);
    }
}
