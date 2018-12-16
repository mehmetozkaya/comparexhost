using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CompareX.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompareX.Jobs
{
    public class SimpleSendEmailJob : BackgroundJob<SimpleSendEmailJobArgs>, ITransientDependency
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IEmailSender _emailSender;

        public SimpleSendEmailJob(IRepository<User, long> userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [UnitOfWork]
        public override void Execute(SimpleSendEmailJobArgs args)
        {
            var senderUser = _userRepository.Get(args.SenderUserId);
            var targetUser = _userRepository.Get(args.TargetUserId);

            _emailSender.Send(senderUser.EmailAddress, targetUser.EmailAddress, args.Subject, args.Body);
        }
    }
}
