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

        public Task<CaseRegisterOutput> RegisterAsync(EntityDto<Guid> input)
        {
            throw new NotImplementedException();
        }
    }
}
