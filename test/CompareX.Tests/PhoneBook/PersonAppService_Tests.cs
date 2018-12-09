using CompareX.PhoneBook;
using CompareX.PhoneBook.Dto;
using Shouldly;
using System;
using Xunit;

namespace CompareX.Tests.PhoneBook
{
    public class PersonAppService_Tests : CompareXTestBase
    {
        private readonly IPersonAppService _personAppService;

        public PersonAppService_Tests()
        {
            _personAppService = Resolve<IPersonAppService>();
        }

        [Fact]
        public void Should_Get_All_People_Without_Any_Filter()
        {
            //Act
            var persons = _personAppService.GetPeople(new GetPeopleInput());
            //Assert
            persons.Items.Count.ShouldBe(3);
        }
    }
}
