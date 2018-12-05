using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using CompareX.Case.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using System.Collections.Generic;
using Abp.UI;
using Abp.Runtime.Session;
using CompareX.Authorization.Users;

namespace CompareX.Case
{
    public class CaseAppService : CompareXAppServiceBase, ICaseAppService
    {
        private readonly ICaseManager _caseManager;
        private readonly IRepository<Case, Guid> _caseRepository;

        public CaseAppService(ICaseManager caseManager, IRepository<Case, Guid> caseRepository)
        {
            _caseManager = caseManager ?? throw new ArgumentNullException(nameof(caseManager));
            _caseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));
        }       

        public async Task<ListResultDto<CaseListDto>> GetListAsync(GetCaseListInput input)
        {
            var cases = await _caseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .WhereIf(!input.IncludeCanceledEvents, e => !e.IsCancelled)
                .OrderByDescending(e => e.CreationTime)
                .Take(64)
                .ToListAsync();

            return new ListResultDto<CaseListDto>(cases.MapTo<List<CaseListDto>>());
        }

        public async Task<CaseDetailOutput> GetDetailAsync(EntityDto<Guid> input)
        {
            var detailedCase = await _caseRepository
                .GetAll()
                .Include(e => e.Registrations)
                .ThenInclude(r => r.User)
                .Where(e => e.Id == input.Id)
                .FirstOrDefaultAsync();

            if (detailedCase == null)
            {
                throw new UserFriendlyException("Could not found the event, maybe it's deleted.");
            }

            return detailedCase.MapTo<CaseDetailOutput>();
        }

        public async Task CreateAsync(CreateCaseInput input)
        {
            var newCase = Case.Create(1, input.Title, input.Date, input.Description, input.MaxRegistrationCount);
            await _caseManager.CreateAsync(newCase);
        }

        public async Task CancelAsync(EntityDto<Guid> input)
        {
            var cancelCase = await _caseManager.GetAsync(input.Id);
            _caseManager.Cancel(cancelCase);
        }

        public async Task<CaseRegisterOutput> RegisterAsync(EntityDto<Guid> input)
        {
            var registration = await RegisterAndSaveAsync(await _caseManager.GetAsync(input.Id), await GetCurrentUserAsync());

            return new CaseRegisterOutput
            {
                RegistrationId = registration.Id
            };
        }

        public async Task CancelRegistrationAsync(EntityDto<Guid> input)
        {
            await _caseManager.CancelRegistrationAsync(
                await _caseManager.GetAsync(input.Id),
                await GetCurrentUserAsync()
                );
        }

        private async Task<CaseRegistration> RegisterAndSaveAsync(Case @event, User user)
        {
            var registration = await _caseManager.RegisterAsync(@event, user);
            await CurrentUnitOfWork.SaveChangesAsync();
            return registration;
        }

    }
}
