using Abp.Application.Services.Dto;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.UI;
using CompareX.Case;
using CompareX.Case.Dto;
using CompareX.EntityFrameworkCore;
using CompareX.Tests.TestDatas;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CompareX.Tests.Case
{
    public class CaseAppService_Tests : CompareXTestBase
    {
        private readonly ICaseAppService _caseAppService;

        public CaseAppService_Tests()
        {
            _caseAppService = Resolve<ICaseAppService>();
        }

        [Fact]
        public async Task Should_Get_Test_Cases()
        {
            var output = await _caseAppService.GetListAsync(new GetCaseListInput());
            output.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Should_Create_Case()
        {
            //Arrange
            var caseTitle = Guid.NewGuid().ToString();

            //Act
            await _caseAppService.CreateAsync(new CreateCaseInput
            {
                Title = caseTitle,
                Description = "A description",
                Date = Clock.Now.AddDays(2)
            });

            //Assert
            UsingDbContext(context =>
            {
                context.Cases.FirstOrDefault(e => e.Title == caseTitle).ShouldNotBe(null);
            });
        }

        [Fact]
        public async Task Should_Not_Create_Cases_In_The_Past()
        {
            //Arrange
            var CaseTitle = Guid.NewGuid().ToString();

            //Act
            await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _caseAppService.CreateAsync(new CreateCaseInput
                {
                    Title = CaseTitle,
                    Description = "A description",
                    Date = Clock.Now.AddDays(-1)
                });
            });
        }

        [Fact]
        public async Task Should_Cancel_Case()
        {
            //Act
            await _caseAppService.CancelAsync(new EntityDto<Guid>(GetTestCase().Id));

            //Assert
            GetTestCase().IsCancelled.ShouldBeTrue();
        }

        [Fact]
        public async Task Should_Register_To_Cases()
        {
            //Arrange
            var testCase = GetTestCase();

            //Act
            var output = await _caseAppService.RegisterAsync(new EntityDto<Guid>(testCase.Id));

            //Assert
            output.RegistrationId.ShouldBeGreaterThan(0);

            UsingDbContext(context =>
            {
                var currentUserId = AbpSession.GetUserId();
                var registration = context.CaseRegistrations.FirstOrDefault(r => r.CaseId == testCase.Id && r.UserId == currentUserId);
                registration.ShouldNotBeNull();
            });
        }

        [Fact]
        public async Task Should_Cancel_Registration()
        {
            //Arrange
            var currentUserId = AbpSession.GetUserId();
            await UsingDbContext(async context =>
            {
                var testCase = GetTestCase(context);
                var currentUser = await context.Users.SingleAsync(u => u.Id == currentUserId);
                var testRegistration = await CaseRegistration.CreateAsync(
                    testCase,
                    currentUser,
                    Substitute.For<ICaseRegistrationPolicy>()
                    );

                context.CaseRegistrations.Add(testRegistration);
            });

            //Act
            await _caseAppService.CancelRegistrationAsync(new EntityDto<Guid>(GetTestCase().Id));

            //Assert
            UsingDbContext(context =>
            {
                var testCase = GetTestCase(context);
                var testRegistration = context.CaseRegistrations.FirstOrDefault(r => r.CaseId == testCase.Id && r.UserId == currentUserId);
                testRegistration.ShouldBeNull();
            });
        }

        private CompareX.Case.Case GetTestCase()
        {
            return UsingDbContext(context => GetTestCase(context));
        }

        private static CompareX.Case.Case GetTestCase(CompareXDbContext context)
        {
            return context.Cases.Single(e => e.Title == TestDataBuilder.TestCaseTitle);
        }
    }
}
