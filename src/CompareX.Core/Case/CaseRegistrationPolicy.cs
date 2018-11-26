using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Timing;
using Abp.UI;
using CompareX.Authorization.Users;
using CompareX.Configuration;

namespace CompareX.Case
{
    public class CaseRegistrationPolicy : ICaseRegistrationPolicy
    {
        private readonly IRepository<CaseRegistration> _caseRegistrationRepository;
        private readonly ISettingManager _settingManager;

        public CaseRegistrationPolicy(IRepository<CaseRegistration> caseRegistrationRepository, ISettingManager settingManager)
        {
            _caseRegistrationRepository = caseRegistrationRepository ?? throw new ArgumentNullException(nameof(caseRegistrationRepository));
            _settingManager = settingManager ?? throw new ArgumentNullException(nameof(settingManager));
        }

        public async Task CheckRegistrationAttemptAsync(Case checkCase, User user)
        {
            if (checkCase == null) { throw new ArgumentNullException("checkCase"); }
            if (user == null) { throw new ArgumentNullException("user"); }

            CheckCaseDate(checkCase);
            await CheckCaseRegistrationFrequencyAsync(user);
        }

        private static void CheckCaseDate(Case checkCase)
        {
            if (checkCase.IsInPast())
            {
                throw new UserFriendlyException("Can not register event in the past!");
            }
        }

        private async Task CheckCaseRegistrationFrequencyAsync(User user)
        {
            var oneMonthAgo = Clock.Now.AddDays(-30);
            var maxAllowedEventRegistrationCountInLast30DaysPerUser = await _settingManager.GetSettingValueAsync<int>(AppSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser);


        }


    }
}
