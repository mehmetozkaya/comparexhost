using Abp.Domain.Entities.Auditing;
using CompareX.People;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompareX.PhoneNumber
{
    [Table("PbPhones")]
    public class Phone : CreationAuditedEntity<long>
    {
        public const int MaxNumberLength = 16;

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public virtual Guid PersonId { get; set; }

        [Required]
        public virtual PhoneType Type { get; set; }

        [Required]
        [MaxLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        public static Phone Create(Guid personId, PhoneType type, string number)
        {
            var newPhone = new Phone
            {
                PersonId = personId,
                Type = type,
                Number = number
            };

            return newPhone;
        }
    }

    public enum PhoneType : byte
    {
        Mobile,
        Home,
        Business
    }
}
