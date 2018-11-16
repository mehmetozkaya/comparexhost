using System.ComponentModel.DataAnnotations;

namespace CompareX.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}