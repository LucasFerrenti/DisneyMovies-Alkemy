using System.ComponentModel.DataAnnotations;

namespace Alkemy.Models.Common
{
    public class EmailContent
    {
        [Required]
        [EmailAddress]
        public string EmailReceptor { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; } = false;
    }
}
