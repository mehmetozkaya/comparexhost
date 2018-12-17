using Abp.Application.Services;
using Abp.BackgroundJobs;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompareX.Jobs
{
    public class CompareXEmailAppService : CompareXAppServiceBase, ICompareXEmailAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public CompareXEmailAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager ?? throw new ArgumentNullException(nameof(backgroundJobManager));
        }

        public async Task SendEmail(SendEmailInput input)
        {
            await _backgroundJobManager.EnqueueAsync<SimpleSendEmailJob, SimpleSendEmailJobArgs>(
                new SimpleSendEmailJobArgs
                {
                    Subject = input.Subject,
                    Body = input.Body,
                    SenderUserId = 1,
                    TargetUserId = input.TargetUserId
                });
        }
    }


    public class TestAppService : CompareXAppServiceBase
    {
        public TestAppService()
        {

        }

        public async Task Reason(SendEmailInput input)
        {
            
        }
    }
}
