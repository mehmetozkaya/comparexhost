using System;
using System.ComponentModel.DataAnnotations;

namespace CompareX.Case.Dto
{
    public class CreateCaseInput
    {
        [Required]
        [StringLength(Case.MaxTitleLength)]
        public string Title { get; set; }

        [StringLength(Case.MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxRegistrationCount { get; set; }
    }
}
