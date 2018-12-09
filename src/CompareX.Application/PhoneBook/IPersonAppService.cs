using Abp.Application.Services;
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
