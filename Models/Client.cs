using System.ComponentModel.DataAnnotations;

namespace BankingControlPanel.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MaxLength(60)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string? PersonalId { get; set; }

        public string? ProfilePhoto { get; set; }

        [Required]
        [Phone]
        public string? MobileNumber { get; set; }

        [Required]
        public string? Sex { get; set; }

        public Address? Address { get; set; }
        public ICollection<Account>? Accounts { get; set; } = new List<Account>();
    }
}
