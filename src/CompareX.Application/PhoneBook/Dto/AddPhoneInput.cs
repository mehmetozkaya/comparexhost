using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CompareX.PhoneNumber;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompareX.PhoneBook.Dto
{
    [AutoMapTo(typeof(Phone))]
    public class AddPhoneInput : FullAuditedEntityDto<long>
    {
        [Required]
        public Guid PersonId { get; set; }

        [Required]
        public PhoneType Type { get; set; }

        [Required]
        [MaxLength(Phone.MaxNumberLength)]
        public string Number { get; set; }
    }
}
