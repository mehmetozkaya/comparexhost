using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CompareX.Authorization;
using CompareX.People;
using CompareX.PhoneBook.Dto;
using CompareX.PhoneNumber;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.PhoneBook
{
    [AbpAuthorize(PermissionNames.Pages_Tenant_PhoneBook)]
    public class PersonAppService : CompareXAppServiceBase, IPersonAppService
    {
        private readonly IRepository<Person, Guid> _personRepository;
        private readonly IRepository<Phone, long> _phoneRepository;

        public PersonAppService(IRepository<Person, Guid> personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }       

        public ListResultDto<PersonDto> GetPeople(GetPeopleInput input)
        {
            var people = _personRepository
                .GetAll()
                .Include(p => p.Phones)
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.Name.Contains(input.Filter) ||
                         p.Surname.Contains(input.Filter) ||
                         p.EmailAddress.Contains(input.Filter)
                )
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Surname)
                .ToList();

            return new ListResultDto<PersonDto>(ObjectMapper.Map<List<PersonDto>>(people));
        }

        [AbpAuthorize(PermissionNames.Pages_Tenant_PhoneBook_CreatePerson)]
        public async Task CreatePerson(CreatePersonInput input)
        {           
            var person = ObjectMapper.Map<Person>(input);
            await _personRepository.InsertAsync(person);
        }
        
        [AbpAuthorize(PermissionNames.Pages_Tenant_PhoneBook_DeletePerson)]
        public async Task DeletePerson(EntityDto<Guid> input)
        {
            await _personRepository.DeleteAsync(input.Id);
        }

        public async Task DeletePhone(EntityDto<long> input)
        {
            await _phoneRepository.DeleteAsync(input.Id);
        }

        public async Task<PhoneInPersonDto> AddPhone(AddPhoneInput input)
        {
            var person = _personRepository.Get(input.PersonId);
            await _personRepository.EnsureCollectionLoadedAsync(person, p => p.Phones);

            var phone = ObjectMapper.Map<Phone>(input);
            person.Phones.Add(phone);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<PhoneInPersonDto>(phone);
        }
    }
}
