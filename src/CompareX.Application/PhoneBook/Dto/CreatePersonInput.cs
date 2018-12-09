using Abp.AutoMapper;
using CompareX.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompareX.PhoneBook.Dto
{
    [AutoMapFrom(typeof(Person))]
    public class CreatePersonInput
    {
        [Required]
        [MaxLength(Person.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(Person.MaxSurnameLength)]
        public string Surname { get; set; }

        [EmailAddress]
        [MaxLength(Person.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}
